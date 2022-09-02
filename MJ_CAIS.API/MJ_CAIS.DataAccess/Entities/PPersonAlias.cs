using System;
using System.Collections.Generic;

namespace MJ_CAIS.DataAccess.Entities
{
    public partial class PPersonAlias : BaseEntity, IBaseIdEntity
    {
        public string Id { get; set; } = null!;
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? Descr { get; set; }
        public decimal? Sex { get; set; }
        public string? Egn { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }
        public string? Suid { get; set; }
        public string? EgnId { get; set; }
        public string? LnchId { get; set; }
        public string? LnId { get; set; }
        public string? SuidId { get; set; }
        public string? PersonMigrationId { get; set; }
        public string? PersonIdCsc { get; set; }
        public string? PersonIdCscId { get; set; }
        public string? CountryId1 { get; set; }
        public string? CountryId2 { get; set; }
        public string? CountryId3 { get; set; }
        public string? CountryId4 { get; set; }

        public virtual GCity? BirthCity { get; set; }
        public virtual GCountry? BirthCountry { get; set; }
        public virtual GCountry? CountryId1Navigation { get; set; }
        public virtual GCountry? CountryId2Navigation { get; set; }
        public virtual GCountry? CountryId3Navigation { get; set; }
        public virtual GCountry? CountryId4Navigation { get; set; }
    }
}
