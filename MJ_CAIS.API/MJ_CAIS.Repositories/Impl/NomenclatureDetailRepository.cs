using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Repositories.Contracts;
using static MJ_CAIS.Common.Constants.ECRISConstants;

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

        public IQueryable<NIntternalReqType> GetInternalRequestStatuses()
        {
            return _dbContext.NIntternalReqTypes.AsNoTracking();
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

        public IQueryable<EEcrisNomenclature> GetEcrisRequestTypes(bool isNotification)
        {
            var query = _dbContext.EEcrisNomenclatures.AsNoTracking();

            if (isNotification)
            {
                query = query.Where(x => x.NomCode == EcrisNomenclatureCode.NotificationResponses);
            }
            else
            {
                query = query.Where(x => x.NomCode == EcrisNomenclatureCode.RequestResponses);
            }

            return query;
        }

        public async Task<IQueryable<GUser>> GetGUsersAsync()
        {
            var query = _dbContext.GUsers.AsNoTracking().Where(x => x.Active == true);
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

        public IQueryable<APaymentMethod> GetDeskAPaymentMethods()
        {
            return _dbContext.APaymentMethods.Where(x => x.IsForDesk == true).AsNoTracking();
        }
        public IQueryable<APaymentMethod> GetWebAPaymentMethods()
        {
            return _dbContext.APaymentMethods.Where(x => x.IsForWeb == true).AsNoTracking();
        }

        public IQueryable<ASrvcResRcptMeth> GetSrvcResRcptMethods()
        {
            return _dbContext.ASrvcResRcptMeths.AsNoTracking();
        }

        public IQueryable<PPersonIdType> GetPidTypes()
        {
            return _dbContext.PPersonIdTypes.AsNoTracking();
        }

        public IQueryable<IBaseNomenclature> GetDbSet(string propertyName)
        {
            var property = _dbContext.GetType().GetProperty(propertyName);
            var dbSet = property.GetValue(_dbContext) as IQueryable<IBaseNomenclature>;
            return dbSet;
        }

        public IQueryable<AApplicationStatus> GetApplicationStatuses()
        {
            return _dbContext.AApplicationStatuses.AsNoTracking();
        }

        public IQueryable<AReportStatus> GetReportStatuses()
        {
            return _dbContext.AReportStatuses.AsNoTracking();
        }
    }
}