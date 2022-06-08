using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinEventRepository : IBaseAsyncRepository<BBulEvent, string, CaisDbContext>
    {
        Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId, string? bulletinId);

        Task<IQueryable<BulletinSancttionsEventDTO>> GetBulletinByPersonIdAsync(string personId);

        Task<string> GetPersonIdByBulletinIdAsync(string bulletinId);

        Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync();
    }
}
