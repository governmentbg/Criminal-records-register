using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("nomenclature-details")]
    [AllowAnonymous] // TODO: remove
    public class NomenclatureDetailsController : BaseApiCrudController<BaseNomenclatureDTO, BaseNomenclatureDTO, BaseNomenclatureDTO, GNomenclature, string>
    {
        private readonly INomenclatureDetailService _nomenclatureDetailService;

        public NomenclatureDetailsController(INomenclatureDetailService nomenclatureDetailService) : base(nomenclatureDetailService)
        {
            _nomenclatureDetailService = nomenclatureDetailService;
        }

        /// <summary>
        /// Връща абсолютно всички номенклатури от този тип
        /// </summary>
        [HttpGet("{tableName}")]
        public IActionResult GetAllNomenclatureValues(string tableName)
        {
            var result = _nomenclatureDetailService.GetBaseNomenclatureValues(tableName);
            return Ok(result);
        }
    }
}
