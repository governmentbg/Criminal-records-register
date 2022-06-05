using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.Public.Models.Conviction
{
    public class ConvictionCodeViewModel
    {
        [Required] // TODO: with resources
        [Display(Name = "Код за достъп")]
        public string Id { get; set; }
    }
}
