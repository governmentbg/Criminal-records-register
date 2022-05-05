using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class FbbcDocType : BaseEntity
    {
        public FbbcDocType()
        {
            Fbbcs = new HashSet<Fbbc>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<Fbbc> Fbbcs { get; set; }
    }
}
