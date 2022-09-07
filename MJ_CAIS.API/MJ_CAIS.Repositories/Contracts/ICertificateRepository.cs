using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface ICertificateRepository : IBaseAsyncRepository<ACertificate, string, CaisDbContext>
    {
        Task<ACertificate> GetByApplicationIdAsync(string appId);

        Task<IQueryable<AAppBulletin>> GetBulletinsCheckByIdAsync(string certId, bool onlyApproved);

        Task<byte[]> GetCertificateContentByWebAppIdAsync(string webAppId);
        Task<ACertificate> GetWithDocContentAsync(string certId);

        Task<DDocContent> GetCertificateDocumentByAccessCode(string accessCode);

        Task<ACertificate> GetCertificateData(string aId);
        Task<ACertificate> GetCertificateWithDocumentContent(string certificateID);
        Task<ACertificate> GetCertificateWithIncludedDataForApplicationAndBulletins(string certificateID);
        Task<ACertificate> GetCertificateDataWithContentAndType(string certificateID);
        Task<IQueryable<CertificateExternalDTO>> SelectExternalCertificates(string userId);
        Task<IQueryable<CertificatePublicDTO>> SelectPublicCertificates(string userId);
        IQueryable<ACertificate> GetCanceledByApplicationId(string appId);

        Task<ArchivedDocumentContentDTO> GetArchiveDocumentContentAsync(string aId);
    }
}
