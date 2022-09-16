using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DataAccess.ExtUsers;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users-external")]
    [Authorize(Roles = RoleConstants.GlobalAdmin)]

    public class UsersExternalController : BaseApiCrudController<UserExternalInDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<LocalGUsersExt> _userManager;

        public UsersExternalController(IUserExternalService baseService, UserManager<LocalGUsersExt> userManager, IMapper mapper) : base(baseService)
        {
            _userManager = userManager;
            _mapper = mapper;
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

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserExternalChangePasswordDTO aInDto)
        {
            if (!string.IsNullOrEmpty(aInDto.UserName) && 
                !string.IsNullOrEmpty(aInDto.Password))
            {
                var user = await _userManager.FindByNameAsync(aInDto.UserName);
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, resetToken, aInDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new { });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                return BadRequest("UserName or password not provided!");
            }
        }

        [HttpGet("unlock/{aId}")]
        public async Task<IActionResult> Unlock(string aId)
        {
            var user = await _userManager.FindByIdAsync(aId);
            await _userManager.ResetAccessFailedCountAsync(user);
            await _userManager.SetLockoutEndDateAsync(user, DateTime.Now.AddDays(-1));
            return Ok();
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserExternalInDTO aInDto)
        {
            if (string.IsNullOrEmpty(aInDto.UserName))
            {
                try
                {
                    var id = await this.baseService.InsertAsync(aInDto);
                    return Ok(new { id });
                }
                catch (ApplicationException)
                {
                    return BadRequest(new { code = "UserAlreadyExists" });
                }
            }
            else
            {
                var entity = _mapper.Map<UserExternalInDTO, LocalGUsersExt>(aInDto);
                entity.Id = BaseEntity.GenerateNewId();
                entity.Version = 1;
                var result = await _userManager.CreateAsync(entity, aInDto.Password);
                if (result.Succeeded)
                {
                    return Ok(new { entity.Id });
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
        }

        [HttpPut("{aId}")]
        public async Task<IActionResult> Put(string aId, [FromBody] UserExternalInDTO aInDto)
        {
            try
            {
                await this.baseService.UpdateAsync(aId, aInDto);
                return Ok();
            }
            catch (ApplicationException)
            {
                return BadRequest(new { code = "UserAlreadyExists" });
            }
        }
    }
}
