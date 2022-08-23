using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.Statistics;

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

        IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority();

        IQueryable<BulletinStatusHistoryDTO> SelectAllStatusHistoryData();

        Task<string> GetBulletinAuthIdAsync(string aId);

        void CreateEcrisTcn(string bulletinId, string action);

        Task<PPerson> GetPersonIdByPidAsync(string pid, string pidType);

        Task<IQueryable<BBulletin>> GetBulletinsByPidIdAsync(string pidId);

        Task<IQueryable<BBulletin>> GetBulletinsForPeriodAsync(DateTime dateFrom, DateTime dateTo);

        Task SaveBulletinsAsync(List<BBulletin> bulletins);

        Task<Dictionary<string, string>> GetAuthIdByEkatteAsync(List<string> ekatteCodes);

        Task<List<StatisticsCountDTO>> GetStatisticsForBulletinsAsync(StatisticsSearchDTO searchParams);

        Task<List<StatisticsCountDTO>> GetStatisticsForApplicationsAsync(StatisticsSearchDTO searchParams);

        Task<BBulletin> GetBulletinData(string bulletinId);

        Task<string> GetDataForSendFinesDataAsync(string egn, string? decisionTypeId, DateTime? decisionDate, string decisionNumber, string? decidingAuthId, string caseNumber, bool caseYearParsed, decimal caseYear, string? caseTypeId, string? caseAuthId);
      
        Task<List<BSanction>> GetDeletedSanctionsAsync(List<string> deletedSanctionIds);
       
        Task<bool> IsEuCitizen(IEnumerable<string> personNationalities);

        Task<List<BulletinForRehabilitationAndEventDTO>> GetBulletinsByPidsIdAsync(List<string> pidsId);
    }
}
