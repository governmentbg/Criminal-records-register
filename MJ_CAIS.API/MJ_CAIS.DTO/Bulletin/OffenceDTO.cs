using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Bulletin
{
    public class OffenceDTO
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
        public AddressDTO OffPlace { get; set; } = new AddressDTO();
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
