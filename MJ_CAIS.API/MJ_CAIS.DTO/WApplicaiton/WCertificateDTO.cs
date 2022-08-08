namespace MJ_CAIS.DTO.WApplicaiton
{
    public class WCertificateDTO
    {
        public string Id { get; set; } = null!;
        public string RegistrationNumber { get; set; } = null!;
        public string AccessCode1 { get; set; } = null!;
        public string? AccessCode2 { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? WApplId { get; set; }
        public string ACertId { get; set; } = null!;
        public byte[] Content { get; set; } = null!;
        public string MimeType { get; set; } = null!;
        public decimal Bytes { get; set; }
        public string? Md5 { get; set; }
        public string? Sha1 { get; set; }
    }
}