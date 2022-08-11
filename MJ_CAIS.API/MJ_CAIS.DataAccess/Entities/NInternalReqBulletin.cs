using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class NInternalReqBulletin : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string BulletinId { get; set; } = null!;
        public string InternalReqId { get; set; } = null!;
        public string? Remarks { get; set; }
        public string? BOffenceId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual BOffence? BOffence { get; set; }
        public virtual BBulletin Bulletin { get; set; } = null!;
        public virtual NInternalRequest InternalReq { get; set; } = null!;
    }
}
