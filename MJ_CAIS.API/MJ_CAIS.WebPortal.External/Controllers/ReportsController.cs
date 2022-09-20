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
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.WebPortal.External.Models.Account;
using MJ_CAIS.WebPortal.External.Models.Reports;
using System.Linq;

namespace MJ_CAIS.WebPortal.External.Controllers
{
    [RedirectNotActive("Index", "Home")]
    [Authorize(Roles = "EReports")]
    public class ReportsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICriminalRecordsReportService _criminalRecordsReportService;
        private readonly IPersonService _personService;
        public ReportsController(
            IMapper mapper,
            ICriminalRecordsReportService criminalRecordsReportService,
            IPersonService personService)
        {
            _mapper = mapper;
            _criminalRecordsReportService = criminalRecordsReportService;
            _personService = personService;
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
        public async Task<ActionResult> LoadSearchData([FromBody]PersonLoadDataModel model)
        {
            PersonSearchFormDTO result = null;
            if (!string.IsNullOrEmpty(model.EGN))
            {
                result = await this._personService.GetPersonDataByPidAsync(model.EGN, "EGN");
            }
            else if (!string.IsNullOrEmpty(model.EGN))
            {
                result = await this._personService.GetPersonDataByPidAsync(model.LNCH, "LNCH");
            }
            return result != null ? Ok(result) : NotFound();
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
                var result = await this._personService.SearchPeopleAsync(
                    new DTO.Person.PersonSearchParamsDTO()
                    {
                        Firstname = model.Firstame,
                        Surname = model.Surname,
                        Familyname = model.Familyname,
                        Fullname = model.Fullname,
                        BirthDate = model.Birthdate,
                        Egn = model.EGN,
                        Lnch = model.LNCH
                    });

                return View(result.Select(r => new CriminalRecordsPersonModel()
                {
                    Id = r.Id,
                    Pids = r.Pids,
                    PersonNames = r.PersonNames,
                    MotherNames = r.MotherNames,
                    FatherNames = r.FatherNames,
                    Sex = r.Sex,
                    BirthDate = r.BirthDate.HasValue ? r.BirthDate.Value.Date.ToString("dd.MM.yyyy г.") : String.Empty,
                    MatchText = r.MatchText,
                    ActionTemplate = 
                         "<form method=\"post\" autocomplete=\"off\" action=\"/Reports\" novalidate=\"novalidate\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"IdentifierType\" name=\"IdentifierType\" value=\"SYSID\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"PID\" name=\"PID\" value=\"" + r.Id + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"LawReason\" name=\"LawReason\" value=\"" + model.LawReason + "\">" +
                        "<input hidden=\"\" type=\"text\" data-val=\"true\" id=\"Remark\" name=\"Remark\" value=\"" + model.Remark + "\">" +
                        "<button class=\"btn btn-link\" type=\"submit\" title=\"Идентификатор: " + r.Id + "\">Справка</button>" +
                        "</form>"
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
