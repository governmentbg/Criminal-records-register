using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class GBgMunicipality : BaseEntity, IBaseIdEntity, IBaseNomenclature
    {
        public GBgMunicipality()
        {
            AApplicants = new HashSet<AApplicant>();
            AReportApplications = new HashSet<AReportApplication>();
            GCities = new HashSet<GCity>();
        }

        public string Id { get; set; } = null!;
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? EkatteCode { get; set; }
        public string? DistrictId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GBgDistrict? District { get; set; }
        public virtual ICollection<AApplicant> AApplicants { get; set; }
        public virtual ICollection<AReportApplication> AReportApplications { get; set; }
        public virtual ICollection<GCity> GCities { get; set; }
    }
}
