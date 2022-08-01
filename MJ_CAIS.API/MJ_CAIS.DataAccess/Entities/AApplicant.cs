using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class AApplicant : BaseEntity, IBaseIdEntity
    {
        public AApplicant()
        {
            AReportApplications = new HashSet<AReportApplication>();
        }

        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PersonForContact { get; set; }
        public string? CityId { get; set; }
        public string? CountryId { get; set; }
        public string? BgMunicipalityId { get; set; }
        public string? BgDistrictId { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual GBgDistrict? BgDistrict { get; set; }
        public virtual GBgMunicipality? BgMunicipality { get; set; }
        public virtual GCity? City { get; set; }
        public virtual GCountry? Country { get; set; }
        public virtual ICollection<AReportApplication> AReportApplications { get; set; }
    }
}
