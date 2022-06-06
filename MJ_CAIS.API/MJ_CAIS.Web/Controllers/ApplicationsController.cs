using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.StaticFiles;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Common.Constants;

namespace MJ_CAIS.Web.Controllers
{
    [Route("applications")]
    [AllowAnonymous]
    public class ApplicationsController : BaseApiCrudController<ApplicationInDTO, ApplicationOutDTO, ApplicationGridDTO, AApplication, string>
    {
        private readonly IApplicationService _applicationService;
        private readonly IUserContext _userContext;

        public ApplicationsController(IApplicationService applicationService, IUserContext userContext)
            : base(applicationService)
        {
            _applicationService = applicationService;
            _userContext = userContext;
        }

        [HttpGet("")]
        public virtual async Task<IActionResult> GetAll(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._applicationService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("create")]
        public async Task<IActionResult> GetWithPersonData([FromQuery] string personId)
        {
            var result = await this._applicationService.SelectWithPersonDataAsync(personId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("certificates")]
        public virtual async Task<IActionResult> GetAllByCertificateStatus(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._applicationService.SelectAllCertWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public virtual async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("searchByIdentifier/{aId}")]
        public virtual async Task<IActionResult> SearchByIdentifier(string aId)
        {
            this._searchByIdentifierService.SearchByIdentifier(aId);
            return await base.Get(aId);
        }

        [HttpPost("")]
        public virtual async Task<IActionResult> Post([FromBody] ApplicationInDTO aInDto)
        {
            // ����:
            // ������ � ��������� �� ������������, � �� �������
            // ��� ��������� �� ������� ������ �� � ���� ����
            // �� ��������� �� �� ������� ���� ����� ����� �� ���� InsertAsync
            aInDto.CsAuthorityId = _userContext.CsAuthorityId ?? "660"; // todo
            aInDto.StatusCode = ApplicationConstants.ApplicationStatuses.NewId;
            aInDto.ApplicationTypeId = "6";
            return await base.Post(aInDto);
        }

        [HttpPut("final-edit/{aId}")]
        public virtual async Task<IActionResult> FinalEdit(string aId, [FromBody] ApplicationInDTO aInDto)
        {
            await this._applicationService.UpdateAsync(aInDto, true);
            return Ok();
        }

        [HttpPut("{aId}")]
        public virtual async Task<IActionResult> Put(string aId, [FromBody] ApplicationInDTO aInDto)
        {
            await this._applicationService.UpdateAsync(aInDto, false);
            return Ok();
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._applicationService.GetDocumentsByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpPost("{aId}/documents")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] ApplicationDocumentDTO aInDto)
        {
            await this._applicationService.InsertApplicationDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._applicationService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
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
        public async Task<IActionResult> GetPersonAlias(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersAliasByApplicationIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-history")]
        public async Task<IActionResult> GetAppplicationHistory(string aId)
        {
            var result = await this._applicationService.SelectApplicationPersStatusHAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/application-certificate")]
        public async Task<IActionResult> GetAppplicationCertificate(string aId)
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
