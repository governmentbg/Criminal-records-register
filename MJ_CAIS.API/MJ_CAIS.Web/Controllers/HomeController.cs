using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("home")]
    [Authorize]
    public class HomeController : BaseApiController
    {
        private readonly IHomeService _service;

        public HomeController(IHomeService service)
        {
            _service = service;
        }

        [HttpGet("bulletin-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> BulletinCounts()
        {
            var result = await this._service.GetBulletinCountByCurrentAuthorityAsync();
            return Ok(result);
        }

        [HttpGet("central-auth-count")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> GetCentralAuthorityCounts()
        {
            var result = await this._service.GetCentralAuthorityCountsAsync();
            return Ok(result);
        }

        [HttpGet("application-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> ApplicationCounts()
        {
            var result = await this._service.GetApplicationCountByCurrentAuthorityAsync();
            return Ok(result);
        }
    }
}