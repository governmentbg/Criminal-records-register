using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IReportApplicationRepository : IBaseAsyncRepository<AReportApplication, string, CaisDbContext>
    {
        IQueryable<ReportAppStatusHistoryDTO> SelectAllStatusHistoryData();

        IQueryable<ReportAppBulletinIdDTO> GetBulletinsByPids(string personId);

        IQueryable<GeneratedReportDTO> SelectAllGeneratedReportsByAppId(string appId);
    }
}
