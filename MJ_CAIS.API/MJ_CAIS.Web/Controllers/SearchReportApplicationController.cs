using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("report-application/search")]
    [AllowAnonymous]
    public class SearchReportApplicationController : BaseApiController
    {
        private readonly ISearchReportApplicationService _service;

        public SearchReportApplicationController(ISearchReportApplicationService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<SearchReportApplicationGridDTO> aQueryOptions)
        {
            var result = await this._service.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}
