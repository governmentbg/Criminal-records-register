using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.Services.Contracts
{
    public interface IInquiryService
    {
        IQueryable<SearchBulletinGridDTO> SearchBulletinsWithPagination(ODataQueryOptions<SearchBulletinGridDTO> aQueryOptions, bool isPageInit = false);
    }
}
