namespace MJ_CAIS.DTO.BulletinEvent
{
    public class SanctionEventDTO
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public byte? SuspentionDurationYears { get; set; }
        public byte? SuspentionDurationMonths { get; set; }
        public byte? SuspentionDurationDays { get; set; }
        public byte? SuspentionDurationHours { get; set; }
    }
}