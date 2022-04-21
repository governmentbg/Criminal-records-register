using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Bulletin
{
    public class SanctionDTO
    {
        public string Id { get; set; } = null!;
        public string? SanctCategoryId { get; set; }
        public string? SanctCategoryName { get; set; }
        public string? EcrisSanctCategId { get; set; }
        public string? EcrisSanctCategName { get; set; }
        public string? Descr { get; set; }
        public byte? DecisionDurationYears { get; set; }
        public byte? DecisionDurationMonths { get; set; }
        public byte? DecisionDurationDays { get; set; }
        public byte? DecisionDurationHours { get; set; }    
        public decimal? FineAmount { get; set; }
        public string? DetenctionDescr { get; set; }
        public byte? SuspentionDurationYears { get; set; }
        public byte? SuspentionDurationMonths { get; set; }
        public byte? SuspentionDurationDays { get; set; }
        public byte? SuspentionDurationHours { get; set; }
        public List<BulletinProbationDTO> Probations { get; set; } = new List<BulletinProbationDTO>();
        public List<TransactionDTO<BulletinProbationDTO>> ProbationsTransactions { get; set; } = new List<TransactionDTO<BulletinProbationDTO>>();
    }
}
