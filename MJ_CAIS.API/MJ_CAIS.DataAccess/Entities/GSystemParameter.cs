using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GSystemParameter
    {
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public string? ValueString { get; set; }
        public DateTime? ValueDate { get; set; }
        public decimal? ValueNumber { get; set; }
        public bool? ValueBool { get; set; }
    }
}
