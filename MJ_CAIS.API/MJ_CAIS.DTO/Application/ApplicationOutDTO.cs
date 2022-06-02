using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.DTO.Application
{
    public class ApplicationOutDTO : BaseDTO
    {
        public PersonDTO Person { get; set; } = new PersonDTO();
        public string? RegistrationNumber { get; set; }
        public string? Purpose { get; set; }

        public string? PersonId { get; set; }
        public string? ApplicantName { get; set; }
        public string? Address { get; set; }

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
