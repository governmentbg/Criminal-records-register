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

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] UserExternalInDTO aInDto)
        {
            if (string.IsNullOrEmpty(aInDto.UserName))
            {
                var id = await this.baseService.InsertAsync(aInDto);
                return Ok(new { id });
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
            await this.baseService.UpdateAsync(aId, aInDto);
            return Ok();
        }
    }
}
