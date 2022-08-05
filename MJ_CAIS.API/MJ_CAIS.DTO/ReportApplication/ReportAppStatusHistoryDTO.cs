namespace MJ_CAIS.DTO.ReportApplication
{
    public class ReportAppStatusHistoryDTO : BaseDTO
    {
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Descr { get; set; }
        public string AReportApplId { get; set; }
        public string AReportId { get; set; }
    }
}
