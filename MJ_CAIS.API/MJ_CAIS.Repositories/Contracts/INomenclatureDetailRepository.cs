using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface INomenclatureDetailRepository : IBaseAsyncRepository<GNomenclature, string, CaisDbContext>
    {
    }
}
