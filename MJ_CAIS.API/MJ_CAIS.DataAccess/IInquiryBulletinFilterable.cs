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
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public string Egn { get; set; }
        public string Lnch { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthCountryId { get; set; }
        public string BirthCityId { get; set; }
        public string BirthPlaceOther { get; set; }
        public decimal Sex { get; set; }
        public string IdDocNumber { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public DateTime? IdDocValidDate { get; set; }
        public bool? EuCitizen { get; set; }
        public bool? TcnCitizen { get; set; }
        public string? CsAuthorityId { get; set; }
    }
}
