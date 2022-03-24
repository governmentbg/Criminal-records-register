using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.DTO.InternalRequest
{
    public class BulletinPersonInfoModelDTO
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Familyname { get; set; }
        public string FirstnameLat { get; set; }
        public string SurnameLat { get; set; }
        public string FamilynameLat { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public string Egn { get; set; }
        public string Lnch { get; set; }
        public string Ln { get; set; }
        public string RegistrationNumber { get; set; }
        public string DecisionTypeName { get; set; }
        public string DecidingAuthName { get; set; }
        public string DecisionNumber { get; set; }
        public DateTime DecisionDate { get; set; }
        public string CaseNumber { get; set; }
        public int CaseYear { get; set; }
        public string MotherFullname { get; set; }
        public string FatherFullname { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string[] Nationalities { get; set; }
        public string ForeignCountryAddress { get; set; }
        public string MunicipalityName { get; set; }
        public string Districtname { get; set; }
        public List<PersonAliasDTO> PersonAliases { get; set; } = new List<PersonAliasDTO>();
    }
}
