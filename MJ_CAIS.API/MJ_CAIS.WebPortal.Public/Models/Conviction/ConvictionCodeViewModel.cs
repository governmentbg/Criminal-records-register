using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.Public.Models.Conviction
{
    public class ConvictionCodeViewModel
    {
        [Required]
        [Display(Name = "Код за достъп")]
        public string Code { get; set; }
    }
}
