using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCountry : BaseEntity, IBaseNomenclature
    {
        public GCountry()
        {
            AAppCitizenships = new HashSet<AAppCitizenship>();
            AApplications = new HashSet<AApplication>();
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            BPersNationalities = new HashSet<BPersNationality>();
            EEcrisMsgNationalities = new HashSet<EEcrisMsgNationality>();
            FbbcBirthCountries = new HashSet<Fbbc>();
            FbbcCountries = new HashSet<Fbbc>();
            GCities = new HashSet<GCity>();
            PPeople = new HashSet<PPerson>();
            PPersonHs = new HashSet<PPersonH>();
            PPersonIds = new HashSet<PPersonId>();
            WApplications = new HashSet<WApplication>();
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
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<AAppCitizenship> AAppCitizenships { get; set; }
        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<BPersNationality> BPersNationalities { get; set; }
        public virtual ICollection<EEcrisMsgNationality> EEcrisMsgNationalities { get; set; }
        public virtual ICollection<Fbbc> FbbcBirthCountries { get; set; }
        public virtual ICollection<Fbbc> FbbcCountries { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
        public virtual ICollection<PPerson> PPeople { get; set; }
        public virtual ICollection<PPersonH> PPersonHs { get; set; }
        public virtual ICollection<PPersonId> PPersonIds { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
