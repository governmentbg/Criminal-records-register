using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Account;
using MJ_CAIS.WebPortal.External.Models.Reports;
using System.Linq;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [RedirectNotActive("Inactive", "Account")]
    [Authorize]
    public class ReportsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICriminalRecordsReportService _criminalRecordsReportService;
        public ReportsController(
            IMapper mapper,
            ICriminalRecordsReportService criminalRecordsReportService)
        {
            _mapper = mapper;
            _criminalRecordsReportService = criminalRecordsReportService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new CriminalRecordsReportRequestView();
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> PersonSearch()
        {
            var model = new PersonSearchModel();
            model.Initial = true;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> PersonSearch(PersonSearchModel model)
        {
            var arg = new DTO.ExternalServicesHost.PersonIdentifierSearchExtendedRequestType()
            {
                CallContext = new DTO.ExternalServicesHost.CallContext()
                {
                    EmployeeIdentifier = CurrentUserEmail,
                    AdministrationName = CurrentUserAdministrationName,
                    EmployeeNames = CurrentUserName,
                    EmployeePosition = CurrentUserPosition,
                    LawReason = model.LawReason,
                    Remark = model.Remark,
                    ServiceType = model.ServiceType,
                    ServiceURI = model.ServiceURI
                    //AdministrationOId,
                    //EmployeeAditionalIdentifier,
                    //ResponsiblePersonIdentifier
                },
                PersonIdentifierSearchRequest = new DTO.ExternalServicesHost.PersonIdentifierSearchRequestType()
                {
                    BirthCountry = model.BirthCountry,
                    Birthdate = model.Birthdate.Value.ToUniversalTime(),
                    BirthDatePrec = model.BirthDatePrec,
                    Birthplace = model.Birthplace,
                    Familyname = model.Familyname,
                    Firstame = model.Firstame,
                    Surname = model.Surname,
                    Fullname = model.Fullname

                }
            };
            var res = await _criminalRecordsReportService.PersonIdentifierSearchAsync(arg);
            model.CriminalRecordsPerson = res.ReportResult.ToList();
            return View(model);
        }


        [HttpPost]
        public IActionResult Index(CriminalRecordsReportRequestView model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CriminalRecordsReportPDF(CriminalRecordsReportRequestView model)
        {
            var result = await _criminalRecordsReportService.GetCriminalRecordsReportPDFAsync(
                new DTO.ExternalServicesHost.CriminalRecordsExtendedRequestType()
                {
                    CallContext = new DTO.ExternalServicesHost.CallContext()
                    {
                        EmployeeIdentifier = CurrentUserEmail,
                        AdministrationName = CurrentUserAdministrationName,
                        EmployeeNames = CurrentUserName,
                        EmployeePosition = CurrentUserPosition,
                        LawReason = model.LawReason,
                        Remark = model.Remark,
                        ServiceType = model.ServiceType,
                        ServiceURI = model.ServiceURI
                        //AdministrationOId,
                        //EmployeeAditionalIdentifier,
                        //ResponsiblePersonIdentifier                     
                    },
                    CriminalRecordsRequest = new DTO.ExternalServicesHost.CriminalRecordsRequestType()
                    {
                        IdentifierTypeSpecified = true,
                        IdentifierType = Enum.Parse<DTO.ExternalServicesHost.IdentifierType>(model.IdentifierType),
                        PID = model.PID
                    }
                });

            return File(result.ResultData, "application/pdf");
        }
    }
}
