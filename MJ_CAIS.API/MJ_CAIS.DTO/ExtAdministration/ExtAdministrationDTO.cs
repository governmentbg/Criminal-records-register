using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.ExtAdministration
{
    public class ExtAdministrationDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public decimal? Version { get; set; }

    }
}
