namespace MJ_CAIS.DataAccess
{
    public interface IInquiryBulletinFilterable : IBaseIdEntity
    {
        public string? RegistrationNumber { get; set; }
        public string? BulletinType { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string? CaseTypeId { get; set; }
        public string? CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string? DecidingAuthId { get; set; }
        public string? DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public DateTime? DecisionFinalDate { get; set; }
        public string? DecisionTypeId { get; set; }
        public string? StatusId { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
