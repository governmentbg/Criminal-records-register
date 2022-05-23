using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
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
        private readonly IBulletinRepository _bulletinRepository;

        public InternalRequestService(IMapper mapper, IInternalRequestRepository internalRequestRepository, IBulletinRepository bulletinRepository)
            : base(mapper, internalRequestRepository)
        {
            _internalRequestRepository = internalRequestRepository;
            _bulletinRepository = bulletinRepository;
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

            var bulletin = new BBulletin
            {
                Id = entity.BulletinId,
                StatusId = bulletinStatus,
                EntityState = EntityStateEnum.Modified,
                Version = aInDto.BulletinVersion,
                ModifiedProperties = new List<string> { nameof(BBulletin.StatusId), nameof(BBulletin.Version) },
            };

            var satusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                BulletinId = entity.BulletinId,
                OldStatusCode = BulletinConstants.Status.ForRehabilitation,
                NewStatusCode = bulletinStatus,
                EntityState = EntityStateEnum.Added,
            };

            bulletin.BInternalRequests = new List<BInternalRequest>() { entity };
            bulletin.BBulletinStatusHes = new List<BBulletinStatusH>() { satusHistory };

            await dbContext.SaveEntityAsync(bulletin, true);
        }

        /// <summary>
        /// Основна информация за бюлетин и лицето към него, 
        /// което се отнася за текущата заявка за реабилитация
        /// </summary>
        /// <param name="bulletinId"></param>
        /// <returns></returns>
        public async Task<BulletinPersonInfoModelDTO> GetBulletinPersonInfoAsync(string bulletinId)
        {
            var bulleint = await _bulletinRepository.SelectBulletinPersonInfoAsync(bulletinId);
            return mapper.Map<BulletinPersonInfoModelDTO>(bulleint);
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
