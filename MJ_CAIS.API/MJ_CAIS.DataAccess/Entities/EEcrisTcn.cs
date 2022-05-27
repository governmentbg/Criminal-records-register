using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class EEcrisTcn : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Action { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? BulletinId { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
    }
}
