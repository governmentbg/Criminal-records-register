using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Helpers;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class NomenclatureDetailService : BaseAsyncService<BaseNomenclatureDTO, BaseNomenclatureDTO, BaseNomenclatureDTO, GNomenclature, string, CaisDbContext>, INomenclatureDetailService
    {
        private readonly INomenclatureDetailRepository _nomenclatureDetailRepository;

        public NomenclatureDetailService(IMapper mapper, INomenclatureDetailRepository nomenclatureDetailRepository)
            : base(mapper, nomenclatureDetailRepository)
        {
            _nomenclatureDetailRepository = nomenclatureDetailRepository;
        }

        public IQueryable<BaseNomenclatureDTO> GetBaseNomenclatureValues(string tableName)
        {
            var query = this.GetNomenclatureDbSet(tableName);
            var result = query.AsNoTracking().ProjectTo<BaseNomenclatureDTO>(mapperConfiguration).OrderBy(x => x.Name);
            return result;
        }

        public IQueryable<BaseNomenclatureDTO> GetMunicipalitiesByDistrict(string districtId)
        {
            return _nomenclatureDetailRepository
                    .GetMunicipalitiesByDistrict(districtId)
                    .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetCitiesByMunicipality(string municipalityId)
        {
            return _nomenclatureDetailRepository
                    .GetCitiesByMunicipality(municipalityId)
                    .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetBulletinStatuses()
        {
            return _nomenclatureDetailRepository
                .GetBulletinStatuses()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetAllFbbcDocTypes()
        {
            return _nomenclatureDetailRepository
                .GetAllFbbcDocTypes()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetAllFbbcSanctTypes()
        {
            return _nomenclatureDetailRepository
                .GetAllFbbcSanctTypes()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetInternalRequestStatuses()
        {
            return _nomenclatureDetailRepository
                .GetInternalRequestStatuses()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<NomenclatureTypeDTO> GetSanctionCategories()
        {
            return _nomenclatureDetailRepository
                .GetSanctionCategories()
                .Select(x => new NomenclatureTypeDTO
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    Type = x.Type,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                });
        }

        public async Task<IQueryable<BaseNomenclatureDTO>> GetDecidingAuthoritiesForBulletinsAsync()
        {
            var query = await _nomenclatureDetailRepository
                .GetDecidingAuthoritiesForBulletinsAsync();

            return query.ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetPidTypes()
        {
            return _nomenclatureDetailRepository
                  .GetPidTypes()
                  .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public async Task<IgPageResult<CountryDTO>> GetCountriesAsync(ODataQueryOptions<CountryDTO> aQueryOptions)
        {
            var entityQuery = _nomenclatureDetailRepository.GetCountries();
            var baseQuery = entityQuery.ProjectTo<CountryDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<CountryDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public IQueryable<BaseNomenclatureDTO> GetAllAPurposes()
        {
            return _nomenclatureDetailRepository
                .GetAllAPurposes()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetAllAPaymentMethods()
        {
            return _nomenclatureDetailRepository
                .GetAllAPaymentMethods()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetSrvcResRcptMethods()
        {
            return _nomenclatureDetailRepository
                .GetSrvcResRcptMethods()
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        public IQueryable<BaseNomenclatureDTO> GetCountriesOrdered()
        {
            return _nomenclatureDetailRepository
                .GetCountries()
                .OrderBy(x => x.Iso31662Code)
                .ProjectTo<BaseNomenclatureDTO>(mapperConfiguration);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        private bool IsSyTable(string tableName)
        {
            return _nomenclatureDetailRepository.SelectAllAsync().Any(x => x.TableName.ToLower() == tableName.ToLower());
        }

        private IQueryable<IBaseNomenclature> GetNomenclatureDbSet(string tableName)
        {
            if (!IsSyTable(tableName))
            {
                throw new BusinessLogicException($"Invalid table name {tableName}");
            }

            var propertyName = StringHelper.ConvertNameToPascalCase(tableName);
            var property = dbContext.GetType().GetProperty(propertyName);
            var dbSet = property.GetValue(dbContext) as IQueryable<IBaseNomenclature>;
            return dbSet;
        }
    }
}
