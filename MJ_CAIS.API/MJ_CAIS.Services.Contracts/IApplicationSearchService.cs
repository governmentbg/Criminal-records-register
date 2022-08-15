using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationSearchService : IBaseAsyncService<ApplicationSearchDTO, ApplicationSearchDTO, ApplicationSearchGridDTO, AApplication, string>
    {
        Task<IgPageResult<ApplicationSearchGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationSearchGridDTO> aQueryOptions);
    }
}
