namespace MJ_CAIS.DTO.Person
{
    public class PersonSearchParamsDTO
    {
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
    }
}
