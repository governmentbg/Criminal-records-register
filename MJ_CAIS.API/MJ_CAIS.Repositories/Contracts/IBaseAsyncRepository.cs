using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBaseAsyncRepository<TEntity, TPk, TContext>
        where TEntity : class, IBaseIdEntity
        where TContext : DbContext
    {
        IQueryable<TEntity> SelectAll();

        Task<TEntity> SelectAsync(TPk id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TPk id);

        TContext GetDbContext();
    }
}
