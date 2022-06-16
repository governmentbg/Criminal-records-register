using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users-external")]
    [Authorize(Roles = RoleConstants.GlobalAdmin)]

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

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            var result = await this.baseService.SelectAsync(aId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserExternalDTO aInDto)
        {
            var id = await this.baseService.InsertAsync(aInDto);
            return Ok(new { id });
        }

        [HttpPut("{aId}")]
        public async Task<IActionResult> Put(string aId, [FromBody] UserExternalDTO aInDto)
        {
            await this.baseService.UpdateAsync(aId, aInDto);
            return Ok();
        }
    }
}
