using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class CertificateService : BaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string, CaisDbContext>, ICertificateService
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public CertificateService(IMapper mapper, ICertificateRepository certificateRepository, IUserContext userContext)
            : base(mapper, certificateRepository)
        {
            _certificateRepository = certificateRepository;
            _mapper = mapper;
            _userContext = userContext;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

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

            certificate.AStatusHes.Add(aStatusH);
            dbContext.AStatusHes.Add(aStatusH);
            dbContext.ACertificates.Update(certificate);


        }

        public async Task<DDocContent> GetCertificateDocumentContent(string accessCode)
        {

            var content = await dbContext.ACertificates.Where(x => x.AccessCode1 == accessCode && x.Doc != null).Select(x => x.Doc.DocContent).FirstOrDefaultAsync();
            if (content == null)
            {
                throw new Exception("Certificate does not exist.");
            }
            return content;
        }

        public async Task SaveSignerDataAsync(CertificateDTO aInDto)
        {
            var entity = _mapper.MapToEntity<CertificateDTO, ACertificate>(aInDto, false);

            var dbContext = _certificateRepository.GetDbContext();
            await dbContext.SaveEntityAsync(entity);
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
            var query = await _certificateRepository.GetBulletinsCheckByIdAsync(certId);
            var result = mapper.ProjectTo<BulletinCheckDTO>(query, mapperConfiguration);

            return result;
        }
    }
}
