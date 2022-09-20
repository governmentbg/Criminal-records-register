namespace MJ_CAIS.DTO.Person
{
    public class PersonGridDTO : BaseGridDTO
    {
        public string? Pids { get; set; }
        public string? PersonNames { get; set; }
        public string? MotherNames { get; set; }
        public string? FatherNames { get; set; }
        public string? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BgCitizen { get; set; }
        public string? NonBGCitizen { get; set; }
        public string? IsConvicted { get; set; }
        public string? MatchText { get; set; }
    }
}
