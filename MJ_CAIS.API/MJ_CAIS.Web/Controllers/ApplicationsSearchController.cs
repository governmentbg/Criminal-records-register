using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("applications/search")]
    [Authorize]
    public class ApplicationsSearchController : BaseApiCrudController<ApplicationSearchDTO, ApplicationSearchDTO, ApplicationSearchGridDTO, AApplication, string>
    {
        private readonly IApplicationSearchService _applicationSearchService;
        private readonly IUserContext _userContext;

        public ApplicationsSearchController(IApplicationSearchService applicationSearchService, IUserContext userContext)
            : base(applicationSearchService)
        {
            _applicationSearchService = applicationSearchService;
            _userContext = userContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<ApplicationSearchGridDTO> aQueryOptions)
        {
            var result = await this._applicationSearchService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}
