using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IApplicationRepository : IBaseAsyncRepository<AApplication, string, CaisDbContext>
    {
        Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync();
    }
}
