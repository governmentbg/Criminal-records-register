namespace MJ_CAIS.DTO.Person
{
    public class PersonFbbcGridDTO : BaseDTO
    {
        public DateTime? ReceiveDate { get; set; }
        public string? Egn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public DateTime? DestroyedDate { get; set; }
    }
}