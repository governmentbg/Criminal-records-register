using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class BulletinAdministrationService : BaseAsyncService<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string, CaisDbContext>, IBulletinAdministrationService
    {
        private readonly IBulletinAdministrationRepository _bulletinAdministrationRepository;

        public BulletinAdministrationService(IMapper mapper, IBulletinAdministrationRepository bulletinAdministrationRepository)
            : base(mapper, bulletinAdministrationRepository)
        {
            _bulletinAdministrationRepository = bulletinAdministrationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public IQueryable<BaseNomenclatureDTO> GetBulletinStatusesByHistory(string aId)
            => _bulletinAdministrationRepository.GetBulletinStatusesByHistory(aId);

        public async Task<IgPageResult<BulletinAdministrationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinAdministrationGridDTO> aQueryOptions, BulletinAdministrationSearchParamDTO searchParams)
        {
            var baseQuery = _bulletinAdministrationRepository.SelectAllNotDeletedAndLockedBulletins(searchParams);

            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinAdministrationGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task UnlockBulletinAsync(UnlockBulletinModelDTO aInDto)
        {
            var bulletin = await _bulletinAdministrationRepository.GetBulletinByIdAsync(aInDto.BulletinId);

            if (bulletin == null) throw new ArgumentNullException($"Bulletin with id {aInDto.BulletinId} does not exist");

            bulletin.EntityState = EntityStateEnum.Modified;
            bulletin.ModifiedProperties = new List<string> { nameof(bulletin.Locked), nameof(bulletin.Version) };
            bulletin.Locked = false;

            if (bulletin.StatusId != aInDto.Status)
            {
                bulletin.StatusId = aInDto.Status;
                bulletin.ModifiedProperties.Add(nameof(bulletin.StatusId));
            }

            var statusHis = new BBulletinStatusH()
            {
                Id = BaseEntity.GenerateNewId(),
                EntityState = EntityStateEnum.Added,
                BulletinId = bulletin.Id,
                Descr = aInDto.Description,
                NewStatusCode = aInDto.Status,
                OldStatusCode = bulletin.StatusId,
                Locked = false,
            };

            _bulletinAdministrationRepository.ApplyChanges(statusHis, new List<IBaseIdEntity>());
            _bulletinAdministrationRepository.ApplyChanges(bulletin, new List<IBaseIdEntity>());
            await _bulletinAdministrationRepository.SaveChangesAsync();
        }
    }
}
