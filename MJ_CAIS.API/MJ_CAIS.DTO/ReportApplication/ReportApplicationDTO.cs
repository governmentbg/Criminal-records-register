namespace MJ_CAIS.DTO.ReportApplication
{
    public class ReportApplicationDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public string? Purpose { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public decimal? Sex { get; set; }
        public string? Egn { get; set; }
        public string? Ln { get; set; }
        public string? Lnch { get; set; }
        public string? ApplicantName { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFullname { get; set; }
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPhone { get; set; }
        public string? AddrEmail { get; set; }
        public string? Description { get; set; }
        public bool? IsLocal { get; set; }
        public string StatusCode { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrecision { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? FullnameLat { get; set; }
        public string? Suid { get; set; }
        public string? AddrCountryId { get; set; }
        public string? AddrCityId { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }
        public string? AddrBgMunicipalityId { get; set; }
        public string? AddrBgDistrictId { get; set; }
        public string CsAuthorityId { get; set; } = null!;
        public string? ApplicantDescr { get; set; }
        public string? EgnId { get; set; }
        public string? LnchId { get; set; }
        public string? LnId { get; set; }
        public string? SuidId { get; set; }
        public string? ApplicantId { get; set; }
        public string? PurposeId { get; set; }
    }
}
