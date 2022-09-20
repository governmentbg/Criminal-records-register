using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("users-citizen")]
    [Authorize(Roles = $"{RoleConstants.Admin},{RoleConstants.GlobalAdmin}")]
    public class UsersCitizenController : BaseApiCrudController<UserCitizenDTO, UserCitizenDTO, UserCitizenGridDTO, GUsersCitizen, string>
    {
        private IPersonService _personService;
        public UsersCitizenController(IUserCitizenService baseService, IPersonService personService) : base(baseService)
        {
            _personService = personService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<UserCitizenGridDTO> aQueryOptions)
        {
            var result = await this.baseService.SelectAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("find-by-egn/{egn}")]
        public async Task<IActionResult> FindByEGN(string egn)
        {

            var result = await this._personService.SelectAllWithPaginationAsync(
                new PersonSearchParamsDTO()
                {
                    Egn = egn
                    //Pid = egn,
                    // PidType = "EGN"
                },
                10,
                1
                );
            if (result.Data.Count() == 1)
            {
                return Ok(result.Data.First().Id);
            }
            else if (result.Data.Count() > 1)
            {
                return BadRequest("Too many results");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
