namespace MJ_CAIS.DTO.Person
{
    public class PersonGridDTO : BaseGridDTO
    {
        public string? Pid { get; set; }
        public string? PidType { get; set; }
        public string? PidTypeName { get; set; }
        public string? FirstName { get; set; }
        public string? SurName { get; set; }
        public string? FamilyName { get; set; }
        public string? FullName { get; set; }
        public string? Sex { get; set; }
        public string? BirthDateDisplay { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthDatePrec { get; set; }
        public string? CountryId { get; set; }
        public string? MunicipalityId { get; set; }
        public string? DistrictId { get; set; }
        public string? CityId { get; set; }
        public string? ForeignCountryAddress { get; set; }
        public int? TotalCount { get; set; }
    }
}
