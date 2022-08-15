namespace MJ_CAIS.DTO.Certificate
{
    public class CertificateGridDTO : BaseGridDTO
    {
        public string? StatusCode { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? FirstSigner { get; set; }
        public string? SecondSigner { get; set; }
    }
}
