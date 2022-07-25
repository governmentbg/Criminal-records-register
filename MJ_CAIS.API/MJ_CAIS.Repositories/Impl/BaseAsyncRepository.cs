using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
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

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync();
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

        public async Task SaveEntityAsync<T>(T entity, bool includeRelations) where T : class, IBaseIdEntity
        {
            await _dbContext.SaveEntityAsync(entity, includeRelations);
        }
        public async Task<IEnumerable<T>> FindAsync<T>
          (Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T> SingleOrDefaultAsync<T>
        (Expression<Func<T, bool>> expression) where T : class
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(expression);
        }
    }
}
