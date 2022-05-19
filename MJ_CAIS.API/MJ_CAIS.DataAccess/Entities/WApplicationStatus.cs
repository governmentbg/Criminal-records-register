using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WApplicationStatus
    {
        public WApplicationStatus()
        {
            WApplications = new HashSet<WApplication>();
            WStatusHes = new HashSet<WStatusH>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<WApplication> WApplications { get; set; }
        public virtual ICollection<WStatusH> WStatusHes { get; set; }
    }
}
