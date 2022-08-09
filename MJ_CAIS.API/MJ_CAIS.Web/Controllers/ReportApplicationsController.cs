using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.ExternalWebServices.Contracts;

namespace MJ_CAIS.Web.Controllers
{
    [Route("a-report-applications")]
    [Authorize(Roles = $"{RoleConstants.Normal}")]
    public class ReportApplicationsController : BaseApiCrudController<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string>
    {
        private readonly IReportApplicationService _reportApplicationService;
        private readonly IReportGenerationService _reportGenerationService;
        private readonly IPrintDocumentService _printDocumentService;

        public ReportApplicationsController(IReportApplicationService reportApplicationService,
            IReportGenerationService reportGenerationService,
            IPrintDocumentService printDocumentService)
            : base(reportApplicationService)
        {
            _reportApplicationService = reportApplicationService;
            _reportGenerationService = reportGenerationService;
            _printDocumentService = printDocumentService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<ReportApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._reportApplicationService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] ReportApplicationDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] ReportApplicationDTO aInDto)
        {
            var reportAppId = await this._reportApplicationService.UpdateAsync(aInDto);
            if (string.IsNullOrEmpty(reportAppId)) return NotFound();
            return Ok();
        }

        [HttpPut("final-edit/{aId}")]
        public async Task<IActionResult> FinalEdit(string aId, [FromBody] ReportApplicationDTO aInDto)
        {
            var reportId = await this._reportApplicationService.FinalUpdateAsync(aInDto);
            if (string.IsNullOrEmpty(reportId)) return NotFound();

            await _reportGenerationService.CreateReport(reportId);
            return Ok();
        }

        [HttpGet("print-report/{aId}")]
        public async Task<IActionResult> PrintReportById(string aId)
        {
            var result = await this._reportApplicationService.GetReportAppContentByIdAsync(aId);
            if (result == null) return NotFound();

            var content = result;
            var fileName = "report.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpPost("cancel/{aId}")]
        public virtual async Task<IActionResult> Cancel(string aId, [FromBody] CancelDTO aInDto)
        {
            await this._reportApplicationService.CancelAsync(aId, aInDto.Description);
            return Ok();
        }

        [HttpPut("deliver/{aId}")]
        public virtual async Task<IActionResult> Deliver(string aId)
        {
            var result = await this._reportApplicationService.DeliverAsync(aId);
            if (result == null) return NotFound();
            return Ok();
        }

        [HttpGet("{aId}/status-history")]
        public IActionResult GetStatusHistory(string aId)
        {
            var result = this._reportApplicationService.GetStatusHistoryByReportAppId(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/reports")]
        public IActionResult GetReports(string aId)
        {
            var result = this._reportApplicationService.GetReportsByAppId(aId);
            return Ok(result);
        }
    }
}
