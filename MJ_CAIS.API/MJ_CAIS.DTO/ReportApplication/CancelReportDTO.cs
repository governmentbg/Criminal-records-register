namespace MJ_CAIS.DTO.ReportApplication
{
    public class CancelReportDTO
    {
        public string Description { get; set; }
        public string? FirstSignerId { get; set; }
        public string? SecondSignerId { get; set; }
        public string ReportId { get; set; }
    }
}
