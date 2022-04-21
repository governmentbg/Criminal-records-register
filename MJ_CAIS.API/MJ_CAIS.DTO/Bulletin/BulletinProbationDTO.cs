namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinProbationDTO
    {
        public string? Id { get; set; }
        public string? SanctionId { get; set; }
        public string? SanctProbCategId { get; set; }
        public string? SanctProbMeasureId { get; set; }
        public decimal? SanctProbValue { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }
    }
}