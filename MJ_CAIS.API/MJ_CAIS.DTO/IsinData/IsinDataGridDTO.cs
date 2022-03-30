namespace MJ_CAIS.DTO.IsinData
{
    public class IsinDataGridDTO : BaseDTO
    {
        public DateTime? MsgDateTime { get; set; }
        public string? Identifier { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountryName { get; set; }
        public string Nationalities { get; set; }
        public string PersonName { get; set; }
        public string? DecisionInfo { get; set; }
        public string? CaseInfo { get; set; }
        public DateTime? SanctionEndDate { get; set; }
        public string? BulletinId { get; set; }
    }
}
