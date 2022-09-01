using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRehabilitationRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        void UpdateRehabilitationData(string bulletinId, decimal? bulletinVersion, DateTime? rehabilitationDate);
    }
}