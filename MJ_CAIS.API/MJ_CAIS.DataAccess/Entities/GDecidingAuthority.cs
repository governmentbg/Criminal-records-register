using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GDecidingAuthority : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public GDecidingAuthority()
        {
            BBulletinBulletinAuthorities = new HashSet<BBulletin>();
            BBulletinCaseAuths = new HashSet<BBulletin>();
            BBulletinDecidingAuths = new HashSet<BBulletin>();
            BDecisions = new HashSet<BDecision>();
            GCsAuthorities = new HashSet<GCsAuthority>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public decimal? EisppId { get; set; }
        public string? EisppCode { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool? ActiveForBulletins { get; set; }
        public decimal? OrderNumber { get; set; }
        public string? ParentId { get; set; }
        public bool? Visible { get; set; }
        public bool? IsGroup { get; set; }
        public string? DisplayName { get; set; }
        public string? OldId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual ICollection<BBulletin> BBulletinBulletinAuthorities { get; set; }
        public virtual ICollection<BBulletin> BBulletinCaseAuths { get; set; }
        public virtual ICollection<BBulletin> BBulletinDecidingAuths { get; set; }
        public virtual ICollection<BDecision> BDecisions { get; set; }
        public virtual ICollection<GCsAuthority> GCsAuthorities { get; set; }
    }
}
