using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.Application
{
    public class ApplicationSearchGridDTO : BaseGridDTO
    {
        public string? CertificateRegistrationNumber { get; set; }
        public string? StatusCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? PersonIdentificator { get; set; }
        public string? Names { get; set; }
        public string? FirstSigner { get; set; }
        public string? SecondSigner { get; set; }
        public string? AccessCode { get; set; }
        public string? StatusCodeDisplayValue { get; set; }
    }
}
