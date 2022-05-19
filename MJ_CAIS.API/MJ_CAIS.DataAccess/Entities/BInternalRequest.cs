using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BInternalRequest : BaseEntity
    {
        public string? RegNumber { get; set; }
        public string? Description { get; set; }
        public string? BulletinId { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? ResponseDescr { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? AAppBulletinId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual BReqStatus? ReqStatusCodeNavigation { get; set; }
    }
}
