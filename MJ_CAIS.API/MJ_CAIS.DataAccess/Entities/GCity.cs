using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GCity : BaseEntity, IBaseNomenclature
    {
        public GCity()
        {
            BBulletins = new HashSet<BBulletin>();
            BOffences = new HashSet<BOffence>();
            Fbbcs = new HashSet<Fbbc>();
            PPeople = new HashSet<PPerson>();
        }

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

        public virtual GCountry? Country { get; set; }
        public virtual GCsAuthority? CsAuthority { get; set; }
        public virtual GBgMunicipality? Municipality { get; set; }
        public virtual ICollection<BBulletin> BBulletins { get; set; }
        public virtual ICollection<BOffence> BOffences { get; set; }
        public virtual ICollection<Fbbc> Fbbcs { get; set; }
        public virtual ICollection<PPerson> PPeople { get; set; }
    }
}
