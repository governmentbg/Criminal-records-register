namespace MJ_CAIS.WebPortal.Public.Models.Conviction
{
    public class ConvictionCodeDisplayModel
    {
        public string SearchCode { get; set; }
        public bool IsEmptyResponse { get; set; }

        public string Id { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Identifier { get; set; }
        public string? Firstname { get; set; }
        public string? Surname { get; set; }
        public string? Familyname { get; set; }
        public string? Fullname { get; set; }
        public string? FirstnameLat { get; set; }
        public string? SurnameLat { get; set; }
        public string? FamilynameLat { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BirthCountryId { get; set; }
        public string? BirthCountryName { get; set; }
        public string? BirthCityId { get; set; }
        public string? BirthCityName { get; set; }
        public string? BirthPlaceOther { get; set; }
    }
}
