using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicaiton;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.ApplicationConstants;

namespace MJ_CAIS.Services
{
    public class WApplicaitonService : BaseAsyncService<WApplicaitonDTO, WApplicaitonDTO, WApplicaitonGridDTO, WApplication, string, CaisDbContext>, IWApplicaitonService
    {
        private readonly IWApplicaitonRepository _wApplicaitonRepository;

        public WApplicaitonService(IMapper mapper, IWApplicaitonRepository wApplicaitonRepository)
            : base(mapper, wApplicaitonRepository)
        {
            _wApplicaitonRepository = wApplicaitonRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public async Task<IgPageResult<WApplicaitonGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<WApplicaitonGridDTO> aQueryOptions, string? statusId)
        {
            var entityQuery = GetSelectAllQueriable();
            entityQuery = entityQuery.Where(x => x.StatusCode == statusId);

            var baseQuery = entityQuery.ProjectTo<WApplicaitonGridDTO>(mapperConfiguration);
            var resultQuery = await ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<WApplicaitonGridDTO>();
            PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }
    }
}
