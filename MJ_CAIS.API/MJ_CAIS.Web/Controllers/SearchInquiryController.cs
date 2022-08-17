using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("inquiry/search-inquiry")]
    [AllowAnonymous]
    public class SearchInquiryController : BaseApiController
    {
        private readonly ISearchInquiryService _service;

        public SearchInquiryController(ISearchInquiryService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<SearchInquiryGridDTO> aQueryOptions)
        {
            var result = await this._service.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public async Task<IActionResult> GetById(string aId)
        {
            var result = await this._service.SelectByIdAsync(aId);
            return Ok(result);
        }
    }
}
