﻿using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users-citizen")]
    [Authorize(Roles = RoleConstants.Admin)]
    public class UsersCitizenController : BaseApiCrudController<UserCitizenDTO, UserCitizenDTO, UserCitizenGridDTO, GUsersCitizen, string>
    {
        public UsersCitizenController(IUserCitizenService baseService) : base(baseService)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<UserCitizenGridDTO> aQueryOptions)
        {
            var result = await this.baseService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }
    }
}
