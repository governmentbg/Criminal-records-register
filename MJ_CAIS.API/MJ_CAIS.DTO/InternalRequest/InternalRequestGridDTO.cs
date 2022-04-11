namespace MJ_CAIS.DTO.InternalRequest
{
    public class InternalRequestGridDTO : BaseDTO
    {
        public string? RegNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? ReqStatus { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? Description { get; set; }
        public string? BulletinNumber { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public string? BulletinId { get; set; }
    }
}
