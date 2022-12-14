using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.DTO.Statistics;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("statistics")]
    [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.CentralAuth},{RoleConstants.Judge},{RoleConstants.Admin},{RoleConstants.GlobalAdmin}")]
    public class StatisticsController : BaseApiController
    {
        private readonly IStatisticsService _service;
        private readonly IPrintDocumentService _printDocumentService;
        public StatisticsController(IStatisticsService service, IPrintDocumentService printDocumentService)
        {
            _service = service;
            _printDocumentService = printDocumentService;
        }

        [HttpGet("bulletins")]
        public async Task<IActionResult> GetStatisticsForBulletins([FromQuery] StatisticsSearchDTO searchParams)
        {
            var result = await this._service.GetStatisticsForBulletinsAsync(searchParams);
            return Ok(result);
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetStatisticsForApplications([FromQuery] StatisticsSearchDTO searchParams)
        {
            var result = await this._service.GetStatisticsForApplicationsAsync(searchParams);
            return Ok(result);
        }

        [HttpGet("daily-statistics")]
        public async Task<IActionResult> GetDailyStatistics([FromQuery] DailyStatisticsSearchDTO statSearch)
        {
            if (string.IsNullOrEmpty(statSearch.StatisticsType))
            {
                throw new BusinessLogicException("Не е избран вид на справката.");
            }
            if (!statSearch.FromDate.HasValue)
            {
                throw new BusinessLogicException("Не е избрана начална дата.");
            }
            if (!statSearch.ToDate.HasValue)
            {
                throw new BusinessLogicException("Не е избрана крайна дата.");
            }
            //нагласяме датите, за да е по-лесна заявката в оракъл

            DateTime dateTo=statSearch.ToDate.Value.Date.AddDays(1);
            DateTime dateFrom = statSearch.FromDate.Value.Date;
        
            byte[] result = null;
            switch (statSearch.StatisticsType) {
                case StatisticsConstants.DailyStatisticsTypes.ReportApplication:
                        result = await _printDocumentService.PrintDailyReportApplications(dateFrom, dateTo);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Application:
                    result = await _printDocumentService.PrintDailyApplications(dateFrom, dateTo);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Certificate:
                    result = await _printDocumentService.PrintDailyCertificates(dateFrom, dateTo);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Bulletin:
                    result = await _printDocumentService.PrintDailyBulletins(dateFrom, dateTo, statSearch.Status);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Report:
                    result = await _printDocumentService.PrintDailyReports(dateFrom, dateTo);
                    break;
            }

            if (result == null) return NotFound();

            //output file current date
            string fileDateFrom = ((DateTime)statSearch.FromDate).ToString("yyyy-MM-dd");
            string fileDateTo = ((DateTime)statSearch.ToDate).ToString("yyyy-MM-dd");
            string fileName;

            if (statSearch.Status != null)
            {
                fileName = $"{statSearch.StatisticsType}_{statSearch.Status}_{fileDateFrom}_{fileDateTo}.pdf";
            }
            else
            {
                fileName = $"{statSearch.StatisticsType}_{fileDateFrom}_{fileDateTo}.pdf";
            }
            

            var content = result;
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

    }
}
