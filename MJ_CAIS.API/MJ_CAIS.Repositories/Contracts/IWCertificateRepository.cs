using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IWCertificateRepository : IBaseAsyncRepository<WCertificate, string, CaisDbContext>
    {
        Task<WCertificate> GetByApplicationIdAsync(string appId);
    }
}
