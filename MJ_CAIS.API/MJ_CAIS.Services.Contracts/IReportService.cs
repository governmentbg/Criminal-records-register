using MJ_CAIS.DTO.Report;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;

namespace MJ_CAIS.Services.Contracts
{
    public interface IReportService : IBaseAsyncService<ReportDTO, ReportDTO, ReportGridDTO, AReport, string>
    {
        Task<AReport> GenerateReportFromApplication(AApplication application, AApplicationStatus applicationStatus, int validityMonths = 6);
        Task<AReport> GenerateReportFromApplication(string applicationID);
        Task<DDocContent> GetReportContent(string reportID);

        Task<string> InsertAsync(ApplicationInDTO aInDto);
    }
}
