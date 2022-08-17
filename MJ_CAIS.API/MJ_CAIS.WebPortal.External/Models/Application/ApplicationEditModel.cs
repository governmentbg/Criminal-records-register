using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.Application
{
    public class ApplicationEditModel : BaseViewModel, IValidatableObject
    {
        public ApplicationEditModel()
        {
            this.PurposeTypes = new List<SelectListItem>();
            this.PurposeInfo = new Dictionary<string, string>();
        }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblEgn))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
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
        public string? RequiredPurposes { get; set; }

        public List<SelectListItem> PurposeTypes { get; set; }
        public Dictionary<string, string> PurposeInfo { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Purpose) &&
                !string.IsNullOrEmpty(PurposeId) &&
                !string.IsNullOrEmpty(RequiredPurposes) &&
                RequiredPurposes.Contains(PurposeId))
            {
                yield return new ValidationResult(
                    string.Format(CommonResources.MsgRequired, ApplicationResources.lblPurpose),
                    new[] { nameof(Purpose) });
            }
        }
    }
}
