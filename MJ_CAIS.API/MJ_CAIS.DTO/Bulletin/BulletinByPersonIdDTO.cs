namespace MJ_CAIS.DTO.Bulletin
{
    public class BulletinByPersonIdDTO
    {
        public string Id { get; set; }
        public string BulletinType { get; set; }
        public string RegistrationNumber { get; set; }
        public string StatusId { get; set; }
        public string BulletinAuthorityId { get; set; }
        public string CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string Egn { get; set; }
        public string Lnch { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
