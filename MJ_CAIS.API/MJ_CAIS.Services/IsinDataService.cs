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
            var entityQuery = SelectAll(status, bulletinId);
            var resultQuery = await this.ApplyOData(entityQuery, aQueryOptions);
            var pageResult = new IgPageResult<IsinDataGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, entityQuery, resultQuery);
            return pageResult;
        }

        public override async Task<IsinDataDTO> SelectAsync(string aId)
        {
            return await SelectIsinDataAsync(aId);
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
            var hasBulletin = await dbContext.BBulletins
               .AnyAsync(x => x.Id == bulletinId);

            if (!hasBulletin)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, bulletinId));

            var isinData = await dbContext.EIsinData
               .FirstOrDefaultAsync(x => x.Id == aId);

            if (isinData == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.isinDataDoesNotExist, aId));

            isinData.Status = IsinDataConstants.Status.Identified;
            isinData.BulletinId = bulletinId;
            await dbContext.SaveChangesAsync();
        }

        public async Task<IsinDataPreviewDTO> SelectForPreviewAsync(string aId)
        {
            var isin = await SelectIsinDataAsync(aId);
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
            var isinData = await dbContext.EIsinData
               .FirstOrDefaultAsync(x => x.Id == aId);

            if (isinData == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.isinDataDoesNotExist, aId));

            isinData.Status = IsinDataConstants.Status.Closed;
            await dbContext.SaveChangesAsync();
        }

        #region Helper methods 

        private IQueryable<IsinDataGridDTO> SelectAll(string? status, string? bulletinId)
        {
            var query = from isin in dbContext.EIsinData.AsNoTracking()
                        join isinMessage in dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                  into isinMessageLeft
                        from isinMessage in isinMessageLeft.DefaultIfEmpty()

                        join bulletins in dbContext.BBulletins.AsNoTracking() on isin.BulletinId equals bulletins.Id
                                  into bulletinsLeft
                        from bulletins in bulletinsLeft.DefaultIfEmpty()

                        join decisionTypes in dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                    into isinDecisionLeft
                        from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                        join caseTypes in dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
                        into isinCaseLeft
                        from isinCase in isinCaseLeft.DefaultIfEmpty()

                        select new IsinDataGridDTO
                        {
                            Id = isin.Id,
                            MsgDateTime = isinMessage.ExecutionDate,
                            BirthCountryName = isin.BirthcountryName,
                            BirthDate = isin.Birthdate,
                            BulletinId = isin.BulletinId,
                            CaseInfo = isinCase.Name + " " + isin.CaseNumber + " " + isin.CaseYear + " " + isin.CaseAuthName,
                            DecisionInfo = isinDecision.Name + " " + isin.DecisionNumber + " " + isin.DecisionDate + " " + isin.DecisionAuthName,
                            Identifier = isin.Identifier,
                            Nationalities = isin.Country1Name + " " + isin.Country2Name,
                            PersonName = isin.Firstname + " " + isin.Surname + " " + isin.Familyname,
                            SanctionEndDate = isin.SanctionEndDate,
                            SanctionStartDate = isin.SanctionStartDate,
                            SourceType = isin.SourceType,
                            Version = isin.Version,
                            Status = isin.Status,
                            SanctionType = isin.SanctionType == IsinDataConstants.SanctionType.Fine ? IsinDataConstants.SanctionTypeDisplay.Fine :
                                       (isin.SanctionType == IsinDataConstants.SanctionType.Probation ? IsinDataConstants.SanctionTypeDisplay.Probation :
                                        (isin.SanctionType == IsinDataConstants.SanctionType.Prison ? IsinDataConstants.SanctionTypeDisplay.Prison :
                                         null)),
                            CsAuthorityId = bulletins.CsAuthorityId,
                            CreatedOn = isin.CreatedOn
                        };

            if (!string.IsNullOrEmpty(bulletinId))
            {
                query = query.Where(x => x.BulletinId == bulletinId);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
                if (status == IsinDataConstants.Status.Identified)
                {
                    query = query.Where(x => x.CsAuthorityId == _userContext.CsAuthorityId);
                }
            }

            return query;
        }

        private async Task<IsinDataPreviewDTO> SelectIsinDataAsync(string aId)
        {
            var isinData = await (from isin in dbContext.EIsinData.AsNoTracking()
                                  join isinMessage in dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                            into isinMessageLeft
                                  from isinMessage in isinMessageLeft.DefaultIfEmpty()

                                  join decisionTypes in dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                              into isinDecisionLeft
                                  from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                                  join caseTypes in dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
                                  into isinCaseLeft
                                  from isinCase in isinCaseLeft.DefaultIfEmpty()
                                  where isin.Id == aId
                                  select new IsinDataPreviewDTO
                                  {
                                      Id = isin.Id,
                                      MsgDateTime = isinMessage.ExecutionDate,
                                      Status = isin.Status,
                                      Identifier = isin.Identifier,
                                      FirstName = isin.Firstname,
                                      SurName = isin.Surname,
                                      FamilyName = isin.Familyname,
                                      BirthDate = isin.Birthdate,
                                      Sex = isin.Sex,
                                      Country1Name = isin.Country1Name,
                                      Country2Name = isin.Country2Name,
                                      BirthCountryName = isin.BirthcountryName,
                                      BirthPlace = isin.BirthPlace,
                                      DecisionType = isinDecision.Name,
                                      DecisionNumber = isin.DecisionNumber,
                                      DecisionDate = isin.DecisionDate,
                                      DecisionFinalDate = isin.DecisionFinalDate,
                                      DecisionAuthName = isin.DecisionAuthName,
                                      CaseType = isinCase.Name,
                                      CaseNumber = isin.CaseNumber,
                                      CaseYear = isin.CaseYear,
                                      CaseAuthName = isin.CaseAuthName,
                                      SanctionStartDate = isin.SanctionStartDate,
                                      SanctionEndDate = isin.SanctionEndDate,
                                      BulletinId = isin.BulletinId,
                                  }).FirstOrDefaultAsync();

            return isinData;
        }

        private IQueryable<IsinBulletinGridDTO> SelectAllBulletin()
        {
            return dbContext.BBulletins.AsNoTracking()
                .Include(x => x.DecidingAuth)
                .Include(x => x.DecisionType)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                .Include(x => x.BPersNationalities)
                    .ThenInclude(x => x.Country)
                .ProjectTo<IsinBulletinGridDTO>(mapperConfiguration);
        }

        #endregion
    }
}
