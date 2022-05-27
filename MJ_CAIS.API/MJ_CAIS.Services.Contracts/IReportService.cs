using MJ_CAIS.DTO.Report;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IReportService : IBaseAsyncService<ReportDTO, ReportDTO, ReportGridDTO, AReport, string>
    {
        Task<AReport> GenerateReportFromApplication(AApplication application, AApplicationStatus applicationStatus, int validityMonths = 6);
    }
}
