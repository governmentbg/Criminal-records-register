using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Rehabilitation;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRehabilitationRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        IQueryable<BulletinForRehabilitationDTO> GetBulletinsByPersonId(string personId);

        void UpdateRehabilitationData(string bulletinId, decimal? bulletinVersion, DateTime? rehabilitationDate, string? status);

        Task<string> GetPersonIdByBulletinIdAsync(string bulletinId);
    }
}