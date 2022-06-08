using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBaseAsyncService<TInDTO, TOutDTO, TGridDTO, TEntity, TPk>
         where TInDTO : BaseDTO
         where TOutDTO : BaseDTO
         where TGridDTO : BaseGridDTO
    {
        Task SaveEntityAsync(IBaseIdEntity entity, bool applyToAllLevels = true);

        Task<List<TGridDTO>> SelectAllAsync(ODataQueryOptions<TGridDTO> aQueryOptions);

        Task<IgPageResult<TGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<TGridDTO> aQueryOptions);

        Task<TOutDTO> SelectAsync(TPk aId);

        Task<string> InsertAsync(TInDTO aInDto);

        Task UpdateAsync(TPk aId, TInDTO aInDto);

        Task DeleteAsync(TPk aId);
    }
}
