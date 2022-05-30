using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.DTO.ExternalServicesHost
{
    public class CriminalRecordsPDFResult
    {
        public bool HasError { get; set; }
        public string ErrorMessage { get; set; }
        public byte[] ResultData { get; set; }
    }
}
