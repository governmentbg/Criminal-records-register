using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonId : BaseEntity
    {
        public PPersonId()
        {
            PAppIds = new HashSet<PAppId>();
            PBulletinIds = new HashSet<PBulletinId>();
        }

        public string? PidTypeId { get; set; }
        public string? Pid { get; set; }
        public string? Issuer { get; set; }
        public string? CountryId { get; set; }
        public string? PersonId { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual PPersonIdType? PidType { get; set; }
        public virtual ICollection<PAppId> PAppIds { get; set; }
        public virtual ICollection<PBulletinId> PBulletinIds { get; set; }
    }
}
