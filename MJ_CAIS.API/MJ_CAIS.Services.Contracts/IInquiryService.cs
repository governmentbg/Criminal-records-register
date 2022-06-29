using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IInquiryService
    {
        IgPageResult<InquiryBulletinGridDTO> SearchBulletinsWithPagination(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, InquirySearchBulletinDTO searchParams);
    }
}
