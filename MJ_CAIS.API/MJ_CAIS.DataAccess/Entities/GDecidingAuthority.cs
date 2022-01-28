using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GDecidingAuthority
    {
        public GDecidingAuthority()
        {
            BBulletinBulletinAuthorities = new HashSet<BBulletin>();
            BBulletinDecidingAuths = new HashSet<BBulletin>();
            BDecisions = new HashSet<BDecision>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public decimal? EisppId { get; set; }
        public string? EisppCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? Active { get; set; }

        public virtual ICollection<BBulletin> BBulletinBulletinAuthorities { get; set; }
        public virtual ICollection<BBulletin> BBulletinDecidingAuths { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
    }
}
