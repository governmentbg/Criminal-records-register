using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class NomenclatureDetailRepository : BaseAsyncRepository<GNomenclature, CaisDbContext>, INomenclatureDetailRepository
    {
        private readonly IUserContext _userContext;

        public NomenclatureDetailRepository(CaisDbContext dbContext, IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
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

        public IQueryable<BSanctionCategory> GetSanctionCategories()
        {
            return _dbContext.BSanctionCategories.AsNoTracking();
        }

        public async Task<IQueryable<GDecidingAuthority>> GetDecidingAuthoritiesForBulletinsAsync()
        {
            var query = _dbContext.GDecidingAuthorities.AsNoTracking()
                .Where(x => x.ActiveForBulletins.HasValue && x.ActiveForBulletins.Value)
                .OrderBy(x => x.OrderNumber);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<GUser>> GetGUsersAsync()
        {
            var query = _dbContext.GUsers.AsNoTracking();
            query = _userContext.FilterByAuthorityForAllRoles(query);
            return await Task.FromResult(query);
        }

        public IQueryable<GCountry> GetCountries()
        {
            return _dbContext.GCountries.AsNoTracking()
                .OrderBy(x => x.Iso31662Code);
        }

        public IQueryable<APurpose> GetAllAPurposes()
        {
            return _dbContext.APurposes.AsNoTracking();
        }

        public IQueryable<APaymentMethod> GetAllAPaymentMethods()
        {
            return _dbContext.APaymentMethods.AsNoTracking();
        }

        public IQueryable<ASrvcResRcptMeth> GetSrvcResRcptMethods()
        {
            return _dbContext.ASrvcResRcptMeths.AsNoTracking();
        }

        public IQueryable<PPersonIdType> GetPidTypes()
        {
            return _dbContext.PPersonIdTypes.AsNoTracking();
        }
    }
}