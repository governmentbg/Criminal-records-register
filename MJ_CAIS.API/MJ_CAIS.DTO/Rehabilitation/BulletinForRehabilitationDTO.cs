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

        public string? Status { get; set; }

        public IEnumerable<SanctionForRehabilitationDTO> Sanctions { get; set; }
    }

    public class SanctionForRehabilitationDTO
    {
        public string? Type { get; set; }

        public byte? SuspentionDurationYears { get; set; }

        public byte? SuspentionDurationMonths { get; set; }

        public byte? SuspentionDurationDays { get; set; }
    }

}
