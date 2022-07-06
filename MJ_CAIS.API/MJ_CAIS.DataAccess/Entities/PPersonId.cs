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
            BBulletinEgnNavigations = new HashSet<BBulletin>();
            BBulletinIdDocNumberNavigations = new HashSet<BBulletin>();
            BBulletinLnNavigations = new HashSet<BBulletin>();
            BBulletinLnchNavigations = new HashSet<BBulletin>();
            BBulletinPersonIdCscNavigations = new HashSet<BBulletin>();
            BBulletinSuidNavigations = new HashSet<BBulletin>();
            FbbcPeople = new HashSet<Fbbc>();
            FbbcSuidNavigations = new HashSet<Fbbc>();
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
        public virtual ICollection<BBulletin> BBulletinEgnNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletinIdDocNumberNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletinLnNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletinLnchNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletinPersonIdCscNavigations { get; set; }
        public virtual ICollection<BBulletin> BBulletinSuidNavigations { get; set; }
        public virtual ICollection<Fbbc> FbbcPeople { get; set; }
        public virtual ICollection<Fbbc> FbbcSuidNavigations { get; set; }
    }
}
