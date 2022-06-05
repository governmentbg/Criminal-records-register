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
        /// ����� ������ �� ������.
        /// ������ ������� ��������� ������ ����� ������. 
        /// ������ �������� �� ��������� ����� ������ ������ ��� ��������� ������� � �������� �� 
        /// </summary>
        /// <param name="aQueryOptions"></param>
        /// <param name="bulletinId"></param>
        /// <returns></returns>
        public virtual async Task<IgPageResult<InternalRequestGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<InternalRequestGridDTO> aQueryOptions, string? statusId, string? bulletinId)
        {
            var entityQuery = this.GetSelectAllQueriable();

            if (!string.IsNullOrEmpty(bulletinId))
            {
                entityQuery = entityQuery.Where(x => x.BulletinId == bulletinId);
            }

            if (!string.IsNullOrEmpty(statusId))
            {
                entityQuery = entityQuery.Where(x => x.ReqStatusCode == statusId);
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
        /// ��� ��������� �� �������� �� �������, ������� �� ������� ��� ��� �� ������� �� ������������,
        /// ��� ���������� ������� �� '�������� �� ������������' �� ������� �� '�������'
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
                StatusId = bulletinStatus,
                EntityState = EntityStateEnum.Modified,
                Version = aInDto.BulletinVersion,
                ModifiedProperties = new List<string> { nameof(BBulletin.StatusId), nameof(BBulletin.Version) },
            };

            var statusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                BulletinId = entity.BulletinId,
                OldStatusCode = aInDto.BulletinStatusId,
                NewStatusCode = bulletinStatus,
                EntityState = EntityStateEnum.Added,
                Locked = true,
            };

            bulletin.BInternalRequests = new List<BInternalRequest>() { entity };
            bulletin.BBulletinStatusHes = new List<BBulletinStatusH>() { statusHistory };

            
            if (string.IsNullOrEmpty(entity.AAppBulletinId))
            {
                bulletin.Id = entity.BulletinId;
                await dbContext.SaveEntityAsync(bulletin, true);
                return;
            }

            // from application form
            var currentBull = await dbContext.AAppBulletins.AsNoTracking()
                .Include(x => x.Certificate)
               .FirstOrDefaultAsync(x => x.Id == entity.AAppBulletinId);

            bulletin.Id = currentBull.BulletinId;

            var certId = currentBull.CertificateId;

            var bullIdsForCert = await dbContext.AAppBulletins.AsNoTracking()
                .Where(x => x.CertificateId == certId && x.BulletinId != bulletin.Id)
                .Select(x => x.Id).ToListAsync();

            if (bullIdsForCert.Any())
            {
                var hasRequests = await dbContext.BInternalRequests.AsNoTracking()
                    .AnyAsync(x => x.ReqStatusCode == InternalRequestStatusTypeConstants.New && bullIdsForCert.Contains(x.AAppBulletinId));

                // change status of certificate
                if (!hasRequests)
                {
                    var cert = currentBull.Certificate;
                    cert.EntityState = EntityStateEnum.Modified;
                    cert.StatusCode = ApplicationConstants.ApplicationStatuses.BulletinsSelection;
                    cert.ModifiedProperties = new List<string> { nameof(cert.StatusCode), nameof(cert.Version) };
                    dbContext.ApplyChanges(cert, new List<IBaseIdEntity>());

                    var result = new AStatusH
                    {
                        Id = BaseEntity.GenerateNewId(),
                        ApplicationId = cert.ApplicationId,
                        CertificateId = certId,
                        StatusCode = cert.StatusCode,
                        Descr = "������ �� ����� ����� �� ��������, ��������� ����� � ����� �� ����������",
                        EntityState = EntityStateEnum.Added
                    };

                    dbContext.ApplyChanges(result, new List<IBaseIdEntity>());
                }
            }

            await dbContext.SaveEntityAsync(bulletin, true);
        }

        /// <summary>
        /// ������� ���������� �� ������� � ������ ��� ����, 
        /// ����� �� ������ �� �������� ������ �� ������������
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
