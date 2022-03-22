using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BOffenceCategory : BaseEntity, IBaseNomenclature
    {
        public BOffenceCategory()
        {
            BOffences = new HashSet<BOffence>();
        }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? OffLevel { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? OrderNumber { get; set; }

        public virtual ICollection<BOffence> BOffences { get; set; }
    }
}
