using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCountry : BaseEntity, IBaseNomenclature
    {
        public GCountry()
        {
            AAppCitizenships = new HashSet<AAppCitizenship>();
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            BPersNationalities = new HashSet<BPersNationality>();
            FbbcBirthCountries = new HashSet<Fbbc>();
            FbbcCountries = new HashSet<Fbbc>();
            GCities = new HashSet<GCity>();
            GCountrySubdivisions = new HashSet<GCountrySubdivision>();
            PPeople = new HashSet<PPerson>();
        }

        public string? EcrisTechnId { get; set; }
        public string? Iso31662Number { get; set; }
        public string? Iso31662Code { get; set; }
        public bool? UsedForNationality { get; set; }
        public string? Remark { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? Iso3166Alpha2 { get; set; }

        public virtual ICollection<AAppCitizenship> AAppCitizenships { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<BPersNationality> BPersNationalities { get; set; }
        public virtual ICollection<Fbbc> FbbcBirthCountries { get; set; }
        public virtual ICollection<Fbbc> FbbcCountries { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
        public virtual ICollection<GCountrySubdivision> GCountrySubdivisions { get; set; }
        public virtual ICollection<PPerson> PPeople { get; set; }
    }
}
