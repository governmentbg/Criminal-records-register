namespace MJ_CAIS.DTO.Person
{
    public class PersonGeneratedReportGridDTO : BaseGridDTO
    {
        public string? RegistrationNumber { get; set; }
        public string? Purpose { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? StatusName { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
