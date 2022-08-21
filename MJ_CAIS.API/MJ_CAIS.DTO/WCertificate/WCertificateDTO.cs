namespace MJ_CAIS.DTO.WCertificate
{
    public class WCertificateDTO : BaseDTO
    {
        public string RegistrationNumber { get; set; } = null!;
        public string AccessCode1 { get; set; } = null!;
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
