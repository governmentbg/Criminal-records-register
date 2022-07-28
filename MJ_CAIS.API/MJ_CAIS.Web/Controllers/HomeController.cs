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
            return Ok(result.Item1);
        }


        [HttpGet("bulletin-event-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> BulletinEventCounts()
        {
            var result = await this._service.GetBulletinCountByCurrentAuthorityAsync();
            return Ok(result.Item2);
        }

        [HttpGet("isin-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.CentralAuth},{RoleConstants.Judge}")]
        public async Task<IActionResult> IsinCounts()
        {
            var result = await this._service.GetBulletinCountByCurrentAuthorityAsync();
            return Ok(result.Item3);
        }

        [HttpGet("ecris-count")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> EcrisCounts()
        {
            var result = await this._service.GetEcrisCountAsync();
            return Ok(result.Item1);
        }

        [HttpGet("application-count")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> ApplicationCounts()
        {
            var result = await this._service.GetApplicationCountByCurrentAuthorityAsync();
            return Ok(result.Item1);
        }

        [HttpGet("for-judge-count")]
        [Authorize(Roles = $"{RoleConstants.Judge}")]
        public async Task<IActionResult> ForJudgeCounts()
        {
            var result = await this._service.GetApplicationCountByCurrentAuthorityAsync();
            return Ok(result.Item2);
        }

        [HttpGet("fbbc-count")]
        [Authorize(Roles = $"{RoleConstants.CentralAuth}")]
        public async Task<IActionResult> FbbcCounts()
        {
            var result = await this._service.GetEcrisCountAsync();
            return Ok(result.Item2);
        }
    }
}