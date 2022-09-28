namespace MJ_CAIS.DataAccess
{
    public interface IBulletinOrder
    {
        DateTime? DecisionFinalDate { get; set; }
        DateTime? DecisionDate { get; set; }
        public decimal? CaseYear { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? Id { get; set; }
    }
}
