using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCountrySubdivision
    {
        public GCountrySubdivision()
        {
            BOffences = new HashSet<BOffence>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Code { get; set; }
        public string? Type { get; set; }
        public string? MemberState { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? CountryId { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
    }
}
