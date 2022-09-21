using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.ExternalWebServices.Schemas.PersonValidator;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.Reports
{
    public class CriminalRecordsReportRequestView
    {
        public List<SelectListItem> IdentifierTypes { get; set; }

        public CriminalRecordsReportRequestView()
        {
            IdentifierTypes = new List<SelectListItem>();
            IdentifierTypes.Add(new SelectListItem { Text = "ЕГН", Value = DTO.ExternalServicesHost.IdentifierType.EGN.ToString() });
            IdentifierTypes.Add(new SelectListItem { Text = "ЛНЧ", Value = DTO.ExternalServicesHost.IdentifierType.LNCH.ToString() });
            IdentifierTypes.Add(new SelectListItem { Text = "ЛН", Value = DTO.ExternalServicesHost.IdentifierType.LN.ToString() });
            IdentifierTypes.Add(new SelectListItem { Text = "Системен идентификатор", Value = DTO.ExternalServicesHost.IdentifierType.SUID.ToString() });
            IdentifierTypes.Add(new SelectListItem { Text = "Вътрешен идентификатор", Value = "SYSID".ToString() });
            IdentifierTypes.Insert(0, new SelectListItem() { Disabled = true, Text = CommonResources.lblChoose, Selected = true });

        }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblIdentifierType))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? IdentifierType { get; set; }

        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblIdentifier))]
        public string? PID { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblLawReason))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? LawReason { get; set; }

        [Display(ResourceType = typeof(ReportResources), Name = nameof(ReportResources.lblRemark))]
        public string? Remark { get; set; }

        public string? ServiceType { get; set; }

        public string? ServiceURI { get; set; }

        public string? Title { get; set; }
    }
}
