using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.DTO.NomenclatureDetail;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("nomenclature-details")]
    [Authorize]
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

        [HttpGet("a-purposes")]
        public IActionResult GetAllAPurposes()
        {
            var result = _nomenclatureDetailService.GetAllAPurposes();
            return Ok(result);
        }

        [HttpGet("a-payment-methods")]
        public IActionResult GetDeskAPaymentMethods()
        {
            var result = _nomenclatureDetailService.GetDeskAPaymentMethods();
            return Ok(result);
        }

        [HttpGet("a-srvc-res-rcpt-methods")]
        public IActionResult GetSrvcResRcptMethods()
        {
            var result = _nomenclatureDetailService.GetSrvcResRcptMethods();
            return Ok(result);
        }

        [HttpGet("fbbc-doc-types")]
        public IActionResult GetAllFbbcDocTypes()
        {
            var result = _nomenclatureDetailService.GetAllFbbcDocTypes();
            return Ok(result);
        }

        [HttpGet("fbbc-sanct-types")]
        public IActionResult GetAllFbbcSanctTypes()
        {
            var result = _nomenclatureDetailService.GetAllFbbcSanctTypes();
            return Ok(result);
        }

        [HttpGet("municipalities/{districtId}")]
        public IActionResult GetMunicipalitiesByProvince(string districtId)
        {
            var result = _nomenclatureDetailService.GetMunicipalitiesByDistrict(districtId);
            return Ok(result);
        }

        [HttpGet("cities/{municipalityId}")]
        public IActionResult GetSettlementsByMunicipality(string municipalityId)
        {
            var result = _nomenclatureDetailService.GetCitiesByMunicipality(municipalityId);
            return Ok(result);
        }

        [HttpGet("bulletin-statuses")]
        public IActionResult GetBulletinStatuses()
        {
            var result = _nomenclatureDetailService.GetBulletinStatuses();
            return Ok(result);
        }

        [HttpGet("internal-request-types")]
        public IActionResult GetInternalRequestTypes()
        {
            var result = _nomenclatureDetailService.GetInternalRequestTypes();
            return Ok(result);
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries(ODataQueryOptions<CountryDTO> aQueryOptions)
        {
            var result = await _nomenclatureDetailService.GetCountriesAsync(aQueryOptions);
            return Ok(result);
        }

        [HttpGet("countries-ordered")]
        public IActionResult GetCountriesOrdered()
        {
            var result = _nomenclatureDetailService.GetCountriesOrdered();
            return Ok(result);
        }

        [HttpGet("sanction-categories")]
        public IActionResult GetSanctionCategories()
        {
            var result = _nomenclatureDetailService.GetSanctionCategories();
            return Ok(result);
        }

        [HttpGet("pid-types")]
        public IActionResult GetPidTypes()
        {
            var result = _nomenclatureDetailService.GetPidTypes();
            return Ok(result);
        }

        [HttpGet("deciding-authorities")]
        public async Task<IActionResult> GetDecidingAuthorities()
        {
            var result = await _nomenclatureDetailService.GetDecidingAuthoritiesForBulletinsAsync();
            return Ok(result);
        }

        // todo: !!! remove this, use grid with pagination
        [HttpGet("g-users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _nomenclatureDetailService.GetGUsersAsync();
            return Ok(result);
        }
    }
}
