using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCsAuthority : BaseEntity, IBaseNomenclature
    {
        public GCsAuthority()
        {
            AApplicationCsAuthorities = new HashSet<AApplication>();
            AApplicationCsAuthorityBirths = new HashSet<AApplication>();
            BBulletins = new HashSet<BBulletin>();
            DCsDocRegisters = new HashSet<DCsDocRegister>();
            GCities = new HashSet<GCity>();
            GUsers = new HashSet<GUser>();
            WApplicationCsAuthorities = new HashSet<WApplication>();
            WApplicationCsAuthorityBirths = new HashSet<WApplication>();
        }

        public string? Name { get; set; }
        public string? DecidingAuthId { get; set; }
        public bool? IsCentral { get; set; }
        public string? OldId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual GDecidingAuthority? DecidingAuth { get; set; }
        public virtual ICollection<AApplication> AApplicationCsAuthorities { get; set; }
        public virtual ICollection<AApplication> AApplicationCsAuthorityBirths { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<DCsDocRegister> DCsDocRegisters { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
        public virtual ICollection<GUser> GUsers { get; set; }
        public virtual ICollection<WApplication> WApplicationCsAuthorities { get; set; }
        public virtual ICollection<WApplication> WApplicationCsAuthorityBirths { get; set; }
    }
}
