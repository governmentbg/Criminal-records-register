using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class NomenclatureDetailRepository : BaseAsyncRepository<GNomenclature, CaisDbContext>, INomenclatureDetailRepository
    {
        public NomenclatureDetailRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<BaseNomenclatureDTO> GetMunicipalitiesByDistrict(string districtId)
        {
            return _dbContext.GBgMunicipalities
                .Where(x => x.DistrictId == districtId)
                .Select(x => new BaseNomenclatureDTO
                {
                    Code = x.Code,
                    Id = x.Id,
                    Name = x.Name,
                    NameEn = x.NameEn,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                });
        }

        public IQueryable<BaseNomenclatureDTO> GetCitiesByMunicipality(string municipalityId)
        {
            return _dbContext.GCities
                .Where(x => x.MunicipalityId == municipalityId)
                .Select(x => new BaseNomenclatureDTO
                {
                    Code = x.EkatteCode,
                    Id = x.Id,
                    Name = x.Name,
                    NameEn = x.NameEn,
                    ValidFrom = x.ValidFrom,
                    ValidTo = x.ValidTo,
                });
        }
    }
}
