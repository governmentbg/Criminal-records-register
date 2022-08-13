using MJ_CAIS.DTO.Common;

namespace MJ_CAIS.DTO.InternalRequest
{
    public class InternalRequestDTO : BaseDTO
    {
        public string? RegNumber { get; set; }
        public string? RegNumberDisplay { get; set; }
        public string? Description { get; set; }
        public string? ReqStatusCode { get; set; }
        public string? ResponseDescr { get; set; }
        public DateTime? RequestDate { get; set; }
        public LookupDTO? PPersIdId { get; set; }
        public string? FromAuthorityId { get; set; }
        public string? ToAuthorityId { get; set; }
        public string NIntReqTypeId { get; set; }
        public List<TransactionDTO<SelectedPersonBulletinGridDTO>> SelectedBulletinsTransactions { get; set; }

    }
}
