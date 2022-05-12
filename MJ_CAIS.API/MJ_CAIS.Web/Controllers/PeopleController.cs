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

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            var result = await this._personService.SelectWithBirthInfoAsync(aId);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("bulletins")]
        public async Task<IActionResult> GetAllBulletins(ODataQueryOptions<PersonBulletinGridDTO> aQueryOptions)
        {
            var result = await this._personService.SelectPersonBulletinAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications(ODataQueryOptions<PersonApplicationGridDTO> aQueryOptions)
        {
            var result = await this._personService.SelectPersonApplicationAllWithPaginationAsync(aQueryOptions);
            return Ok(result);
        }

        //[HttpPost("")]
        //public new async Task<IActionResult> Post([FromBody] PersonDTO aInDto)
        //{
        //    throw new NotImplementedException();
        //    return await base.Post(aInDto);
        //}
    }
}
