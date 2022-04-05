using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class NomenclatureDetailRepository : BaseAsyncRepository<GNomenclature, CaisDbContext>, INomenclatureDetailRepository
    {
        public NomenclatureDetailRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<GBgMunicipality> GetMunicipalitiesByDistrict(string districtId)
        {
            return _dbContext.GBgMunicipalities.AsNoTracking()
                     .Where(x => x.DistrictId == districtId);
        }


        public IQueryable<GCity> GetCitiesByMunicipality(string municipalityId)
        {
            return _dbContext.GCities.AsNoTracking()
                    .Where(x => x.MunicipalityId == municipalityId);
        }

        public IQueryable<BBulletinStatus> GetBulletinStatuses()
        {
            return _dbContext.BBulletinStatuses.AsNoTracking();
        }

        public IQueryable<FbbcDocType> GetAllFbbcDocTypes()
        {
            return _dbContext.FbbcDocTypes.AsNoTracking();
        }
        public IQueryable<FbbcSanctType> GetAllFbbcSanctTypes()
        {
            return _dbContext.FbbcSanctTypes.AsNoTracking();
        }
        public IQueryable<BReqStatus> GetInternalRequestStatuses()
        {
            return _dbContext.BReqStatuses.AsNoTracking();
        }

        public IQueryable<GCountry> GetCountries()
        {
            return _dbContext.GCountries.AsNoTracking();
        }
    }
}