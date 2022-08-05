namespace MJ_CAIS.DTO.WApplicationReport
{
    public class WApplicationReportDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
        public string? RegistrationNumber { get; set; }
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? ResultType { get; set; }
        public decimal? ApiServiceCallId { get; set; }
        public string? CServiceUri { get; set; }
        public string? CServiceType { get; set; }
        public string? CEmplId { get; set; }
        public string? CEmpNames { get; set; }
        public string? CEmpAddId { get; set; }
        public string? CEmpPos { get; set; }
        public string? CRespPersId { get; set; }
        public string? CLawReason { get; set; }
        public string? CRemark { get; set; }
        public string? CAdministrationOid { get; set; }
        public string? CAdministrationName { get; set; }
    }
}
