using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MJ_CAIS.Web.Controllers.Common
{
    [Authorize]
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
