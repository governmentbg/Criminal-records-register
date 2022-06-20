using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("")]
        public async Task<IActionResult> GetCount()
        {
            var result = await this._service.GetCountAsync();
            return Ok(result);
        }
    }
}
