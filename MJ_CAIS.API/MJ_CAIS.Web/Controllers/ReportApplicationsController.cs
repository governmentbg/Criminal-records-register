using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.AspNet.OData.Query;

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
    }
}
