namespace MJ_CAIS.DTO.Shared
{
    public class BulletinPersonInfoModelDTO
    {
        public string? PersonId { get; set; }
        public string? BulletinId { get; set; }
        public DateTime? BulletinReceivedDate { get; set; }
        public string CsAuthorityName { get; set; }
        public string BulletinType { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public string FirstnameLat { get; set; }
        public string SurnameLat { get; set; }
        public string FamilynameLat { get; set; }
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Egn { get; set; }
        public string Lnch { get; set; }
        public string Ln { get; set; }
        public string RegistrationNumber { get; set; }
        public string DecidingAuthName { get; set; }
        public string DecisionNumber { get; set; }
        public DateTime? DecisionDate { get; set; }
        public string CaseNumber { get; set; }
        public decimal? CaseYear { get; set; }
        public string MotherFirstname { get; set; }
        public string MotherSurname { get; set; }
        public string MotherFamilyname { get; set; }
        public string MotherFullname { get; set; }
        public string FatherFirstname { get; set; }
        public string FatherSurname { get; set; }
        public string FatherFamilyname { get; set; }
        public string FatherFullname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public IEnumerable<string> Nationalities { get; set; }
        public string ForeignCountryAddress { get; set; }
        public string MunicipalityName { get; set; }
        public string Districtname { get; set; }
        public IEnumerable<PersonAliasDTO> PersonAliases { get; set; } = new List<PersonAliasDTO>();
    }
}