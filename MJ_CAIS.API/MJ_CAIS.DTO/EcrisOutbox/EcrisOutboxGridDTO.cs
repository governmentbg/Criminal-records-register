namespace MJ_CAIS.DTO.EcrisOutbox
{
    public class EcrisOutboxGridDTO : BaseGridDTO
    {
        public string? Status { get; set; } 
        public string? StatusName { get; set; }
        public string Operation { get; set; } = null!;
        public DateTime? ExecutionDate { get; set; }
        public bool? HasError { get; set; }
        public decimal? Attempts { get; set; }
        public string? EcrisMsgId { get; set; }
        public string? Error { get; set; }
    }
}
