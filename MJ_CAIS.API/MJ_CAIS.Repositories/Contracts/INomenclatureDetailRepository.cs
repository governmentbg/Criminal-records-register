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

        IQueryable<BReqStatus> GetInternalRequestStatuses();

        IQueryable<GCountry> GetCountries();
    }
}