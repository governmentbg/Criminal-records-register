using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("applications")]
    [AllowAnonymous]
    public class ApplicationsController : BaseApiCrudController<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService) : base(applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] ApplicationDTO aInDto)
        {
            return await base.Post(aInDto);
        }
    }
}