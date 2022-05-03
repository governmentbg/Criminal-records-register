using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IIsinDataRepository : IBaseAsyncRepository<EIsinDatum, string, CaisDbContext>
    {
        Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync();
    }
}
