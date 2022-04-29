using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("people")]
    [AllowAnonymous]
    public class PeopleController : BaseApiCrudController<PersonDTO, PersonDTO, PersonGridDTO, PPerson, string>
    {
        private readonly IPersonService _personService;

        public PeopleController(IPersonService personService) : base(personService)
        {
            _personService = personService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<PersonGridDTO> aQueryOptions, bool isPageInit = false)
        {         
            var result = await this._personService.SelectAllWithPaginationAsync(aQueryOptions, isPageInit);
            return Ok(result);
        }
    }
}
