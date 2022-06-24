using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.EWebRequest;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.PersonConstants;

namespace MJ_CAIS.Services
{
    public class ApplicationService :
        BaseAsyncService<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, AApplication, string, CaisDbContext>,
        IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ICertificateService _certificateService;
        private readonly IEWebRequestsRepository _eWebRequestsRepository;
        private readonly IPersonService _personService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IUserContext _userContext;

        public ApplicationService(IMapper mapper,
            IApplicationRepository applicationRepository,
            IRegisterTypeService registerTypeService,
            ICertificateService certificateService,
            IUserContext userContext,
            IPersonService personService,
            IEWebRequestsRepository eWebRequestsRepository)
            : base(mapper, applicationRepository)
        {
            _applicationRepository = applicationRepository;
            _registerTypeService = registerTypeService;
            _certificateService = certificateService;
            _userContext = userContext;
            _personService = personService;
            _eWebRequestsRepository = eWebRequestsRepository;
        }


        public virtual async Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(
            ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = GetSelectAllQueriable();
            if (!string.IsNullOrEmpty(statusId))
            {
                var statues = statusId.Split(',');
                entityQuery = entityQuery.Where(x => statues.Contains(x.StatusCode));
            }

            var baseQuery = entityQuery.ProjectTo<ApplicationGridDTO>(mapperConfiguration);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ApplicationGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<IgPageResult<ApplicationGridDTO>> SelectAllCertWithPaginationAsync(
            ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = await _applicationRepository.SelectAllCertificateAsync();
            if (!string.IsNullOrEmpty(statusId))
            {
                var statues = statusId.Split(',');
                entityQuery = entityQuery.Where(x => statues.Contains(x.StatusCode));
            }

            var baseQuery = entityQuery.ProjectTo<ApplicationGridDTO>(mapperConfiguration);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ApplicationGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<ApplicationOutDTO> SelectWithPersonDataAsync(string personId)
        {
            var result = new ApplicationOutDTO();
            result.Id = BaseEntity.GenerateNewId();
            var authId = _userContext.CsAuthorityId;
            result.CsAuthorityId = authId;
            var person = await _personService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
        }

        public override async Task<string> InsertAsync(ApplicationInDTO aInDto)
        {
            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, true);
            entity.CsAuthorityId = _userContext.CsAuthorityId;
            entity.ApplicationTypeId = "6"; //TODO: For test purposes (remove later)
            TransformDataOnInsert(entity);
            await UpdateTransactionsAsync(aInDto, entity);
            await dbContext.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public async Task ChangeApplicationStatusToCanceled(string aId)
        {
            var repoObj = await baseAsyncRepository.SelectAsync(aId);
            repoObj.StatusCode = ApplicationConstants.ApplicationStatuses.Canceled;
            await baseAsyncRepository.UpdateAsync(repoObj);
            await dbContext.SaveChangesAsync();
        }

        public async Task ChangeApplicationStatusToCheckPayment(string aId)
        {
            var repoObj = await baseAsyncRepository.SelectAsync(aId);
            repoObj.StatusCode = ApplicationConstants.ApplicationStatuses.CheckPayment;
            await baseAsyncRepository.UpdateAsync(repoObj);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationInDTO aInDto, bool isFinal)
        {
            var applicationDb = await dbContext.AApplications.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == aInDto.Id);

            if (applicationDb == null)
            {
                throw new ArgumentException($"Application with id {aInDto.Id} is missing");
            }

            ValidateData(aInDto);

            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, false);

            await UpdateApplicationAsync(aInDto, entity);

            if (isFinal)
            {
                var regNumber =
                    await _registerTypeService.GetRegisterNumberForApplicationOnDesk(applicationDb.CsAuthorityId);
                entity.RegistrationNumber = regNumber;
                await UpdatePersonDataAsync(aInDto, entity);

                await GenerateCertificateFromApplication(applicationDb.Id);
                return;
            }

            await SaveEntityAsync(entity);
        }

