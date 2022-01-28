using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BOffenceCategory
    {
        public BOffenceCategory()
        {
            BOffences = new HashSet<BOffence>();
            InverseParentCatGroup = new HashSet<BOffenceCategory>();
        }

        public string Id { get; set; } = null!;
        public string? ParentCatGroupId { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? OffLevel { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual BOffenceCategory? ParentCatGroup { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<BOffenceCategory> InverseParentCatGroup { get; set; }
    }
}
