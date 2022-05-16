using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.User;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users")]
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseApiCrudController<UserDTO, UserDTO, UserGridDTO, GUser, string>
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<UserGridDTO> aQueryOptions)
        {
            var result = await this._userService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            var result = await this._userService.SelectAsync(aId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserDTO aInDto)
        {
            var id = await this._userService.InsertAsync(aInDto);
            return Ok(new { id });
        }

        [HttpPut("{aId}")]
        public async Task<IActionResult> Put(string aId, [FromBody] UserDTO aInDto)
        {
            await this._userService.UpdateAsync(aId, aInDto);
            return Ok();
        }
    }
}
