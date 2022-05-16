using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class WApplication : BaseEntity
    {
        public WApplication()
        {
            WStatusHes = new HashSet<WStatusH>();
            WWebRequests = new HashSet<WWebRequest>();
        }

        public string? ClientIp { get; set; }

        public virtual ICollection<WStatusH> WStatusHes { get; set; }
        public virtual ICollection<WWebRequest> WWebRequests { get; set; }
    }
}