        public async Task<string> GenerateCertificateFromApplication(string id)
        {
            var statuses = await Task.FromResult(dbContext.AApplicationStatuses.Where(a =>
                a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck ||
                a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady
                || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToList());
            if (statuses.Count != 3)
            {
                throw new Exception(
                    $"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");
            }

            var systemParameters = await Task.FromResult(dbContext.GSystemParameters.Where(x =>
                x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
                || x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME).ToList());
            if (systemParameters.Count != 2)
            {
                throw new Exception(
                    $"Application statuses do not exist. Statuses: {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS}, {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME}");
            }

            var certificateValidityMonths = systemParameters.First(x =>
                    x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS)
                .ValueNumber;
            if (certificateValidityMonths == null)
            {
                throw new Exception(
                    $"System parameter {SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS} not set.");
            }

            var signingCertificateName = systemParameters.First(x =>
                x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME).ValueString;
            if (signingCertificateName == null)
            {
                throw new Exception(
                    $"System parameter {SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME} not set.");
            }

            var applicationStatus =
                statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication);
            var certificateContentReadyStatus = statuses.First(a =>
                a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady);
            var bulletinCheckStatus =
                statuses.First(a => a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck);
            var application = await dbContext.AApplications
                .Include(a => a.EgnNavigation)
                .Include(a => a.LnchNavigation)
                .Include(a => a.LnNavigation)
                .Include(a => a.SuidNavigation)
                .Include(a => a.ApplicationType)
                .FirstOrDefaultAsync(aa => aa.Id == id);
            if (application == null)
            {
                throw new Exception($"Application with id = {id} does not exist.");
            }

            await GenerateCertificateFromApplication(application, applicationStatus, bulletinCheckStatus,
                certificateContentReadyStatus, (int)certificateValidityMonths);

            await dbContext.SaveChangesAsync();

            return application.ACertificates.First().Id;
        }

        public async Task GenerateCertificateFromApplication(AApplication application,
            AApplicationStatus applicationStatus, AApplicationStatus certificateWithBulletinStatus,
            AApplicationStatus certificateWithoutBulletinStatus,
            int certificateValidityMonths =
                6) //string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
        {
            //трябва да са попълнени следните стойности:
            //       .Include(a => a.EgnNavigation)
            //       .Include(a => a.LnchNavigation)
            //       .Include(a => a.LnNavigation)
            //       .Include(a => a.SuidNavigation)
            var pids = new List<string>();
            if (application.EgnId != null && application.EgnNavigation != null)
            {
                pids.Add(application.EgnNavigation.PersonId);
            }

            if (application.LnchId != null && application.LnchNavigation != null)
            {
                pids.Add(application.LnchNavigation.PersonId);
            }

            if (application.LnId != null && application.LnNavigation != null)
            {
                pids.Add(application.LnNavigation.PersonId);
            }

            if (application.SuidId != null && application.SuidNavigation != null)
            {
                pids.Add(application.SuidNavigation.PersonId);
            }

            //await dbContext.PAppIds.Where(p => p.ApplicationId == application.Id).Select(prop => prop.PersonId).ToListAsync();
            if (pids.Count > 0)
            {
                var bulletins = await dbContext.BBulletins.Where(b => b.Status.Code != BulletinConstants.Status.Deleted
                                                                      && b.PBulletinIds.Any(bulID =>
                                                                          pids.Contains(bulID.Person.PersonId)))
                    .ToListAsync();
                if (bulletins.Count > 0)
                {
                    await ProcessApplicationWithBulletinsAsync(application, bulletins, certificateWithBulletinStatus,
                        certificateValidityMonths, applicationStatus);
                    return;
                }
            }

            await ProcessApplicationWithoutBulletinsAsync(application, certificateWithoutBulletinStatus,
                certificateValidityMonths, applicationStatus);
        }

        public async Task<IQueryable<ApplicationDocumentDTO>> GetDocumentsByApplicationIdAsync(string aId)
        {
            var result = dbContext.DDocuments
                .AsNoTracking()
                .Include(x => x.DocType)
                .Include(x => x.DocContent)
                .Where(x => x.ApplicationId == aId)
                .ProjectTo<ApplicationDocumentDTO>(mapper.ConfigurationProvider);

            return await Task.FromResult(result);
        }

