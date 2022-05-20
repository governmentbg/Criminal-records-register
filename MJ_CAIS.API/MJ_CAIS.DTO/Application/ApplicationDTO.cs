using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Application
{
    public class ApplicationDTO : BaseDTO
    {
        public string? RegistrationNumber { get; set; }
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
        public string? PersonId { get; set; }
        public string? ApplicantName { get; set; }
        public string? Address { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFullname { get; set; }
        public string? PurposeCountry { get; set; }
        public string? PurposePosition { get; set; }
        public string? SrvcResRcptMethId { get; set; }
        public string? AddrName { get; set; }
        public string? AddrStr { get; set; }
        public string? AddrDistrict { get; set; }
        public string? AddrTown { get; set; }
        public string? AddrState { get; set; }
        public string? AddrPhone { get; set; }
        public string? AddrEmail { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }
        public string? ApplicationTypeId { get; set; }
        public string? CsAuthorityId { get; set; }
        public bool? IsLocal { get; set; }
        public decimal? Version { get; set; }
        public string? PurposeId { get; set; }
        public string? PaymentMethodId { get; set; }
        public bool? FromCosul { get; set; }
        public string? DocContentId { get; set; }
        public string? StatusCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrecision { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? UserCitizenId { get; set; }
        public string? UserId { get; set; }
        public string? UserExtId { get; set; }
    }
}
