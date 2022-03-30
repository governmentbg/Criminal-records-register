using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class IsinDataService : BaseAsyncService<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string, CaisDbContext>, IIsinDataService
    {
        private readonly IIsinDataRepository _isinDataRepository;

        public IsinDataService(IMapper mapper, IIsinDataRepository isinDataRepository)
            : base(mapper, isinDataRepository)
        {
            _isinDataRepository = isinDataRepository;
        }

        public virtual async Task<IgPageResult<IsinDataGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string status)
        {
            var entityQuery = this.GetSelectAllQueriable().Where(x => x.Status == status);
            var baseQuery = entityQuery.ProjectTo<IsinDataGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinDataGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
