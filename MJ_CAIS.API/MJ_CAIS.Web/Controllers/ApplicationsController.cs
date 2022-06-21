using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("applications")]
    [Authorize]
    public class ApplicationsController : BaseApiCrudController<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, AApplication, string>
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserContext _userContext;
        private readonly ISearchByIdentifierService _searchByIdentifierService;
        private readonly IPrintDocumentService _printDocumentService;

        public ApplicationsController(IApplicationService applicationService, IUserContext userContext, ISearchByIdentifierService searchByIdentifierService, IPrintDocumentService printDocumentService)
            : base(applicationService)
        {
            _applicationService = applicationService;
            _userContext = userContext;
            _searchByIdentifierService = searchByIdentifierService;
            _printDocumentService = printDocumentService;
        }

        [HttpGet("")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._applicationService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }


        [Authorize(Roles = $"{RoleConstants.Normal}")]
        [HttpGet("create")]
        public async Task<IActionResult> GetWithPersonData([FromQuery] string personId)
        {
            var result = await this._applicationService.SelectWithPersonDataAsync(personId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("certificates")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> GetAllByCertificateStatus(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._applicationService.SelectAllCertWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("cancelApplication/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal}")]
        public virtual async Task<IActionResult> cancelApplicationByIdentifier(string aId)
        {
            await this._applicationService.ChangeApplicationStatusToCanceled(aId);
            return Ok();
        }



        [HttpGet("changeStatusToCheckPayment/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal}")]
        public virtual async Task<IActionResult> changeStatusToCheckPayment(string aId)
        {
            await this._applicationService.ChangeApplicationStatusToCheckPayment(aId);
            return Ok();
        }

        [HttpGet("searchByIdentifier/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal}")]
        public virtual async Task<IActionResult> SearchByIdentifier(string aId)
        {
            var result = await this._searchByIdentifierService.SearchByIdentifier(aId);
            return Ok(new { id = result });
        }

        [HttpGet("searchByIdentifierLNCH/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal}")]
        public virtual async Task<IActionResult> searchByIdentifierLNCH(string aId)
        {
            var result = await this._searchByIdentifierService.SearchByIdentifierLNCH(aId);
            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = $"{RoleConstants.Normal}")]
        public virtual async Task<IActionResult> Post([FromBody] ApplicationInDTO aInDto)
        {
            aInDto.CsAuthorityId = _userContext.CsAuthorityId ?? "660"; // todo
            aInDto.StatusCode = ApplicationConstants.ApplicationStatuses.NewId;
            aInDto.ApplicationTypeId = "6";
            return await base.Post(aInDto);
        }

        [HttpPut("final-edit/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> FinalEdit(string aId, [FromBody] ApplicationInDTO aInDto)
        {
            await this._applicationService.UpdateAsync(aInDto, true);
            return Ok();
        }

        [HttpPut("{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> Put(string aId, [FromBody] ApplicationInDTO aInDto)
        {
            await this._applicationService.UpdateAsync(aInDto, false);
            return Ok();
        }


        [HttpGet("printApplication/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> PrintApplicationById(string aId)
        {
            var result = await this._printDocumentService.PrintApplication(aId);
            if (result == null) return NotFound();

            var content = result;
            var fileName = "certificate.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("{aId}/documents")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._applicationService.GetDocumentsByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpPost("{aId}/documents")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] ApplicationDocumentDTO aInDto)
        {
            await this._applicationService.InsertApplicationDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._applicationService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._applicationService.GetDocumentContentAsync(documentId);
            if (result == null) return NotFound();

            var content = result.DocumentContent;
            var fileName = result.Name;
            var mimeType = getContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("{aId}/person-alias")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetPersonAlias(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/eWeb-requests")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetEWebRequestsByApplicationId(string aId)
        {
            var result = await this._applicationService.SelectAllEWebRequestsByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-history")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetApplicationHistory(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersStatusHAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-certificate")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetApplicationCertificate(string aId)
        {
            var result = await this._applicationService.SelectApplicationCertificateByApplicationIdAsync(aId);
            return Ok(result);
        }

        private string getContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string? contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
