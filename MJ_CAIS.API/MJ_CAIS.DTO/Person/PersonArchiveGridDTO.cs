namespace MJ_CAIS.DTO.Person
{
    public class PersonArchiveGridDTO : BaseGridDTO
    {
        public string? Type { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? CsAuthority { get; set; }
        public string? ApplicantName { get; set; }
        public string? Egn { get; set; }
        public string? Lnch { get; set; }
        public string? FullName { get; set; }
        public DateTime? BithDate { get; set; }
    }
}
