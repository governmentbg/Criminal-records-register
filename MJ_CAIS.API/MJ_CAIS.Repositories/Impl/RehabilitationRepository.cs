using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
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

        public IQueryable<BulletinForRehabilitationDTO> GetBulletinsByPersonId(string personId)
        {
            var query = _dbContext.BBulletins
                   .Include(x => x.EgnNavigation)
                   .Include(x => x.LnchNavigation)
                   .Include(x => x.LnNavigation)
                   .Include(x => x.IdDocNumberNavigation)
                   .Include(x => x.SuidNavigation)
                   .Where(bulletin => bulletin.StatusId == BulletinConstants.Status.Active ||
                         bulletin.StatusId == BulletinConstants.Status.ForRehabilitation)
                   .Where(x => x.EgnNavigation.PersonId == personId ||
                             x.LnchNavigation.PersonId == personId ||
                             x.LnNavigation.PersonId == personId ||
                             x.IdDocNumberNavigation.PersonId == personId ||
                             x.SuidNavigation.PersonId == personId)
                   .Select(bulletin => new BulletinForRehabilitationDTO
                   {
                       Id = bulletin.Id,
                       DecisionDate = bulletin.DecisionDate,
                       DecisionFinalDate = bulletin.DecisionFinalDate,
                       Status = bulletin.StatusId,
                       RehabilitationDate = bulletin.RehabilitationDate,
                       Version = bulletin.Version,
                       CaseType = bulletin.CaseTypeId,
                       BirthDate = bulletin.BirthDate,
                       Sanctions = bulletin.BSanctions.Select(x => new SanctionForRehabilitationDTO
                       {
                           SuspentionDuration = new Duration
                           {
                               Years = x.SuspentionDurationYears,
                               Months = x.SuspentionDurationMonths,
                               Days = x.SuspentionDurationDays,
                           },
                           Type = x.SanctCategoryId,
                           PropbationDurations = x.BProbations.Select(p => new Duration
                           {
                               Years = p.DecisionDurationYears,
                               Months = p.DecisionDurationMonths,
                               Days = p.DecisionDurationDays,
                               Hours = p.DecisionDurationHours,
                           })
                       }),
                       Decisions = bulletin.BDecisions.Select(x => new DecisionForRehabilitationDTO
                       {
                           Type = x.DecisionChTypeId,
                           ChangeDate = x.ChangeDate,
                       }),
                       OffencesEndDates = bulletin.BOffences.Select(o => o.OffEndDate)
                   }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

            return query;
        }

        public async Task<string> GetPersonIdByBulletinIdAsync(string bulletinId)
        {
            var bulletin = await _dbContext.BBulletins.AsNoTracking()
                        .Include(x => x.EgnNavigation)
                        .Include(x => x.LnchNavigation)
                        .Include(x => x.LnNavigation)
                        .Include(x => x.IdDocNumberNavigation)
                        .Include(x => x.SuidNavigation)
                        .FirstOrDefaultAsync(x => x.Id == bulletinId);

            if (!string.IsNullOrEmpty(bulletin.EgnNavigation?.PersonId))
            {
                return bulletin.EgnNavigation.PersonId;
            }

            if (!string.IsNullOrEmpty(bulletin.LnchNavigation?.PersonId))
            {
                return bulletin.LnchNavigation.PersonId;
            }

            if (!string.IsNullOrEmpty(bulletin.LnNavigation?.PersonId))
            {
                return bulletin.LnNavigation.PersonId;
            }

            if (!string.IsNullOrEmpty(bulletin.IdDocNumberNavigation?.PersonId))
            {
                return bulletin.IdDocNumberNavigation.PersonId;
            }

            if (!string.IsNullOrEmpty(bulletin.SuidNavigation?.PersonId))
            {
                return bulletin.SuidNavigation.PersonId;
            }

            return null;
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
        public void UpdateRehabilitationData(string bulletinId, decimal? bulletinVersion, DateTime? rehabilitationDate, string? status)
        {
            var bulletin = new BBulletin
            {
                Id = bulletinId,
                RehabilitationDate = rehabilitationDate,
                Version = bulletinVersion,
                EntityState = EntityStateEnum.Modified
            };

            bulletin.ModifiedProperties = new List<string>
            {
                nameof(bulletin.RehabilitationDate),
                nameof(bulletin.Version),
            };

            if (!string.IsNullOrEmpty(status))
            {
                bulletin.StatusId = status;
                bulletin.ModifiedProperties.Add(nameof(bulletin.StatusId));
            }

            _dbContext.ApplyChanges(bulletin, new List<IBaseIdEntity>());
        }
    }
}

