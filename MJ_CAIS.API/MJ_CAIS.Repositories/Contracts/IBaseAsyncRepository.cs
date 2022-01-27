using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBaseAsyncRepository<TEntity, TPk, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        IQueryable<TEntity> SelectAllAsync();

        Task<TEntity> SelectAsync(TPk id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TPk id);

        TContext GetDbContext();
    }
}
