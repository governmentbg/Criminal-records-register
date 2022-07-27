using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInternalRequestRepository : IBaseAsyncRepository<BInternalRequest, string, CaisDbContext>
    {
        Task<int> GetCountOfNewRequestsAsync();
        Task<bool> HasRequests(BInternalRequest entity, List<string> bullIdsForCert);
        Task<AAppBulletin> GetBulletinsInCertificate(BInternalRequest entity);
    }
}
