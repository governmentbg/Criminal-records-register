using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.Application
{
    public class ApplicationPreviewModel
    {
        public string? Id { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblRegistrationNumber))]
        public string? RegistrationNumber { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblName))]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblCreatedOn))]
        public DateTime? CreatedOn { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblEgn))]
        public string? Egn { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblEmail))]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblPurposeId))]
        public string? PurposeName { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblPurpose))]
        public string? Purpose { get; set; }

        [Display(ResourceType = typeof(ApplicationResources), Name = nameof(ApplicationResources.lblStatus))]
        public string? Status { get; set; }

        public bool HasGeneratedCertificate { get; set; }
        public bool? FromNew { get; set; }
    }
}
