using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ASrvcResRcptMeth : BaseEntity, IBaseIdEntity
    {
        public ASrvcResRcptMeth()
        {
            AApplications = new HashSet<AApplication>();
            WApplications = new HashSet<WApplication>();
        }
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? Descr { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
