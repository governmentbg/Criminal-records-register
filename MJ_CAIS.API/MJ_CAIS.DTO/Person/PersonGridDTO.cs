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
        public string? EgnMatch { get; set; }
        public string? LnchMatch { get; set; }
        public string? FirstnameMatch { get; set; }
        public string? SurnameMatch { get; set; }
        public string? FamilynameMatch { get; set; }
        public string? FullnameMatch { get; set; }
        public string? BirthdateMatch { get; set; }
        public string? BirthyearMatch { get; set; }
        public string? InitialsMatch { get; set; }
        public string? TwoWordsOfNameMatch { get; set; }
        public string? TwoInitialsOfNameMatch { get; set; }
        public string? BgCitizenMatch { get; set; }
        public string? NonBGCitizenMatch { get; set; }
    }
}
