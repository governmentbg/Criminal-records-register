using MJ_CAIS.DTO.NomenclatureDetail;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.Services.Contracts
{
    public interface INomenclatureDetailService : IBaseAsyncService<BaseNomenclatureDTO, BaseNomenclatureDTO, BaseNomenclatureDTO, GNomenclature, string>
    {
        IQueryable<BaseNomenclatureDTO> GetBaseNomenclatureValues(string tableName);
    }
}
