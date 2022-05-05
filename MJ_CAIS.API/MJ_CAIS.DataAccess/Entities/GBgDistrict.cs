using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GBgDistrict : BaseEntity, IBaseNomenclature
    {
        public GBgDistrict()
        {
            GBgMunicipalities = new HashSet<GBgMunicipality>();
        }

        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? Code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? EkatteCode { get; set; }
        public string? EcrisTechnId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public decimal? Version { get; set; }

        public virtual ICollection<GBgMunicipality> GBgMunicipalities { get; set; }
    }
}
