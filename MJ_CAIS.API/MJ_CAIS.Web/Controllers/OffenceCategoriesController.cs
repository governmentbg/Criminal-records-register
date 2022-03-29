using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.OffenceCategory;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;

namespace MJ_CAIS.Web.Controllers
{
    [Route("offence-categories")]
    [AllowAnonymous] // TODO: remove
    public class OffenceCategoriesController : BaseApiCrudController<OffenceCategoryDTO, OffenceCategoryDTO, OffenceCategoryGridDTO, BOffenceCategory, string>
    {
        private readonly IOffenceCategoryService _offenceCategoryService;

        public OffenceCategoriesController(IOffenceCategoryService offenceCategoryService) : base(offenceCategoryService)
        {
            _offenceCategoryService = offenceCategoryService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll(ODataQueryOptions<OffenceCategoryGridDTO> aQueryOptions)
        {
            return await base.GetAll(aQueryOptions);
        }
    }
}
