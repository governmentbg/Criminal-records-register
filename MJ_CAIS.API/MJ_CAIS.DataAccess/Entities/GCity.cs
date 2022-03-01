using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCity
    {
        public GCity()
        {
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            Fbbcs = new HashSet<Fbbc>();
            PPeople = new HashSet<PPerson>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? CountryId { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<Fbbc> Fbbcs { get; set; }
        public virtual ICollection<PPerson> PPeople { get; set; }
    }
}
