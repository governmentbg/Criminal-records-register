using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WCertificate;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class WCertificateService : BaseAsyncService<WCertificateDTO, WCertificateDTO, WCertificateGridDTO, WCertificate, string, CaisDbContext>, IWCertificateService
    {
        private readonly IWCertificateRepository _wCertificateRepository;

        public WCertificateService(IMapper mapper, IWCertificateRepository wCertificateRepository)
            : base(mapper, wCertificateRepository)
        {
            _wCertificateRepository = wCertificateRepository;
        }


        public async Task<WCertificateDTO> GetByApplicationIdAsync(string appId)
        {
            var certificate = await _wCertificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null) return null;

            var result = mapper.Map<WCertificate, WCertificateDTO>(certificate);

            return result;
        }

        public async Task<byte[]> GetContentByApplicationIdAsync(string appId)
        {
            var certificate = await _wCertificateRepository.GetByApplicationIdAsync(appId);
            if (certificate == null || certificate.Content == null) return null;

            return certificate.Content;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
