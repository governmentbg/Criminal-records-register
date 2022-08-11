using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

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

        public override async Task<string> InsertAsync(InternalRequestDTO aInDto)
        {
            aInDto.ReqStatusCode = InternalRequestStatusTypeConstants.Draft;
            aInDto.FromAuthorityId = _userContext.CsAuthorityId;
            aInDto.RegNumber = await _registerTypeService.GetRegisterNumberForInternalRequest(_userContext.CsAuthorityId);
            return await base.InsertAsync(aInDto);
        }

        public override async Task UpdateAsync(string aId, InternalRequestDTO aInDto)
        {
            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aInDto.Id);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (dbEntity.ReqStatusCode != InternalRequestStatusTypeConstants.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.mgsNotAllowedToEditRequest);

            var entity = mapper.MapToEntity<InternalRequestDTO, NInternalRequest>(aInDto, false);

            await _internalRequestRepository.SaveEntityAsync(entity, true);
        }

        public override async Task DeleteAsync(string aId)
        {
            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aId);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (dbEntity.ReqStatusCode != InternalRequestStatusTypeConstants.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.mgsNotAllowedToDeleteRequest);

            dbEntity.EntityState = EntityStateEnum.Deleted;
            await _internalRequestRepository.SaveEntityAsync(dbEntity, false);
        }


        public async Task ChangeStatusAsync(string aId, string status)
        {
            var dbEntity = await _internalRequestRepository.SingleOrDefaultAsync<NInternalRequest>(x => x.Id == aId);

            if (dbEntity == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.msgRequestDoesNotExist, aId));

            if (status == InternalRequestStatusTypeConstants.Sent && dbEntity.ReqStatusCode != InternalRequestStatusTypeConstants.Draft)
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

            if (dbEntity.ReqStatusCode == InternalRequestStatusTypeConstants.Cancelled ||
                dbEntity.ReqStatusCode == InternalRequestStatusTypeConstants.Ready)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgReplayExist);

            if (dbEntity.ReqStatusCode == InternalRequestStatusTypeConstants.Draft)
                throw new BusinessLogicException(BusinessLogicExceptionResources.msgReplayNotAllowed);

            dbEntity.ReqStatusCode = accepted ? InternalRequestStatusTypeConstants.Ready : InternalRequestStatusTypeConstants.Cancelled;
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
                if (entity.ReqStatusCode != InternalRequestStatusTypeConstants.Cancelled &&
                    entity.ReqStatusCode != InternalRequestStatusTypeConstants.Ready)
                    throw new BusinessLogicException(BusinessLogicExceptionResources.msgReadIsNotAllowed);

                entity.ReqStatusCode = entity.ReqStatusCode == InternalRequestStatusTypeConstants.Cancelled ?
                    InternalRequestStatusTypeConstants.ReadCancelled : InternalRequestStatusTypeConstants.ReadReady;

                entity.EntityState = EntityStateEnum.Modified;
                entity.ModifiedProperties = new List<string> { nameof(entity.ReqStatusCode), nameof(entity.Version) };
            }

            await _internalRequestRepository.SaveEntityListAsync(entities, false);
        }
    }
}
