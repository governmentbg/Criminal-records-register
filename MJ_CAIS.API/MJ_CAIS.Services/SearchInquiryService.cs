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
    public class SearchInquiryService : BaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, AReport, string, CaisDbContext>, ISearchInquiryService
    {
        private readonly ISearchInquiryRepository _searchInquiryRepository;

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public SearchInquiryService(ISearchInquiryRepository searchInquiryRepository, IMapper mapper)
        : base(mapper, searchInquiryRepository)
        {
            _searchInquiryRepository = searchInquiryRepository;
        }

        public virtual async Task<IgPageResult<SearchInquiryGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<SearchInquiryGridDTO> aQueryOptions)
        {
            var baseQuery = await _searchInquiryRepository.SelectAllAsync();
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<SearchInquiryGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<SearchInquiryDTO> SelectByIdAsync(string aId)
        {
            return await this._searchInquiryRepository.SelectByIdAsync(aId);
        }
    }
}
