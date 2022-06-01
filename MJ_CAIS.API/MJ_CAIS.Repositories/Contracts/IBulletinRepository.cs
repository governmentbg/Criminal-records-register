using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        Task<DDocument> SelectDocumentAsync(string documentId);

        Task<IQueryable<DDocument>> SelectAllDocumentsAsync();

        Task<IQueryable<BBullPersAlias>> SelectBullPersAliasByBulletinIdAsync(string aId);

        Task<IQueryable<BOffence>> SelectAllOffencesAsync();

        Task<IQueryable<BSanction>> SelectAllSanctionsAsync();

        Task<IQueryable<BDecision>> SelectAllDecisionsAsync();

        Task<BBulletin> SelectBulletinPersonInfoAsync(string bulletinId);

        Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync();

        Task<IQueryable<BBulletinStatusH>> SelectAllStatusHistoryDataAsync();

        Task SaveChangesAsync();

        Task<string> GetBulletinAuthIdAsync(string aId);

        void CreateEcrisTcn(string bulletinId, string action);

        Task<PPerson> GetPersonIdByPidAsync(string pid, string pidType);

        Task<IQueryable<BBulletin>> GetBulletinsByPidIdAsync(string pidId);

        void SaveBulletins(List<BBulletin> bulletins);

        Dictionary<string, string> GetAuthIdByEkkate(List<string> ekatteCodes);
    }
}
