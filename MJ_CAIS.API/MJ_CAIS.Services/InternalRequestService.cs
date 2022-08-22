using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.InternalRequestConstants;

namespace MJ_CAIS.Services
{
    public class InternalRequestService : BaseAsyncService<InternalRequestDTO, InternalRequestDTO, InternalRequestGridDTO, NInternalRequest, string, CaisDbContext>, IInternalRequestService
    {
        private readonly IInternalRequestRepository _internalRequestRepository;
        private readonly IUserContext _userContext;
        private readonly IRegisterTypeService _registerTypeService;

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public InternalRequestService(IMapper mapper,
            IInternalRequestRepository internalRequestRepository,
            IUserContext userContext,
            IRegisterTypeService registerTypeService)
            : base(mapper, internalRequestRepository)
        {
            _internalRequestRepository = internalRequestRepository;
            _userContext = userContext;
            _registerTypeService = registerTypeService;
        }

        public async Task<RequestCountDTO> GetInternalRequestsCount()
            => await _internalRequestRepository.GetInternalRequestsCountAsync();

        public IQueryable<SelectedPersonBulletinGridDTO> GetPersonBulletins(string personId)
            => _internalRequestRepository.GetPersonBulletins(personId);

        public IQueryable<SelectedPersonBulletinGridDTO> GetSelectedBulletins(string aId)
            => _internalRequestRepository.GetSelectedBulletins(aId);

        public async Task <SelectedPersonBulletinGridDTOExtended> GetBulletinWithPidDataByBulletinIdAsync(string aId)
            => await _internalRequestRepository.GetBulletinWithPidDataByBulletinIdAsync(aId);

        public async Task<SelectedPersonBulletinGridDTOExtended> GetBulletinWithPidDataByPersonIdAsync(string aId)
            =>  await _internalRequestRepository.GetBulletinWithPidDataByPersonIdAsync(aId);

