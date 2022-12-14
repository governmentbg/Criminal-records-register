using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IReportApplicationRepository : IBaseAsyncRepository<AReportApplication, string, CaisDbContext>
    {
        Task SaveSignersAsync(string reportId, string firstSignerId, string secondSignerId);

        IQueryable<ReportAppStatusHistoryDTO> SelectAllStatusHistoryData();

        IQueryable<GeneratedReportGridDTO> SelectAllGeneratedReports();

        Task<List<ReportAppBulletinIdDTO>> GetBulletinsByPersonIdAsync(string personId);

        IQueryable<GeneratedReportDTO> SelectAllGeneratedReportsByAppId(string appId);

        Task<byte[]> GetReportAppContentByIdAsync(string aId);

        Task<AReport> GetFullAppReportByIdAsync(string aId);

        Task<string> GetPersonIdByPidIdsAsync(string egnId, string lnchId, string lnId, string suidId);
    }
}
