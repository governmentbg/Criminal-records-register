using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Rehabilitation;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRehabilitationRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        Task<IQueryable<BulletinForRehabilitationDTO>> GetBulletinByPersonIdAsync(string personId);

        void UpdateRehabilitationData(string bulletinId, DateTime? rehabilitationDate, string? status);

        Task SaveChangesAsync();

        Task<string> GetPersonIdByBulletinIdAsync(string bulleintId);
    }
}