        public virtual async Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string statuses, bool fromAuth)
        {
            var entityQuery = this.GetSelectAllQueryable();

            if (fromAuth)
            {
                entityQuery = entityQuery.Where(x => x.FromAuthorityId == _userContext.CsAuthorityId);
            }
            else
            {
                entityQuery = entityQuery.Where(x => x.ToAuthorityId == _userContext.CsAuthorityId);
            }

            var statuesArr = statuses.Split(',');
            entityQuery = entityQuery.Where(x => statuesArr.Contains(x.ReqStatusCode));

            var baseQuery = entityQuery.ProjectTo<InternalRequestGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InternalRequestGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public virtual async Task<IgPageResult<InternalRequestForJudgeGridDTO>> SelectAllForJudgeWithPaginationAsync(ODataQueryOptions<InternalRequestForJudgeGridDTO> aQueryOptions, string statuses)
        {
            var entityQuery = _internalRequestRepository.SelectAllForJudge();
            entityQuery = entityQuery.Where(x => x.ToAuthorityId == _userContext.CsAuthorityId && x.NIntReqTypeId == Types.Rehabilitation);

            var statuesArr = statuses.Split(',');
            entityQuery = entityQuery.Where(x => statuesArr.Contains(x.ReqStatusCode));

            var baseQuery = entityQuery.ProjectTo<InternalRequestForJudgeGridDTO>(mapperConfiguration);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<InternalRequestForJudgeGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task<IgPageResult<SelectPidGridDTO>> SelectAllPidsForSelectionWithPaginationAsync(ODataQueryOptions<SelectPidGridDTO> aQueryOptions)
        {
            var entityQuery = _internalRequestRepository.SelectAllPidsForSelection();
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<SelectPidGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public override async Task<string> InsertAsync(InternalRequestDTO aInDto)
        {
            aInDto.ReqStatusCode = Status.Draft;
            aInDto.FromAuthorityId = _userContext.CsAuthorityId;
            aInDto.RegNumber = await _registerTypeService.GetRegisterNumberForInternalRequest(_userContext.CsAuthorityId);

            var entity = mapper.MapToEntity<InternalRequestDTO, NInternalRequest>(aInDto, isAdded: true);
            entity.NInternalReqBulletins = mapper.MapTransactions<SelectedPersonBulletinGridDTO, NInternalReqBulletin>(aInDto.SelectedBulletinsTransactions);

            await this.SaveEntityAsync(entity, true);
            return entity.Id;
        }

        public override async Task UpdateAsync(string aId, InternalRequestDTO aInDto)
        {
            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aInDto.Id);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (dbEntity.ReqStatusCode != Status.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.mgsNotAllowedToEditRequest);

            if (aInDto.ReqStatusCode == Status.Sent && dbEntity.ReqStatusCode != Status.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgRequestIsNotDraft);

            var entity = mapper.MapToEntity<InternalRequestDTO, NInternalRequest>(aInDto, false);
            entity.NInternalReqBulletins = mapper.MapTransactions<SelectedPersonBulletinGridDTO, NInternalReqBulletin>(aInDto.SelectedBulletinsTransactions);

            await _internalRequestRepository.SaveEntityAsync(entity, true);
        }

        public override async Task DeleteAsync(string aId)
        {
            var dbEntity = await _internalRequestRepository.SelectForDeleteAsync(aId);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (dbEntity.ReqStatusCode != Status.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.mgsNotAllowedToDeleteRequest);

            dbEntity.EntityState = EntityStateEnum.Deleted;

            foreach (var bulletin in dbEntity.NInternalReqBulletins)
            {
                bulletin.EntityState = EntityStateEnum.Deleted;
            }

            await _internalRequestRepository.SaveEntityAsync(dbEntity, true);
        }

        public async Task ChangeStatusAsync(string aId, string status)
        {
            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aId);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (status == Status.Sent && dbEntity.ReqStatusCode != Status.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgRequestIsNotDraft);

            dbEntity.ReqStatusCode = status;
            dbEntity.EntityState = EntityStateEnum.Modified;
            dbEntity.ModifiedProperties = new List<string> { nameof(dbEntity.ReqStatusCode), nameof(dbEntity.Version) };

            await _internalRequestRepository.SaveEntityAsync(dbEntity, false);
        }

        public async Task ReplayAsync(string aId, bool accepted, string responseDesc)
        {
            if (string.IsNullOrEmpty(responseDesc))
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.fieldIsRequired, nameof(responseDesc)));

            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aId);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            var myAuthId = _userContext.CsAuthorityId;
            if (dbEntity.ToAuthorityId != myAuthId)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgRequestForDifferentAuth);

            if (dbEntity.ReqStatusCode == Status.Cancelled ||
                dbEntity.ReqStatusCode == Status.Ready)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgReplayExist);

            if (dbEntity.ReqStatusCode == Status.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgReplayNotAllowed);

            dbEntity.ReqStatusCode = accepted ? Status.Ready : Status.Cancelled;
            dbEntity.EntityState = EntityStateEnum.Modified;
            dbEntity.ResponseDescr = responseDesc;
            dbEntity.ModifiedProperties = new List<string> { nameof(dbEntity.ReqStatusCode), nameof(dbEntity.Version), nameof(dbEntity.ResponseDescr) };

            await _internalRequestRepository.SaveEntityAsync(dbEntity, false);
        }

        public async Task MarkAsReaded(List<string> ids)
        {
            // todo: parameters from base class
            // this is max page size 
            if (ids.Count > 25)
            {
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgMoreThenAllowedMsgIsReaded, 25));
            }

            var query = _internalRequestRepository.SelectAllByIdsAsync(ids);

            var entities = await query.ToListAsync();

            foreach (var entity in entities)
            {
                if (entity.ReqStatusCode != Status.Cancelled &&
                    entity.ReqStatusCode != Status.Ready)
                    throw new BusinessLogicException(BusinessLogicExceptionResources.msgReadIsNotAllowed);

                entity.ReqStatusCode = entity.ReqStatusCode == Status.Cancelled ?
                    Status.ReadCancelled : Status.ReadReady;

                entity.EntityState = EntityStateEnum.Modified;
                entity.ModifiedProperties = new List<string> { nameof(entity.ReqStatusCode), nameof(entity.Version) };
            }

            await _internalRequestRepository.SaveEntityListAsync(entities, false);
        }
    }
}
