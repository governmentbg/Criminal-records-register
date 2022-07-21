using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ZLog
    {
        public DateTime? LogDate { get; set; }
        public int? Line { get; set; }
        public string? ProcedureName { get; set; }
        public string? Info { get; set; }
    }
}
