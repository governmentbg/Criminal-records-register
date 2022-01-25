using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public abstract class BaseAsyncRepository<TEntity, TContext> : IBaseAsyncRepository<TEntity, string, TContext>
        where TEntity : BaseEntity, new()
        where TContext : CaisDbContext
    {
        protected readonly TContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAsyncRepository{T}"/> class.
        /// </summary>
        /// <param name="aDbContext"></param>
        protected BaseAsyncRepository(TContext aDbContext)
        {
            this._dbContext = aDbContext;
        }

        public TContext GetDbContext()
        {
            return this._dbContext;
        }

        public virtual IQueryable<TEntity> SelectAllAsync()
        {
            var result = this._dbContext.Set<TEntity>().AsNoTracking();
            return result;
        }

        public virtual async Task<TEntity> SelectAsync(string aId)
        {
            var result = await this._dbContext.Set<TEntity>().AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == aId);
            return result;
        }

        // TODO: change with new tracker
        public virtual async Task<TEntity> InsertAsync(TEntity aEntity)
        {
            _dbContext.Entry(aEntity).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return aEntity;
        }

        // TODO: change with new tracker
        public virtual async Task<TEntity> UpdateAsync(TEntity aEntity)
        {
            _dbContext.Entry(aEntity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return aEntity;
        }

        // TODO: change with new tracker
        public virtual async Task<TEntity> DeleteAsync(string aId)
        {
            TEntity repoObj = await this.SelectAsync(aId);
            _dbContext.Entry(repoObj).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();
            return repoObj;
        }
    }
}
