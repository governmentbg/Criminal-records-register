using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Certificate;
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

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId)
            => await _certificateRepository.GetCertificateContentByWebAppIdAsync(webAppId);

        public void SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description)
        {
            certificate.StatusCode = newStatus.Code;
            certificate.StatusCodeNavigation = newStatus;
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

            certificate.AStatusHes.Add(aStatusH);
            dbContext.AStatusHes.Add(aStatusH);
            dbContext.ACertificates.Update(certificate);

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

            var aapplicationStatus = await dbContext.AApplicationStatuses.FirstOrDefaultAsync(x =>
               x.Code == ApplicationConstants.ApplicationStatuses.CertificateForDelivery);
            SetCertificateStatus(result, aapplicationStatus, "����������� ��� ������ �� ��������");
            await dbContext.SaveChangesAsync();

        }


        public async Task<DDocContent> GetCertificateDocumentContent(string accessCode)
        {
            var content = await dbContext.ACertificates.Where(x => x.AccessCode1 == accessCode && x.Doc != null).Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
            //if (content == null)
            //{
            //    throw new Exception("Certificate does not exist.");
            //}
            return content;
        }

        public async Task SaveSignerDataAsync(CertificateDTO aInDto)
        {
            var entity = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            var dbContext = _certificateRepository.GetDbContext();
            await dbContext.SaveEntityAsync(entity);
        }

        public async Task SaveSignerDataByJudgeAsync(CertificateDTO aInDto)
        {
            var certificate = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            var dbContext = _certificateRepository.GetDbContext();

            var allCertificateBulletins = dbContext.AAppBulletins
                .Where(x => x.CertificateId == certificate.Id);

            foreach (var item in allCertificateBulletins)
            {
                item.Approved = aInDto.SelectedBulletinsIds.Contains(item.Id);
            }

            await dbContext.SaveChangesAsync();
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
            var dbContext = _certificateRepository.GetDbContext();

            var certificate = await dbContext.ACertificates
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == aId);

            if (certificate == null)
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));

            UpdateCertificateStatus(dbContext, certificate, ApplicationConstants.ApplicationStatuses.BulletinsSelection);
            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode, CertificateResources.msgStatusForRehabilitation);

            await dbContext.SaveChangesAsync();
        }

        public async Task SetBulletinsForRehabilitationAsync(string aId, string[] ids)
        {
            var dbContext = _certificateRepository.GetDbContext();

            var certificate = await dbContext.ACertificates.AsNoTracking()
                .Include(x => x.AAppBulletins)
                .Where(x => x.Id == aId)
                .FirstOrDefaultAsync();

            if (certificate == null)
                throw new BusinessLogicException(string.Format(CertificateResources.msgCertificateDoesNotExist, aId));

            UpdateCertificateStatus(dbContext, certificate, ApplicationConstants.ApplicationStatuses.BulletinsRehabilitation);

            CreateAStatusH(certificate.ApplicationId, certificate.Id, certificate.StatusCode, CertificateResources.msgStatusForRehabilitation);

            var request = new List<BInternalRequest>();
            foreach (var appBullId in ids)
            {
                request.Add(new BInternalRequest
                {
                    Id = BaseEntity.GenerateNewId(),
                    BulletinId = certificate.AAppBulletins.FirstOrDefault(x => x.Id == appBullId)?.BulletinId,
                    AAppBulletinId = appBullId,
                    ReqStatusCode = InternalRequestStatusTypeConstants.New,
                    Description = CertificateResources.msgStatusForRehabilitationDesc,
                    EntityState = Common.Enums.EntityStateEnum.Added,
                    RequestDate = DateTime.UtcNow
                });
            }

            dbContext.ApplyChanges(request);
            await dbContext.SaveChangesAsync();
        }

        private static void UpdateCertificateStatus(CaisDbContext dbContext, ACertificate? certificate, string statusCode)
        {
            certificate.StatusCode = statusCode;
            certificate.EntityState = Common.Enums.EntityStateEnum.Modified;
            certificate.ModifiedProperties = new List<string> { nameof(certificate.StatusCode), nameof(certificate.Version) };
            dbContext.ApplyChanges(certificate, new List<IBaseIdEntity>());
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

            dbContext.ApplyChanges(result, new List<IBaseIdEntity>());
        }
    }
}
