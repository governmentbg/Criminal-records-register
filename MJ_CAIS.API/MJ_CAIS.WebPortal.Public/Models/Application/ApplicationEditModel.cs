using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.Public.Models.Application
{
    public class ApplicationEditModel : BaseViewModel
    {
        public ApplicationEditModel()
        {
            this.PurposeTypes = new List<SelectListItem>();
        }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblEgn))]
        public string? Egn { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblRegistrationNumber))]
        public string? RegistrationNumber { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgEmailInvalid))]
        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblEmail))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblPurposeId))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? PurposeId { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblPurpose))]
        public string? Purpose { get; set; }

        public string? ClientIp { get; set; }

        public List<SelectListItem> PurposeTypes { get; set; }
    }
}
