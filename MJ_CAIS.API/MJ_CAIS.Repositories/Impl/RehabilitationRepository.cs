using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Rehabilitation;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class RehabilitationRepository<TContext> : IRehabilitationRepository<TContext>
        where TContext : CaisDbContext
    {
        protected readonly TContext _dbContext;

        protected RehabilitationRepository(TContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public TContext GetDbContext()
        {
            return this._dbContext;
        }

        public async Task<IQueryable<BulletinForRehabilitationDTO>> GetBulletinByPersonIdAsync(string personId)
        {
            var query = (from bulletin in _dbContext.BBulletins.AsNoTracking()

                        join bulletinPersonId in _dbContext.PBulletinIds.AsNoTracking() on bulletin.Id equals bulletinPersonId.BulletinId
                                  into bulletinPersonLeft
                        from bulletinPersonId in bulletinPersonLeft.DefaultIfEmpty()

                        join personIds in _dbContext.PPersonIds.AsNoTracking() on bulletinPersonId.PersonId equals personIds.Id
                                    into personIdsLeft
                        from personIds in personIdsLeft.DefaultIfEmpty()

                        where personIds.PersonId == personId && (bulletin.StatusId == BulletinConstants.Status.Active ||
                        bulletin.StatusId == BulletinConstants.Status.ForRehabilitation) // todo: ? 
                         select
                        new BulletinForRehabilitationDTO
                        {
                            Id = bulletin.Id,
                            DecisionDate = bulletin.DecisionDate,
                            DecisionFinalDate = bulletin.DecisionFinalDate,
                            Status = bulletin.StatusId,
                            Sanctions = bulletin.BSanctions.Select(x => new SanctionForRehabilitationDTO
                            {
                                SuspentionDurationDays = x.SuspentionDurationDays,
                                SuspentionDurationMonths = x.SuspentionDurationMonths,
                                SuspentionDurationYears = x.SuspentionDurationYears,
                                Type = x.SanctCategoryId
                            }),
                        }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

            return await Task.FromResult(query);
        }

        public async Task UpdateForRehabilitationAsync(string bulletinId, DateTime rehabilitationDate, bool changeStatus)
        {
            var bulletin = new BBulletin
            {
                Id = bulletinId,
                RehabilitationDate = rehabilitationDate,
            };

            if (changeStatus)
            {
                bulletin.StatusId = BulletinConstants.Status.ForRehabilitation;
            }

            _dbContext.Update(bulletin);
           await  _dbContext.SaveChangesAsync();
        }
    }
}

