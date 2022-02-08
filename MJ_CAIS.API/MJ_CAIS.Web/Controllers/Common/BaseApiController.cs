using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MJ_CAIS.Web.Controllers.Common
{
    [Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        public string? UserId => User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
