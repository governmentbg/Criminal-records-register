using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

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
    }
}
