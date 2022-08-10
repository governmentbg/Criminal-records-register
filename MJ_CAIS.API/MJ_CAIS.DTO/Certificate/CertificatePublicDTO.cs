using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.Certificate
{
    public class CertificatePublicDTO
    {
        public string? WAppId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public string? AccessCode1 { get; set; }
        public string? PurposeName { get; set; }
        public string? Purpose { get; set; }
    }
}
