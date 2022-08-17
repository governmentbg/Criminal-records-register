namespace MJ_CAIS.DTO.EcrisInbox
{
    public class EcrisInboxDTO : BaseDTO
    {
        public string? Status { get; set; }
        public DateTime? ImportedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? EcrisMsgId { get; set; }
        public bool? HasError { get; set; }
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public string? XmlMessage { get; set; }
        public string? XmlMessageTraits { get; set; }
    }
}
