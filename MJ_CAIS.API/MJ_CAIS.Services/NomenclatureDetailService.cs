using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Helpers;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

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

            var dbContext = _nomenclatureDetailRepository.GetDbContext();
            var propertyName = StringHelper.ConvertNameToPascalCase(tableName);
            var property = dbContext.GetType().GetProperty(propertyName);
            var dbSet = property.GetValue(dbContext) as IQueryable<IBaseNomenclature>;
            return dbSet;
        }
    }
}
