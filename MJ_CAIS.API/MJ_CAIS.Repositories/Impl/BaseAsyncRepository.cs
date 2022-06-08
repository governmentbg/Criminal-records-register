using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public abstract class BaseAsyncRepository<TEntity, TContext> : IBaseAsyncRepository<TEntity, string, TContext>
        where TEntity : class, IBaseIdEntity, new()
        where TContext : CaisDbContext
    {
        protected readonly TContext _dbContext;

        protected BaseAsyncRepository(TContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public TContext GetDbContext()
        {
            return this._dbContext;
        }

        public virtual IQueryable<TEntity> SelectAll()
        {
            var result = this._dbContext.Set<TEntity>().AsNoTracking();
            return result;
        }

        public virtual async Task<TEntity> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(string id)
        {
            TEntity repoObj = await this.SelectAsync(id);
            _dbContext.Entry(repoObj).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return repoObj;
        }
    }
}
