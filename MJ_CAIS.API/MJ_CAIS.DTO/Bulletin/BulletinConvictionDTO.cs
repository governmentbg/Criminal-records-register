namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinConvictionDTO : BaseDTO
    {


        #region Decision data

        public string? DecisionTypeId { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecidingAuthId { get; set; }
        public string? DecisionEcli { get; set; }

        #endregion

        #region Case data

        public string? CaseTypeId { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? CaseAuthId { get; set; }
        public string? ConvRemarks { get; set; }

        #endregion

    }
}
