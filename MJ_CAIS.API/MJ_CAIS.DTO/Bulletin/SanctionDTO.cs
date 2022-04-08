namespace MJ_CAIS.DTO.Bulletin
{
    public class SanctionDTO
    {
        public string Id { get; set; } = null!;
        public string? SanctCategoryId { get; set; }
        public string? SanctCategoryName { get; set; }
        public string? SanctProbCategId { get; set; }
        public string? SanctProbCategName { get; set; }
        public string? EcrisSanctCategId { get; set; }
        public string? EcrisSanctCategName { get; set; }
        public int? SanctProbValue { get; set; }
        public string? SanctProbMeasureId { get; set; }
        public string? SanctProbMeasureName { get; set; }
        public string? Descr { get; set; }
        public bool? SpecificToMinor { get; set; }
        public DateTime? DecisionStartDate { get; set; }
        public DateTime? DecisionEndDate { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }
        public DateTime? ExecutionStartDate { get; set; }
        public DateTime? ExecutionEndDate { get; set; }
        public byte? ExecutionDurationYears { get; set; }
        public byte? ExecutionDurationMonths { get; set; }
        public byte? ExecutionDurationDays { get; set; }
        public byte? ExecutionDurationHours { get; set; }
        public decimal? FineAmount { get; set; }
        public string? DetenctionDescr { get; set; }
        public byte? SuspentionDurationYears { get; set; }
        public byte? SuspentionDurationMonths { get; set; }
        public byte? SuspentionDurationDays { get; set; }
        public byte? SuspentionDurationHours { get; set; }
        public string? ProbationDescr { get; set; }
        public string? SanctActivityId { get; set; }
        public string? SanctActivityName { get; set; }
        public string? SanctActivityDescr { get; set; }
    }
}
