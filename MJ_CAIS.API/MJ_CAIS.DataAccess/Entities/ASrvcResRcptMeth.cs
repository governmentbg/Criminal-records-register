using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ASrvcResRcptMeth : BaseEntity
    {
        public ASrvcResRcptMeth()
        {
            AApplications = new HashSet<AApplication>();
        }

        public string? Name { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        public virtual ICollection<AApplication> AApplications { get; set; }
    }
}
