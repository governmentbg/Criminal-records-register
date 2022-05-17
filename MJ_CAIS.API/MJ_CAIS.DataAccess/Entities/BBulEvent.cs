using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBulEvent : BaseEntity
    {
        public string? EventType { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }
        public string? Description { get; set; }
        public string? BulletinId { get; set; }
        public string? StatusCode { get; set; }
        public string? Remarks { get; set; }
        public string? DocId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual DDocument? Doc { get; set; }
        public virtual BEventType? EventTypeNavigation { get; set; }
        public virtual BEventStatus? StatusCodeNavigation { get; set; }
    }
}
