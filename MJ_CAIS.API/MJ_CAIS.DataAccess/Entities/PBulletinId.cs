using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PBulletinId : BaseEntity
    {
        public string? PersonId { get; set; }
        public string? BulletinId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual PPersonId? Person { get; set; }
    }
}
