using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonH : BaseEntity, IBaseIdEntity
    {
        public PPersonH()
        {
            PPersonHCitizenships = new HashSet<PPersonHCitizenship>();
            PPersonIdsHes = new HashSet<PPersonIdsH>();
        }

        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }
        public string Id { get; set; } = null!;
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public string? FullnameLat { get; set; }
        public string? Tablename { get; set; }
        public string? TableId { get; set; }
        public string? Descr { get; set; }

        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual ICollection<PPersonHCitizenship> PPersonHCitizenships { get; set; }
        public virtual ICollection<PPersonIdsH> PPersonIdsHes { get; set; }
    }
}
