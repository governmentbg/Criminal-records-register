using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Certificate;

namespace MJ_CAIS.Services.Contracts
{
    public interface ICertificateService : IBaseAsyncService<CertificateDTO, CertificateDTO, CertificateGridDTO, ACertificate, string>
    {
        public void SetCertificateStatus(ACertificate certificate, AApplicationStatus newStatus, string description);
        Task<DDocContent> GetCertificateDocumentContent(string accessCode);
        Task SaveSignerDataAsync(CertificateDTO aInDto);
        Task SaveSignerDataByJudgeAsync(CertificateDTO aInDto);
        Task<CertificateDTO> GetByApplicationIdAsync(string appId);
        Task<IQueryable<BulletinCheckDTO>> GetBulletinsCheckByIdAsync(string appId);
        Task SetBulletinsForRehabilitationAsync(string aId, string[] ids);
        Task SetCertificateForSelectionAsync(string aId);

        Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId);

        Task UploadSignedDocumet(string certID, CertificateDocumentDTO aInDto);

        Task UpdateCertificateStatus(string certID);
    }
}
