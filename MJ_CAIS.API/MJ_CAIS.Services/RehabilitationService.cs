using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Rehabilitation;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class RehabilitationService : BaseAsyncService<BulletinBaseDTO, BulletinBaseDTO, BulletinGridDTO, BBulletin, string, CaisDbContext>, IRehabilitationService
    {
        private readonly IRehabilitationRepository _rehabilitationRepository;

        public RehabilitationService(IMapper mapper, IRehabilitationRepository rehabilitationRepository)
            : base(mapper, rehabilitationRepository)
        {
            _rehabilitationRepository = rehabilitationRepository;
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        /// <summary>
        /// Then change status from NewOffice/NewEISS to Active
        /// Algorithm for rehabilitation
        /// https://bg.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B0%D0%B1%D0%B8%D0%BB%D0%B8%D1%82%D0%B0%D1%86%D0%B8%D1%8F
        /// </summary>
        public async Task ApplyRehabilitationOnChangeStatusAsync(BBulletin currentAttachedBull, string personId)
        {
            var bulletinsQuery = await _rehabilitationRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // when status is changed from newoffice to active or neweiss to active
            // only first point may be applied
            await ApplyFirstPoinAsync(bulletins, currentAttachedBull);
        }

        /// <summary>
        /// When has edit of active bulletin
        /// </summary>
        /// <param name="currentAttachedBull"></param>
        /// <returns></returns>
        public async Task ApplyRehabilitationOnUpdateAsync(BBulletin currentAttachedBull)
        {
            var personId = await _rehabilitationRepository.GetPersonIdByBulletinIdAsync(currentAttachedBull.Id);
            if (string.IsNullOrEmpty(personId)) return;

            var bulletinsQuery = await _rehabilitationRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            var appliedChangesSecondPoint = await ApplySecondPoinAsync(bulletins, currentAttachedBull);
            if (appliedChangesSecondPoint) return;
        }

        private async Task<bool> ApplyFirstPoinAsync(List<BulletinForRehabilitationDTO> bulletins, BBulletin currentAttachedBull)
        {
            var appliedChanges = false;

            var currentBulletin = bulletins.FirstOrDefault(x => x.Id == currentAttachedBull.Id);
            if (currentBulletin == null || !currentBulletin.DecisionFinalDate.HasValue) return appliedChanges;

            var startDate = currentBulletin.DecisionFinalDate.Value;

            // the person's first bulletin with an estimated date
            // on which the person would potentially receive rehabilitation for that bulletin
            // this is the first condition from the first point
            if (bulletins.Count == 1)
            {
                // get sanctions of the bulletin 
                // if the bulletin does not contain sanctions of this type,
                // point 1 may not apply
                var sanction = bulletins.First().Sanctions.FirstOrDefault(x => x.Type == "nkz_lishavane_ot_svoboda");
                if (sanction == null) return appliedChanges;

                var endDate = startDate.AddYears(sanction.SuspentionDuration.Years ?? 0);
                endDate = endDate.AddMonths(sanction.SuspentionDuration.Months ?? 0);
                endDate = endDate.AddDays(sanction.SuspentionDuration.Days ?? 0);

                await SaveRehabilitationDataAsync(currentAttachedBull, endDate);

                appliedChanges = true;
                return appliedChanges;
            }

            var currentBullOffEndDates = currentBulletin.OffencesEndDates;

            var bulletinWithRehabilitationDate = bulletins
                .Where(x => x.Id != currentAttachedBull.Id && x.RehabilitationDate.HasValue)
                .ToList();

            foreach (var currentBull in bulletinWithRehabilitationDate)
            {
                // todo: start date ?? 
                var removeRehabilitation = currentBullOffEndDates
                    .Any(offEndDate => offEndDate >= currentBull.DecisionFinalDate &&
                                       offEndDate <= currentBull.RehabilitationDate);

                if (removeRehabilitation)
                {
                    // if there is a offence during the conditional release,
                    // the date of rehabilitation for this bulletin is deleted
                    // todo:?? should the status of the bulletin be changed if it has already been proposed for rehabilitation
                    // this is another bulletin not attached to context
                    _rehabilitationRepository.UpdateRehabilitationData(currentBull.Id, null, null);
                    appliedChanges = true;
                }
            }

            await _rehabilitationRepository.SaveChangesAsync();
            return appliedChanges;
        }

        /// <summary>
        /// Apply of point 2 of the rehabilitation rules
        /// </summary>
        /// <param name="bulletins">All bulletin of the person</param>
        /// <param name="currentAttachedBull">Currently edited bulletin attached to the dbContext object</param>
        /// <returns></returns>
        private async Task<bool> ApplySecondPoinAsync(List<BulletinForRehabilitationDTO> bulletins, BBulletin currentAttachedBull)
        {
            var appliedChanges = false;

            // if this is the first bulletin
            // we can calculate data on rehabilitation
            if (bulletins.Count == 1)
            {
                var bulletinDTO = bulletins.First();

                if (!bulletinDTO.Decisions.Any(x=> x.Type == "DCH-00-N")) return appliedChanges;

                // todo: би трябвало наказанието да е едно ?? 
                var sanctionLos = bulletinDTO.Sanctions
                    .Where(x => x.Type == "nkz_lishavane_ot_svoboda")
                    .ToList();
                var hasSanctionLos = sanctionLos.Any();
                var losIdInPeriod = hasSanctionLos && sanctionLos
                    .All(x => IsInDurationInYears(x.SuspentionDuration, 3));

                var sanctionProb = bulletinDTO.Sanctions
                    .Where(x => x.Type == "nkz_lprobacia");
                var hasSanctionProd = sanctionProb.Any();
                var propbIsInPeriod = hasSanctionProd && sanctionProb
                    .SelectMany(x => x.PropbationDurations)
                    .All(x => IsInDurationInYears(x, 3));

                // first bulletin for the person in which probation OR imprisonment is introduced up to three years
                var isSuccess = (losIdInPeriod && !hasSanctionProd) ||
                                (propbIsInPeriod && !hasSanctionLos);
                if (isSuccess)
                {
                    var endDate = bulletinDTO
                        .Decisions
                        .Where(x => x.Type == "DCH-00-N") // todo: крайн на изтърпяването на наказанието или помилване
                        .OrderBy(x => x.ChangeDate)
                        .FirstOrDefault()?.ChangeDate;

                    if (endDate != null)
                    {
                        endDate = endDate.Value.AddYears(3);
                        await SaveRehabilitationDataAsync(currentAttachedBull, endDate);
                        appliedChanges = true;
                    }
                }
            }

            return appliedChanges;
        }

        #region Helpers

        private async Task SaveRehabilitationDataAsync(BBulletin bulletin, DateTime? rehabilitationDate)
        {
            var status = rehabilitationDate <= DateTime.UtcNow ? BulletinConstants.Status.ForRehabilitation : null;// todo: ?

            // this entity is attached to context
            bulletin.RehabilitationDate = rehabilitationDate;
            if (!string.IsNullOrEmpty(status))
            {
                var satusHistory = new BBulletinStatusH
                {
                    Id = Guid.NewGuid().ToString(),
                    BulletinId = bulletin.Id,
                    OldStatusCode = bulletin.StatusId,
                    NewStatusCode = status,
                    EntityState = EntityStateEnum.Added,
                    CreatedOn = DateTime.UtcNow,
                };

                dbContext.ApplyChanges(satusHistory, new List<IBaseIdEntity>());
                bulletin.StatusId = status;
            }

            await _rehabilitationRepository.SaveChangesAsync();
        }

        private static bool IsInDurationInYears(Duration duration, int maxYear)
        {
            var startDate = new DateTime(1, 1, 1);
            var endDate = startDate;
            endDate = endDate.AddYears(duration.Years ?? 0);
            endDate = endDate.AddMonths(duration.Months ?? 0);
            endDate = endDate.AddDays(duration.Days ?? 0);
            endDate = endDate.AddHours(duration.Hours ?? 0);

            var durationDate = new DateTime((endDate - startDate).Ticks);
            var years = durationDate.Year - 1; // new date is 01.01.000
            var month = durationDate.Month - 1;
            var day = durationDate.Day - 1;
            var hour = durationDate.Hour - 1;

            if (years < maxYear) return true;

            if (years == maxYear && month == -1 && day == -1 && hour == -1) return true;

            return false;
        }

        #endregion
    }
}