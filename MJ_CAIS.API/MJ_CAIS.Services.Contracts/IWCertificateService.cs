using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WCertificate;

namespace MJ_CAIS.Services.Contracts
{
    public interface IWCertificateService : IBaseAsyncService<WCertificateDTO, WCertificateDTO, WCertificateGridDTO, WCertificate, string>
    {
        Task<WCertificateDTO> GetByApplicationIdAsync(string appId);
        Task<byte[]> GetContentByApplicationIdAsync(string appId);
    }
}
