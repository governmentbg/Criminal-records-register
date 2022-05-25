using AutoMapper;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO;
using MJ_CAIS.DTO.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MJ_CAIS.AutoMapperContainer
{
    public static class CaisMapper
    {
        private const string CollectionNamespace = "System.Collections.Generic";

        public static List<TDestination> MapToList<TSource, TDestination>(this IMapper mapper, ICollection<TSource> aSourceList)
        {
            if (aSourceList == null)
            {
                return null;
            }

            List<TDestination> result = new List<TDestination>();
            foreach (var item in aSourceList)
            {
                result.Add(mapper.Map<TDestination>(item));
            }

            return result;
        }

        public static EntityType MapToEntity<ViewModelType, EntityType>(this IMapper mapper, ViewModelType viewModel, bool isAdded)
            where ViewModelType : class
            where EntityType : class, IBaseIdEntity
        {
            if (viewModel == null)
            {
                return null;
            }

            EntityType entity = mapper.Map<ViewModelType, EntityType>(viewModel);
            mapper.ApplyChangesToEntity(viewModel, entity, isAdded);

            return entity;
        }

        public static EntityType MapToEntity<ViewModelType, EntityType>(this IMapper mapper, ViewModelType viewModel, EntityType entity, bool isAdded)
            where ViewModelType : class
            where EntityType : class, IBaseIdEntity
        {
            if (viewModel == null)
            {
                return null;
            }

            mapper.Map(viewModel, entity);
            mapper.ApplyChangesToEntity(viewModel, entity, isAdded);

            return entity;
        }

        private static void ApplyChangesToEntity<ViewModelType, EntityType>(this IMapper mapper, ViewModelType viewModel, EntityType entity, bool isAdded)
            where ViewModelType : class
            where EntityType : IBaseIdEntity
        {
            if (isAdded)
            {
                entity.EntityState = EntityStateEnum.Added;

                var versionPropertyInfo = entity.GetType().GetProperty(nameof(BaseEntity.Version));
                if (versionPropertyInfo != null && versionPropertyInfo.PropertyType == typeof(decimal?))
                {
                    versionPropertyInfo.SetValue(entity, null);
                }

                var navigationProperties = DBContextExtensions.GetNavigationDependencies(entity);
                foreach (var property in navigationProperties)
                {
                    property.EntityState = EntityStateEnum.Added;
                }
            }
            else
            {
                entity.EntityState = EntityStateEnum.Modified;

                var destMappedProperties = mapper.GetDestMappedProperties(typeof(ViewModelType), typeof(EntityType));
                if (entity.ModifiedProperties == null)
                {
                    entity.ModifiedProperties = destMappedProperties;
                }
                else
                {
                    var uniqueProperties = new HashSet<string>(entity.ModifiedProperties);
                    uniqueProperties.UnionWith(destMappedProperties);

                    entity.ModifiedProperties = uniqueProperties.ToList();
                }
            }
        }

        public static List<EntityType> MapToEntityList<ViewModelType, EntityType>(this IMapper mapper, List<ViewModelType> viewModels, bool isAdded, bool generateNewId = false)
            where ViewModelType : class
            where EntityType : class, IBaseIdEntity
        {
            var result = new List<EntityType>();
            foreach (var model in viewModels)
            {
                EntityType entity = mapper.MapToEntity<ViewModelType, EntityType>(model, isAdded);
                if (generateNewId)
                {
                    entity.Id = BaseEntity.GenerateNewId();
                }

                result.Add(entity);
            }

            return result;
        }

        public static List<string> GetDestMappedProperties(this IMapper mapper, Type srcType, Type destType)
        {
            var map = mapper.ConfigurationProvider.FindTypeMapFor(srcType, destType);

            if (map == null) return null;

            var properties = map.PropertyMaps
                .Where(x => x.SourceType != null &&
                    x.SourceType.Namespace != CollectionNamespace &&
                    x.DestinationType.Namespace != CollectionNamespace &&
                    !x.DestinationType.IsSubclassOf(typeof(BaseEntity)) &&
                    !HasMappedAttribute(x) &&
                    !x.Ignored).ToList();

            var destProps = properties.Select(x => x.DestinationMember.Name)
                .Where(x => x != nameof(IBaseIdEntity.Id))
                .Where(x => x != nameof(BaseEntity.EntityState))
                .Where(x => x != nameof(BaseEntity.PrimaryKeyName))
                .ToList();

            return destProps;
        }

        public static EntityType MapTransaction<ViewModelType, EntityType>(this IMapper mapper, TransactionDTO<ViewModelType> transaction, string entityPKName = "Id")
            where ViewModelType : BaseDTO
            where EntityType : class, IBaseIdEntity
        {
            EntityType entity = null;
            switch (transaction.Type)
            {
                case TransactionTypesEnum.ADD:
                    entity = mapper.MapToEntity<ViewModelType, EntityType>(transaction.NewValue, isAdded: true);
                    ChangeTransactionIdOnAdd(entity, entityPKName);
                    entity.EntityState = EntityStateEnum.Added;
                    break;

                case TransactionTypesEnum.UPDATE:
                    entity = mapper.MapToEntity<ViewModelType, EntityType>(transaction.NewValue, isAdded: false);
                    entity.ModifiedProperties = mapper.GetDestMappedProperties(typeof(ViewModelType), typeof(EntityType));
                    entity.EntityState = EntityStateEnum.Modified;
                    break;

                case TransactionTypesEnum.DELETE:
                    entity = CreateDummyEntity<EntityType>(transaction.Id, entityPKName, transaction.NewValue?.Version);
                    entity.EntityState = EntityStateEnum.Deleted;
                    break;

                default:
                    throw new NotSupportedException(CommonResources.MsgNotSuportedTransaction);
            }

            return entity;
        }

        public static List<EntityType> MapTransactions<ViewModelType, EntityType>(this IMapper mapper, List<TransactionDTO<ViewModelType>> transList, string entityPKName = "Id")
            where ViewModelType : BaseDTO
            where EntityType : class, IBaseIdEntity
        {
            if (transList == null) return null;

            List<EntityType> result = new List<EntityType>();
            foreach (var tr in transList)
            {
                EntityType entity = mapper.MapTransaction<ViewModelType, EntityType>(tr, entityPKName);
                result.Add(entity);
            }

            return result;
        }

        public static List<TEntityType> MapMultipleChooseToEntityList<TEntityType, TPrimaryKey, TForeignKey>(MultipleChooseDTO<TPrimaryKey, TForeignKey> multipleChooseEditor,
            string pkName, string fkName)
             where TEntityType : IBaseIdEntity
        {
            var entitiesForUpdate = new List<TEntityType>();
            if (!multipleChooseEditor.IsChanged) return entitiesForUpdate;

            if (multipleChooseEditor.SelectedPrimaryKeys != null)
            {
                //for delete
                foreach (var item in multipleChooseEditor.SelectedPrimaryKeys)
                {
                    var entity = Activator.CreateInstance<TEntityType>();
                    entity.EntityState = EntityStateEnum.Deleted;
                    entity.GetType().GetProperty(pkName)?.SetValue(entity, item);
                    entitiesForUpdate.Add(entity);
                }
            }

            if (multipleChooseEditor.SelectedForeignKeys != null)
            {
                //for add
                foreach (var item in multipleChooseEditor.SelectedForeignKeys)
                {
                    var entity = Activator.CreateInstance<TEntityType>();
                    ChangeTransactionIdOnAdd(entity, pkName);
                    entity.EntityState = EntityStateEnum.Added;
                    entity.GetType().GetProperty(fkName)?.SetValue(entity, item);
                    entitiesForUpdate.Add(entity);
                }
            }

            return entitiesForUpdate;
        }

        private static EntityType CreateDummyEntity<EntityType>(string transactionRowID, string entityPKName, decimal? version) where EntityType : IBaseIdEntity
        {
            EntityType entity = Activator.CreateInstance<EntityType>();
            var pkPropertyInfo = entity.GetType().GetProperty(entityPKName);
            if (pkPropertyInfo == null)
            {
                throw new ArgumentNullException(entityPKName, "Entity type does not have explicitly given property");
            }

            Type pkPropertyType = pkPropertyInfo.PropertyType;
            if (pkPropertyType.Equals(typeof(decimal)))
            {
                decimal rowID = decimal.Parse(transactionRowID);
                pkPropertyInfo.SetValue(entity, rowID);
            }

            if (pkPropertyType.Equals(typeof(int)))
            {
                int rowID = int.Parse(transactionRowID);
                pkPropertyInfo.SetValue(entity, rowID);
            }

            if (pkPropertyType.Equals(typeof(long)))
            {
                long rowID = long.Parse(transactionRowID);
                pkPropertyInfo.SetValue(entity, rowID);
            }

            if (pkPropertyType.Equals(typeof(string)))
            {
                pkPropertyInfo.SetValue(entity, transactionRowID);
            }

            var versionPropertyInfo = entity.GetType().GetProperty(nameof(BaseEntity.Version));
            if (versionPropertyInfo == null)
            {
                throw new ArgumentNullException(nameof(BaseEntity.Version), "Entity type does not have explicitly given property");
            }

            if (versionPropertyInfo.PropertyType == typeof(decimal?))
            {
                versionPropertyInfo.SetValue(entity, version);
            }

            return entity;
        }

        private static void ChangeTransactionIdOnAdd<EntityType>(EntityType entity, string entityPKName) where EntityType : IBaseIdEntity
        {
            if (entity is BaseEntity)
            {
                var currentEntity = entity as IBaseIdEntity;
                currentEntity.Id = Guid.NewGuid().ToString();
            }
        }

        private static bool HasMappedAttribute(PropertyMap propMap)
        {
            // get custom attributes of the current property
            var attributes = propMap?.DestinationMember?.CustomAttributes;
            var hasMappedAttr = attributes?.Any(a => a.AttributeType == typeof(NotMappedAttribute));
            var result = hasMappedAttr ?? false;
            return result;
        }
    }
}
