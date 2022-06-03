using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        private readonly IUserContext _userContext;

        public BulletinRepository(CaisDbContext dbContext, IUserContext userContext)
            : base(dbContext)
        {
            this._userContext = userContext;
        }

        public override IQueryable<BBulletin> SelectAllAsync()
        {
            var query = this._dbContext.BBulletins
                                .Include(x => x.Status)
                                .Include(x => x.BulletinAuthority)
                                .AsNoTracking();

            query = _userContext.FilterByAuthority(query);

            return query;
        }

        public override async Task<BBulletin> SelectAsync(string aId)
        {
            var bulletin = await _dbContext.BBulletins
               .Include(x => x.BPersNationalities)
               .Include(x => x.CsAuthority)
               .Include(x => x.BirthCountry)
               .Include(x => x.BirthCity)
                   .ThenInclude(x => x.Municipality)
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return bulletin;
        }

        public async Task<string> GetBulletinAuthIdAsync(string aId)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking()
               .FirstOrDefaultAsync(x => x.Id == aId);

            return bulletin?.CsAuthorityId;
        }

        public async Task<DDocument> SelectDocumentAsync(string documentId)
        {
            return await _dbContext.DDocuments.AsNoTracking()
               .Include(x => x.DocContent)
               .FirstOrDefaultAsync(x => x.Id == documentId);
        }

        public async Task<IQueryable<DDocument>> SelectAllDocumentsAsync()
        {
            var query = _dbContext.DDocuments.AsNoTracking()
               .Include(x => x.DocContent);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BBullPersAlias>> SelectBullPersAliasByBulletinIdAsync(string aId)
        {
            return await Task.FromResult(_dbContext.BBullPersAliases.AsNoTracking()
                .Where(x => x.BulletinId == aId));
        }

        public async Task<IQueryable<BOffence>> SelectAllOffencesAsync()
        {
            var query = _dbContext.BOffences
                 .AsNoTracking()
                 .Include(x => x.OffenceCat)
                 .Include(x => x.EcrisOffCat)
                 .Include(x => x.OffPlaceCountry)
                 .Include(x => x.OffPlaceCity)
                     .ThenInclude(x => x.Municipality);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BSanction>> SelectAllSanctionsAsync()
        {
            var query = _dbContext.BSanctions
                 .AsNoTracking()
                 .Include(x => x.EcrisSanctCateg)
                 .Include(x => x.SanctCategory)
                 .Include(x => x.BProbations);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BDecision>> SelectAllDecisionsAsync()
        {
            var query = _dbContext.BDecisions
                .AsNoTracking()
                .Include(x => x.DecisionAuth)
                .Include(x => x.DecisionChType)
                .Include(x => x.DecisionType);

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BBulletinStatusH>> SelectAllStatusHistoryDataAsync()
        {
            var query = _dbContext.BBulletinStatusHes.AsNoTracking()
                 .Include(x => x.NewStatusCodeNavigation)
                 .Include(x => x.OldStatusCodeNavigation)
                 .OrderByDescending(x => x.CreatedOn);

            return await Task.FromResult(query);
        }

        public async Task<BBulletin> SelectBulletinPersonInfoAsync(string bulletinId)
        {
            var bulleint = await _dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.BirthCountry)
                    .Include(x => x.CsAuthority)
                    .Include(x => x.BirthCity)
                        .ThenInclude(x => x.Municipality)
                            .ThenInclude(x => x.District)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Include(x => x.BBullPersAliases)
                    .Include(x => x.PBulletinIds)
                        .ThenInclude(x => x.Person)
               .FirstOrDefaultAsync(x => x.Id == bulletinId);

            return bulleint;
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                .Where(x => x.StatusId == BulletinConstants.Status.NewOffice ||
                            x.StatusId == BulletinConstants.Status.NewEISS ||
                            x.StatusId == BulletinConstants.Status.ForRehabilitation ||
                            x.StatusId == BulletinConstants.Status.ForDestruction)
                .GroupBy(x => x.StatusId)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }

        public void CreateEcrisTcn(string bulletinId, string action)
        {
            var ecrisTcn = new EEcrisTcn
            {
                Id = BaseEntity.GenerateNewId(),
                BulletinId = bulletinId,
                Status = BulletinEventConstants.Status.New,
                Action = action
            };

            _dbContext.Add(ecrisTcn);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PPerson> GetPersonIdByPidAsync(string pid, string pidType)
        {
            var personId = await _dbContext.PPersonIds.AsNoTracking()
                .Include(x => x.PidType)
                .Where(p => p.Pid == pid && p.PidType.Code == pidType)
                .FirstOrDefaultAsync();

            if (personId == null) return null;

            var result = await _dbContext.PPeople.AsNoTracking()
                   .Include(x => x.PPersonIds).ThenInclude(x => x.PBulletinIds)
                   .Include(x => x.PPersonIds).ThenInclude(x => x.PidType)
                   .Include(x => x.BirthCity)
                   .Include(x => x.BirthCountry)
                   .FirstOrDefaultAsync(x => x.Id == personId.PersonId);

            return result;
        }

        public async Task<IQueryable<BBulletin>> GetBulletinsByPidIdAsync(string pidId)
        {
            var result = _dbContext.BBulletins.AsNoTracking()
                    .Include(x => x.PBulletinIds)
                    .Include(x => x.BirthCity)
                    .Include(x => x.BirthCountry)
                    .Include(x => x.DecidingAuth)
                    .Include(x => x.DecisionType)
                    .Include(x => x.CaseType)
                    .Include(x => x.CaseAuth)

                    .Include(x => x.BOffences).ThenInclude(x => x.OffenceCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.EcrisOffCat)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCountry)
                    .Include(x => x.BOffences).ThenInclude(x => x.OffPlaceCity)

                    .Include(x => x.BSanctions).ThenInclude(x => x.SanctCategory)
                    .Include(x => x.BSanctions).ThenInclude(x => x.EcrisSanctCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbCateg)
                    .Include(x => x.BSanctions).ThenInclude(x => x.BProbations).ThenInclude(x => x.SanctProbMeasure)

                    .Include(x => x.BDecisions).ThenInclude(x => x.DecisionAuth)
                    .Include(x => x.BulletinAuthority)
                    .Include(x => x.CsAuthority)
                    .Include(x => x.BPersNationalities)
                        .ThenInclude(x => x.Country)
                    .Where(x => x.PBulletinIds.Any(x => x.PersonId == pidId));

            return await Task.FromResult(result);
        }

        public async Task SaveBulletinsAsync(List<BBulletin> bulletins)
        {
            _dbContext.BBulletins.AddRange(bulletins);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Dictionary<string, string>> GetAuthIdByEkkateAsync(List<string> ekatteCodes)
        {
            var result = await _dbContext.GCities.AsNoTracking()
                .Where(x => ekatteCodes.Contains(x.EkatteCode))
                .Select(x => new KeyValuePair<string, string>(x.EkatteCode, x.CsAuthorityId))
                .ToDictionaryAsync(x => x.Key, x => x.Value);

            return result;
        }
    }
}
