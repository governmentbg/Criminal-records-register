using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BEcrisOffCategory : BaseEntity, IBaseNomenclature
    {
        public BEcrisOffCategory()
        {
            BOffences = new HashSet<BOffence>();
        }

        public string? EcrisTechnId { get; set; }
        public string? Category { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? CategoryIsOpen { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<BOffence> BOffences { get; set; }
    }
}
