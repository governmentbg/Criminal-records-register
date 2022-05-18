namespace MJ_CAIS.DTO.BulletinEvent
{
    public class SanctionEventDTO
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }
    }
}