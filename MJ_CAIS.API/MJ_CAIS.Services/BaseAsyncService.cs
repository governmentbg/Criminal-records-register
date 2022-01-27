using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Query.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Entities;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using System.Text;
using MJ_CAIS.AutoMapperContainer;

namespace MJ_CAIS.Services
{
    public abstract class BaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk, TContext> : IBaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk>
        where TInDTO : class
        where TOutDTO : class
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private const int MAX_PAGE_SIZE = 100;

        private const char QUESTION_MARK = '?';
        private const char QUERY_OPTIONS_SEPARATOR = '&';
        private const char QUERY_OPTIONS_DELIMITER = '=';
        private const string QUERY_OPTIONS_SKIP = "$skip";
        private const string QUERY_OPTIONS_TOP = "$top";

        protected IBaseAsyncRepository<TEntity, TPk, TContext> baseAsyncRepository;
        protected IMapper mapper;
        protected IConfigurationProvider mapperConfiguration => mapper.ConfigurationProvider;

        /// <summary>
        /// Dictionary, в което се описва mapping-а между имената на полетата в dto-to и entity-то
        /// </summary>
        protected Dictionary<string, string> dtoFieldsToEntityFields;

        protected FilterQueryValidator queryValidator;

        protected ODataValidationSettings validationSettings;

        protected BaseAsyncService(IMapper mapper, IBaseAsyncRepository<TEntity, TPk, TContext> baseAsyncRepository, FilterQueryValidator queryValidator = null)
        {
            this.mapper = mapper;
            this.baseAsyncRepository = baseAsyncRepository;
            this.queryValidator = queryValidator ?? new CustomQueryValidator<TGridDTO>();
            this.validationSettings = new ODataValidationSettings();
            this.dtoFieldsToEntityFields = new Dictionary<string, string>();
            this.PopulateDtoToEntityFieldsMapping();
        }

        public ICollection<T> ApplyCheckboxChanges<T>(int[] ids, string foreignKey, bool isAdded, ICollection<T> dbEntities = null) where T : BaseEntity
        {
            dbEntities = dbEntities ?? new List<T>();
            var result = new List<T>();
            var fkProperty = typeof(T).GetProperty(foreignKey);
            var esProperty = typeof(T).GetProperty(nameof(BaseEntity.EntityState));

            // Mark for add
            foreach (var fkId in ids)
            {
                var contains = dbEntities.Any(x => (int)fkProperty.GetValue(x) == fkId);
                if (!contains)
                {
                    var instance = Activator.CreateInstance<T>();
                    fkProperty.SetValue(instance, fkId);
                    esProperty.SetValue(instance, EntityStateEnum.Added);
                    result.Add(instance);
                }
            }

            // Mark for delete
            foreach (var dbEntity in dbEntities)
            {
                var searchId = (int)fkProperty.GetValue(dbEntity);
                if (!ids.Contains(searchId))
                {
                    dbEntity.EntityState = EntityStateEnum.Deleted;
                    result.Add(dbEntity);
                }
            }

            return result;
        }

        public virtual async Task SaveEntityAsync(BaseEntity entity, bool applyToAllLevels = true)
        {
            var dbContext = this.baseAsyncRepository.GetDbContext();
            await dbContext.SaveEntityAsync(entity, applyToAllLevels);
        }

        public virtual async Task<List<TGridDTO>> SelectAllAsync(ODataQueryOptions<TGridDTO> aQueryOptions)
        {
            var query = this.GetSelectAllQueriable();
            var baseQuery = query.ProjectTo<TGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var repoList = resultQuery.ToList();
            return repoList;
        }

