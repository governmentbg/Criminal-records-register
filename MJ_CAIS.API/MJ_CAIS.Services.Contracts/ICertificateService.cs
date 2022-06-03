using MJ_CAIS.DTO.Certificate;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface ICertificateService : IBaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
        public void SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description);
        Task<DDocContent> GetCertificateDocumentContent(string accessCode);
        Task SaveSignerDataAsync(CertificateSignerDTO aInDto);
    }
}
