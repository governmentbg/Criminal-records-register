using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WApplicationStatus : BaseEntity
    {
        public WApplicationStatus()
        {
            WStatusHes = new HashSet<WStatusH>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<WStatusH> WStatusHes { get; set; }
    }
}
