using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GBgMunicipality : BaseEntity
    {
        public GBgMunicipality()
        {
            GCities = new HashSet<GCity>();
        }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? EkatteCode { get; set; }
        public string? DistrictId { get; set; }

        public virtual GBgDistrict? District { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
    }
}
