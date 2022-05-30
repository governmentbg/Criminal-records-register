using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IFbbcRepository : IBaseAsyncRepository<Fbbc, string, CaisDbContext>
    {
        Task<IQueryable<FbbcGridDTO>> SelectByStatusCodeAsync(string statusCode);
    }
}
