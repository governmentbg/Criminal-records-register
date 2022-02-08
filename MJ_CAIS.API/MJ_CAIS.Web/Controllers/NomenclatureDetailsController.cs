using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("nomenclature-details")]
    [AllowAnonymous] // TODO: remove
    public class NomenclatureDetailsController : BaseApiController
    {
        public NomenclatureDetailsController() 
        {
        }

        /// <summary>
        /// Връща абсолютно всички номенклатури от този тип
        /// </summary>
        [HttpGet("{tableName}")]
        public IActionResult GetAllNomenclatureValues(string tableName)
        {
            // TODO:
            //var result = _nomenclatureDetailsService.GetBaseNomenclatureValues(tableName);
            //return Ok(result);

            var result = new List<BaseNomenclatureDTO>();
            if (tableName == "g_countries")
            {
                result.Add(new BaseNomenclatureDTO { Id = "CO-00-004-AFG", Name = "Афганистан", NameEn = "Afghanistan" });
                result.Add(new BaseNomenclatureDTO { Id = "CO-00-008-ALB", Name = "Албания", NameEn = "Albania" });
            }
            if (tableName == "b_case_types")
            {
                result.Add(new BaseNomenclatureDTO { Id = "sign_vnaxd", Code = "sign_vnaxd", Name = "ВНАХД" });
                result.Add(new BaseNomenclatureDTO { Id = "sign_vnoxd", Code = "sign_vnoxd", Name = "ВНОХД" });
            }

            return Ok(result);
        }

        /// <summary>
        /// Връща всички номенклатури от този тип със filtering, sorting, paging за визуализация в грид
        /// </summary>
        [HttpGet("get-details/{nomenclatureId}")]
        public async Task<IActionResult> GetNomenclaturesWithPaginationAsync(ODataQueryOptions<BaseNomenclatureDTO> aQueryOptions, int nomenclatureId)
        {
            // TODO:
            //var result = await _nomenclatureDetailsService.GetNomenclatureDetails(aQueryOptions, nomenclatureId);
            //return Ok(result);
            return Ok(new string[] { });
        }
    }
}
