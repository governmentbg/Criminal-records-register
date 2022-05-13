using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users-external")]
    [AllowAnonymous]
    public class UsersExternalController : BaseApiCrudController<UserExternalDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string>
    {
        public UsersExternalController(IUserExternalService baseService) : base(baseService)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<UserExternalGridDTO> aQueryOptions)
        {
            var result = await this.baseService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}
