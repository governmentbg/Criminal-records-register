using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class InternalRequestService : BaseAsyncService<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, BInternalRequest, string, CaisDbContext>, IInternalRequestService
    {
        private readonly IInternalRequestRepository _internalRequestRepository;

        public InternalRequestService(IMapper mapper, IInternalRequestRepository internalRequestRepository)
            : base(mapper, internalRequestRepository)
        {
            _internalRequestRepository = internalRequestRepository;
        }

        public virtual async Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string? bulletinId)
        {
            var entityQuery = this.GetSelectAllQueriable();

            if (!string.IsNullOrEmpty(bulletinId))
            {
                entityQuery = entityQuery.Where(x => x.BulletinId == bulletinId);
            }

            var baseQuery = entityQuery.ProjectTo<InternalRequestGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InternalRequestGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public override Task<string> InsertAsync(InternalRequestDTO aInDto)
        {
            aInDto.ReqStatusCode = InternalRequestStatusTypeConstants.New;
            return base.InsertAsync(aInDto);
        }

        public override async Task UpdateAsync(string aId, InternalRequestDTO aInDto)
        {
            var entity = mapper.MapToEntity<InternalRequestDTO, BInternalRequest>(aInDto, false);

            var bulletinStatus = entity.ReqStatusCode == InternalRequestStatusTypeConstants.Approved ?
                BulletinStatusTypeConstants.Rehabilitated : BulletinStatusTypeConstants.Active;

            entity.Bulletin = new BBulletin
            {
                Id = entity.BulletinId,
                StatusId = bulletinStatus,
                EntityState = Common.Enums.EntityStateEnum.Modified,
                ModifiedProperties = new List<string> { nameof(BBulletin.StatusId) }
            };

            await SaveEntityAsync(entity, true);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
