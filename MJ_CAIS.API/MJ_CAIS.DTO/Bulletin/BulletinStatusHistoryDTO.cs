namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinStatusHistoryDTO
    {
        public string Id { get; set; }
        public string? NewStatus { get; set; }
        public string? OldStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Descr { get; set; }
        public bool? Locked { get; set; }
    }
}
