using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class ARepBulletin : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? BulletinId { get; set; }
        public string? ReportId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? OrderNumber { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual AReport? Report { get; set; }
    }
}
