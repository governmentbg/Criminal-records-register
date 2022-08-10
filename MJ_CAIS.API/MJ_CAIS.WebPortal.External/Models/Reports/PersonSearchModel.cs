using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DTO.ExternalServicesHost;
using System.ComponentModel.DataAnnotations;
using static MJ_CAIS.Common.Constants.GlobalConstants;

namespace MJ_CAIS.WebPortal.External.Models.Reports
{
    public class PersonSearchModel
    {
        public List<CriminalRecordsPersonDataType> CriminalRecordsPerson { get; set; }

        public PersonSearchModel()
        {
            BirthDatePrecList = new List<SelectListItem>();
            CriminalRecordsPerson = new List<CriminalRecordsPersonDataType>();
            BirthDatePrecList.Add(new SelectListItem() { Text = "Точна", Value = DatePrecisionType.YMD });
            BirthDatePrecList.Add(new SelectListItem() { Text = "Година и месец", Value = DatePrecisionType.YM });
            BirthDatePrecList.Add(new SelectListItem() { Text = "Година", Value = DatePrecisionType.Y });
            BirthDatePrecList.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true });
            Initial = false;
        }
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblFirstName))]

        public string? Firstame { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblSurname))]

        public string? Surname { get; set; }
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblFamilyname))]

        public string? Familyname { get; set; }
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblFullname))]

        public string? Fullname { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblBirthdate))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        [DataType(DataType.Date)]
        public DateTime? Birthdate { get; set; }

        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblBirthDatePrec))]
        public string? BirthDatePrec { get; set; }

        public List<SelectListItem> BirthDatePrecList { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblBirthCountry))]
        public string? BirthCountry { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblBirthPlace))]
        public string? Birthplace { get; set; }

        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblLawReason))]
        public string? LawReason { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblRemark))]
        public string? Remark { get; set; }

        public string? ServiceType { get; set; }

        public string? ServiceURI { get; set; }

        public bool Initial { get; set; }
    }
}
