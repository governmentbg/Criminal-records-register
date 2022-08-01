using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBaseNomenclatureRepository<TEntity, TContext>
          where TEntity : class, IBaseNomenclature, new()
        where TContext : CaisDbContext

    {
        Task<TEntity> SelectByCodeAsync(string code);
    }
}
