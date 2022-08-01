using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.IsinData;
using Microsoft.Extensions.Configuration;

namespace MJ_CAIS.Repositories.Impl
{
    public class IsinDataRepository : BaseAsyncRepository<EIsinDatum, CaisDbContext>, IIsinDataRepository
    {
        private readonly IUserContext _userContext;

        public IsinDataRepository(CaisDbContext dbContext,
            IUserContext userContext) : base(dbContext)
        {
            _userContext = userContext;
        }

        public IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority()
        {
            var query = _dbContext.EIsinData.AsNoTracking()
                .Include(x => x.Bulletin)
                .Where(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId && x.Status == IsinDataConstants.Status.New || x.Status == IsinDataConstants.Status.Identified)
                .GroupBy(x => x.Status)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }

        public async Task<bool> HasBulletin(string bulletinId)
        {
            return await _dbContext.BBulletins
               .AnyAsync(x => x.Id == bulletinId);
        }

        public IQueryable<IsinDataGridDTO> SelectAll(string? status, string? bulletinId)
        {
            var query = from isin in _dbContext.EIsinData.AsNoTracking()
                        join isinMessage in _dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                  into isinMessageLeft
                        from isinMessage in isinMessageLeft.DefaultIfEmpty()

                        join bulletins in _dbContext.BBulletins.AsNoTracking() on isin.BulletinId equals bulletins.Id
                                  into bulletinsLeft
                        from bulletins in bulletinsLeft.DefaultIfEmpty()

                        join decisionTypes in _dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                    into isinDecisionLeft
                        from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                        join caseTypes in _dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
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

        public async Task<IsinDataPreviewDTO> SelectIsinDataAsync(string aId)
        {
            var isinData = await (from isin in _dbContext.EIsinData.AsNoTracking()
                                  join isinMessage in _dbContext.EWebRequests.AsNoTracking() on isin.WebRequestId equals isinMessage.Id
                                            into isinMessageLeft
                                  from isinMessage in isinMessageLeft.DefaultIfEmpty()

                                  join decisionTypes in _dbContext.BDecisionTypes.AsNoTracking() on isin.DecisionTypeId equals decisionTypes.Code
                                              into isinDecisionLeft
                                  from isinDecision in isinDecisionLeft.DefaultIfEmpty()

                                  join caseTypes in _dbContext.BCaseTypes.AsNoTracking() on isin.CaseTypeId equals caseTypes.Code
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

        public IQueryable<BBulletin> SelectAllBulletin()
        {
            return _dbContext.BBulletins.AsNoTracking()
                .Include(x => x.DecidingAuth)
                .Include(x => x.DecisionType)
                .Include(x => x.BirthCountry)
                .Include(x => x.BirthCity)
                .Include(x => x.BPersNationalities)
                    .ThenInclude(x => x.Country);
               
        }
    }
}
