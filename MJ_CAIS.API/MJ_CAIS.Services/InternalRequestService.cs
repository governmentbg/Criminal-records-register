using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DTO.Shared;
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
                BulletinConstants.Status.Rehabilitated : BulletinConstants.Status.Active;

            entity.Bulletin = new BBulletin
            {
                Id = entity.BulletinId,
                StatusId = bulletinStatus,
                EntityState = Common.Enums.EntityStateEnum.Modified,
                ModifiedProperties = new List<string> { nameof(BBulletin.StatusId) }
            };

            await SaveEntityAsync(entity, true);
        }

        public async Task<BulletinPersonInfoModelDTO> GetBulletinPersonInfoAsync(string aId, bool isBulletinId)
        {
            // id of internal request (when action is edit)
            if (!isBulletinId)
            {
                var bulleintFromIR = await dbContext.BInternalRequests.AsNoTracking()
                    .Include(x => x.Bulletin)
                    .Include(x => x.Bulletin.BirthCountry)
                    .Include(x => x.Bulletin.BirthCity)
                    .Include(x => x.Bulletin.BirthCity)
                        .ThenInclude(x => x.Municipality)
                            .ThenInclude(x => x.District)
                    .Include(x => x.Bulletin.DecidingAuth)
                    .Include(x => x.Bulletin.DecisionType)
                    .Include(x => x.Bulletin.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.Bulletin.BBullPersAliases)
                    .Where(x => x.Id == aId)
                    .Select(x => x.Bulletin)
                    .FirstOrDefaultAsync();

                return mapper.Map<BulletinPersonInfoModelDTO>(bulleintFromIR);
            }

            // id of bulletin (when action is create)
            var bulleint = await dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.BirthCountry)
                    .Include(x => x.BirthCity)
                    .Include(x => x.BirthCity)
                        .ThenInclude(x => x.Municipality)
                            .ThenInclude(x => x.District)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.BBullPersAliases)
               .FirstOrDefaultAsync(x => x.Id == aId);

            return mapper.Map<BulletinPersonInfoModelDTO>(bulleint);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
