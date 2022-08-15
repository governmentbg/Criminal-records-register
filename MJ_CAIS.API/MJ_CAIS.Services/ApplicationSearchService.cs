using AutoMapper;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class ApplicationSearchService :
        BaseAsyncService<ApplicationSearchDTO, ApplicationSearchDTO, ApplicationSearchGridDTO, AApplication, string, CaisDbContext>,
        IApplicationSearchService
    {
        private readonly IApplicationSearchRepository _applicationSearchRepository;
        private readonly IUserContext _userContext;

        public ApplicationSearchService(IMapper mapper,
            IApplicationSearchRepository applicationSearchRepository,
            IUserContext userContext)
            : base(mapper, applicationSearchRepository)
        {
            _applicationSearchRepository = applicationSearchRepository;
            _userContext = userContext;
        }
        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }

        public virtual async Task<IgPageResult<ApplicationSearchGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationSearchGridDTO> aQueryOptions)
        {
            var baseQuery = await _applicationSearchRepository.SelectAllAsync();
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<ApplicationSearchGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }
    }
}
