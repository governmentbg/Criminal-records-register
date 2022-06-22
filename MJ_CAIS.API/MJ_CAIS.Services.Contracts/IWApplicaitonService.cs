using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IWApplicaitonService : IBaseAsyncService<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string>
    {
        Task<IgPageResult<WApplicaitonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId);
    }
}
