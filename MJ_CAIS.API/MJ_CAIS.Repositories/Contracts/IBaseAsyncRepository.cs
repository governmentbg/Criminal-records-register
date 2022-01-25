using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBaseAsyncRepository<TEntity, TPk, TContext>
        where TEntity : class
        where TContext : DbContext
    {
        IQueryable<TEntity> SelectAllAsync();

        Task<TEntity> SelectAsync(TPk aId);

        Task<TEntity> InsertAsync(TEntity aEntity);

        Task<TEntity> UpdateAsync(TEntity aEntity);

        Task<TEntity> DeleteAsync(TPk aId);

        TContext GetDbContext();
    }
}
