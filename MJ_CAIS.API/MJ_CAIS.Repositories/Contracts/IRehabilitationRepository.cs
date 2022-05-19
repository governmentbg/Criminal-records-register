using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Rehabilitation;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRehabilitationRepository<TContext>
        where TContext : DbContext
    {
        TContext GetDbContext();

        Task<IQueryable<BulletinForRehabilitationDTO>> GetBulletinByPersonIdAsync(string personId);

        Task UpdateForRehabilitationAsync(string bulletinId, DateTime rehabilitationDate, bool changeStatus);
    }
}