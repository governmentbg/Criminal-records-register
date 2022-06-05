namespace MJ_CAIS.DTO.Certificate
{
    public class CertificateDTO : BaseDTO
    {
        public string? CurrentUserAuthId { get; set; }
        public string? ApplicationId { get; set; }
        public string? StatusCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public string? AccessCode1 { get; set; }
        public string? AccessCode2 { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string[]? SelectedBulletinsIds { get; set; }
    }
}
