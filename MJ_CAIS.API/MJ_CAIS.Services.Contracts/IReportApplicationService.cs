using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IReportApplicationService : IBaseAsyncService<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string>
    {
        Task<IgPageResult<ReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ReportApplicationGridDTO> aQueryOptions, string? statusCode);

        Task<string> UpdateAsync(ReportApplicationDTO aInDto);

        Task<string> FinalUpdateAsync(ReportApplicationDTO aInDto);

        Task<string> CancelAsync(string aId, string cancelDesc);

        IQueryable<ReportAppStatusHistoryDTO> GetStatusHistoryByReportAppId(string aId);

        IQueryable<GeneratedReportDTO> GetReportsByAppId(string aId);
    }
}