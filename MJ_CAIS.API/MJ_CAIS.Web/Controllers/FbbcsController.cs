using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("fbbcs")]
    [AllowAnonymous]
    public class FbbcsController : BaseApiCrudController<FbbcDTO, FbbcDTO, FbbcGridDTO, Fbbc, string>
    {
        private readonly IFbbcService _fbbcService;

        public FbbcsController(IFbbcService fbbcService) : base(fbbcService)
        {
            _fbbcService = fbbcService;
        }

        [HttpGet("")]
        public new async Task<IActionResult> GetAll(ODataQueryOptions<FbbcGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }

        [HttpGet("getAll")]
        public new async Task<IActionResult> GetAllNoWrap(ODataQueryOptions<FbbcGridDTO> aQueryOptions)
        {
            return await base.GetAllNoWrap(aQueryOptions);
        }
    }
}
