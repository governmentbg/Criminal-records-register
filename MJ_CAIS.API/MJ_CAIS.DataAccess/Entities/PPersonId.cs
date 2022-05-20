using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonId : BaseEntity, IBaseIdEntity
    {
        public PPersonId()
        {
            AApplicationEgnNavigations = new HashSet<AApplication>();
            AApplicationLnNavigations = new HashSet<AApplication>();
            AApplicationLnchNavigations = new HashSet<AApplication>();
            AApplicationSuidNavigations = new HashSet<AApplication>();
            FbbcPeople = new HashSet<Fbbc>();
            FbbcSuidNavigations = new HashSet<Fbbc>();
            PAppIds = new HashSet<PAppId>();
            PBulletinIds = new HashSet<PBulletinId>();
        }

        public string Id { get; set; } = null!;
        public string? PidTypeId { get; set; }
        public string? Pid { get; set; }
        public string? Issuer { get; set; }
        public string? CountryId { get; set; }
        public string? PersonId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual PPerson? Person { get; set; }
        public virtual PPersonIdType? PidType { get; set; }
        public virtual ICollection<AApplication> AApplicationEgnNavigations { get; set; }
        public virtual ICollection<AApplication> AApplicationLnNavigations { get; set; }
        public virtual ICollection<AApplication> AApplicationLnchNavigations { get; set; }
        public virtual ICollection<AApplication> AApplicationSuidNavigations { get; set; }
        public virtual ICollection<Fbbc> FbbcPeople { get; set; }
        public virtual ICollection<Fbbc> FbbcSuidNavigations { get; set; }
        public virtual ICollection<PAppId> PAppIds { get; set; }
        public virtual ICollection<PBulletinId> PBulletinIds { get; set; }
    }
}
