namespace MJ_CAIS.DTO.InternalRequest
{
    public class InternalRequestForJudgeGridDTO : BaseGridDTO
    {
        public string? RegNumber { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? ReqStatusName { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? FromAuthorityName { get; set; }
    }
}
