using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class VOffence : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? OffenceCatId { get; set; }
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public string? LegalProvisions { get; set; }
        public DateTime? OffStartDate { get; set; }
        public string? OffStartDatePrec { get; set; }
        public DateTime? OffEndDate { get; set; }
        public string? OffEndDatePrec { get; set; }
        public string? OffPlaceCountryId { get; set; }
        public string? OffPlaceCityId { get; set; }
        public string? OffPlaceDescr { get; set; }
        public string? BulletinId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? FormOfGuiltId { get; set; }
    }
}
