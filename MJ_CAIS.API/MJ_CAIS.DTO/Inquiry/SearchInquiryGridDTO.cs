namespace MJ_CAIS.DTO.Inquiry
{
    public class SearchInquiryGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? StatusCodeDisplayValue { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? ReportApplicationRegistrationNumber { get; set; }
        public string? ApplicantData { get; set; }
        public string PersonIdentificators { get; set; }
        public string? Names { get; set; }
        public string? Purpose { get; set; }
        public string? FirstSigner { get; set; }
        public string? SecondSigner { get; set; }
    }
}
