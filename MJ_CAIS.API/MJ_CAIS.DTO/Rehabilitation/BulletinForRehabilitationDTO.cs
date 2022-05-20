namespace MJ_CAIS.DTO.Rehabilitation
{
    public class BulletinForRehabilitationDTO
    {
        public string? Id { get; set; }

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

        public IEnumerable<SanctionForRehabilitationDTO> Sanctions { get; set; }

        public IEnumerable<DecisionForRehabilitationDTO> Decisions { get; set; }

        public IEnumerable<DateTime?> OffencesEndDates { get; set; }
    }

    public class SanctionForRehabilitationDTO
    {
        public string? Type { get; set; }

        public Duration SuspentionDuration { get; set; } = new Duration();

        public IEnumerable<SanctionForRehabilitationDTO> Sanctions { get; set; }

        public IEnumerable<Duration> PropbationDurations { get; set; } = new List<Duration>();    
    }

    public class Duration
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
