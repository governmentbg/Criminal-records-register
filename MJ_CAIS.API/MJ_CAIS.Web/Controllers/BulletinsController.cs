using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("bulletins")]
    [Authorize]
    public class BulletinsController : BaseApiCrudController<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string>
    {
        private readonly IBulletinService _bulletinService;
        private readonly IDocumentService _documentService;
        public readonly IPrintDocumentService _printDocumentService;

        public BulletinsController(IBulletinService bulletinService,
            IDocumentService documentService,
            IPrintDocumentService printDocumentService)
            : base(bulletinService)
        {
            _bulletinService = bulletinService;
            _documentService = documentService;
            _printDocumentService = printDocumentService;
        }

        [HttpGet("")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<BulletinGridDTO> aQueryOptions, string? statusId)
        {
            var result = await this._bulletinService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> Search(ODataQueryOptions<BulletinGridDTO> aQueryOptions, [FromQuery] BulletinSearchParamDTO searchParams)
        {
            var result = await this._bulletinService.SearchBulletinAsync(aQueryOptions, searchParams);
            return Ok(result);
        }

        [HttpGet("export")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> ExportAllAsync(ODataQueryOptions<BulletinGridDTO> aQueryOptions, [FromQuery] BulletinSearchParamDTO searchParams)
        {
            var result = await this._bulletinService.ExportAllAsync(aQueryOptions, searchParams);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge},{RoleConstants.CentralAuth}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("getConvitionsOnly/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge},{RoleConstants.CentralAuth}")]
        public async Task<IActionResult> GetConvitionOnly(string aId)
        {
            var result = await this._bulletinService.GetConvictionOnlyAsync(aId);
            return Ok(result);
        }

        [HttpGet("create")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> GetWithPersonData([FromQuery] string personId)
        {
            var result = await this._bulletinService.SelectWithPersonDataAsync(personId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> Post([FromBody] BulletinAddDTO aInDto)
        {
            var id = await this._bulletinService.InsertAsync(aInDto);
            return Ok(new { id });
        }

        [HttpPut("{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public async Task<IActionResult> Put(string aId, [FromBody] BulletinEditDTO aInDto)
        {
            await this._bulletinService.UpdateAsync(aInDto, false);
            return Ok();
        }

        [HttpPut("final-edit/{aId}")]
        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        public virtual async Task<IActionResult> FinalEdit(string aId, [FromBody] BulletinEditDTO aInDto)
        {
            await this._bulletinService.UpdateAsync(aInDto, true);
            return Ok();
        }

        [Authorize(Roles = $"{RoleConstants.Normal},{RoleConstants.Judge}")]
        [HttpPut("{aId}/change-status/{statusId}")]
        public async Task<IActionResult> ChangeStatus(string aId, string statusId)
        {
            await this._bulletinService.ChangeStatusAsync(aId, statusId);
            return Ok();
        }

        [HttpPut("{aId}/delete")]
        public async Task<IActionResult> DeleteBulletin(string aId)
        {
            await this._bulletinService.DeleteBulletinByIdAsync(aId, BulletinResources.msgDeleteBulletin);
            return Ok();
        }

        [HttpGet("{aId}/offences")]
        public async Task<IActionResult> GetOffences(string aId)
        {
            var result = await this._bulletinService.GetOffencesByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/sanctions")]
        public async Task<IActionResult> GetSanctions(string aId)
        {
            var result = await this._bulletinService.GetSanctionsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/decisions")]
        public async Task<IActionResult> GetDecisions(string aId)
        {
            var result = await this._bulletinService.GetDecisionsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._bulletinService.GetDocumentsByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/status-history")]
        public IActionResult GetStatusHistory(string aId)
        {
            var result = this._bulletinService.GetStatusHistoryByBulletinId(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/status-history-content")]
        public async Task<IActionResult> GetContentOnly(string aId)
        {
            var resultXML = await this._bulletinService.GetHistoryContentAsync(aId);
            var fileNameHtml = "bulletin-history.html";
            var mimeTypeHtml = "application/octet-stream";

            Response.Headers.Add("File-Name", fileNameHtml);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(resultXML, mimeTypeHtml, fileNameHtml);
        }

        [HttpPost("{aId}/documents")]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] DocumentDTO aInDto)
        {
            await this._bulletinService.InsertBulletinDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._documentService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._documentService.GetDocumentContentAsync(documentId);
            if (result == null) return NotFound();

            var content = result.DocumentContent;
            var fileName = result.Name;
            var mimeType = GetContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpGet("{aId}/person-alias")]
        public async Task<IActionResult> GetPersonAlias(string aId)
        {
            var result = await this._bulletinService.GetPersonAliasByBulletinIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/print")]
        public async Task<IActionResult> Print(string aId)
        {
            var result = await this._printDocumentService.PrintBulletin(aId);
            if (result == null) return NotFound();

            var content = result;
            var fileName = "bulletin.pdf";
            var mimeType = "application/octet-stream";

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        private string GetContentType(string fileName)
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
