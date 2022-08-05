namespace MJ_CAIS.DTO.ReportApplication
{
    public class GeneratedReportDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? FirstSigner { get; set; }
        public string? SecondSigner { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? DocId { get; set; }
        public string? StatusCode { get; set; }
        public string? StatusName { get; set; }
    }
}
