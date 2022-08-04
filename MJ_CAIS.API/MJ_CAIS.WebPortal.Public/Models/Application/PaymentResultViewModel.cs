using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.Common;
using MJ_CAIS.Common.Resources;
using System.ComponentModel.DataAnnotations;

namespace MJ_CAIS.WebPortal.Public.Models.Application
{
    public class PaymentResultViewModel
    {
        public PaymentResultViewModel()
        {
        }

        public bool Canceled { get; set; }

        public string PaymentStatus{get;set;}
    }
}
