using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class CertificateService :
        BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>,
        ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IDDocContentRepository _dDocContentRepository;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;


        public CertificateService(IMapper mapper,
            ICertificateRepository certificateRepository,
            IUserContext userContext,
            IDDocContentRepository dDocContentRepository
        )
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _userContext = userContext;
            _dDocContentRepository = dDocContentRepository;
        }

        public async Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId)
        {
            return await _certificateRepository.GetCertificateContentByWebAppIdAsync(webAppId);
        }

        public void SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description)
        {
            certificate.StatusCode = newStatus.Code;
            //certificate.StatusCodeNavigation = newStatus;
            var aStatusH = new AStatusH();
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
            aStatusH.EntityState = EntityStateEnum.Added;

            certificate.AStatusHes.Add(aStatusH);
            if (certificate.EntityState != EntityStateEnum.Added)
            {
                certificate.EntityState = EntityStateEnum.Modified;
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
                    var a = new AApplication();
                    a.Id = certificate.ApplicationId;
                    a.EntityState = EntityStateEnum.Modified;
                    a.ModifiedProperties = new List<string> { nameof(a.StatusCode) };
                    a.StatusCode = ApplicationConstants.ApplicationStatuses.DeliveredApplication;
                    certificate.Application = a;
                }
                else
                {
                    certificate.Application.EntityState = EntityStateEnum.Modified;
                    if (certificate.Application.ModifiedProperties == null)
                    {
                        certificate.Application.ModifiedProperties = new List<string>
                            { nameof(certificate.Application.StatusCode) };
                    }
                    else
                    {
                        certificate.Application.ModifiedProperties.Add(nameof(certificate.Application.StatusCode));
                    }

                    certificate.Application.StatusCode = ApplicationConstants.ApplicationStatuses.DeliveredApplication;
                }

                CreateAStatusH(certificate.ApplicationId, null,
                    ApplicationConstants.ApplicationStatuses.DeliveredApplication, "��������� ������������");
            }

            baseAsyncRepository.ApplyChanges(certificate, new List<IBaseIdEntity>());
            baseAsyncRepository.ApplyChanges(aStatusH, new List<IBaseIdEntity>());
            //dbContext.AStatusHes.Add(aStatusH);
            //dbContext.ACertificates.Update(certificate);
        }

        public async Task UploadSignedDocumet(string certID, CertificateDocumentDTO aInDto)
        {
            var result = await _certificateRepository.GetWithDocContentAsync(certID);
            result.Doc.DocContent.Content = aInDto.DocumentContent;
            await _dDocContentRepository.UpdateAsync(result.Doc.DocContent);
        }

        public async Task UpdateCertificateStatus(string certID)
        {
            var result = await _certificateRepository.SelectAsync(certID);

            var aapplicationStatus = await _certificateRepository.SingleOrDefaultAsync<AApplicationStatus>(x =>
                x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            //await dbContext.AApplicationStatuses.FirstOrDefaultAsync(x =>
            //  x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            SetCertificateStatus(result, aapplicationStatus, "����������� ��� ������ �� ��������");
            await _certificateRepository.SaveChangesAsync();
        }


        public async Task<DDocContent> GetCertificateDocumentContent(string accessCode)
        {
            var content = await _certificateRepository.GetCertificateDocumentByAccessCode(accessCode);
            return content;
        }

        public async Task<WCertificateDTO> GetWebCertificateByAccessCodeAsync(string accessCode)
        {
            var cert = await baseAsyncRepository.SingleOrDefaultAsync<WCertificate>(w => w.AccessCode1 == accessCode);

            var content = _mapper.Map<WCertificate, WCertificateDTO>(cert);
            return content;
        }


        public async Task SaveSignerDataAsync(CertificateDTO aInDto)
        {
            var entity = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            //var dbContext = _certificateRepository.GetDbContext();
            await _certificateRepository.SaveEntityAsync(entity, false, true);
        }

        public async Task SaveSignerDataByJudgeAsync(CertificateDTO aInDto)
        {
            var certificate = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            // var dbContext = _certificateRepository.GetDbContext();

            var allCertificateBulletins =
                await _certificateRepository.FindAsync<AAppBulletin>(x => x.CertificateId == certificate.Id);


            foreach (var item in allCertificateBulletins)
            {
                item.Approved = aInDto.SelectedBulletinsIds.Contains(item.Id);
            }

            await _certificateRepository.SaveChangesAsync();
        }


        public async Task<CertificateDTO> GetByApplicationIdAsync(string appId)
        {
            var certificate = await _certificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null)
            {
                return null;
            }

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
            //���� �� �� �� ������ Select ������ �� ������������
            var certificate = await _certificateRepository.SingleOrDefaultAsync<ACertificate>(a => a.Id == aId);

            if (certificate == null)
            {
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));
            }

            UpdateCertificateStatus(certificate, ApplicationConstants.ApplicationStatuses.BulletinsSelection);
            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode,
                CertificateResources.msgStatusForRehabilitation);

            await _certificateRepository.SaveChangesAsync();
        }

        public async Task SetBulletinsForRehabilitationAsync(string aId, string[] ids)
        {
            //var dbContext = _certificateRepository.GetDbContext();

            var certificate = await _certificateRepository.GetCertificateData(aId);

            if (certificate == null)
            {
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));
            }

            UpdateCertificateStatus(certificate, ApplicationConstants.ApplicationStatuses.BulletinsRehabilitation);

            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode,
                CertificateResources.msgStatusForRehabilitation);

            //todo: change
            var request = new List<IBaseIdEntity>();
            //var request = new List<BInternalRequest>();
            //foreach (var appBullId in ids)
            //{
            //    request.Add(new BInternalRequest
            //    {
            //        Id = BaseEntity.GenerateNewId(),
            //        BulletinId = certificate.AAppBulletins.FirstOrDefault(x => x.Id == appBullId)?.BulletinId,
            //        AAppBulletinId = appBullId,
            //        ReqStatusCode = InternalRequestStatusTypeConstants.New,
            //        Description = CertificateResources.msgStatusForRehabilitationDesc,
            //        EntityState = Common.Enums.EntityStateEnum.Added,
            //        RequestDate = DateTime.Now
            //    });
            //}

            _certificateRepository.ApplyChanges(request, new List<IBaseIdEntity>());
            await _certificateRepository.SaveChangesAsync();
        }


        public Task<IQueryable<CertificateExternalDTO>> SelectExternalCertificates(string userId)
        {
            return _certificateRepository.SelectExternalCertificates(userId);
        }

        public Task<IQueryable<CertificatePublicDTO>> SelectPublicCertificates(string userId)
        {
            return _certificateRepository.SelectPublicCertificates(userId);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }


        private void MoveCertificateToWCertificate(ACertificate certificate)
        {
            if (certificate.Doc?.DocContent == null || certificate.Doc.DocContent.Content == null
                                                    || certificate.Doc.DocContent.Content.Length == 0)
            {
                throw new Exception("�������������� ���� ��������� pdf.");
            }

            var wcert = new WCertificate();
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

            wcert.EntityState = EntityStateEnum.Added;
            baseAsyncRepository.ApplyChanges(wcert, new List<IBaseIdEntity>());
        }


        private void UpdateCertificateStatus(ACertificate? certificate, string statusCode)
        {
            certificate.StatusCode = statusCode;
            certificate.EntityState = EntityStateEnum.Modified;
            certificate.ModifiedProperties = new List<string>
                { nameof(certificate.StatusCode), nameof(certificate.Version) };
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
                EntityState = EntityStateEnum.Added
            };

            _certificateRepository.ApplyChanges(result, new List<IBaseIdEntity>());
        }
    }
}