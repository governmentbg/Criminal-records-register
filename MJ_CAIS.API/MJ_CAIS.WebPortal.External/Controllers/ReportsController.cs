using AutoMapper;
using Infragistics.Web.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Account;
using MJ_CAIS.WebPortal.External.Models.Reports;
using System.Linq;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [RedirectNotActive("Inactive", "Account")]
    [Authorize(Roles = "EReports")]
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

        [HttpGet]
        [GridDataSourceAction]
        public async Task<ActionResult> AjaxPersonSearch(PersonSearchModel model)
        {
            if (model == null || model.IsInitialSearch)
            {
                var data = new List<CriminalRecordsPersonDataType>().AsQueryable();
                return View(data);
            }
            else
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
                return View(res.ReportResult.Select(r => new CriminalRecordsPersonModel()
                {
                    FirstNameBg = r.NamesBg?.FirstName,
                    SurNameBg = r.NamesBg?.SurName,
                    FamilyNameBg = r.NamesBg?.FamilyName,
                    FullNameBg = r.NamesBg?.FullName,
                    FirstNameEn = r.NamesEn?.FirstName,
                    SurNameEn = r.NamesEn?.SurName,
                    FamilyNameEn = r.NamesEn?.FamilyName,
                    FullNameEn = r.NamesEn?.FullName,
                    MotherNames = r.MotherNames?.FirstName + " " + r.MotherNames?.SurName + " " + r.MotherNames?.FamilyName + " " + r.MotherNames?.FullName,
                    FatherNames = r.FatherNames?.FirstName + " " + r.FatherNames?.SurName + " " + r.FatherNames?.FamilyName + " " + r.FatherNames?.FullName,
                    CityName = r.BirthPlace?.City?.CityName,
                    CountryName = r.BirthPlace?.Country?.CountryName,
                    BirthPlaceDescr = r.BirthPlace?.Descr,
                    BirthDate = r.BirthDate.Date,
                    BirthDatePrecision = r.BirthDate.DatePrecision,
                    EGN = r.IdentityNumber?.EGN,
                    SUID = r.IdentityNumber?.SUID,
                    LNCh = r.IdentityNumber?.LNCh,
                    LN = r.IdentityNumber?.LN,
                    ActionTemplate =
                    !string.IsNullOrEmpty(r.IdentityNumber?.EGN) ?
                         "<form method=\"post\" autocomplete=\"off\" action=\"/Reports\" novalidate=\"novalidate\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"IdentifierType\" name=\"IdentifierType\" value=\"EGN\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"PID\" name=\"PID\" value=\"" + r.IdentityNumber?.EGN + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"LawReason\" name=\"LawReason\" value=\""+ model.LawReason +"\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"Remark\" name=\"Remark\" value=\"" + model.Remark + "\">" +
                        "<button class=\"btn btn-link\" type=\"submit\" title=\"ЕГН: " + r.IdentityNumber?.EGN + "\">ЕГН: " + r.IdentityNumber?.EGN + "</button>" +
                        "</form>" :
                    !string.IsNullOrEmpty(r.IdentityNumber?.SUID) ?
                         "<form method=\"post\" autocomplete=\"off\" action=\"/Reports\" novalidate=\"novalidate\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"IdentifierType\" name=\"IdentifierType\" value=\"SUID\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"PID\" name=\"PID\" value=\"" + r.IdentityNumber?.SUID + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"LawReason\" name=\"LawReason\" value=\"" + model.LawReason + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"Remark\" name=\"Remark\" value=\"" + model.Remark + "\">" +
                        "<button class=\"btn btn-link\" type=\"submit\" title=\"СисИд: " + r.IdentityNumber?.SUID + "\">СисИд: " + r.IdentityNumber?.SUID + "</button>" +
                        "</form>" :
                    !string.IsNullOrEmpty(r.IdentityNumber?.LN) ?
                         "<form method=\"post\" autocomplete=\"off\" action=\"/Reports\" novalidate=\"novalidate\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"IdentifierType\" name=\"IdentifierType\" value=\"LN\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"PID\" name=\"PID\" value=\"" + r.IdentityNumber?.LN + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"LawReason\" name=\"LawReason\" value=\"" + model.LawReason + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"Remark\" name=\"Remark\" value=\"" + model.Remark + "\">" +
                        "<button class=\"btn btn-link\" type=\"submit\" title=\"ЛН: " + r.IdentityNumber?.LN + "\">ЛН: " + r.IdentityNumber?.LN + "</button>" +
                        "</form>" :
                    !string.IsNullOrEmpty(r.IdentityNumber?.LNCh) ?
                         "<form method=\"post\" autocomplete=\"off\" action=\"/Reports\" novalidate=\"novalidate\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"IdentifierType\" name=\"IdentifierType\" value=\"LNCH\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"PID\" name=\"PID\" value=\"" + r.IdentityNumber?.LNCh + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"LawReason\" name=\"LawReason\" value=\"" + model.LawReason + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"Remark\" name=\"Remark\" value=\"" + model.Remark + "\">" +
                        "<button class=\"btn btn-link\" type=\"submit\" title=\"ЛНЧ: " + r.IdentityNumber?.LNCh + "\">ЛНЧ: " + r.IdentityNumber?.LNCh + "</button>" +
                        "</form>" : "",
                    Remark = model.Remark,
                    LawReason = model.LawReason
                }).AsQueryable());
            }
        }


        [HttpPost]
        public IActionResult Index(CriminalRecordsReportRequestView model)
        {
            return View(model);
        }



        [HttpPost]
        public async Task<ContentResult> CriminalRecordsReportHTML(CriminalRecordsReportRequestView model)
        {
            var result = await _criminalRecordsReportService.GetCriminalRecordsReportHTMLAsync(
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
            return Content(result, "text/html");
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
