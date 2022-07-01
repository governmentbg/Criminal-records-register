namespace MJ_CAIS.DTO.Inquiry
{
    public class InquirySearchBulletinByPersonDTO : PeriodSearchDTO
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlaceCountryId { get; set; }
        public string? BirthPlaceMunicipalityId { get; set; }
        public string? BirthPlaceDistrictId { get; set; }
        public string? BirthPlaceCityId { get; set; }
        public string? BirthPlaceDesc { get; set; }
        public decimal? Sex { get; set; }
        public string? IdDocNumber { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public DateTime? IdDocValidDate { get; set; }
    }
}
