using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IInternalRequestService : IBaseAsyncService<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, BInternalRequest, string>
    {
        Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string? statusId, string? bulletinId);

        Task<BulletinPersonInfoModelDTO> GetBulletinPersonInfoAsync(string bulletinId);
    }
}
