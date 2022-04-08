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

        /// <summary>
        /// Връща списък от заявки.
        /// Когато адвокат разглежда заявки вижда всички. 
        /// Когато служител БС разглежда вижда всички заявки към конкретен бюлетин в неговото БС 
        /// </summary>
        /// <param name="aQueryOptions"></param>
        /// <param name="bulletinId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// При одобрение на заявката от адвокат, статуса на бюлетин към нея се променя на реабилитиран,
        /// при отхвърляне статуса от 'Подлежащ на реабилитация' се променя на 'Активен'
        /// </summary>
        /// <param name="aId"></param>
        /// <param name="aInDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Основна информация за бюлетин и лицето към него, 
        /// което се отнася за текущата заявка за реабилитация
        /// </summary>
        /// <param name="bulletinId"></param>
        /// <returns></returns>
        public async Task<BulletinPersonInfoModelDTO> GetBulletinPersonInfoAsync(string bulletinId)
        {
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
               .FirstOrDefaultAsync(x => x.Id == bulletinId);

            return mapper.Map<BulletinPersonInfoModelDTO>(bulleint);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
