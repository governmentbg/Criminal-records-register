using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;

        public InquiryService(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public IQueryable<SearchBulletinGridDTO> SearchBulletinsWithPagination(ODataQueryOptions<SearchBulletinGridDTO> aQueryOptions, bool isPageInit = false)
        {
            throw new NotImplementedException();
        }
    }
}
