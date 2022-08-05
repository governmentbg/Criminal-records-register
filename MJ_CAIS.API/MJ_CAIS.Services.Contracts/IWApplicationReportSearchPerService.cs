using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;
using MJ_CAIS.DTO.WApplicationReportSearchPer;

namespace MJ_CAIS.Services.Contracts
{
    public interface IWApplicationReportSearchPerService : IBaseAsyncService<WApplicationReportSearchPerDTO, WApplicationReportSearchPerDTO, WApplicationReportSearchPerGridDTO, WReportSearchPer, string>
    {
    }
}
