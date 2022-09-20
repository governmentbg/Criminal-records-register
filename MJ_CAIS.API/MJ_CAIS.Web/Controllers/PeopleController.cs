using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using MJ_CAIS.Common.Constants;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.InternalRequest;

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

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] PersonSearchParamsDTO searchParams)
        {
            var result = await this._personService.SearchPeopleAsync(searchParams);
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

        [HttpGet("archive")]
        public async Task<IActionResult> GetAllArchive(ODataQueryOptions<PersonArchiveGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonArchiveAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("e-applications")]
        public async Task<IActionResult> GetAllEApplications(ODataQueryOptions<PersonEApplicationGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonEApplicationAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpGet("report-applications")]
        public async Task<IActionResult> GetAllGeneratedReports(ODataQueryOptions<PersonGeneratedReportGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonReportApplAllWithPaginationAsync(aQueryOptions, personId);
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

        [HttpGet("person-history")]
        public async Task<IActionResult> GetAllPersonHistoryData(ODataQueryOptions<PersonHistoryDataGridDTO> aQueryOptions, string personId)
        {
            var result = await this._personService.SelectPersonHistoryDataAllWithPaginationAsync(aQueryOptions, personId);
            return Ok(result);
        }

        [HttpPost("connect")]
        public async Task<IActionResult> ConnectPeople([FromBody] ConnectPeopleDTO aInDto)
        {
            await this._managePersonService.ConnectPeopleAsync(aInDto.Id, aInDto.PersonToBeConnected, aInDto.Desc);
            return Ok();
        }

        [HttpPost("remove-pid")]
        public async Task<IActionResult> RemovePid([FromBody] RemovePidDTO aInDto)
        {
            var result = await this._managePersonService.RemovePidAsync(aInDto);
            if (result == null) return NotFound();

            return Ok();
        }

        [HttpGet("person-data-by-pid")]
        public async Task<IActionResult> GerPersonDataByPid([FromQuery] GetPersonDataByPidParamDTO model)
        {
            var result = await this._personService.GetPersonDataByPidAsync(model.Pid, model.PidType);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}