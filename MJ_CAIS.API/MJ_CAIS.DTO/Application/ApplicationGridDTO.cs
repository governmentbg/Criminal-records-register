namespace MJ_CAIS.DTO.Application
{
    public class ApplicationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? Purpose { get; set; }
        //public string? EgnLnch { get; set; }
        public string? Egn { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthPlaceOther { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public string? BirthCityId { get; set; }
        public string? CsAuthorityBirth { get; set; }
    }
}