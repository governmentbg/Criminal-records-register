using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;

namespace MJ_CAIS.Services.Contracts
{
    public interface IWApplicationReportService : IBaseAsyncService<WApplicationReportDTO, WApplicationReportDTO, WApplicationReportGridDTO, WReport, string>
    {
    }
}
