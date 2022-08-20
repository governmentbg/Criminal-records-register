using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.ExtAdministration
{
    public class ExtAdministrationInDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? Descr { get; set; }
        public string? Role { get; set; }

        public List<TransactionDTO<ExtAdministrationUicDTO>> ExtAdministrationUics { get; set; }
    }
}
