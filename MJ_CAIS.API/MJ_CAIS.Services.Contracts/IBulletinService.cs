using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinService : IBaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, BBulletin, string>
    {
        Task<IQueryable<OffenceDTO>> GetOffencesByBulletinIdAsync(string aId);

        Task<IQueryable<SanctionDTO>> GetSanctionsByBulletinIdAsync(string aId);
    }
}
