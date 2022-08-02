namespace MJ_CAIS.DTO.WApplicationReportSearchPer
{
    public class WApplicationReportSearchPerDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
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
        public string? Fullname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? BirthdatePrec { get; set; }
        public string? Birthplace { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
    }
}
