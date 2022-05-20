using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Rehabilitation;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class RehabilitationRepository : BaseAsyncRepository<BBulletin, CaisDbContext>, IRehabilitationRepository
    {
        public RehabilitationRepository(CaisDbContext dbContext) : base(dbContext)
        {
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
                         bulletin.StatusId == BulletinConstants.Status.ForRehabilitation)
                         select
                        new BulletinForRehabilitationDTO
                        {
                            Id = bulletin.Id,
                            DecisionDate = bulletin.DecisionDate,
                            DecisionFinalDate = bulletin.DecisionFinalDate,
                            Status = bulletin.StatusId,
                            RehabilitationDate = bulletin.RehabilitationDate,
                            Sanctions = bulletin.BSanctions.Select(x => new SanctionForRehabilitationDTO
                            {
                                SuspentionDurationDays = x.SuspentionDurationDays,
                                SuspentionDurationMonths = x.SuspentionDurationMonths,
                                SuspentionDurationYears = x.SuspentionDurationYears,
                                Type = x.SanctCategoryId
                            }),
                            OffencesEndDates = bulletin.BOffences.Select(o => o.OffEndDate)
                        }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

            return await Task.FromResult(query);
        }

        /// <summary>
        /// Add or delete a rehabilitation date.
        /// Change the status of the bulletin if necessary.
        /// </summary>
        /// <param name="bulletinId">Bulletin identifier</param>
        /// <param name="rehabilitationDate">The date must be null 
        /// if a crime was committed during the probation period</param>
        /// <param name="status">The status may be missing if you do not need to change the status of bulletin</param>
        /// <returns></returns>
        public void UpdateRehabilitationData(string bulletinId, DateTime? rehabilitationDate, string? status)
        {
            var bulletin = new BBulletin
            {
                Id = bulletinId,
                RehabilitationDate = rehabilitationDate,
            };

            if (!string.IsNullOrEmpty(status))
            {
                bulletin.StatusId = status;
            }

            _dbContext.Update(bulletin);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

