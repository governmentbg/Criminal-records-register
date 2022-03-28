using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

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
    }
}
