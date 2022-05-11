using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IBulletinRepository
    {
        public BulletinRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<BBulletin> SelectAllAsync()
        {
            return this._dbContext.BBulletins.AsNoTracking()
                                .Include(x => x.Status)
                                .Include(x => x.BulletinAuthority);
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
                    .Include(x=>x.PBulletinIds)
                        .ThenInclude(x=>x.Person)
               .FirstOrDefaultAsync(x => x.Id == bulletinId);

            return bulleint;
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.BBulletins.AsNoTracking()
                .GroupBy(x => x.StatusId)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
