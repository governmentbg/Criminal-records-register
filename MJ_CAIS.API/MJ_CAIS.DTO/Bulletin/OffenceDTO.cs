using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.DTO.Bulletin
{
    public class OffenceDTO : BaseDTO
    {
        public LookupDTO OffenceCategory { get; set; } = new LookupDTO();
        public string? FormOfGuiltId { get; set; }
        public string? Remarks { get; set; }
        public string? EcrisOffCatId { get; set; }
        public string? EcrisOffCatName { get; set; }
        public string? LegalProvisions { get; set; }
        public DatePrecisionDTO OffStartDate { get; set; }
        public DatePrecisionDTO OffEndDate { get; set; }
        public AddressDTO OffPlace { get; set; } = new AddressDTO();
    }
}
