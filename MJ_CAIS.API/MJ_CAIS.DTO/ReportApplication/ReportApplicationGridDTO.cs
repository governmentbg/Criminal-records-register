namespace MJ_CAIS.DTO.ReportApplication
{
    public class ReportApplicationGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? Purpose { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CsAuthorityBirth { get; set; }
    }
}
