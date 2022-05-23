namespace MJ_CAIS.DTO.InternalRequest
{
    public class InternalRequestDTO : BaseDTO
    {
        public string? RegNumber { get; set; }
        public string? Description { get; set; }
        public string? BulletinId { get; set; }
        public decimal? BulletinVersion { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? ReqStatusName { get; set; }
        public string? ResponseDescr { get; set; }
        public DateTime? RequestDate { get; set; }
    }
}
