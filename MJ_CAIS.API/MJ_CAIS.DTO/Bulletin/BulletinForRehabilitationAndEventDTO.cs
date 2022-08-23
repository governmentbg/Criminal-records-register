namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinForRehabilitationAndEventDTO
    {
        public string? Id { get; set; }

        public string? StatusId { get; set; }

        public string? BulletinType { get; set; }

        /// <summary>
        /// Дата на издаване на акта (осъждане)
        /// </summary>
        public DateTime? DecisionDate { get; set; }

        /// <summary>
        /// Дата на влизане в сила на акта (осъждане)
        /// </summary>
        public DateTime? DecisionFinalDate { get; set; }

        public DateTime? RehabilitationDate { get; set; }

        public string? Status { get; set; }

        public decimal? Version { get; set; }

        public string? CaseType { get; set; }

        public DateTime? BirthDate { get; set; }

        public IEnumerable<SanctionForRehabilitationDTO> Sanctions { get; set; }

        public IEnumerable<DecisionForRehabilitationDTO> Decisions { get; set; }

        public IEnumerable<DateTime?> OffencesEndDates { get; set; }
    }

    public class SanctionForRehabilitationDTO
    {
        public string? Type { get; set; }

        public DurationDTO SuspensionDuration { get; set; } = new DurationDTO();

        public IEnumerable<SanctionForRehabilitationDTO> Sanctions { get; set; }

        public IEnumerable<DurationDTO> ProbationDurations { get; set; } = new List<DurationDTO>();
    }

    public class DurationDTO
    {
        public byte? Years { get; set; }
        public byte? Months { get; set; }
        public byte? Days { get; set; }
        public byte? Hours { get; set; }
    }

    public class DecisionForRehabilitationDTO
    {
        public string Type { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}
