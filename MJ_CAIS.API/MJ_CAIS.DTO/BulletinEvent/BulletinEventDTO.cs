namespace MJ_CAIS.DTO.BulletinEvent
{
    public class BulletinEventDTO : BaseDTO
    {
        public string? EventType { get; set; }
        public decimal? Version { get; set; }
        public string? Description { get; set; }
        public string? BulletinId { get; set; }
        public string? StatusCode { get; set; }
        public string? Remarks { get; set; }
        public string? DocId { get; set; }
    }
}
