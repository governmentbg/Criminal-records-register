using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GDecidingAuthority : BaseEntity, IBaseNomenclature
    {
        public GDecidingAuthority()
        {
            BBulletinBulletinAuthorities = new HashSet<BBulletin>();
            BBulletinDecidingAuths = new HashSet<BBulletin>();
            BDecisions = new HashSet<BDecision>();
            GCsAuthorities = new HashSet<GCsAuthority>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public decimal? EisppId { get; set; }
        public string? EisppCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal? ActiveForBulletins { get; set; }
        public decimal? OrderNumber { get; set; }
        public string? ParentId { get; set; }
        public decimal? Visible { get; set; }
        public decimal? IsGroup { get; set; }
        public string? DisplayName { get; set; }
        public string? OldId { get; set; }

        public virtual ICollection<BBulletin> BBulletinBulletinAuthorities { get; set; }
        public virtual ICollection<BBulletin> BBulletinDecidingAuths { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
        public virtual ICollection<GCsAuthority> GCsAuthorities { get; set; }
    }
}
