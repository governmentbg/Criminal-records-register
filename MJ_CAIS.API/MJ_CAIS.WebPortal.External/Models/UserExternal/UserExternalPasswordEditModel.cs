using Microsoft.AspNetCore.Identity;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.UserExternal
{
    public class UserExternalPasswordEditModel
    {
        public string? Id { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblEmail))]
        [EmailAddress(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgEmailInvalid))]
        public string? Email { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblName))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string? Name { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblIsAdmin))]
        public bool IsAdmin { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblAdministration))]
        public string? AdministrationName { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblIsActive))]
        public bool Active { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblPosition))]
        public string? Position { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblPhone))]
        public string? Phone { get; set; }

        public bool IsAdded { get; set; }
        public bool HasUnlock{ get; set; }

        public string? Version { get; set; }

        public string? ConcurrencyStamp { get; set; }

        public IEnumerable<IdentityError> CreateErrors { get; set; } = new IdentityError[0];

        //public PasswordChangeResult{get;set;}
    }
}
