using MJ_CAIS.Common;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.External.Models.Conviction
{
    public class ConvictionCodeViewModel
    {
        [Display(Name = "Код за достъп")]
        [Required(ErrorMessageResourceType = typeof(CommonResources), ErrorMessageResourceName = nameof(CommonResources.MsgRequired))]
        public string Id { get; set; }
    }
}
