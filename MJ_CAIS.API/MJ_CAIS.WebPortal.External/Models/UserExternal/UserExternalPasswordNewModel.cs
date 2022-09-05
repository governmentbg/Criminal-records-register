using Microsoft.AspNetCore.Identity;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.UserExternal
{
    public class UserExternalPasswordNewModel : UserExternalPasswordEditModel
    {

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblUserName))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]

        public string UserName { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblPassword))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string Password { get; set; }

        [Display(ResourceType = typeof(UsersResources), Name = nameof(UsersResources.lblConfirmPassword))]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        [Compare("Password", ErrorMessageResourceType = typeof(UsersResources), ErrorMessageResourceName = nameof(UsersResources.msgPasswordsMustMatch))]
        public string ConfirmPassword { get; set; }
    }
}
