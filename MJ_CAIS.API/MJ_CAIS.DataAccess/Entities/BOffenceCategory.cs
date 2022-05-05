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
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<BOffence> BOffences { get; set; }
    }
}
