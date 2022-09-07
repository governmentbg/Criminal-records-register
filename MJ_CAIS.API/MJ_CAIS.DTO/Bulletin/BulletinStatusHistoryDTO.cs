namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinStatusHistoryDTO : BaseDTO
    {
        public string? NewStatus { get; set; }
        public string? OldStatus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Descr { get; set; }
        public bool? Locked { get; set; }
        public string? BulletinId { get; set; }
        public bool? HasContent { get; set; }
    }
}
