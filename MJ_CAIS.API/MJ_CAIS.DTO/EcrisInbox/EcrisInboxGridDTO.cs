namespace MJ_CAIS.DTO.EcrisInbox
{
    public class EcrisInboxGridDTO : BaseGridDTO
    {
        public string? Status { get; set; }
        public string? StatusName { get; set; }
        public DateTime ImportedOn { get; set; }
        public string? EcrisMsgId { get; set; }
        public bool? HasError { get; set; }
    }
}
