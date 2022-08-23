using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinEventRepository : IBaseAsyncRepository<BBulEvent, string, CaisDbContext>
    {
        Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId, string? bulletinId);

        IQueryable<DateTime?> GetOffencesEndDatesByBulletinId(string bulletinId);

        IQueryable<SanctionEventDTO> GetSanctionsSuspentionByBulletinId(List<string> bulletinId);

        Task<bool> GetExistingEventsAsync(string bulletinId);

        IQueryable<BuletinEventTypeDTO> GetExistingEventsByType(BBulletin currentAttachedBulletin);
    }
}
