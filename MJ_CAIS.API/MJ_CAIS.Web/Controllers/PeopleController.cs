using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Web.Controllers
{
    [Route("people")]
    [Authorize(Roles = $"{RoleConstants.Judge},{RoleConstants.Normal}")]
    public class PeopleController : BaseApiCrudController<PersonDTO, PersonDTO, PersonGridDTO, PPerson, string>
    {
        private readonly IPersonService _personService;
        private readonly IManagePersonService _managePersonService;

        public PeopleController(IPersonService personService,
            IManagePersonService managePersonService) : base(personService)
        {
            _personService = personService;
            _managePersonService = managePersonService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<PersonGridDTO> aQueryOptions, [FromQuery] PersonSearchParamsDTO searchParams)
        {
            var result = await this._personService.SelectAllWithPaginationAsync(aQueryOptions, searchParams);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            var result = await this._managePersonService.SelectWithBirthInfoAsync(aId);
            var count = await this._personService.GetBulletinsCountByPersonId(aId).ToListAsync();
            result.Bulletin78ACount = count.FirstOrDefault(x => x.Status == BulletinConstants.Type.Bulletin78A)?.Count ?? 0;
            result.ConvictionBulletinCount = count.FirstOrDefault(x => x.Status == BulletinConstants.Type.ConvictionBulletin)?.Count ?? 0;
            result.BulletinUnspecifiedCount = count.FirstOrDefault(x => x.Status == BulletinConstants.Type.Unspecified)?.Count ?? 0;

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("bulletins")]
        public async Task<IActionResult> GetAllBulletins(ODataQueryOptions<PersonBulletinGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonBulletinAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetAllApplications(ODataQueryOptions<PersonApplicationGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonApplicationAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("e-applications")]
        public async Task<IActionResult> GetAllEApplications(ODataQueryOptions<PersonEApplicationGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonEApplicationAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("fbbcs")]
        public async Task<IActionResult> GetAllFbbcs(ODataQueryOptions<PersonFbbcGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonFbbcAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("pids")]
        public async Task<IActionResult> GetAllPids(ODataQueryOptions<PersonPidGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonPidAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpPost("{aId}/connect/{personToBeConnected}")]
        public async Task<IActionResult> ConnectPeople(string aId, string personToBeConnected)
        {
            await this._managePersonService.ConnectPeopleAsync(aId, personToBeConnected);
            return Ok();
        }

        [HttpPost("remove-pid")]
        public async Task<IActionResult> RemovePid([FromBody] RemovePidDTO aInDto)
        {
            var result = await this._managePersonService.RemovePidAsync(aInDto);
            if (result == null) return NotFound();

            return Ok(result.PersonId);
        }
    }
}