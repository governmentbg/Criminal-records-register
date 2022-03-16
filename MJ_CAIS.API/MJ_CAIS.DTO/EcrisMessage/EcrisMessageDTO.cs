namespace MJ_CAIS.DTO.EcrisMessage
{
    public class EcrisMessageDTO : BaseDTO
    {
        public string Id { get; set; } = null!;
        public string? RequestMsgId { get; set; }
        public string? FromAuthId { get; set; }
        public string? ToAuthId { get; set; }
        public string? Identifier { get; set; }
        public string? EcrisIdentifier { get; set; }
        public DateTime? MsgTimestamp { get; set; }
        public string? MsgTypeId { get; set; }
        public string? ResponseTypeId { get; set; }
        public string? EcrisMsgStatus { get; set; }
        public string? PersonNames { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountry { get; set; }
        public string? BirthCity { get; set; }
        public string? FbbcId { get; set; }
    }
}
