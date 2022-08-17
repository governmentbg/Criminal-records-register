namespace MJ_CAIS.DTO.Application.Public
{
    public class ApplicationPreviewDTO
    {
        public string? Id { get; set; }

        public string? RegistrationNumber { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? Egn { get; set; }

        public string? Email { get; set; }

        public string? PurposeName { get; set; }

        public string? Purpose { get; set; }

        public string? PaymentMethodName { get; set; }

        public string? PaymentMethodCode { get; set; }

        public bool? IsPaid { get; set; }

        public string? Status { get; set; }

        public string? StatusCode { get; set; }

        public string CertificateStatusCode { get; set; }

        public string? InvoiceNumber { get; set; }

        public string? PayEgovBGCode { get; set; }
    }
}