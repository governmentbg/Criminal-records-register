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


        [HttpGet("bulletin-event-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> BulletinEventCounts()
        {
            var result = await this._service.GetBulletinEventCountByCurrentAuthorityAsync();
            return Ok(result);
        }

        [HttpGet("isin-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.CentralAuth}")]
        public async Task<IActionResult> IsinCounts()
        {
            var result = await this._service.GetIsinCountByCurrentAuthorityAsync();
            return Ok(result);
        }

        [HttpGet("ecris-count")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> EcrisCounts()
        {
            var result = await this._service.GetEcrisCountAsync();
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