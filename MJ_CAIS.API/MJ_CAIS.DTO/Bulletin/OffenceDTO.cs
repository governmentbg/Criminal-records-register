using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.Bulletin
{
    public class OffenceDTO
    {
        public string Id { get; set; }
        public LookupDTO OffenceCategory { get; set; } = new LookupDTO();
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public string? EcrisOffCatName { get; set; }
        public string? LegalProvisions { get; set; }
        public DateTime? OffStartDate { get; set; }
        public DateTime? OffEndDate { get; set; }
        public AddressDTO OffPlace { get; set; } = new AddressDTO();
        public decimal? Occurrences { get; set; }
        public bool? IsContiniuous { get; set; }
        public bool? RespExemption { get; set; }
        public bool? Recidivism { get; set; }
        public string? FormOfGuilt { get; set; }
    }
}
