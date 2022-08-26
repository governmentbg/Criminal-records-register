using AutoMapper;
using AutoMapper.QueryableExtensions;
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
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IUserContext _userContext;
        private readonly IDDocContentRepository _dDocContentRepository;
        private readonly IMapper _mapper;
        private readonly IRegisterTypeService _registerTypeService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ILogger<ApplicationService> _logger;

        public CertificateService(IMapper mapper,
            ICertificateRepository certificateRepository,
            IUserContext userContext,
            IDDocContentRepository dDocContentRepository,
            IRegisterTypeService registerTypeService,
            IApplicationRepository applicationRepository,
            ILogger<ApplicationService> logger
            )
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _userContext = userContext;
            _dDocContentRepository = dDocContentRepository;
            _registerTypeService = registerTypeService;
            _applicationRepository = applicationRepository;
            _logger = logger;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId)
            => await _certificateRepository.GetCertificateContentByWebAppIdAsync(webAppId);

        public async Task SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description)
        {
            certificate.StatusCode = newStatus.Code;
            //certificate.StatusCodeNavigation = newStatus;
            AStatusH aStatusH = new AStatusH();
            aStatusH.Descr = description;
            aStatusH.StatusCode = newStatus.Code;
            aStatusH.StatusCodeNavigation = newStatus;
            if (certificate.AStatusHes == null)
            {
                certificate.AStatusHes = new List<AStatusH>();
            }
            aStatusH.ReportOrder = certificate.AStatusHes.Count(x => x.StatusCode == newStatus.Code) + 1;
            aStatusH.Id = BaseEntity.GenerateNewId();
            aStatusH.CertificateId = certificate.Id;
            aStatusH.ApplicationId = certificate.ApplicationId;
            aStatusH.EntityState = Common.Enums.EntityStateEnum.Added;

            certificate.AStatusHes.Add(aStatusH);
            if (certificate.EntityState != Common.Enums.EntityStateEnum.Added)
            {
                certificate.EntityState = Common.Enums.EntityStateEnum.Modified;
            }
            if (certificate.ModifiedProperties == null)
            {
                certificate.ModifiedProperties = new List<string>();
            }
            certificate.ModifiedProperties.Add(nameof(certificate.StatusCode));
            certificate.ModifiedProperties.Add(nameof(certificate.AStatusHes));

            if (newStatus.Code == ApplicationConstants.ApplicationStatuses.Delivered)
            {
                await MoveCertificateToWCertificate(certificate);

                if (certificate.Application == null)
                {

                    AApplication a = await _certificateRepository.SingleOrDefaultAsync<AApplication>(x => x.Id == certificate.ApplicationId);
                    //a.Id = certificate.ApplicationId;
                    a.EntityState = Common.Enums.EntityStateEnum.Modified;
                    a.ModifiedProperties = new List<string>() { nameof(a.StatusCode) };
                    a.StatusCode = ApplicationConstants.ApplicationStatuses.DeliveredApplication;
                    certificate.Application = a;

                }
                else
                {
                    certificate.Application.EntityState = Common.Enums.EntityStateEnum.Modified;
                    if (certificate.Application.ModifiedProperties == null)
                    {
                        certificate.Application.ModifiedProperties = new List<string>() { nameof(certificate.Application.StatusCode) };

                    }
                    else
                    {
                        certificate.Application.ModifiedProperties.Add(nameof(certificate.Application.StatusCode));
                    }
                    certificate.Application.StatusCode = ApplicationConstants.ApplicationStatuses.DeliveredApplication;
                }
                CreateAStatusH(certificate.ApplicationId, null, ApplicationConstants.ApplicationStatuses.DeliveredApplication, "Доставено свидетелство");
            }
            baseAsyncRepository.ApplyChanges(certificate, new List<IBaseIdEntity>());
            baseAsyncRepository.ApplyChanges(aStatusH, new List<IBaseIdEntity>());
            //dbContext.AStatusHes.Add(aStatusH);
            //dbContext.ACertificates.Update(certificate);

        }

        private async Task MoveCertificateToWCertificate(ACertificate certificate)
        {
            ACertificate certificateFromDb = new ACertificate();
            bool isFromDB = false;
            if (certificate.Doc?.DocContent == null || certificate.Doc.DocContent.Content == null
                || certificate.Doc.DocContent.Content.Length == 0)
            {
                try
                {
                    certificateFromDb = await _certificateRepository.GetCertificateWithDocumentContent(certificate.Id);
                    isFromDB = true;
                    if (certificateFromDb.Doc?.DocContent == null || certificateFromDb.Doc.DocContent.Content == null
               || certificateFromDb.Doc.DocContent.Content.Length == 0)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Свидетелството няма генериран pdf.");
                }

            }
            WCertificate wcert = new WCertificate();
            wcert.Id = BaseEntity.GenerateNewId();
            wcert.ACertId = certificate.Id;
            wcert.Md5 = isFromDB ? certificateFromDb.Doc.DocContent.Md5Hash : certificate.Doc.DocContent.Md5Hash;
            wcert.AccessCode1 = certificate.AccessCode1;
            wcert.AccessCode2 = certificate.AccessCode2;
            if (!isFromDB && certificate.Doc.DocContent.Bytes.HasValue)
            {
                wcert.Bytes = certificate.Doc.DocContent.Bytes.Value;
            }
            if (isFromDB && certificateFromDb.Doc.DocContent.Bytes.HasValue)
            {
                wcert.Bytes = certificateFromDb.Doc.DocContent.Bytes.Value;
            }
            wcert.Sha1 = isFromDB ? certificateFromDb.Doc.DocContent.Sha1Hash : certificate.Doc.DocContent.Sha1Hash;

            wcert.ValidTo = certificate.ValidTo;
            wcert.Content = isFromDB ? certificateFromDb.Doc.DocContent.Content : certificate.Doc.DocContent.Content;
            wcert.MimeType = isFromDB ? certificateFromDb.Doc.DocContent.MimeType : certificate.Doc.DocContent.MimeType;
            wcert.RegistrationNumber = certificate.RegistrationNumber;
            wcert.WApplId = certificate.Application?.WApplicationId;

            wcert.EntityState = Common.Enums.EntityStateEnum.Added;
            baseAsyncRepository.ApplyChanges(wcert, new List<IBaseIdEntity>());
        }

        public async Task UploadSignedDocumet(string certID, CertificateDocumentDTO aInDto)
        {
            var result = await this._certificateRepository.GetWithDocContentAsync(certID);
            result.Doc.DocContent.Content = aInDto.DocumentContent;
            await this._dDocContentRepository.UpdateAsync(result.Doc.DocContent);
        }

        public async Task UpdateCertificateStatus(string certID)
        {
            var result = await this._certificateRepository.SelectAsync(certID);

            var aapplicationStatus = await _certificateRepository.SingleOrDefaultAsync<AApplicationStatus>(x =>
             x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            //await dbContext.AApplicationStatuses.FirstOrDefaultAsync(x =>
            //  x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            await SetCertificateStatus(result, aapplicationStatus, "Преминаване към готово за връчване");
            await _certificateRepository.SaveChangesAsync();

        }




        public async Task<DDocContent> GetCertificateDocumentContent(string accessCode)
        {
            DDocContent? content = await _certificateRepository.GetCertificateDocumentByAccessCode(accessCode);
            return content;
        }

        public async Task<WCertificateDTO> GetWebCertificateByAccessCodeAsync(string accessCode)
        {
            WCertificate? cert = await baseAsyncRepository.SingleOrDefaultAsync<WCertificate>(w => w.AccessCode1 == accessCode);

            var content = _mapper.Map<WCertificate, WCertificateDTO>(cert);
            return content;
        }


        public async Task SaveSignerDataAsync(CertificateDTO aInDto)
        {
            var entity = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            //var dbContext = _certificateRepository.GetDbContext();
            await _certificateRepository.SaveEntityAsync(entity, false, clearTracker: true);

        }

        public async Task SaveSignerDataByJudgeAsync(CertificateDTO aInDto)
        {
            var certificate = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            //var dbContext = _certificateRepository.GetDbContext();

            IQueryable<AAppBulletin> allCertificateBulletins = await _certificateRepository.FindAsync<AAppBulletin>(x => x.CertificateId == aInDto.Id);


            foreach (var item in allCertificateBulletins)
            {
                item.Approved = aInDto.SelectedBulletinsIds.Contains(item.Id);
                item.ModifiedProperties = new List<string>() { nameof(item.Approved) };
                item.EntityState = EntityStateEnum.Modified;
                _certificateRepository.ApplyChanges(certificate);
                _certificateRepository.ApplyChanges(item);
            }

            await _certificateRepository.SaveChangesAsync();
        }



        public async Task<CertificateDTO> GetByApplicationIdAsync(string appId)
        {
            var certificate = await _certificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null) return null;

            var result = mapper.Map<ACertificate, CertificateDTO>(certificate);
            result.CurrentUserAuthId = _userContext.CsAuthorityId;
            return result;
        }

        public async Task SetStatusToDelivered(string appId)
        {
            var certificate = await _certificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null)
            {
                throw new BusinessLogicException("Invalid application Id!");
            }

            var aapplicationStatus = await _certificateRepository.SingleOrDefaultAsync<AApplicationStatus>(x =>
                x.Code == ApplicationConstants.ApplicationStatuses.Delivered);
            SetCertificateStatus(certificate, aapplicationStatus,
                "�������!");

            await _certificateRepository.SaveEntityAsync(certificate, false, true);
        }

        public async Task SetStatusToCanceled(string appId)
        {
            var certificate = await _certificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null)
            {
                throw new BusinessLogicException("Invalid application Id!");
            }

            var aapplicationStatus = await _certificateRepository.SingleOrDefaultAsync<AApplicationStatus>(x =>
                x.Code == ApplicationConstants.ApplicationCertificateStatuses.CanceledCertificate);
            SetCertificateStatus(certificate, aapplicationStatus,
                "�������!");

            await GenerateCertificateFromApplication(appId);

            await _certificateRepository.SaveEntityAsync(certificate, false, true);
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
            //трябва да са попълнени следните стойности:
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
                    throw new BusinessLogicException("Идентификаторите на това заявление са за повече от един човек!");
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
                    throw new BusinessLogicException("Идентификаторите на това заявление са за повече от един човек!");
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
                    throw new BusinessLogicException("Идентификаторите на това заявление са за повече от един човек!");
                }
            }

            if (personId != null)
            {
                //todo: дали да не  е union? - да добре е да е Union
                _logger.LogTrace($"{application.Id}: Before _applicationRepository.SelectBulletinIdsAsync.");
                var bulletins = await (await _applicationRepository.SelectBulletinIdsAsync(personId)).ToListAsync();//.ToListAsync();
                _logger.LogTrace($"{application.Id}: After _applicationRepository.SelectBulletinIdsAsync.");
                //var bulletins = await (await baseAsyncRepository.FindAsync<BBulletin>(b => b.Status.Code != BulletinConstants.Status.Deleted &&
                //               (pids.Contains(b.EgnId) ||
                //               pids.Contains(b.LnchId) ||
                //               pids.Contains(b.LnId) ||
                //               pids.Contains(b.IdDocNumberId) ||
                //               pids.Contains(b.SuidId)))).ToListAsync();
                //var bulletins = await dbContext.BBulletins
                //    .Where(b => b.Status.Code != BulletinConstants.Status.Deleted &&
                //                //&& b.PBulletinIds.Any(bulID =>
                //                //    pids.Contains(bulID.Person.PersonId
                //                (pids.Contains(b.EgnId) ||
                //                 pids.Contains(b.LnchId) ||
                //                 pids.Contains(b.LnId) ||
                //                 pids.Contains(b.IdDocNumberId) ||
                //                 pids.Contains(b.SuidId)))
                //    .ToListAsync();
                bulletins = bulletins.Where(b => b.StatusId != BulletinConstants.Status.Deleted).ToList();
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

        private async Task<ACertificate> ProcessApplicationWithoutBulletinsAsync(AApplication application,
            AApplicationStatus certificateStatus, int certificateValidityMonths, AApplicationStatus aStatus)
        {
            var cert = await CreateCertificateAsync(application.Id, certificateStatus, certificateValidityMonths,
                application.CsAuthorityId, application.ApplicationType.Code);
            //todo: add resources
            if (application.StatusCode != aStatus.Code)
            {
                SetApplicationStatus(application, aStatus, "Създаване на сертификат");
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

        private async Task<ACertificate> ProcessApplicationWithBulletinsAsync(AApplication application, List<BBulletin> bulletins,
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
                        OrderNumber = orderNumber,
                        EntityState = EntityStateEnum.Added
                    };
                }).ToList();
            //todo: add resources
            if (application.StatusCode != aStatus.Code)
            {
                SetApplicationStatus(application, aStatus, "Създаване на сертификат");
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

        public void SetApplicationStatus(AApplication application, AApplicationStatus newStatus, string description,
            bool includeInDbContext = true)
        {
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
            baseAsyncRepository.ApplyChanges(aStatusH, new List<IBaseIdEntity>());

            // application.AStatusHes.Add(aStatusH);
            //if (includeInDbContext)
            //{
            //    dbContext.AStatusHes.Add(aStatusH);
            //    dbContext.AApplications.Update(application);
            //}
        }

        private async Task<ACertificate> CreateCertificateAsync(string applicationId, AApplicationStatus aStatus,
            int certificateValidityMonths, string csAuthorityId, string applicationTypeCode)
        {
            var cert = new ACertificate();
            cert.Id = BaseEntity.GenerateNewId();
            cert.EntityState = EntityStateEnum.Added;
            cert.ApplicationId = applicationId;
            await SetCertificateStatus(cert, aStatus, "Създаване на сертификат");


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

        public async Task<List<CertificateGridDTO>> GetCanceledByApplicationIdAsync(string appId)
        {
            var query = _certificateRepository.GetCanceledByApplicationId(appId);
            var baseQuery = query.ProjectTo<CertificateGridDTO>(mapperConfiguration);
            var result = baseQuery.ToList();

            return await Task.FromResult(result);
        }

        public async Task<IQueryable<BulletinCheckDTO>> GetBulletinsCheckByIdAsync(string certId)
        {
            var query = await _certificateRepository.GetBulletinsCheckByIdAsync(certId, false);
            var result = mapper.ProjectTo<BulletinCheckDTO>(query, mapperConfiguration);

            return result;
        }

        public async Task SetCertificateForSelectionAsync(string aId)
        {
            //var dbContext = _certificateRepository.GetDbContext();

            //var certificate = await dbContext.ACertificates
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(a => a.Id == aId);
            //дали да не се ползва Select метода от репозиторито
            var certificate = await _certificateRepository.SingleOrDefaultAsync<ACertificate>(a => a.Id == aId);

            if (certificate == null)
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));

            UpdateCertificateStatus(certificate, ApplicationConstants.ApplicationStatuses.BulletinsSelection);
            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode, CertificateResources.msgStatusForRehabilitation);

            await _certificateRepository.SaveChangesAsync();
        }

        /// <summary>
        /// newRequestId generated from client
        /// PUT API Response 201 (Created), 200 (OK) or 204 (No Content) 
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="newRequestId"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public async Task SetBulletinsForRehabilitationAsync(string aId, string newRequestId, string[] ids)
        {
            ACertificate? certificate = await _certificateRepository.GetCertificateData(aId);

            if (certificate == null)
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));

            //UpdateCertificateStatus(certificate, ApplicationConstants.ApplicationStatuses.BulletinsSelection);

            //CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode, CertificateResources.msgStatusForRehabilitation);

            var currentAppl = certificate.Application;
            if (currentAppl == null)
                throw new BusinessLogicException(string.Format(ApplicationResources.msgApplicationDoesNotExist, aId));

            var pidId = string.Empty;

            if (!string.IsNullOrEmpty(currentAppl.EgnId))
            {
                pidId = currentAppl.EgnId;
            }
            else if (!string.IsNullOrEmpty(currentAppl.LnchId))
            {
                pidId = currentAppl.LnchId;
            }
            else if (!string.IsNullOrEmpty(currentAppl.LnId))
            {
                pidId = currentAppl.LnId;
            }
            else
            {
                pidId = currentAppl.SuidId;
            }

            var regNumberr = await _registerTypeService.GetRegisterNumberForInternalRequest(_userContext.CsAuthorityId);

            var request = new NInternalRequest
            {
                Id = newRequestId, // used for redirect
                RequestDate = DateTime.Now,
                FromAuthorityId = _userContext.CsAuthorityId,
                PPersIdId = pidId,
                ReqStatusCode = InternalRequestConstants.Status.Draft,
                RegNumber = regNumberr,
                NIntReqTypeId = InternalRequestConstants.Types.Rehabilitation,
                EntityState = EntityStateEnum.Added,
                NInternalReqBulletins = new List<NInternalReqBulletin>()
            };

            foreach (var appBullId in ids)
            {
                request.NInternalReqBulletins.Add(new NInternalReqBulletin
                {
                    Id = BaseEntity.GenerateNewId(),
                    BulletinId = certificate.AAppBulletins.FirstOrDefault(x => x.Id == appBullId)?.BulletinId,
                    InternalReqId = request.Id,
                    EntityState = EntityStateEnum.Added,
                });
            }

            _certificateRepository.ApplyChanges(request, applyToAllLevels: true);
            await _certificateRepository.SaveChangesAsync();
        }

        private void UpdateCertificateStatus(ACertificate? certificate, string statusCode)
        {
            certificate.StatusCode = statusCode;
            certificate.EntityState = Common.Enums.EntityStateEnum.Modified;
            certificate.ModifiedProperties = new List<string> { nameof(certificate.StatusCode), nameof(certificate.Version) };
            _certificateRepository.ApplyChanges(certificate, new List<IBaseIdEntity>());
        }

        private void CreateAStatusH(string appId, string certId, string status, string desc)
        {
            var result = new AStatusH
            {
                Id = BaseEntity.GenerateNewId(),
                ApplicationId = appId,
                CertificateId = certId,
                StatusCode = status,
                Descr = desc,
                EntityState = Common.Enums.EntityStateEnum.Added
            };

            _certificateRepository.ApplyChanges(result, new List<IBaseIdEntity>());
        }

        public Task<IQueryable<CertificateExternalDTO>> SelectExternalCertificates(string userId)
        {
            return _certificateRepository.SelectExternalCertificates(userId);
        }
        public Task<IQueryable<CertificatePublicDTO>> SelectPublicCertificates(string userId)
        {
            return _certificateRepository.SelectPublicCertificates(userId);
        }
    }
}
