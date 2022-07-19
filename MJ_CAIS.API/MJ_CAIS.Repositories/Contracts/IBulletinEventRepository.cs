using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinEventRepository : IBaseAsyncRepository<BBulEvent, string, CaisDbContext>
    {
        Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId, string? bulletinId);

        IQueryable<BulletinSancttionsEventDTO> GetBulletinsByPersonId(string personId);

        IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority();

        IQueryable<DateTime?> GetOffencesEndDatesByBulletinId(string bulletinId);

        IQueryable<SanctionEventDTO> GetSanctionsSuspentionByBulletinId(List<string> bulletinId);
    }
}
