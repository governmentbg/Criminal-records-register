using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinEventService : IBaseAsyncService<BulletinEventDTO, BulletinEventDTO, BulletinEventGridDTO, BBulEvent, string>
    {
        Task<IgPageResult<BulletinEventGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinEventGridDTO> aQueryOptions, string groupCode, string? statusId, string? bulletinId);

        Task ChangeStatusAsync(string aInDto, string statusId);

        Task GenerateEventWhenChangeStatusOfBullAsync(BBulletin currentAttachedBulletin, string personId);

        Task GenerateEventWhenUpdateBullAsync(BBulletin currentAttachedBulletin);
    }
}
