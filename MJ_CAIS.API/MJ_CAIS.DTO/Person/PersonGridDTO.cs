namespace MJ_CAIS.DTO.Person
{
    public class PersonGridDTO : BaseDTO
    {
        public string Identifier { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string FamilyName { get; set; }
        public string FullName { get; set; }
        public string BirthDateDisplay { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthDatePrec { get; set; }
        public int TotalCount { get; set; }
    }
}
