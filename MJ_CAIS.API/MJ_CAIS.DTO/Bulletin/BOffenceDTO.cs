﻿namespace MJ_CAIS.DTO.Bulletin
{
    public class BOffenceDTO
    {
        public string Id { get; set; }
        public string? OffenceCatId { get; set; }
        public string? OffenceCatName { get; set; }
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public string? EcrisOffCatName { get; set; }
        public string? LegalProvisions { get; set; }
        public DateTime? OffStartDate { get; set; }
        public DateTime? OffEndDate { get; set; }
        public string? OffPlaceCountryId { get; set; }
        public string? OffPlaceCountryName { get; set; }
        public string? OffPlaceSubdivId { get; set; }
        public string? OffPlaceSubdivName { get; set; }
        public string? OffPlaceCityId { get; set; }
        public string? OffPlaceCityName { get; set; }
        public string? OffPlaceDescr { get; set; }
        public decimal? Occurrences { get; set; }
        public decimal? IsContiniuous { get; set; }
        public string? OffLvlComplId { get; set; }
        public string? OffLvlComplName { get; set; }
        public string? OffLvlPartId { get; set; }
        public string? OffLvlPartName { get; set; }
        public decimal? RespExemption { get; set; }
        public decimal? Recidivism { get; set; }
        public string? FormOfGuilt { get; set; }
    }
}
