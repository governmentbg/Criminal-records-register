namespace MJ_CAIS.DTO.Common
{
    public class AddressDTO
    {
        public LookupDTO Country { get; set; } = new LookupDTO();
        public string? MunicipalityId { get; set; }
        public string? MunicipalityDisplayName { get; set; }
        public string? DistrictId { get; set; }
        public string? DistrictDisplayName { get; set; }
        public string? CityId { get; set; }
        public string? CityDisplayName { get; set; }
        public string? ForeignCountryAddress { get; set; }
    }
}