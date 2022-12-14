namespace MJ_CAIS.DTO.EcrisMessage
{
    public class EcrisMessageGridDTO : BaseGridDTO
    {
        public string? DocTypeId { get; set; }
        public string? DocTypeName { get; set; }
        public string? Identifier { get; set; }
        public string? EcrisIdentifier { get; set; }
        public DateTime? MsgTimestamp { get; set; }
        public string? EcrisMsgStatus { get; set; }
        public string? EcrisMsgStatusName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountry { get; set; }
        public string? BirthCountryName { get; set; }
        public string? BirthCity { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Nationality1Code { get; set; }
        public string? Nationality1Name { get; set; }
        public string? Nationality2Code { get; set; }
        public string? Nationality2Name { get; set; }
        public string? MsgType { get; set; }
    }
}
