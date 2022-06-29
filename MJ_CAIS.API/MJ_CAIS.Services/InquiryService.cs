using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;

        public InquiryService(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public IgPageResult<InquiryBulletinGridDTO> SearchBulletinsWithPagination(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, InquirySearchBulletinDTO searchParams)
        {
            throw new NotImplementedException();
        }
    }
}
