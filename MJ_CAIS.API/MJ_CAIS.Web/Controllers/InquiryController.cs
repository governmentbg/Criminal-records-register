﻿using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Web.Controllers.Common;

namespace MJ_CAIS.Web.Controllers
{
    [Route("inquiry")]
    [AllowAnonymous]
    public class InquiryController : BaseApiController
    {
        private readonly IInquiryService _service;

        public InquiryController(IInquiryService service)
        {
            _service = service;
        }

        [HttpGet("search-bulletins")]
        public IActionResult SearchBulletins(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, [FromQuery] InquirySearchBulletinDTO searchParams)
        {
            var result = this._service.SearchBulletinsWithPagination(aQueryOptions, searchParams);
            return Ok(result);
        }
    }
}