        public async Task InsertApplicationDocumentAsync(string applicationId, ApplicationDocumentDTO aInDto)
        {
            if (aInDto == null)
            {
                throw new ArgumentNullException(nameof(aInDto));
            }

            if (aInDto.DocumentContent?.Length == 0)
            {
                throw new ArgumentNullException("Document is empty");
            }

            var docContentId = string.IsNullOrEmpty(aInDto.DocumentContentId)
                ? Guid.NewGuid().ToString()
                : aInDto.DocumentContentId;

            var document = mapper.Map<ApplicationDocumentDTO, DDocument>(aInDto);
            document.ApplicationId = applicationId;
            document.DocContentId = docContentId;

            var documentContent = new DDocContent
            {
                Id = docContentId,
                Content = aInDto.DocumentContent,
                MimeType = aInDto.MimeType
            };

            dbContext.Add(document);
            dbContext.Add(documentContent);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDocumentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null)
            {
                throw new ArgumentException($"Document with id: {documentId} is missing");
            }

            document.EntityState = EntityStateEnum.Deleted;
            if (document.DocContent != null)
            {
                document.DocContent.EntityState = EntityStateEnum.Deleted;
            }

            await dbContext.SaveEntityAsync(document, true);
        }

        public async Task<ApplicationDocumentDTO> GetDocumentContentAsync(string documentId)
        {
            var document = await dbContext.Set<DDocument>().AsNoTracking()
                .Include(x => x.DocContent)
                .FirstOrDefaultAsync(x => x.Id == documentId);

            if (document == null || document.DocContent == null)
            {
                return null;
            }

            return new ApplicationDocumentDTO
            {
                Name = document.Name,
                DocumentContent = document.DocContent.Content,
                MimeType = document.DocContent.MimeType
            };
        }

        public void SetApplicationStatus(AApplication application, AApplicationStatus newStatus, string description,
            bool includeInDbContext = true)
        {
            application.StatusCode = newStatus.Code;
            application.StatusCodeNavigation = newStatus;
            var aStatusH = new AStatusH();
            aStatusH.EntityState = EntityStateEnum.Added;
            aStatusH.Descr = description;
            aStatusH.StatusCode = newStatus.Code;
            aStatusH.StatusCodeNavigation = newStatus;
            if (application.AStatusHes == null)
            {
                application.AStatusHes = new List<AStatusH>();
            }

            aStatusH.ReportOrder = application.AStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            aStatusH.Id = BaseEntity.GenerateNewId();
            aStatusH.ApplicationId = application.Id;
            aStatusH.Application = application;

            application.AStatusHes.Add(aStatusH);
            if (includeInDbContext)
            {
                dbContext.AStatusHes.Add(aStatusH);
                dbContext.AApplications.Update(application);
            }
        }

        public async Task<IQueryable<EWebRequestGridDTO>> SelectAllEWebRequestsByApplicationIdAsync(string aId)
        {
            var result = await _eWebRequestsRepository.SelectAllByApplicationId(aId);
            return result.ProjectTo<EWebRequestGridDTO>(mapper.ConfigurationProvider);
        }

        public async Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return result.ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider); //AAppPersAlias
        }

        public async Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationCertificateByApplicationIdAsync(aId);
            return result.ProjectTo<ACertificate>(mapper.ConfigurationProvider);
        }

        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationPersStatusHAsync(aId);
            return result;
        }

        private async Task UpdatePersonDataAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            // тодо: да се прегледат дали се сетват правилно идентификраторите
            var person = await _personService.CreatePersonAsync(aInDto.Person);
            foreach (var personIdObj in person.PPersonIds)
            {
                if (personIdObj.PidTypeId == PidType.Egn)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Egn));
                    entity.EgnNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Lnch)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Lnch));
                    entity.LnchNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Ln)
                {
                    entity.ModifiedProperties.Add(nameof(entity.Ln));
                    entity.LnNavigation = personIdObj;
                }
                else if (personIdObj.PidTypeId == PidType.Suid)
                {
                    entity.ModifiedProperties.Add(nameof(entity.SuidId));
                    entity.ModifiedProperties.Add(nameof(entity.Suid));
                    entity.Suid = personIdObj.Pid;
                    entity.SuidId = personIdObj.Id;
                    entity.SuidNavigation = personIdObj;
                }

                dbContext.ApplyChanges(personIdObj, new List<IBaseIdEntity>());
            }

            dbContext.ApplyChanges(entity, new List<IBaseIdEntity>());
            await dbContext.SaveChangesAsync();
        }


        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private async Task UpdateApplicationAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            await UpdateTransactionsAsync(aInDto, entity);

            var passedNavigationProperties = new List<IBaseIdEntity>();
            dbContext.ApplyChanges(entity, passedNavigationProperties, true);
        }

        private async Task UpdateTransactionsAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            entity.AAppPersAliases =
                mapper.MapTransactions<PersonAliasDTO, AAppPersAlias>(aInDto.Person.PersonAliasTransactions);
            entity.AAppCitizenships = CaisMapper.MapMultipleChooseToEntityList<AAppCitizenship, string, string>(
                aInDto.Person.Nationalities, nameof(AAppCitizenship.Id), nameof(AAppCitizenship.CountryId));
        }

        private async Task ProcessApplicationWithoutBulletinsAsync(AApplication application,
            AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            var cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths,
                application.CsAuthorityId, application.ApplicationType.Code);
            //todo: add resources
            SetApplicationStatus(application, aStatus, "Създаване на сертификат");
            //  application.StatusCode = ApplicationConstants.ApplicationStatuses.ApprovedApplication;
            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AApplications.Update(application);
        }

        private async Task<ACertificate> CreateCertificateAsync(string applicationId, AApplicationStatus aStatus,
            int certificateValidityMonths, string csAuthorityId, string applicationTypeCode)
        {
            var cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.ApplicationId = applicationId;
            _certificateService.SetCertificateStatus(cert, aStatus, "Създаване на сертификат");


            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.DeskCertificate)
            {
                cert.RegistrationNumber =
                    await _registerTypeService.GetRegisterNumberForCertificateOnDesk(csAuthorityId);
            }

            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.WebCertificate)
            {
                cert.RegistrationNumber = await _registerTypeService.GetRegisterNumberForCertificateWeb(csAuthorityId);
            }

            if (applicationTypeCode == ApplicationConstants.ApplicationTypes.WebExternalCertificate)
            {
                cert.RegistrationNumber =
                    await _registerTypeService.GetRegisterNumberForCertificateWebExternal(csAuthorityId);
            }

            cert.AccessCode1 = Guid.NewGuid().ToString();
            cert.ValidFrom = DateTime.UtcNow;
            cert.ValidTo = DateTime.UtcNow.AddMonths(certificateValidityMonths);
            // cert.AccessCode2 = Guid.NewGuid().ToString();
            return cert;
        }

        private async Task ProcessApplicationWithBulletinsAsync(AApplication application, List<BBulletin> bulletins,
            AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            var cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths,
                application.CsAuthorityId, application.ApplicationType.Code);
            var orderNumber = 0;
            cert.AAppBulletins = bulletins
                .OrderByDescending(b => b.CreatedOn.HasValue ? b.CreatedOn.Value.Date : DateTime.Now)
                .ThenByDescending(b => b.DecisionDate).Select(b =>
                {
                    orderNumber++;
                    return new AAppBulletin
                    {
                        Id = BaseEntity.GenerateNewId(),
                        BulletinId = b.Id,
                        CertificateId = cert.Id,
                        ConvictionText = b.ConvRemarks,
                        OrderNumber = orderNumber
                    };
                }).ToList();
            //todo: add resources
            SetApplicationStatus(application, aStatus, "Създаване на сертификат");

            application.ACertificates.Add(cert);
            dbContext.ACertificates.Add(cert);
            dbContext.AAppBulletins.AddRange(cert.AAppBulletins);
            dbContext.AApplications.Update(application);
        }
    }
}