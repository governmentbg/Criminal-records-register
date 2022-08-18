using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using MJ_CAIS.ExternalWebServices;
using MJ_CAIS.ExternalWebServices.Schemas.PersonValidator;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("ecris-messages")]
    [Authorize(Roles = RoleConstants.CentralAuth)]
    public class EcrisMessagesController : BaseApiCrudController<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
        private readonly IEcrisMessageService _ecrisMessageService;
        private readonly PersonValidatorClient _personValidatorClient;
        private readonly IRequestService _requestService;
        private readonly IMapper _mapper;

        public EcrisMessagesController(IEcrisMessageService ecrisMessageService,
            PersonValidatorClient personValidatorClient,
            IRequestService requestService,
            IMapper mapper)
            : base(ecrisMessageService)
        {
            _ecrisMessageService = ecrisMessageService;
            _personValidatorClient = personValidatorClient;
            _requestService = requestService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._ecrisMessageService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}/bulletins")]
        public async Task<IActionResult> GetEcrisBulletins(string aId)
        {
            var result = await this._ecrisMessageService.GetEcrisBulletinsByIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/fbbcs")]
        public async Task<IActionResult> GetEcrisFbbcs(string aId)
        {
            var result = await this._ecrisMessageService.GetEcrisFbbcsByIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        // TODO: remove, no adding
        [HttpPost("")]
        public new async Task<IActionResult> Post([FromBody] EcrisMessageDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        // TODO: remove, no updating
        [HttpPut("{aId}")]
        public new async Task<IActionResult> Put(string aId, [FromBody] EcrisMessageDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }


        [HttpGet("{aId}/nationalities")]
        public async Task<IActionResult> GetNationalities(string aId)
        {
            var result = await this._ecrisMessageService.GetNationalitiesAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/names")]
        public async Task<IActionResult> GetNames(string aId)
        {
            var result = await this._ecrisMessageService.GetNamesAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/grao-people")]
        public IActionResult GetEcrisIdentifiedPeople(string aId)
        {
            var result = this._ecrisMessageService.GetEcrisIdentifiedPeople(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/document/{type}")]
        public async Task<IActionResult> GetEcrisDocument(string aId, string type)
        {
            if (type == "EcrisNot")
            {
                var ecrisNot = await this._ecrisMessageService.GetEcrisNotificationByIdAsync(aId);
                return Ok(ecrisNot);
            }
            else if (type == "EcrisReqResp")
            {
                var ecrisNot = await this._ecrisMessageService.GetEcrisResponseByIdAsync(aId);//RSponse
                return Ok(ecrisNot);
            }
            else if (type == "EcrisRequest")
            {
                var ecrisRequest = await this._ecrisMessageService.GetEcrisRequestByIdAsync(aId);
                return Ok(ecrisRequest);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{aId}/identify/{egn}")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> Identify(string aId, string egn)
        {
            await this._ecrisMessageService.IdentifyAsync(aId, egn);
            return Ok();
        }

        [HttpGet("search-person")]
        public async Task<IActionResult> SearchPerson([FromQuery] SearchParamsDTO searchParams)
        {
            var sex = searchParams.Sex == 1 ? PersonInfoGenderType.male : searchParams.Sex == 2 ? PersonInfoGenderType.female : PersonInfoGenderType.female;
            var people = await _personValidatorClient.GetPersonInfo(searchParams.Firstname, searchParams.Surname, searchParams.Familyname, sex, searchParams.BirthDate);

            var result = new List<PersonSearchResultDataDTO>();
            foreach (var item in people)
            {
                var person = new PersonSearchResultDataDTO
                {
                    Identifier = item.person.personalNumber,
                    FirstName = item.person.name.firstName,
                    SurName = item.person.name.secondName,
                    FamilyName = item.person.name.familyName
                };

                var birthDate = item.person.birthDate;
                if (birthDate != null)
                {
                    person.BirthDate = new DateTime(birthDate.year, birthDate.month, birthDate.day);
                }

                result.Add(person);
            }

            return Ok(result);

        }

        [HttpPut("{aId}/cancel-identification/{reasonId}")]
        public async Task<IActionResult> CancelIdentification(string aId, string reasonId)
        {
            await this._requestService.GenerateUnsuccessfulResponce(aId, reasonId);
            return Ok();
        }
    }
}
