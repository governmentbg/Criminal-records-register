using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
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
        private readonly IManagePersonService _managePersonService;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IUserContext _userContext;
        private readonly ILogger<ApplicationService> _logger;
        public ApplicationService(IMapper mapper,
            IApplicationRepository applicationRepository,
            IRegisterTypeService registerTypeService,
            ICertificateService certificateService,
            IUserContext userContext,
            IEWebRequestsRepository eWebRequestsRepository,
            IManagePersonService managePersonService,
            ILogger<ApplicationService> logger)
            : base(mapper, applicationRepository)
        {
            _applicationRepository = applicationRepository;
            _registerTypeService = registerTypeService;
            _certificateService = certificateService;
            _userContext = userContext;
            _eWebRequestsRepository = eWebRequestsRepository;
            _managePersonService = managePersonService;
            _logger = logger;
        }


        public virtual async Task<ApplicationOutDTO> SelectAsync(string aId)
        {
            var repoObj = await baseAsyncRepository.SelectAsync(aId);
            var result = mapper.Map<ApplicationOutDTO>(repoObj);
            return result;
        }

        public virtual async Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(
            ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = GetSelectAllQueryable();
            if (!string.IsNullOrEmpty(statusId))
            {
                var statues = statusId.Split(',');
                entityQuery = entityQuery.Where(x => statues.Contains(x.StatusCode)).Select(e => e)
                    .Union(entityQuery.Where(x => x.ACertificates.Any(c => statues.Contains(c.StatusCode)))).Select(e => e);
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
            var person = await _managePersonService.SelectWithBirthInfoAsync(personId);
            result.Person = person ?? new PersonDTO();
            return result;
        }

        public override async Task<string> InsertAsync(ApplicationInDTO aInDto)
        {
            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, true);
            //todo: ???????? ???? ???? ?????????? ?? ?????????!
            entity.CsAuthorityId = _userContext.CsAuthorityId;
            entity.ApplicationTypeId = "6"; //TODO: For test purposes (remove later)
            TransformDataOnInsertAsync(entity);
            await UpdateTransactionsAsync(aInDto, entity);
            await _applicationRepository.SaveEntityAsync<AApplication>(entity, true);
            //await dbContext.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public async Task ChangeApplicationStatusToCanceled(string aId, ApplicationCancelDTO aInDto)
        {
            var repoObj = await _applicationRepository.SelectEntityAsync(aId);
            // await dbContext.AApplications
            //   .FirstOrDefaultAsync(x => x.Id == aId);
            var statusCanceledApplication = await baseAsyncRepository.SingleOrDefaultAsync<AApplicationStatus>(a =>
                    a.Code == ApplicationConstants.ApplicationStatuses.Canceled );
            // await dbContext.AApplicationStatuses.FirstOrDefaultAsync(a =>
            //   a.Code == ApplicationConstants.ApplicationStatuses.Canceled);
            await SetApplicationStatus(repoObj, statusCanceledApplication, aInDto.Description);
            await _applicationRepository.SaveChangesAsync();
            // await dbContext.SaveChangesAsync();
        }

        public async Task ChangeApplicationStatusToCheckPayment(string aId, string description = "")
        {
            var repoObj = await baseAsyncRepository.SelectAsync(aId);
            //repoObj.StatusCode = ApplicationConstants.ApplicationStatuses.CheckPayment;
            //await baseAsyncRepository.UpdateAsync(repoObj);
            //await dbContext.SaveChangesAsync();
            var statusCheckPayment = await baseAsyncRepository.SingleOrDefaultAsync<AApplicationStatus>(a =>
                  a.Code == ApplicationConstants.ApplicationStatuses.CheckPayment);
           await  SetApplicationStatus(repoObj, statusCheckPayment, description);
            await _applicationRepository.SaveChangesAsync();

        }

        public async Task UpdateAsync(ApplicationInDTO aInDto, bool isFinal)
        {
            var applicationDb = await baseAsyncRepository.SingleOrDefaultAsync<AApplication>(a => a.Id == aInDto.Id);
            //await dbContext.AApplications.AsNoTracking()
            //  .FirstOrDefaultAsync(x => x.Id == aInDto.Id);

            if (applicationDb == null)
            {
                throw new ArgumentException($"Application with id {aInDto.Id} is missing");
            }
            if (applicationDb.CsAuthorityId != _userContext.CsAuthorityId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.editIsUnauthorized);


            ValidateData(aInDto);

            var entity = mapper.MapToEntity<ApplicationInDTO, AApplication>(aInDto, false);
            entity.ApplicationTypeId = applicationDb.ApplicationTypeId;

            await UpdateApplicationAsync(aInDto, entity);

            if (isFinal)
            {
                if (string.IsNullOrEmpty(entity.RegistrationNumber))
                {
                    var regNumber =
                    await _registerTypeService.GetRegisterNumberForApplicationOnDesk(applicationDb.CsAuthorityId);
                    entity.RegistrationNumber = regNumber;
                }

                await UpdatePersonDataAsync(aInDto, entity);

                await GenerateCertificateFromApplication(applicationDb.Id);
                return;
            }

            await SaveEntityAsync(entity);
        }

        public async Task<string> GenerateCertificateFromApplication(string id)
        {
            var statuses = (await _applicationRepository.FindAsync<AApplicationStatus>(a =>
              a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck ||
              a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady
              || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication));
            //var statuses = await Task.FromResult(dbContext.AApplicationStatuses.Where(a =>
            //    a.Code == ApplicationConstants.ApplicationStatuses.BulletinsCheck ||
            //    a.Code == ApplicationConstants.ApplicationStatuses.CertificateContentReady
            //    || a.Code == ApplicationConstants.ApplicationStatuses.ApprovedApplication).ToList());
            if (statuses.Count() != 3)
            {
                throw new Exception(
                    $"Application statuses do not exist. Statuses: {ApplicationConstants.ApplicationStatuses.ApprovedApplication}, {ApplicationConstants.ApplicationStatuses.BulletinsCheck}, {ApplicationConstants.ApplicationStatuses.CertificateContentReady}");
            }

            var systemParameters = //await Task.FromResult(dbContext.GSystemParameters.Where(x =>
                 await _applicationRepository.FindAsync<GSystemParameter>(x =>
                x.Code == SystemParametersConstants.SystemParametersNames.CERTIFICATE_VALIDITY_PERIOD_MONTHS
                || x.Code == SystemParametersConstants.SystemParametersNames.SYSTEM_SIGNING_CERTIFICATE_NAME);
            if (systemParameters.Count() != 2)
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
            var application = await _applicationRepository.GetApplicationForCertificateGeneration(id);
            //var application = await dbContext.AApplications
            //    .Include(a => a.EgnNavigation)
            //    .Include(a => a.LnchNavigation)
            //    .Include(a => a.LnNavigation)
            //    .Include(a => a.SuidNavigation)
            //    .Include(a => a.ApplicationType)
            //    .Include(a => a.AStatusHes)
            //    .FirstOrDefaultAsync(aa => aa.Id == id);
            if (application == null)
            {
                throw new Exception($"Application with id = {id} does not exist.");
            }

            await GenerateCertificateFromApplication(application, applicationStatus, bulletinCheckStatus,
                certificateContentReadyStatus, (int)certificateValidityMonths);
            await _applicationRepository.SaveChangesAsync();
            //await dbContext.SaveChangesAsync();

            return application.ACertificates.First().Id;
        }

        public async Task<ACertificate> GenerateCertificateFromApplication(AApplication application,
            AApplicationStatus applicationStatus, AApplicationStatus certificateWithBulletinStatus,
            AApplicationStatus certificateWithoutBulletinStatus,
            int certificateValidityMonths =
                6) //string certificateWithoutBulletinStatusID = ApplicationConstants.ApplicationStatuses.CertificateContentReady, string certificateWithBulletinStatusID = ApplicationConstants.ApplicationStatuses.BulletinsCheck)
        {
            //???????????? ???? ???? ?????????????????? ???????????????? ??????????????????:
            //       .Include(a => a.EgnNavigation)
            //       .Include(a => a.LnchNavigation)
            //       .Include(a => a.LnNavigation)
            //       .Include(a => a.SuidNavigation)
            //var pids = new List<string>();
            string personId = null;
            if (application.EgnId != null && application.EgnNavigation != null)
            {
                personId = application.EgnNavigation.PersonId;
            }

            if (application.LnchId != null && application.LnchNavigation != null)
            {
                if (personId == null)
                {
                    personId = application.LnchNavigation.PersonId;
                }
                else if (personId != application.LnchNavigation.PersonId)
                {
                    throw new BusinessLogicException("???????????????????????????????? ???? ???????? ?????????????????? ???? ???? ???????????? ???? ???????? ??????????!");
                }
            }

            if (application.LnId != null && application.LnNavigation != null)
            {
                if (personId == null)
                {
                    personId = application.LnNavigation.PersonId;
                }
                else if (personId != application.LnNavigation.PersonId)
                {
                    throw new BusinessLogicException("???????????????????????????????? ???? ???????? ?????????????????? ???? ???? ???????????? ???? ???????? ??????????!");
                }
            }

            if (application.SuidId != null && application.SuidNavigation != null)
            {
                if (personId == null)
                {
                    personId = application.SuidNavigation.PersonId;
                }
                else if (personId != application.SuidNavigation.PersonId)
                {
                    throw new BusinessLogicException("???????????????????????????????? ???? ???????? ?????????????????? ???? ???? ???????????? ???? ???????? ??????????!");
                }
            }

            if (personId != null)
            {
                //todo: ???????? ???? ????  ?? union? - ???? ?????????? ?? ???? ?? Union
                _logger.LogTrace($"{application.Id}: Before _applicationRepository.SelectBulletinIdsAsync.");
                var bulletins = await (await _applicationRepository.SelectBulletinIdsAsync(personId)).ToListAsync();//.ToListAsync();
                _logger.LogTrace($"{application.Id}: After _applicationRepository.SelectBulletinIdsAsync.");
                
                if (bulletins.Count() > 0)
                {
                    _logger.LogTrace($"{application.Id}: Before ProcessApplicationWithBulletinsAsync.");
                    return await ProcessApplicationWithBulletinsAsync(application, bulletins, certificateWithBulletinStatus,
                        certificateValidityMonths, applicationStatus);

                }
            }
            _logger.LogTrace($"{application.Id}: Before ProcessApplicationWithoutBulletinsAsync.");
            return await ProcessApplicationWithoutBulletinsAsync(application, certificateWithoutBulletinStatus,
                certificateValidityMonths, applicationStatus);
        }


        public async Task  SetApplicationStatus(AApplication application, AApplicationStatus newStatus, string description,
            bool includeInDbContext = true)
        {
            var oldValue = application.StatusCode;  
            if(oldValue == ApplicationConstants.ApplicationStatuses.DeliveredApplication )
            {
                throw new BusinessLogicException("?????????????????????? ?? ??????????????????");
                
            }
            application.StatusCode = newStatus.Code;
            if (application.EntityState != EntityStateEnum.Added)
            {
                application.EntityState = EntityStateEnum.Modified;
            }
            if (application.ModifiedProperties == null)
            {
                application.ModifiedProperties = new List<string>();
            }
            application.ModifiedProperties.Add(nameof(application.StatusCode));

            //application.StatusCodeNavigation = newStatus;
            var aStatusH = new AStatusH();
            aStatusH.EntityState = EntityStateEnum.Added;
            aStatusH.Id = BaseEntity.GenerateNewId();
            aStatusH.Descr = description;
            aStatusH.StatusCode = newStatus.Code;
            aStatusH.StatusCodeNavigation = newStatus;
            //if (application.AStatusHes == null)
            //{
            //    application.AStatusHes = new List<AStatusH>();
            //}

            aStatusH.ReportOrder = application.AStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;

            aStatusH.ApplicationId = application.Id;
            aStatusH.Application = application;
            if (oldValue == ApplicationConstants.ApplicationStatuses.ApprovedApplication && newStatus.Code == ApplicationConstants.ApplicationStatuses.Canceled)
            {
                await _certificateService.CancelCertificate(application.Id);
            }
            baseAsyncRepository.ApplyChanges(aStatusH, new List<IBaseIdEntity>());

            // application.AStatusHes.Add(aStatusH);
            //if (includeInDbContext)
            //{
            //    dbContext.AStatusHes.Add(aStatusH);
            //    dbContext.AApplications.Update(application);
            //}
        }

        //private async Task CancelAllCertificates(AApplication application,string descr)
        //{
        //    var aStatus = await baseAsyncRepository.SingleOrDefaultAsync<AApplicationStatus>(x => x.Code == ApplicationConstants.ApplicationCertificateStatuses.CanceledCertificate);
        //    var certs = await baseAsyncRepository.FindAsync<ACertificate>(x => x.ApplicationId == application.Id && x.StatusCode != ApplicationConstants.ApplicationCertificateStatuses.CanceledCertificate);
        //    foreach (var cert in certs)
        //    {
        //        await _certificateService.SetCertificateStatus(cert, aStatus, descr);
        //    }
        //}

        public async Task<IQueryable<EWebRequestGridDTO>> SelectAllEWebRequestsByApplicationIdAsync(string aId)
        {
            //todo: ???????? ?? ???? ???????!
            var result = await _eWebRequestsRepository.SelectAllByApplicationId(aId);
            return result.ProjectTo<EWebRequestGridDTO>(mapper.ConfigurationProvider);
        }

        public async Task<IQueryable<PersonAliasDTO>> SelectApplicationPersAliasByApplicationIdAsync(string aId)
        {

            var result = await _applicationRepository.FindAsync<AAppPersAlias>(x => x.ApplicationId == aId);
            //await _applicationRepository.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return result.ProjectTo<PersonAliasDTO>(mapper.ConfigurationProvider); //AAppPersAlias
        }

        public async Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId)
        {
            //todo: ???????? ?? ???? ???????!

            var result = await _applicationRepository.FindAsync<ACertificate>(x => x.ApplicationId == aId);
            //await _applicationRepository.SelectApplicationCertificateByApplicationIdAsync(aId);
            return result.ProjectTo<ACertificate>(mapper.ConfigurationProvider);
        }

        public async Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId)
        {
            var result = await _applicationRepository.SelectApplicationPersStatusHAsync(aId);
            return result;
        }

        private async Task UpdatePersonDataAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            aInDto.Person.TableName = ContextTable.Application;
            aInDto.Person.TableId = aInDto.Id;

            var person = await _managePersonService.CreatePersonAsync(aInDto.Person);
            _managePersonService.UpdatePidDataData(person.PPersonIds, entity);
            _applicationRepository.ApplyChanges(entity);

            // this method call save changes
            await _managePersonService.SavePersonAndUpdateSearchAttributesAsync(person, clearTracker: true);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private async Task UpdateApplicationAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            await UpdateTransactionsAsync(aInDto, entity);

            var passedNavigationProperties = new List<IBaseIdEntity>();
            _applicationRepository.ApplyChanges(entity, passedNavigationProperties, true);

            // dbContext.ApplyChanges(entity, passedNavigationProperties, true);
        }

        private async Task UpdateTransactionsAsync(ApplicationInDTO aInDto, AApplication entity)
        {
            entity.AAppPersAliases =
                mapper.MapTransactions<PersonAliasDTO, AAppPersAlias>(aInDto.Person.PersonAliasTransactions);
            entity.AAppCitizenships = CaisMapper.MapMultipleChooseToEntityList<AAppCitizenship, string, string>(
                aInDto.Person.Nationalities, nameof(AAppCitizenship.Id), nameof(AAppCitizenship.CountryId));
        }

        private async Task<ACertificate> ProcessApplicationWithoutBulletinsAsync(AApplication application,
            AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            var cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths,
                application.CsAuthorityId, application.ApplicationType.Code);
            //todo: add resources
            if (application.StatusCode != aStatus.Code)
            {
                await SetApplicationStatus(application, aStatus, "?????????????????? ???? ????????????????????");
            }

            application.ACertificates.Add(cert);

            //dbContext.ACertificates.Add(cert);
            application.EntityState = EntityStateEnum.Modified;
            if (application.ModifiedProperties == null)
            {
                application.ModifiedProperties = new List<string>();
            }
            application.ModifiedProperties.Add(nameof(application.ACertificates));
            baseAsyncRepository.ApplyChanges(application, new List<IBaseIdEntity>());
            //baseAsyncRepository.ApplyChanges(cert, new List<IBaseIdEntity>());
            return cert;
            //dbContext.AApplications.Update(application);
        }

        private async Task<ACertificate> CreateCertificateAsync(string applicationId, AApplicationStatus aStatus,
            int certificateValidityMonths, string csAuthorityId, string applicationTypeCode)
        {
            var cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.EntityState = EntityStateEnum.Added;
            cert.ApplicationId = applicationId;
            await _certificateService.SetCertificateStatus(cert, aStatus, "?????????????????? ???? ????????????????????");


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
            cert.ValidFrom = DateTime.Now;
            cert.ValidTo = DateTime.Now.AddMonths(certificateValidityMonths);
            // cert.AccessCode2 = Guid.NewGuid().ToString();
            return cert;
        }

        private async Task<ACertificate> ProcessApplicationWithBulletinsAsync(AApplication application, List<BBulletin> orderedBulletins,
            AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            var cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths,
                application.CsAuthorityId, application.ApplicationType.Code);
            var orderNumber = 0;
            cert.AAppBulletins = orderedBulletins
                .Select(b =>
                {
                    orderNumber++;
                    return new AAppBulletin
                    {
                        Id = BaseEntity.GenerateNewId(),
                        BulletinId = b.Id,
                        CertificateId = cert.Id,
                        //ConvictionText = b.ConvRemarks, ???????? ?? Null, ???? ???? ?????????? ?? ????????????????
                        OrderNumber = orderNumber,
                        EntityState = EntityStateEnum.Added
                    };
                }).ToList();
            //todo: add resources
            if (application.StatusCode != aStatus.Code)
            {
                await SetApplicationStatus(application, aStatus, "?????????????????? ???? ????????????????????");
            }

            application.ACertificates.Add(cert);
            application.EntityState = EntityStateEnum.Modified;
            if (application.ModifiedProperties == null)
            {
                application.ModifiedProperties = new List<string>();
            }
            application.ModifiedProperties.Add(nameof(application.ACertificates));
            baseAsyncRepository.ApplyChanges(application, new List<IBaseIdEntity>());
            baseAsyncRepository.ApplyChanges(cert, new List<IBaseIdEntity>());
            return cert;
            //dbContext.ACertificates.Add(cert);
            // dbContext.AAppBulletins.AddRange(cert.AAppBulletins);
            //dbContext.AApplications.Update(application);
        }
    }
}