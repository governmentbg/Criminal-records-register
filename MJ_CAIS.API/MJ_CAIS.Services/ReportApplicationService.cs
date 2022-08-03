using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class ReportApplicationService : BaseAsyncService<ReportApplicationDTO, ReportApplicationDTO, ReportApplicationGridDTO, AReportApplication, string, CaisDbContext>, IReportApplicationService
    {
        private readonly IReportApplicationRepository _reportApplicationRepository;

        public ReportApplicationService(IMapper mapper, IReportApplicationRepository reportApplicationRepository)
            : base(mapper, reportApplicationRepository)
        {
            _reportApplicationRepository = reportApplicationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public virtual async Task<IgPageResult<ReportApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ReportApplicationGridDTO> aQueryOptions, string? statusCode)
        {
            var entityQuery = GetSelectAllQueryable().Where(x => x.StatusCode == statusCode);

            var baseQuery = entityQuery.ProjectTo<ReportApplicationGridDTO>(mapperConfiguration);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ReportApplicationGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }
    }
}
