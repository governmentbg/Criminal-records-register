using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IInternalRequestRepository : IBaseAsyncRepository<NInternalRequest, string, CaisDbContext>
    {
        Task<RequestCountDTO> GetInternalRequestsCountAsync();
    }
}
