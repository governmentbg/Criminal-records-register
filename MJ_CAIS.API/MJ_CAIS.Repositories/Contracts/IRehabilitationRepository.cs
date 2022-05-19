using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Rehabilitation;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRehabilitationRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        Task<IQueryable<BulletinForRehabilitationDTO>> GetBulletinByPersonIdAsync(string personId);

        Task UpdateForRehabilitationAsync(string bulletinId, DateTime rehabilitationDate, bool changeStatus);
    }
}