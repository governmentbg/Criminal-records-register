using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Statistics;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("statistics")]
    [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.CentralAuth},{RoleConstants.Judge},{RoleConstants.Admin},{RoleConstants.GlobalAdmin}")]
    public class StatisticsController : BaseApiController
    {
        private readonly IStatisticsService _service;

        public StatisticsController(IStatisticsService service)
        {
            _service = service;
        }

        [HttpGet("bulletins")]
        public async Task<IActionResult> GetStatisticsForBulletins([FromQuery] StatisticsSearchDTO searchParams)
        {
            var result = await this._service.GetStatisticsForBulletinsAsync(searchParams);
            return Ok(result);
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetStatisticsForApplications([FromQuery] StatisticsSearchDTO searchParams)
        {
            var result = await this._service.GetStatisticsForApplicationsAsync(searchParams);
            return Ok(result);
        }
    }
}
