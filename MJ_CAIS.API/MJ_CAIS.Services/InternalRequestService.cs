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

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public InternalRequestService(IMapper mapper, IInternalRequestRepository internalRequestRepository, IUserContext userContext)
            : base(mapper, internalRequestRepository)
        {
            _internalRequestRepository = internalRequestRepository;
            _userContext = userContext;
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
            var entity = mapper.MapToEntity<InternalRequestDTO, NInternalRequest>(aInDto, false);

            var bulletinStatus = entity.ReqStatusCode == InternalRequestStatusTypeConstants.Approved ?
                BulletinConstants.Status.Rehabilitated : BulletinConstants.Status.Active;

            var bulletin = new BBulletin
            {
                StatusId = bulletinStatus,
                EntityState = EntityStateEnum.Modified,
                Version = aInDto.BulletinVersion,
                ModifiedProperties = new List<string> { nameof(BBulletin.StatusId), nameof(BBulletin.Version) },
            };

            var statusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                //todo: change
                //BulletinId = entity.BulletinId,
                OldStatusCode = aInDto.BulletinStatusId,
                NewStatusCode = bulletinStatus,
                EntityState = EntityStateEnum.Added,
                Locked = true,
            };
            //todo: change
            //bulletin.BInternalRequests = new List<BInternalRequest>() { entity };
            bulletin.BBulletinStatusHes = new List<BBulletinStatusH>() { statusHistory };
            //todo: change

            //if (string.IsNullOrEmpty(entity.AAppBulletinId))
            //{
            //    bulletin.Id = entity.BulletinId;
            //    await _internalRequestRepository.SaveEntityAsync(bulletin, true);
            //    return;
            //}

            // from application form
            //todo: change
            AAppBulletin? currentBull = null; // await _internalRequestRepository.GetBulletinsInCertificate(entity);

            bulletin.Id = currentBull.BulletinId;

            var certId = currentBull.CertificateId;

            var bullIdsForCert = await (await _internalRequestRepository.FindAsync<AAppBulletin>(x => x.CertificateId == certId))//await dbContext.AAppBulletins.AsNoTracking()
                                                                                                                                 //.Where(x => x.CertificateId == certId)
                .Select(x => x.Id).ToListAsync();

            if (bullIdsForCert.Any())
            {
                //todo:change
                bool hasRequests = false; //await _internalRequestRepository.HasRequests(entity, bullIdsForCert);

                // change status of certificate
                if (!hasRequests)
                {
                    var cert = currentBull.Certificate;
                    cert.EntityState = EntityStateEnum.Modified;
                    cert.StatusCode = ApplicationConstants.ApplicationStatuses.BulletinsSelection;
                    cert.ModifiedProperties = new List<string> { nameof(cert.StatusCode), nameof(cert.Version) };
                    _internalRequestRepository.ApplyChanges(cert, new List<IBaseIdEntity>());

                    var result = new AStatusH
                    {
                        Id = BaseEntity.GenerateNewId(),
                        ApplicationId = cert.ApplicationId,
                        CertificateId = certId,
                        StatusCode = cert.StatusCode,
                        Descr = ApplicationResources.descChangeStatus,
                        EntityState = EntityStateEnum.Added
                    };

                    _internalRequestRepository.ApplyChanges(result, new List<IBaseIdEntity>());
                }
            }

            await _internalRequestRepository.SaveEntityAsync(bulletin, true);
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
