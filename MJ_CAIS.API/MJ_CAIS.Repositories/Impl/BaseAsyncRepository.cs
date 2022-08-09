using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

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

        private TContext GetDbContext()
        {
            return this._dbContext;
        }

        public string? GetCurrentUserId()
        {
            return _dbContext.CurrentUserId;
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

        public async Task SaveChangesAsync(bool clearTracker = false)
        {
            await this._dbContext.SaveChangesAsync();
            if (clearTracker)
            {
                this._dbContext.ChangeTracker.Clear();
            }
        }

        public void ApplyChanges<T>(ICollection<T> listEntries, List<IBaseIdEntity> passedNavigationProperties, bool applyToAllLevels = false)
           where T : class, IBaseIdEntity
        {
            _dbContext.ApplyChanges(listEntries, passedNavigationProperties, applyToAllLevels);
        }

        public void ApplyChanges<T>(T entity, List<IBaseIdEntity> passedNavigationProperties, bool applyToAllLevels = false, bool isRoot = true)
           where T : class, IBaseIdEntity
        {
            _dbContext.ApplyChanges(entity, passedNavigationProperties, applyToAllLevels,isRoot);
        }

        public async Task SaveEntityAsync<T>(T entity, bool includeRelations, bool clearTracker = false) where T : class, IBaseIdEntity
        {
            await _dbContext.SaveEntityAsync(entity, includeRelations);
            if (clearTracker)
            {
                this._dbContext.ChangeTracker.Clear();
            }
        }
        public async Task<IQueryable<T>> FindAsync<T>
          (Expression<Func<T, bool>> expression) where T : class
        {
            return await Task.FromResult( _dbContext.Set<T>().AsNoTracking().Where(expression));
        }

        public async Task<T> SingleOrDefaultAsync<T>
        (Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>().AsNoTracking().SingleOrDefaultAsync(expression);
        }

        public async Task SaveEntityListAsync<T>(ICollection<T> entities, bool applyToAllLevels = false)
            where T : class, IBaseIdEntity
        {
            await _dbContext.SaveEntityListAsync(entities, applyToAllLevels);
        }
    }
}
