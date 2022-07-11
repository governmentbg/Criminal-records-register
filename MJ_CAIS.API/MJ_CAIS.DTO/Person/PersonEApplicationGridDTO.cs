namespace MJ_CAIS.DTO.Person
{
    public class PersonEApplicationGridDTO : BaseGridDTO
    {
        public string? Type { get; set; }
        public string? CertificateStatus { get; set; }
        public string? CertifivateRegistrationNumber { get; set; }
        public DateTime? CertifivateValidDate { get; set; }
        public string? ExtAdministration { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? FullName { get; set; }
        public DateTime? BithDate { get; set; }
    }
}
