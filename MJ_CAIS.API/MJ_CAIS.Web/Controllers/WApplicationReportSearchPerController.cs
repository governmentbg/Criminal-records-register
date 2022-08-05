using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReportSearchPer;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("e-applicaiton-reports/search-pers")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class WApplicationReportSearchPerController : BaseApiCrudController<WApplicationReportSearchPerDTO, WApplicationReportSearchPerDTO, WApplicationReportSearchPerGridDTO, WReportSearchPer, string>
    {
        private readonly IWApplicationReportSearchPerService _wApplicaitonReportSearchPerService;
        public WApplicationReportSearchPerController(IWApplicationReportSearchPerService wApplicaitonReportSearchPerService)
            : base(wApplicaitonReportSearchPerService)
        {
            _wApplicaitonReportSearchPerService = wApplicaitonReportSearchPerService;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetReports(ODataQueryOptions<WApplicationReportSearchPerGridDTO> aQueryOptions)
        {
            var result = await this._wApplicaitonReportSearchPerService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}

