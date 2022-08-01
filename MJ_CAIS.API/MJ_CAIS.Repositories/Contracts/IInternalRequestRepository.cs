using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInternalRequestRepository : IBaseAsyncRepository<NInternalRequest, string, CaisDbContext>
    {
        Task<int> GetCountOfNewRequestsAsync();
        Task<bool> HasRequests(NInternalRequest entity, List<string> bullIdsForCert);
        Task<AAppBulletin> GetBulletinsInCertificate(NInternalRequest entity);
    }
}
