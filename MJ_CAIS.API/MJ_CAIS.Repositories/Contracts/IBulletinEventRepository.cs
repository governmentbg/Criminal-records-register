using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinEventRepository : IBaseAsyncRepository<BBulEvent, string, CaisDbContext>
    {
        Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId);

        Task<IQueryable<BulletinSancttionsEventDTO>> GetBulletinByPersonIdAsync(string personId);
    }
}
