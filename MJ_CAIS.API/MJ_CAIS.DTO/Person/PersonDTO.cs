using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.Person
{
    public class PersonDTO : BaseDTO
    {
        public string? ContextType { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public string? FullnameLat { get; set; }
        public List<TransactionDTO<PersonAliasDTO>>? PersonAliasTransactions { get; set; } = new List<TransactionDTO<PersonAliasDTO>>();
        public decimal? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? Ln { get; set; }
        public AddressDTO BirthPlace { get; set; } = new AddressDTO();
        public MultipleChooseDTO Nationalities { get; set; } = new MultipleChooseDTO();
        public List<string> NationalitiesNames { get; set; } = new List<string>();
        public string? AfisNumber { get; set; }
        public string? Suid { get; set; }
        public string? IdDocNumber { get; set; }
        public string? IdDocCategoryId { get; set; }
        public string? IdDocTypeDescr { get; set; }
        public string? IdDocIssuingAuthority { get; set; }
        public DateTime? IdDocIssuingDate { get; set; }
        public DateTime? IdDocValidDate { get; set; }
        public string? MotherFirstname { get; set; }
        public string? MotherSurname { get; set; }
        public string? MotherFamilyname { get; set; }
        public string? MotherFullname { get; set; }
        public string? FatherFirstname { get; set; }
        public string? FatherSurname { get; set; }
        public string? FatherFamilyname { get; set; }
        public string? FatherFullname { get; set; }
        public int Bulletin78ACount { get; set; }
        public int ConvictionBulletinCount { get; set; }
        public int BulletinUnspecifiedCount { get; set; }
        public string? TableName { get; set; }
        public string? TableId { get; set; }
        public string? TableDesc { get; set; }
    }
}
