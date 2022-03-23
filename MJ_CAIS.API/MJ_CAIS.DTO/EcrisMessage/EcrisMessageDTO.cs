namespace MJ_CAIS.DTO.EcrisMessage
{
    public class EcrisMessageDTO : BaseDTO
    {
        public string? RequestMsgId { get; set; }
        public string? FromAuthId { get; set; }
        public string? ToAuthId { get; set; }
        public string? Identifier { get; set; }
        public string? EcrisIdentifier { get; set; }
        public DateTime? MsgTimestamp { get; set; }
        public string? ResponseTypeId { get; set; }
        public string? EcrisMsgStatus { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountry { get; set; }
        public string? BirthCity { get; set; }
        public string? FbbcId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public decimal? Sex { get; set; }
        public string? Nationality1Code { get; set; }
        public string? Nationality2Code { get; set; }
        public string? MsgTypeId { get; set; }
    }
}
