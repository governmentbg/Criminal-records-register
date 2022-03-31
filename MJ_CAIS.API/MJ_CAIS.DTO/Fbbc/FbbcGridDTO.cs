namespace MJ_CAIS.DTO.Fbbc
{
    public class FbbcGridDTO : BaseDTO
    {
        public DateTime? ReceiveDate { get; set; }
        public string? Egn { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCountryId { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }

    }
}
