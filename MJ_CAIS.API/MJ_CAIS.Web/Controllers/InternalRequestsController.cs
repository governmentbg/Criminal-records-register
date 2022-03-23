using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Web.Controllers
{
    [Route("internal-requests")]
    [AllowAnonymous] // TODO: remove
    public class InternalRequestsController : BaseApiCrudController<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, BInternalRequest, string>
    {
        private readonly IInternalRequestService _internalRequestService;

        public InternalRequestsController(IInternalRequestService internalRequestService) : base(internalRequestService)
        {
            _internalRequestService = internalRequestService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }
    }
}
