using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;
using MJ_CAIS.DTO.WApplicationReportSearchPer;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("e-applicaiton-reports/reports")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class WApplicationReportController : BaseApiCrudController<WApplicationReportDTO, WApplicationReportDTO, WApplicationReportGridDTO, WReport, string>
    {
        private readonly IWApplicationReportService _wApplicaitonReportService;
        public WApplicationReportController(IWApplicationReportService wApplicaitonReportService)
            : base(wApplicaitonReportService)
        {
            _wApplicaitonReportService = wApplicaitonReportService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetReports(ODataQueryOptions<WApplicationReportGridDTO> aQueryOptions)
        {
            var result = await this._wApplicaitonReportService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}


