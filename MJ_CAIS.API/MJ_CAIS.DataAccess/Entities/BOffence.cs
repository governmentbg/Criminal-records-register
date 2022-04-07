using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class BOffence : BaseEntity
    {
        public string? OffenceCatId { get; set; }
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public string? LegalProvisions { get; set; }
        public DateTime? OffStartDate { get; set; }
        public string? OffStartDatePrec { get; set; }
        public DateTime? OffEndDate { get; set; }
        public string? OffEndDatePrec { get; set; }
        public string? OffPlaceCountryId { get; set; }
        public string? OffPlaceSubdivId { get; set; }
        public string? OffPlaceCityId { get; set; }
        public string? OffPlaceDescr { get; set; }
        public decimal? Occurrences { get; set; }
        public bool? IsContiniuous { get; set; }
        public string? OffLvlComplId { get; set; }
        public string? OffLvlPartId { get; set; }
        public bool? RespExemption { get; set; }
        public bool? Recidivism { get; set; }
        public string? BulletinId { get; set; }
        public string? FormOfGuilt { get; set; }

        public virtual BBulletin? Bulletin { get; set; }
        public virtual BEcrisOffCategory? EcrisOffCat { get; set; }
        public virtual BOffenceLvlCompletion? OffLvlCompl { get; set; }
        public virtual BEcrisOffLvlPart? OffLvlPart { get; set; }
        public virtual GCity? OffPlaceCity { get; set; }
        public virtual GCountry? OffPlaceCountry { get; set; }
        public virtual GCountrySubdivision? OffPlaceSubdiv { get; set; }
        public virtual BOffenceCategory? OffenceCat { get; set; }
    }
}
