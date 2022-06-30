using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO;
using MJ_CAIS.DTO.Inquiry;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class InquiryService : BaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, VBulletin, string, CaisDbContext>, IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;
        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public InquiryService(IInquiryRepository inquiryRepository, IMapper mapper)
        : base(mapper, inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        public async Task<IgPageResult<InquiryBulletinGridDTO>> SearchBulletinsWithPaginationAsync(ODataQueryOptions<InquiryBulletinGridDTO> aQueryOptions, InquirySearchBulletinDTO searchParams)
        {
            var baseQuery = _inquiryRepository.FilterBulletins(searchParams);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InquiryBulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }   
    }
}
