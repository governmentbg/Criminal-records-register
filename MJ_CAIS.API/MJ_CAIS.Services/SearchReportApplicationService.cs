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
    public class SearchReportApplicationService : BaseAsyncService<BaseDTO, BaseDTO, BaseGridDTO, AReport, string, CaisDbContext>, ISearchReportApplicationService
    {
        private readonly ISearchReportApplicationRepository _searchReportApplicationRepository;

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public SearchReportApplicationService(ISearchReportApplicationRepository searchReportApplicationRepository, IMapper mapper)
        : base(mapper, searchReportApplicationRepository)
        {
            _searchReportApplicationRepository = searchReportApplicationRepository;
        }

        public virtual async Task<IgPageResult<SearchReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<SearchReportApplicationGridDTO> aQueryOptions)
        {
            var baseQuery = await _searchReportApplicationRepository.SelectAllAsync();
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<SearchReportApplicationGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }
    }
}
