using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public CertificateService(IMapper mapper,
            ICertificateRepository certificateRepository,
            IUserContext userContext,
            IDDocContentRepository dDocContentRepository,
            IRegisterTypeService registerTypeService
            )
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _userContext = userContext;
            _dDocContentRepository = dDocContentRepository;
            _registerTypeService = registerTypeService;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId)
            => await _certificateRepository.GetCertificateContentByWebAppIdAsync(webAppId);

        public void SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description)
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
                MoveCertificateToWCertificate(certificate);

                if (certificate.Application == null)
                {
                    AApplication a = new AApplication();
                    a.Id = certificate.ApplicationId;
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
                CreateAStatusH(certificate.ApplicationId, null, ApplicationConstants.ApplicationStatuses.DeliveredApplication, "��������� ������������");
            }
            baseAsyncRepository.ApplyChanges(certificate, new List<IBaseIdEntity>());
            baseAsyncRepository.ApplyChanges(aStatusH, new List<IBaseIdEntity>());
            //dbContext.AStatusHes.Add(aStatusH);
            //dbContext.ACertificates.Update(certificate);

        }

        private void MoveCertificateToWCertificate(ACertificate certificate)
        {
            if (certificate.Doc?.DocContent == null || certificate.Doc.DocContent.Content == null
                || certificate.Doc.DocContent.Content.Length == 0)
            {
                throw new Exception("�������������� ���� ��������� pdf.");
            }
            WCertificate wcert = new WCertificate();
            wcert.Id = BaseEntity.GenerateNewId();
            wcert.ACertId = certificate.Id;
            wcert.Md5 = certificate.Doc.DocContent.Md5Hash;
            wcert.AccessCode1 = certificate.AccessCode1;
            wcert.AccessCode2 = certificate.AccessCode2;
            if (certificate.Doc.DocContent.Bytes.HasValue)
            {
                wcert.Bytes = certificate.Doc.DocContent.Bytes.Value;
            }
            wcert.Sha1 = certificate.Doc.DocContent.Sha1Hash;
            if (certificate.ValidFrom.HasValue)
            {
                wcert.ValidFrom = certificate.ValidFrom.Value;
            }
            wcert.ValidTo = certificate.ValidTo;
            wcert.Content = certificate.Doc.DocContent.Content;
            wcert.MimeType = certificate.Doc.DocContent.MimeType;
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
            SetCertificateStatus(result, aapplicationStatus, "����������� ��� ������ �� ��������");
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

            // var dbContext = _certificateRepository.GetDbContext();

            IQueryable<AAppBulletin> allCertificateBulletins = await _certificateRepository.FindAsync<AAppBulletin>(x => x.CertificateId == certificate.Id);


            foreach (var item in allCertificateBulletins)
            {
                item.Approved = aInDto.SelectedBulletinsIds.Contains(item.Id);
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
            //���� �� �� �� ������ Select ������ �� ������������
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

            UpdateCertificateStatus(certificate, ApplicationConstants.ApplicationStatuses.BulletinsRehabilitation);

            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode, CertificateResources.msgStatusForRehabilitation);

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
