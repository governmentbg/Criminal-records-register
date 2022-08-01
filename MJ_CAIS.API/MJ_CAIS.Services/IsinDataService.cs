using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.DTO.Shared;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services
{
    public class IsinDataService : BaseAsyncService<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string, CaisDbContext>, IIsinDataService
    {
        private readonly IIsinDataRepository _isinDataRepository;
        private readonly IBulletinRepository _bulletinRepository;
        private readonly IUserContext _userContext;
        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public IsinDataService(IMapper mapper, IIsinDataRepository isinDataRepository, IBulletinRepository bulletinRepository, IUserContext userContext)
            : base(mapper, isinDataRepository)
        {
            _isinDataRepository = isinDataRepository;
            _bulletinRepository = bulletinRepository;
            _userContext = userContext;
        }

        public virtual async Task<IgPageResult<IsinDataGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string? status, string? bulletinId)
        {
            var entityQuery = _isinDataRepository.SelectAll(status, bulletinId);
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinDataGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public override async Task<IsinDataDTO> SelectAsync(string aId)
        {
            return await _isinDataRepository.SelectIsinDataAsync(aId);
        }

        public async Task<IgPageResult<IsinBulletinGridDTO>> SelectIsinBulletinAllWithPaginationAsync(ODataQueryOptions<IsinBulletinGridDTO> aQueryOptions)
        {
            var entityQuery = SelectAllBulletin();
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinBulletinGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public async Task SelectBulletinAsync(string aId, string bulletinId)
        {
            bool hasBulletin = await _isinDataRepository.HasBulletin(bulletinId);

            if (!hasBulletin)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, bulletinId));

            var isinData = await _isinDataRepository.SingleOrDefaultAsync<EIsinDatum>(x => x.Id == aId);
            // await dbContext.EIsinData
            //.FirstOrDefaultAsync(x => x.Id == aId);

            if (isinData == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.isinDataDoesNotExist, aId));

            isinData.Status = IsinDataConstants.Status.Identified;
            isinData.BulletinId = bulletinId;
            await _isinDataRepository.SaveChangesAsync();
        }

      

        public async Task<IsinDataPreviewDTO> SelectForPreviewAsync(string aId)
        {
            var isin = await _isinDataRepository.SelectIsinDataAsync(aId);
            if (isin == null) return null;

            var bulletin = await _bulletinRepository.SelectBulletinPersonInfoAsync(isin.BulletinId);
            if (bulletin == null) return null;

            var result = mapper.Map<BulletinPersonInfoModelDTO>(bulletin);
            if (!string.IsNullOrEmpty(bulletin.EgnNavigation?.PersonId))
            {
                result.PersonId = bulletin.EgnNavigation.PersonId;
            }
            else if (!string.IsNullOrEmpty(bulletin.LnchNavigation?.PersonId))
            {
                result.PersonId = bulletin.LnchNavigation.PersonId;
            }
            else if (!string.IsNullOrEmpty(bulletin.LnNavigation?.PersonId))
            {
                result.PersonId = bulletin.LnNavigation.PersonId;
            }
            else if (!string.IsNullOrEmpty(bulletin.IdDocNumberNavigation?.PersonId))
            {
                result.PersonId = bulletin.IdDocNumberNavigation.PersonId;
            }
            else if (!string.IsNullOrEmpty(bulletin.SuidNavigation?.PersonId))
            {
                result.PersonId = bulletin.SuidNavigation.PersonId;
            }

            isin.BulletinPersonInfo = result;
            return isin;
        }

        public async Task CloseAsync(string aId)
        {
            var isinData = await _isinDataRepository.SelectAsync(aId);//dbContext.EIsinData
               //.FirstOrDefaultAsync(x => x.Id == aId);

            if (isinData == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.isinDataDoesNotExist, aId));

            isinData.Status = IsinDataConstants.Status.Closed;
            await _isinDataRepository.SaveChangesAsync();
        }

        #region Helper methods 
        private IQueryable<IsinBulletinGridDTO> SelectAllBulletin()
        {
            return _isinDataRepository.SelectAllBulletin()
               .ProjectTo<IsinBulletinGridDTO>(mapperConfiguration);
        }
        #endregion
    }
}
