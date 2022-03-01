namespace MJ_CAIS.DTO.Bulletin
{
    public class BOffenceDTO
    {
        public string Id { get; set; }
        public string? OffenceCatId { get; set; }
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public DateTime? OffStartDate { get; set; }
        public DateTime? OffEndDate { get; set; }
        public string? OffPlaceCountryId { get; set; }
        public string? OffPlaceSubdivId { get; set; }
        public string? OffPlaceCityId { get; set; }
        public string? OffPlaceDescr { get; set; }
        public decimal? Occurrences { get; set; }
        public decimal? IsContiniuous { get; set; }
        public string? OffLvlComplId { get; set; }
        public string? OffLvlPartId { get; set; }
        public decimal? RespExemption { get; set; }
        public decimal? Recidivism { get; set; }
        public string? FormOfGuilt { get; set; }
    }
}
