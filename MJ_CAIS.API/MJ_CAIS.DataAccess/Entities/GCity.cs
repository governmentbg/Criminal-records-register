using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCity : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public GCity()
        {
            AApplications = new HashSet<AApplication>();
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            Fbbcs = new HashSet<Fbbc>();
            PPeople = new HashSet<PPerson>();
            PPersonHs = new HashSet<PPersonH>();
            WApplications = new HashSet<WApplication>();
        }

        public string Id { get; set; } = null!;
        public string? EcrisTechnId { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? CountryId { get; set; }
        public string? MunicipalityId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? EkatteCode { get; set; }
        public decimal? OrderNumber { get; set; }
        public string? CodeRel { get; set; }
        public string? CsAuthorityId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GCountry? Country { get; set; }
        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual GBgMunicipality? Municipality { get; set; }
        public virtual ICollection<AApplication> AApplications { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<Fbbc> Fbbcs { get; set; }
        public virtual ICollection<PPerson> PPeople { get; set; }
        public virtual ICollection<PPersonH> PPersonHs { get; set; }
        public virtual ICollection<WApplication> WApplications { get; set; }
    }
}
