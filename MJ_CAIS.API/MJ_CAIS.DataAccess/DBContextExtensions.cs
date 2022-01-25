using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Entities;
using MJ_CAIS.Entities.Common;
using System.Collections;
using System.Linq.Expressions;

namespace MJ_CAIS.DataAccess
{
    public static class DBContextExtensions
    {
        private static string TrackableValidationMessage = $"Parameter should be {nameof(BaseEntity)}";

        public static IQueryable<T> GetAsNoTracking<T>(this DbContext dbContext, Expression<Func<DbContext, DbSet<T>>> expression) where T : BaseEntity
        {
            var method = expression.Compile();
            var result = method(dbContext).AsNoTracking();
            return result;
        }

        public static void ApplyChanges<T>(this DbContext dbContext, ICollection<T> listEntries, List<BaseEntity> passedNavigationProperties, bool applyToAllLevels = false) where T : BaseEntity
        {


            for (int i = listEntries.Count - 1; i >= 0; i--)
            {
                T entity = listEntries.ElementAt(i);
                dbContext.ApplyChanges(entity, passedNavigationProperties, applyToAllLevels);
            }
        }

        private static void ChangeEntityState<T>(this DbContext dbContext, T entity) where T : BaseEntity
        {
            EntityEntry<T> entry = dbContext.Entry(entity);

            var entityState = entity.EntityState;
            switch (entityState)
            {
                case EntityStateEnum.Added:
                    entry.State = EntityState.Added;
                    break;

                case EntityStateEnum.Modified:
                    entry.State = EntityState.Added; //Important row
                    entry.State = EntityState.Unchanged;
                    entry.MarkModifiedProperies();
                    break;

                case EntityStateEnum.Deleted:
                    entry.State = EntityState.Deleted;
                    break;

                default:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        private static void ChangeEntityStateAsDeleted<T>(this DbContext dbContext, T entity) where T : BaseEntity
        {
            var entityAsDeletable = entity as IDeletableEntity;
            entityAsDeletable.IsDeleted = true;

            entity.EntityState = EntityStateEnum.Modified;
            entity.ModifiedProperties = new List<string>() { nameof(IDeletableEntity.IsDeleted) };
            dbContext.ChangeEntityState(entity);
        }

        public static void ApplyChanges<T>(this DbContext dbContext, T entity, List<BaseEntity> passedNavigationProperties, bool applyToAllLevels = false, bool isRoot = true) where T : BaseEntity
        {
            if (entity == null) return;

            // Applying db state changes to root element
            dbContext.ChangeEntityState(entity);

            // Applying db state changes to navigation properties
            var entityState = entity.EntityState;
            if (entityState == EntityStateEnum.Added || entityState == EntityStateEnum.Modified)
            {
                dbContext.ApplyChangesNavigationProperties(entity, passedNavigationProperties, applyToAllLevels, false);
            }
            else if (entityState == EntityStateEnum.Deleted && isRoot)
            {
                if (entity is IDeletableEntity)
                {
                    dbContext.ChangeEntityStateAsDeleted(entity);
                }
                else
                {
                    dbContext.ApplyChangesNavigationProperties(entity, passedNavigationProperties, applyToAllLevels, false);
                }
            }

            // Applying db state changes to collection elements (childs)
            if (applyToAllLevels)
            {
                var childCollections = entity.GetDependencies();
                foreach (List<BaseEntity> children in childCollections)
                {
                    foreach (BaseEntity child in children)
                    {
                        dbContext.ApplyChanges(child, passedNavigationProperties, applyToAllLevels, false);
                    }
                }
            }
        }

        private static void ApplyChangesNavigationProperties<T>(this DbContext dbContext, T entity, List<BaseEntity> passedNavigationProperties, bool applyToAllLevels = false, bool isRoot = true) where T : BaseEntity
        {
            var navigationValues = GetNavigationDependencies(entity);
            foreach (BaseEntity item in navigationValues)
            {
                if (!passedNavigationProperties.Contains(item))
                {
                    passedNavigationProperties.Add(item);
                    dbContext.ApplyChanges(item, passedNavigationProperties, applyToAllLevels, isRoot);
                }
            }
        }

        public static void SaveEntity<T>(this DbContext dbContext, T entity, bool applyToAllLevels = false) where T : BaseEntity
        {
            if (entity == null) return;
            if (!(entity is BaseEntity)) throw new ArgumentException(TrackableValidationMessage, nameof(entity));

            var passedNavigationProperties = new List<BaseEntity>();

            dbContext.ApplyChanges<T>(entity, passedNavigationProperties, applyToAllLevels);
            dbContext.SaveChanges();
        }

        public static async Task SaveEntityAsync<T>(this DbContext dbContext, T entity, bool applyToAllLevels = false) where T : BaseEntity
        {
            if (entity == null) return;
            if (!(entity is BaseEntity))
            {
                throw new ArgumentException(TrackableValidationMessage, nameof(entity));
            }

            var passedNavigationProperties = new List<BaseEntity>();

            dbContext.ApplyChanges<T>(entity, passedNavigationProperties, applyToAllLevels);
            await dbContext.SaveChangesAsync();
        }

        public static void SaveEntityList<T>(this DbContext dbContext, ICollection<T> entityList, bool applyToAllLevels = false) where T : BaseEntity
        {
            if (entityList == null || entityList.Count == 0) return;
            if (!typeof(T).IsSubclassOf(typeof(BaseEntity)))
            {
                throw new ArgumentException(TrackableValidationMessage, nameof(entityList));
            }

            var passedNavigationProperties = new List<BaseEntity>();

            dbContext.ApplyChanges<T>(entityList, passedNavigationProperties, applyToAllLevels);
            dbContext.SaveChanges();
        }

        public static async Task SaveEntityListAsync<T>(this DbContext dbContext, ICollection<T> entityList, bool applyToAllLevels = false) where T : BaseEntity
        {
            if (entityList == null || entityList.Count == 0) return;
            if (!typeof(T).IsSubclassOf(typeof(BaseEntity)))
            {
                throw new ArgumentException(TrackableValidationMessage, nameof(entityList));
            }

            var passedNavigationProperties = new List<BaseEntity>();

            dbContext.ApplyChanges<T>(entityList, passedNavigationProperties, applyToAllLevels);
            await dbContext.SaveChangesAsync();
        }

        private static List<List<BaseEntity>> GetDependencies<T>(this T entity) where T : class
        {
            var dependents = new List<List<BaseEntity>>();

            var properties = entity.GetType()
                .GetProperties()
                .Where(p => typeof(IEnumerable<BaseEntity>).IsAssignableFrom(p.PropertyType) && !typeof(string).IsAssignableFrom(p.PropertyType));

            foreach (var property in properties)
            {
                var values = property.GetValue(entity) as IEnumerable;
                if (values == null) continue;

                IEnumerable<BaseEntity> entityList = from object value in values select value as BaseEntity;
                if (entityList != null)
                {
                    dependents.Add(entityList.ToList());
                }
            }

            return dependents;
        }

        public static List<BaseEntity> GetNavigationDependencies(BaseEntity entity)
        {
            var dependents = new List<BaseEntity>();

            var properties = entity.GetType()
                .GetProperties()
                .Where(p => typeof(BaseEntity).IsAssignableFrom(p.PropertyType));

            foreach (var property in properties)
            {
                var values = property.GetValue(entity) as BaseEntity;
                if (values == null) continue;

                dependents.Add(values);
            }

            return dependents;
        }

        private static void MarkModifiedProperies<T>(this EntityEntry<T> entityEntry) where T : class
        {
            var entity = entityEntry.Entity as BaseEntity;
            foreach (string property in entity.ModifiedProperties)
            {
                entityEntry.Member(property).IsModified = true;
            }
        }
    }
}
