using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface INomenclatureDetailService : IBaseAsyncService<BaseNomenclatureDTO, BaseNomenclatureDTO, BaseNomenclatureDTO, GNomenclature, string>
    {
        IQueryable<BaseNomenclatureDTO> GetBaseNomenclatureValues(string tableName);

        IQueryable<BaseNomenclatureDTO> GetMunicipalitiesByDistrict(string districtId);

        IQueryable<BaseNomenclatureDTO> GetCitiesByMunicipality(string municipalityId);

        IQueryable<BaseNomenclatureDTO> GetBulletinStatuses();

        IQueryable<BaseNomenclatureDTO> GetAllFbbcDocTypes();

        IQueryable<BaseNomenclatureDTO> GetAllFbbcSanctTypes();

        IQueryable<BaseNomenclatureDTO> GetInternalRequestTypes();

        Task<IgPageResult<CountryDTO>> GetCountriesAsync(ODataQueryOptions<CountryDTO> aQueryOptions);

        IQueryable<NomenclatureTypeDTO> GetSanctionCategories();

        IQueryable<PurposeDTO> GetAllAPurposes();

        IQueryable<BaseNomenclatureDTO> GetAllAPaymentMethods();

        IQueryable<BaseNomenclatureDTO> GetDeskAPaymentMethods();

        IQueryable<PaymentMethodDTO> GetWebAPaymentMethods();

        IQueryable<BaseNomenclatureDTO> GetSrvcResRcptMethods();

        IQueryable<BaseNomenclatureDTO> GetPidTypes();

        Task<IQueryable<BaseNomenclatureDTO>> GetDecidingAuthoritiesForBulletinsAsync();

        IQueryable<BaseNomenclatureDTO> GetCountriesOrdered();

        Task<IQueryable<BaseNomenclatureDTO>> GetGUsersAsync();
    }
}