        public virtual async Task<IgPageResult<TGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<TGridDTO> aQueryOptions)
        {
            var entityQuery = this.GetSelectAllQueriable();
            var baseQuery = entityQuery.ProjectTo<TGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<TGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        protected void PopulatePageResultAsync<T, TGrid>(
            IgPageResult<TGrid> pageResult,
            ODataQueryOptions<T> aQueryOptions,
            IQueryable<T> baseQuery,
            IQueryable<T> resultQuery)
        {
            pageResult.PerPage = this.CalculateTop(aQueryOptions);
            pageResult.Total = this.GetTotalPages(aQueryOptions, baseQuery);
            pageResult.CurrentPage = this.CalculateCurrentPage(aQueryOptions);
            pageResult.LastPage = this.CalculateLastPage(aQueryOptions, pageResult.Total);

            List<T> data;
            if (pageResult.LastPage > 0 && (pageResult.CurrentPage > pageResult.LastPage))
            {
                pageResult.CurrentPage = pageResult.LastPage;
                int lastPageSkip = (pageResult.LastPage * pageResult.PerPage) - (pageResult.PerPage);
                var queryOptionsWithLastPageItems = this.SetSkip(aQueryOptions, lastPageSkip);
                var queryWithLastPageItems = (IQueryable<T>)queryOptionsWithLastPageItems.ApplyTo(baseQuery);

                data = queryWithLastPageItems.ToList();
            }
            else
            {
                data = resultQuery.ToList();
            }

            pageResult.Data = mapper.MapToList<T, TGrid>(data);
        }

        /// <summary>
        /// The GetTotalPages
        /// </summary>
        /// <param name="aQueryOptions">The aQueryOptions<see cref="ODataQueryOptions{TEntity}"/></param>
        /// <returns>The <see cref="int"/></returns>
        protected int GetTotalPages<T>(ODataQueryOptions<T> aQueryOptions, IQueryable<T> aQueriable)
        {
            var newQueryOptions = (ODataQueryOptions<T>)RemoveTopSkip(aQueryOptions);
            IQueryable queryable = newQueryOptions.ApplyTo(aQueriable);
            return ((IQueryable<T>)queryable).Count();
        }

        protected virtual IQueryable<TEntity> GetSelectAllQueriable()
        {
            return this.baseAsyncRepository.SelectAllAsync();
        }

        /// <summary>
        /// The Select
        /// </summary>
        /// <param name="aId">The aId<see cref="decimal"/></param>
        /// <returns>The <see cref="U"/></returns>
        public virtual async Task<TOutDTO> SelectAsync(TPk aId)
        {
            TEntity repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            TOutDTO result = mapper.Map<TOutDTO>(repoObj);
            return result;
        }

        /// <summary>
        /// The Insert
        /// </summary>
        /// <param name="aInDto">The aInDto<see cref="S"/></param>
        /// <returns>The <see cref="U"/></returns>
        public virtual async Task<string> InsertAsync(TInDTO aInDto)
        {
            this.ValidateData(aInDto);
            TEntity entity = mapper.MapToEntity<TInDTO, TEntity>(aInDto, isAdded: true);
            this.TransformDataOnInsert(entity);
            await this.SaveEntityAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="aId">The aId<see cref="decimal"/></param>
        /// <param name="aInDto">The aInDto<see cref="S"/></param>
        /// <returns>The <see cref="U"/></returns>
        public virtual async Task UpdateAsync(TPk aId, TInDTO aInDto)
        {
            // TODO: should not select from db, but it must check if the saveChanges has returned true (or 1)
            TEntity repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            if (repoObj == null)
            {
                throw new Exception("Object with id [" + aId + "] was not found!");
            }

            this.ValidateData(aInDto);

            TEntity entity = mapper.MapToEntity<TInDTO, TEntity>(aInDto, isAdded: false);
            await this.SaveEntityAsync(entity);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="aId">The aId<see cref="decimal"/></param>
        /// <returns>The <see cref="U"/></returns>
        public virtual async Task DeleteAsync(TPk aId)
        {
            TEntity repoObj = await this.baseAsyncRepository.SelectAsync(aId);
            if (repoObj == null)
            {
                throw new Exception("Object with id [" + aId + "] not found!");
            }

            List<string> parentsList = new List<string>();
            if (this.IsChildRecord(aId, parentsList))
            {
                string errorMessage = this.FormatParentsMessage(parentsList);
                throw new Exception(errorMessage);
            }

            repoObj.EntityState = EntityStateEnum.Deleted;
            await this.baseAsyncRepository.GetDbContext().SaveEntityAsync(repoObj);
        }

        /// <summary>
        /// Този метод се извиква преди insert и update на данни
        /// </summary>
        /// <param name="aInDto"></param>
        protected virtual void ValidateData(TInDTO aInDto)
        {
        }

        /// <summary>
        /// Този метод се извиква преди да се запазят данните в базата при insert
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void TransformDataOnInsert(TEntity entity)
        {
        }

        /// <summary>
        /// Този метод се override-ва във всеки child и в него се попълват полетата, които трябва 
        /// да бъдат мапнати към съответното име в entity-то (използва се dtoFieldsToEntityFields)
        /// </summary>
        protected virtual void PopulateDtoToEntityFieldsMapping()
        {
        }

        /// <summary>
        /// The FormatParentsMessage
        /// </summary>
        /// <param name="aList"></param>
        /// <returns></returns>
        protected string FormatParentsMessage(List<string> aList)
        {
            string message = "Object is referenced by: ";
            foreach (string parent in aList)
            {
                message += "[" + parent + "]";
            }
            return "Child record found! " + message;
        }

        protected ODataQueryOptions RemoveTopSkip<T>(ODataQueryOptions<T> aQueryOptions)
        {
            var uri = new Uri(aQueryOptions.Request.GetDisplayUrl());
            var queryParameters = uri.Query
                .TrimStart(new char[] { QUERY_OPTIONS_DELIMITER, QUESTION_MARK })
                .Split(QUERY_OPTIONS_SEPARATOR)
                .ToDictionary(e => e.Split(QUERY_OPTIONS_DELIMITER).FirstOrDefault(),
                              e => e.Split(QUERY_OPTIONS_DELIMITER).LastOrDefault());
            var newQueryOptions = new StringBuilder();
            foreach (string key in queryParameters.Keys)
            {

                if (key != QUERY_OPTIONS_SKIP && key != QUERY_OPTIONS_TOP)
                {
                    AppendKeyValue(newQueryOptions, key, queryParameters[key]);
                }
            }

            return this.CreateNewOdataQueryOptions(aQueryOptions, newQueryOptions.ToString());
        }

        /// <summary>
        /// Checks whether the object is a child record
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="aParentsList"></param>
        /// <returns></returns>
        protected abstract bool IsChildRecord(TPk aId, List<string> aParentsList);

        protected virtual async Task<IQueryable<T>> ApplyOData<T>(IQueryable<T> query, ODataQueryOptions<T> aQueryOptions)
        {
            if (aQueryOptions.Filter != null)
            {
                this.queryValidator.Validate(aQueryOptions.Filter, this.validationSettings);
            }

            IQueryable resultQuery;
            ODataQuerySettings querySetting = new ODataQuerySettings { PageSize = MAX_PAGE_SIZE };

            // Ако в url, няма top option за странициране или заявения top е повече от разрешения се прилага default-ния paging
            if (aQueryOptions.Top == null || aQueryOptions.Top.Value > MAX_PAGE_SIZE)
            {
                resultQuery = aQueryOptions.ApplyTo(query, querySetting);
            }
            else
            {
                // В противен случай се прилага paging-а от url параметъра
                resultQuery = aQueryOptions.ApplyTo(query);
            }

            var result = (IQueryable<T>)resultQuery;
            return await Task.FromResult(result);
        }

        /// <summary>
        /// Имплементира логиката спрямо, която се заместват имената на dto полетата към имената на entity полетата
        /// Засега не се ползва.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aQueryOptions"></param>
        /// <returns></returns>
        protected ODataQueryOptions MapDtoToEntityFields<T>(ODataQueryOptions<T> aQueryOptions)
        {
            var uri = new Uri(aQueryOptions.Request.GetDisplayUrl());
            string newQueryOptions = uri.Query;
            foreach (var key in dtoFieldsToEntityFields.Keys)
            {
                string value = dtoFieldsToEntityFields[key];
                newQueryOptions = newQueryOptions.Replace(key, value);
            }

            var result = this.CreateNewOdataQueryOptions(aQueryOptions, newQueryOptions);
            return result;
        }

        protected int CalculateTop<T>(ODataQueryOptions<T> aQueryOptions)
        {
            int top = MAX_PAGE_SIZE;
            if (aQueryOptions.Top != null && aQueryOptions.Top.Value >= 0 && aQueryOptions.Top.Value < MAX_PAGE_SIZE)
            {
                top = aQueryOptions.Top.Value;
            }

            return top;
        }

        protected int CalculateLastPage<T>(ODataQueryOptions<T> aQueryOptions, int aTotalElements)
        {
            int top = CalculateTop(aQueryOptions);
            if (top == 0)
            {
                return 0;
            }

            return (int)Math.Ceiling((aTotalElements) / (double)top);
        }

        protected int CalculateCurrentPage<T>(ODataQueryOptions<T> aQueryOptions)
        {
            int skip = 0;
            int top = CalculateTop(aQueryOptions);
            if (top == 0)
            {
                return 0;
            }

            if (aQueryOptions.Skip != null)
            {
                skip = aQueryOptions.Skip.Value;
            }

            return ((int)(skip / (double)top)) + 1;
        }

        private void AssignNewUri<T>(Uri resultUri, ODataQueryOptions<T> aQueryOptions)
        {
            aQueryOptions.Request.Scheme = resultUri.Scheme;
            aQueryOptions.Request.Host = HostString.FromUriComponent(resultUri.Host);
            aQueryOptions.Request.Path = PathString.FromUriComponent(resultUri.AbsolutePath);
            aQueryOptions.Request.QueryString = QueryString.FromUriComponent(resultUri.Query);
        }

        // Създава новият обект ODataQueryOptions, който се получава след съответната обработка
        private ODataQueryOptions<T> CreateNewOdataQueryOptions<T>(ODataQueryOptions<T> aQueryOptions, string newQueryOptions)
        {
            var uri = new Uri(aQueryOptions.Request.GetDisplayUrl());
            Uri baseResultUri = new Uri($"{uri.Scheme}://{uri.Authority}{uri.AbsolutePath}");
            Uri resultUri;
            if (newQueryOptions.StartsWith("?"))
            {
                resultUri = new Uri(baseResultUri.OriginalString + newQueryOptions);
            }
            else
            {
                resultUri = new Uri(baseResultUri.OriginalString + "?" + newQueryOptions);
            }

            this.AssignNewUri(resultUri, aQueryOptions);
            var newOdataQueryOptions = (ODataQueryOptions<T>)Activator.CreateInstance(aQueryOptions.GetType(), aQueryOptions.Context, aQueryOptions.Request);
            return newOdataQueryOptions;
        }

        private void AppendKeyValue(StringBuilder aNewQueryOptions, string aKey, object aValue)
        {
            aNewQueryOptions.Append(QUERY_OPTIONS_SEPARATOR);
            aNewQueryOptions.Append(aKey);
            aNewQueryOptions.Append(QUERY_OPTIONS_DELIMITER);
            aNewQueryOptions.Append(aValue);
        }

        private ODataQueryOptions SetSkip<T>(ODataQueryOptions<T> aQueryOptions, int skip)
        {
            var uri = new Uri(aQueryOptions.Request.GetDisplayUrl());
            var queryParameters = uri.Query
                .TrimStart(new char[] { QUERY_OPTIONS_DELIMITER, QUESTION_MARK })
                .Split(QUERY_OPTIONS_SEPARATOR)
                .ToDictionary(e => e.Split(QUERY_OPTIONS_DELIMITER).FirstOrDefault(),
                              e => e.Split(QUERY_OPTIONS_DELIMITER).LastOrDefault());
            var newQueryOptions = new StringBuilder();
            foreach (string key in queryParameters.Keys)
            {
                if (key != QUERY_OPTIONS_SKIP)
                {
                    AppendKeyValue(newQueryOptions, key, queryParameters[key]);
                }
            }

            AppendKeyValue(newQueryOptions, QUERY_OPTIONS_SKIP, skip);
            return this.CreateNewOdataQueryOptions(aQueryOptions, newQueryOptions.ToString());
        }
    }
}
