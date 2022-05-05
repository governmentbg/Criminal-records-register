using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BBulletinStatus
    {
        public BBulletinStatus()
        {
            BBulletinStatusHNewStatusCodeNavigations = new HashSet<BBulletinStatusH>();
            BBulletinStatusHOldStatusCodeNavigations = new HashSet<BBulletinStatusH>();
            BBulletins = new HashSet<BBulletin>();
        }

        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<BBulletinStatusH> BBulletinStatusHNewStatusCodeNavigations { get; set; }
        public virtual ICollection<BBulletinStatusH> BBulletinStatusHOldStatusCodeNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
    }
}
