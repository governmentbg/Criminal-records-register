namespace MJ_CAIS.DTO.IsinData
{
    public class IsinDataGridDTO : BaseGridDTO
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
        public DateTime? SanctionStartDate { get; set; }
        public string? BulletinId { get; set; }
        public string? Status { get; set; }
        public string? SourceType { get; set; }
        public string? SanctionType { get; set; }
        public string? CsAuthorityId { get; set; }
    }
}
