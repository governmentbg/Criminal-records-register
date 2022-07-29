using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBulletinStatusH : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? BulletinId { get; set; }
        public string? NewStatusCode { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Descr { get; set; }
        public bool? Locked { get; set; }
        public string? OldStatusCode { get; set; }
        public string? Content { get; set; }
        public string? ContentVersion { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual BBulletinStatus? NewStatusCodeNavigation { get; set; }
        public virtual BBulletinStatus? OldStatusCodeNavigation { get; set; }
    }
}
