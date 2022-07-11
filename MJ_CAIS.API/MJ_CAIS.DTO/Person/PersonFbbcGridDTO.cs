namespace MJ_CAIS.DTO.Person
{
    public class PersonFbbcGridDTO : BaseGridDTO
    {
        public string DocType { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string? Country { get; set; }
        public string? Egn { get; set; }
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}