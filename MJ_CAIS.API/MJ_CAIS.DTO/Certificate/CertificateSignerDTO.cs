namespace MJ_CAIS.DTO.Certificate
{
    public class CertificateSignerDTO : BaseDTO
    {
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
