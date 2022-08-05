using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("a-report-applications")]
    [Authorize(Roles = $"{RoleConstants.Normal}")]
    public class ReportApplicationsController : BaseApiCrudController<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string>
    {
        private readonly IReportApplicationService _reportApplicationService;

        public ReportApplicationsController(IReportApplicationService reportApplicationService) : base(reportApplicationService)
        {
            _reportApplicationService = reportApplicationService;
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
            var result = await this._reportApplicationService.UpdateAsync(aInDto, false);
            if (string.IsNullOrEmpty(result)) return NotFound();
            return Ok();
        }

        [HttpPut("final-edit/{aId}")]
        public async Task<IActionResult> FinalEdit(string aId, [FromBody] ReportApplicationDTO aInDto)
        {
            var result = await this._reportApplicationService.UpdateAsync(aInDto, true);
            if (string.IsNullOrEmpty(result)) return NotFound();
            return Ok();
        }

        [HttpPost("cancel/{aId}")]
        public virtual async Task<IActionResult> Cancel(string aId, [FromBody] CancelDTO aInDto)
        {
            await this._reportApplicationService.CancelAsync(aId, aInDto.Description);
            return Ok();
        }

        [HttpGet("{aId}/status-history")]
        public IActionResult GetStatusHistory(string aId)
        {
            var result = this._reportApplicationService.GetStatusHistoryByReportAppId(aId);
            return Ok(result);
        }
    }
}
