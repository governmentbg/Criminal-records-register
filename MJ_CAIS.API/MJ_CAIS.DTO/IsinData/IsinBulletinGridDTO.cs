namespace MJ_CAIS.DTO.IsinData
{
    public class IsinBulletinGridDTO : BaseGridDTO
    {
        public string? BulletinType { get; set; }
        public string? RegistrationNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Identifier { get; set; }
        public IEnumerable<string> Nationalities { get; set; }
        public string? BirthPlace { get; set; }
        public string? PersonName { get; set; }
        public string? DecisionType { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string CaseNumber { get; set; }
        public string? DecisionAuthName { get; set; }
    }
}
