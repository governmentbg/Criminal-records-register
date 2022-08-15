using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IApplicationSearchRepository : IBaseAsyncRepository<AApplication, string, CaisDbContext>
    {
        Task<IQueryable<ApplicationSearchGridDTO>> SelectAllAsync();
    }
}
