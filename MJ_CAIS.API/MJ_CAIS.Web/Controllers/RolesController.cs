using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Role;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("roles")]
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.GlobalAdmin}")]
    public class RolesController : BaseApiCrudController<RoleDTO, RoleDTO, RoleGridDTO, GRole, string>
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService) : base(roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<RoleGridDTO> aQueryOptions)
        {
            var result = await this._roleService.SelectAllAsync(aQueryOptions);
            return Ok(result);
        }
    }
}
