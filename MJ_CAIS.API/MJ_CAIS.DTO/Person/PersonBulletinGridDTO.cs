namespace MJ_CAIS.DTO.Person
{
    public class PersonBulletinGridDTO : BaseDTO
    {
        public string? BulletinType { get; set; }
        public string? StatusName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? AlphabeticalIndex { get; set; }
        public string BulletinAuthorityName { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}