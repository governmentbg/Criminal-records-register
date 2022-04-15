namespace MJ_CAIS.DTO.IsinData
{
    public class IsinDataDTO : BaseDTO
    {
        public DateTime? MsgDateTime { get; set; }
        public string? Status { get; set; }
        public string? Identifier { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Sex { get; set; }
        public string? Country1Name { get; set; }
        public string? Country2Name { get; set; }
        public string? BirthCountryName { get; set; }
        public string? BirthPlace { get; set; }
        public string? BulletinId { get; set; }

        #region Акт

        public string? DecisionType { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionAuthName { get; set; }

        #endregion

        #region Дело

        public string? CaseType { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? CaseAuthName { get; set; }

        #endregion

        #region Наказание

        public DateTime? SanctionStartDate { get; set; }
        public DateTime? SanctionEndDate { get; set; }

        #endregion
    }
}
