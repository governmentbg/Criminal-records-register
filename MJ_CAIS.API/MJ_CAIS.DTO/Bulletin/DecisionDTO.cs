namespace MJ_CAIS.DTO.Bulletin
{
    public class DecisionDTO : BaseDTO
    {
        public string? DecisionChTypeId { get; set; }
        public string? DecisionChTypeName { get; set; }
        public string? DecisionEcli { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? ChangeDate { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionAuthId { get; set; }
        public string? DecisionAuthName { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? DecisionTypeName { get; set; }
        public string? Descr { get; set; }
    }
}
