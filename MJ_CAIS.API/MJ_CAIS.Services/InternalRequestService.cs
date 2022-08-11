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

        public virtual async Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string statuses, bool isForSender)
        {
            var entityQuery = this.GetSelectAllQueryable();

            if (isForSender)
            {
                entityQuery.Where(x => x.FromAuthorityId == _userContext.CsAuthorityId);
            }
            else
            {
                entityQuery.Where(x => x.ToAuthorityId == _userContext.CsAuthorityId);
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

        /// <summary>
        /// Основна информация за бюлетин и лицето към него, 
        /// което се отнася за текущата заявка за реабилитация
        /// </summary>
        /// <param name="bulletinId"></param>
        /// <returns></returns>
        public async Task<BulletinPersonInfoModelDTO> GetBulletinPersonInfoAsync(string bulletinId)
        {
            throw new NotImplementedException();
            //var bulletin = await _bulletinRepository.SelectBulletinPersonInfoAsync(bulletinId);
            //if (bulletin == null) return null;

            //var result = mapper.Map<BulletinPersonInfoModelDTO>(bulletin);
            //if (!string.IsNullOrEmpty(bulletin.EgnNavigation?.PersonId))
            //{
            //    result.PersonId = bulletin.EgnNavigation.PersonId;
            //}
            //else if (!string.IsNullOrEmpty(bulletin.LnchNavigation?.PersonId))
            //{
            //    result.PersonId = bulletin.LnchNavigation.PersonId;
            //}
            //else if (!string.IsNullOrEmpty(bulletin.LnNavigation?.PersonId))
            //{
            //    result.PersonId = bulletin.LnNavigation.PersonId;
            //}
            //else if (!string.IsNullOrEmpty(bulletin.IdDocNumberNavigation?.PersonId))
            //{
            //    result.PersonId = bulletin.IdDocNumberNavigation.PersonId;
            //}
            //else if (!string.IsNullOrEmpty(bulletin.SuidNavigation?.PersonId))
            //{
            //    result.PersonId = bulletin.SuidNavigation.PersonId;
            //}

            //return result;
        }
    }
}
