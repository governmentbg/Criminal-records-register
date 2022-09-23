using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface INomenclatureDetailRepository : IBaseAsyncRepository<GNomenclature, string, CaisDbContext>
    {
        IQueryable<GBgMunicipality> GetMunicipalitiesByDistrict(string districtId);

        IQueryable<GCity> GetCitiesByMunicipality(string municipalityId);

        IQueryable<BBulletinStatus> GetBulletinStatuses();

        IQueryable<FbbcDocType> GetAllFbbcDocTypes();

        IQueryable<FbbcSanctType> GetAllFbbcSanctTypes();

        IQueryable<NIntternalReqType> GetInternalRequestStatuses();

        IQueryable<GCountry> GetCountries();

        IQueryable<BSanctionCategory> GetSanctionCategories();

        IQueryable<APurpose> GetAllAPurposes();

        IQueryable<APaymentMethod> GetAllAPaymentMethods();

        IQueryable<APaymentMethod> GetWebAPaymentMethods();

        IQueryable<APaymentMethod> GetDeskAPaymentMethods();

        IQueryable<ASrvcResRcptMeth> GetSrvcResRcptMethods();

        IQueryable<PPersonIdType> GetPidTypes();

        Task<IQueryable<GDecidingAuthority>> GetDecidingAuthoritiesForBulletinsAsync();

        Task<IQueryable<GUser>> GetGUsersAsync();
        IQueryable<IBaseNomenclature> GetDbSet(string propertyName);

        IQueryable<EEcrisNomenclature> GetEcrisRequestTypes(bool isNotification);

        IQueryable<AApplicationStatus> GetApplicationStatuses();
    }
}