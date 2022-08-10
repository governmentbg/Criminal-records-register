using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DTO.ExtAdministration;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.Account
{
    public class InactiveViewModel : BaseViewModel
    {
        public InactiveViewModel()
        {
            Administrations = new List<SelectListItem>();
        }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblAdministration))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? AdministrationId { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblEmail))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblName))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblPosition))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Position { get; set; }

        public List<SelectListItem> Administrations { get; set; }
    }
}
