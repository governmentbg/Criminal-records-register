using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("fbbcs")]
    [Authorize]
    public class FbbcsController : BaseApiCrudController<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
        private readonly IFbbcService _fbbcService;

        public FbbcsController(IFbbcService fbbcService) : base(fbbcService)
        {
            _fbbcService = fbbcService;
        }

        [HttpGet("")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> GetAll(ODataQueryOptions<FbbcGridDTO> aQueryOptions, string statusId)
        {
            var result = await this._fbbcService.SelectAllWithPaginationAsync(aQueryOptions, statusId);
            return Ok(result);
        }

        [HttpGet("{aId}")]
        public new async Task<IActionResult> Get(string aId)
        {
            return await base.Get(aId);
        }

        [HttpGet("create")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> GetWithPersonData([FromQuery] string personId)
        {
            var result = await this._fbbcService.SelectWithPersonDataAsync(personId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public new async Task<IActionResult> Post([FromBody] FbbcDTO aInDto)
        {
            return await base.Post(aInDto);
        }

        [HttpPut("{aId}")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public new async Task<IActionResult> Put(string aId, [FromBody] FbbcDTO aInDto)
        {
            return await base.Put(aId, aInDto);
        }

        [HttpDelete("{aId}")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public new async Task<IActionResult> Delete(string aId)
        {
            return await base.Delete(aId);
        }

        [HttpGet("{aId}/ecris-messages")]
        [Authorize]
        public async Task<IActionResult> GetEcrisMessages(string aId)
        {
            var result = await this._fbbcService.GetEcrisMessagesByFbbcIdAsync(aId);
            return Ok(result);
        }

        [HttpGet("{aId}/documents")]
        public async Task<IActionResult> GetDocuments(string aId)
        {
            var result = await this._fbbcService.GetDocumentsByFbbcIdAsync(aId);
            return Ok(result);
        }

        [HttpPost("{aId}/documents")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> PostDocument(string aId, [FromBody] FbbcDocumentDTO aInDto)
        {
            await this._fbbcService.InsertFbbcDocumentAsync(aId, aInDto);
            return Ok();
        }

        [HttpDelete("{aId}/documents/{documentId}")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> DeleteDocument(string documentId)
        {
            await this._fbbcService.DeleteDocumentAsync(documentId);
            return Ok();
        }

        [HttpGet("{aId}/documents-download/{documentId}")]
        public async Task<IActionResult> GetContents(string documentId)
        {
            var result = await this._fbbcService.GetDocumentContentAsync(documentId);
            if (result == null) return NotFound();

            var content = result.DocumentContent;
            var fileName = result.Name;
            var mimeType = getContentType(fileName);

            Response.Headers.Add("File-Name", fileName);
            Response.Headers.Add("Access-Control-Expose-Headers", "File-Name");

            return File(content, mimeType, fileName);
        }

        [HttpPut("{aId}/change-status/{statusId}")]
        [Authorize(Roles = RoleConstants.CentralAuth)]
        public async Task<IActionResult> ChangeStatus(string aId, string statusId)
        {
            await this._fbbcService.ChangeStatusAsync(aId, statusId);
            return Ok();
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
