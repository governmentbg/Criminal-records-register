using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BReqStatus
    {
        public BReqStatus()
        {
            BInternalRequests = new HashSet<BInternalRequest>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<BInternalRequest> BInternalRequests { get; set; }
    }
}
