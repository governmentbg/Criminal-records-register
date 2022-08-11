using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class NIntternalReqType : BaseEntity
    {
        public NIntternalReqType()
        {
            NInternalRequests = new HashSet<NInternalRequest>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<NInternalRequest> NInternalRequests { get; set; }
    }
}
