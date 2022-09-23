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

        [HttpGet("dayly-statistics")]
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
                        result = await _printDocumentService.PrintDailyReportApplications(dateFrom, dateTo, statSearch.Status);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Application:
                    result = await _printDocumentService.PrintDailyApplications(dateFrom, dateTo, statSearch.Status);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Certificate:
                    result = await _printDocumentService.PrintDailyCertificates(dateFrom, dateTo, statSearch.Status);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Bulletin:
                    result = await _printDocumentService.PrintDailyBulletins(dateFrom, dateTo, statSearch.Status);
                    break;
                case StatisticsConstants.DailyStatisticsTypes.Report:
                    result = await _printDocumentService.PrintDailyReports(dateFrom, dateTo, statSearch.Status);
                    break;
            }

            if (result == null) return NotFound();

            var content = result;
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

    }
}
