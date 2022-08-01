using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class NReqStatus
    {
        public NReqStatus()
        {
            NInternalRequests = new HashSet<NInternalRequest>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<NInternalRequest> NInternalRequests { get; set; }
    }
}
