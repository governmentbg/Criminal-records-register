﻿using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using System.Linq.Expressions;

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

        //TContext GetDbContext();
        string? GetCurrentUserId();

        Task SaveChangesAsync();

        Task SaveEntityAsync<T>(T entity, bool includeRelations) where T : class, IBaseIdEntity;

        void ApplyChanges<T>(T entity, List<IBaseIdEntity> passedNavigationProperties, bool applyToAllLevels = false, bool isRoot = true)
            where T : class, IBaseIdEntity;

        public void ApplyChanges<T>(ICollection<T> listEntries, List<IBaseIdEntity> passedNavigationProperties, bool applyToAllLevels = false)
            where T : class, IBaseIdEntity;

        Task<IQueryable<T>> FindAsync<T>
      (Expression<Func<T, bool>> expression) where T : class;


        Task<T> SingleOrDefaultAsync<T>
        (Expression<Func<T, bool>> expression) where T : class;

        Task SaveEntityListAsync<T>(ICollection<T> entities, bool applyToAllLevels = false)
            where T : class, IBaseIdEntity;

    }
}
