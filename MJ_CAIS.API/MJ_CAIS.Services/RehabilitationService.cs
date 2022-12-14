using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.ExternalServicesHost;
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
        /// When change status from NewOffice/NewEISS to Active
        /// Applying changes of bulletin, without calling dbContext => SaveChanges
        /// Algorithm for rehabilitation
        /// https://bg.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B0%D0%B1%D0%B8%D0%BB%D0%B8%D1%82%D0%B0%D1%86%D0%B8%D1%8F
        /// </summary>
        public void ApplyRehabilitationData(BBulletin currentAttachedBull, List<BulletinForRehabilitationAndEventDTO> allPersonBulletins)
        {
            var isFirstPointApplied = ApplyFirstPoint(currentAttachedBull, allPersonBulletins);
            if (isFirstPointApplied) return;

            var isSecondPointApplied = ApplySecondPoint(currentAttachedBull, allPersonBulletins);
            if (isSecondPointApplied) return;

            var isThirdPointApplied = ApplyThirdPoint(currentAttachedBull, allPersonBulletins);
            if (isThirdPointApplied) return;

            var isFourthPointApplied = ApplyFourthPoint(currentAttachedBull, allPersonBulletins);
            if (isFourthPointApplied) return;
        }

        private bool ApplyFirstPoint(BBulletin currentAttachedBull, List<BulletinForRehabilitationAndEventDTO> allPersonBulletins)
        {
            // we cannot  apply first point
            if (!currentAttachedBull.DecisionFinalDate.HasValue) return false;
            var appliedChanges = false;

            // the person's first bulletin with an estimated date
            // on which the person would potentially receive rehabilitation for that bulletin
            // this is the first condition from the first point
            if (allPersonBulletins.Count == 0) // person has only one bulletin
            {
                var startDate = currentAttachedBull.DecisionFinalDate.Value;

                // get sanctions of the bulletin 
                // if the bulletin does not contain sanctions of this type,
                // point 1 may not apply
                var sanction = currentAttachedBull.BSanctions.FirstOrDefault(x => x.SanctCategoryId == BulletinConstants.SanctionType.Imprisonment);
                if (sanction == null) return appliedChanges;

                var endDate = startDate.AddYears(sanction.SuspentionDurationYears ?? 0);
                endDate = endDate.AddMonths(sanction.SuspentionDurationMonths ?? 0);
                endDate = endDate.AddDays(sanction.SuspentionDurationDays ?? 0);

                SetRehabilitationData(currentAttachedBull, endDate);

                appliedChanges = true;
                return appliedChanges;
            }

            var currentBullOffEndDates = currentAttachedBull.BOffences.Select(x => x.OffEndDate);

            var bulletinWithRehabilitationDate = allPersonBulletins
                .Where(x => x.RehabilitationDate.HasValue)
                .ToList();

            foreach (var currentBull in bulletinWithRehabilitationDate)
            {
                var currentBullStartDate = currentAttachedBull.DecisionFinalDate.Value;

                var sanction = currentAttachedBull.BSanctions.FirstOrDefault(x => x.SanctCategoryId == BulletinConstants.SanctionType.Imprisonment);
                if (sanction == null) continue;

                var currentBullEndDate = currentBullStartDate.AddYears(sanction.SuspentionDurationYears ?? 0);
                currentBullEndDate = currentBullEndDate.AddMonths(sanction.SuspentionDurationMonths ?? 0);
                currentBullEndDate = currentBullEndDate.AddDays(sanction.SuspentionDurationDays ?? 0);

                var removeRehabilitation = currentBullOffEndDates
                    .Any(offEndDate => offEndDate >= currentBullStartDate &&
                                       offEndDate <= currentBullEndDate);

                if (removeRehabilitation)
                {
                    // if there is a offence during the conditional release,
                    // the date of rehabilitation for this bulletin is deleted
                    // todo:?? should the status of the bulletin be changed if it has already been proposed for rehabilitation
                    // this is another bulletin not attached to context
                    _rehabilitationRepository.UpdateRehabilitationData(currentBull.Id, currentBull.Version, null);
                    appliedChanges = true;
                }
            }

            return appliedChanges;
        }

        /// <summary>
        /// Apply of point 2 of the rehabilitation rules
        /// </summary>
        /// <param name="bulletins">All bulletin of the person</param>
        /// <param name="currentAttachedBull">Currently edited bulletin attached to the dbContext object</param>
        /// <returns></returns>
        private bool ApplySecondPoint(BBulletin currentAttachedBull, List<BulletinForRehabilitationAndEventDTO> allPersonBulletins)
        {
            var appliedChanges = false;

            // if this is the first bulletin
            // we can calculate data on rehabilitation
            if (allPersonBulletins.Count == 0)
            {
                if (!currentAttachedBull.BDecisions.Any(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty)) return appliedChanges;

                // todo: 
                var sanctionLos = currentAttachedBull.BSanctions
                    .FirstOrDefault(x => x.SanctCategoryId == BulletinConstants.SanctionType.Imprisonment);

                var losIdInPeriod = sanctionLos != null && IsInDurationInYears(new DurationDTO
                {
                    Years = sanctionLos.SuspentionDurationYears,
                    Months = sanctionLos.SuspentionDurationMonths,
                    Days = sanctionLos.SuspentionDurationDays,
                }, 3);

                var sanctionProb = currentAttachedBull.BSanctions.FirstOrDefault(x => x.SanctCategoryId == BulletinConstants.SanctionType.Probation);

                var sanctionProbDuration = sanctionProb != null ? sanctionProb.BProbations.Select(p => new DurationDTO
                {
                    Years = p.DecisionDurationYears,
                    Months = p.DecisionDurationMonths,
                    Days = p.DecisionDurationDays,
                    Hours = p.DecisionDurationHours,
                }) : new List<DurationDTO>();

                var propbIsInPeriod = sanctionProbDuration.All(x => IsInDurationInYears(x, 3));

                // first bulletin for the person in which probation OR imprisonment is introduced up to three years
                var isSuccess = (losIdInPeriod && sanctionProb == null) ||
                                (propbIsInPeriod && sanctionLos == null);
                if (isSuccess)
                {
                    var endDate = currentAttachedBull
                        .BDecisions
                        .Where(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty ||
                                    x.DecisionChTypeId == BulletinConstants.DecisionType.Pardon)
                        .OrderBy(x => x.ChangeDate)
                        .FirstOrDefault()?.ChangeDate;

                    if (endDate != null)
                    {
                        endDate = endDate.Value.AddYears(3);

                        SetRehabilitationData(currentAttachedBull, endDate);
                        appliedChanges = true;
                    }
                }

                return appliedChanges;
            }

            var bulletinWithRehabilitationDate = allPersonBulletins
                .Where(x => x.RehabilitationDate.HasValue)
                .ToList();

            var hasSanctionOfType = allPersonBulletins.SelectMany(x => x.Sanctions)
                   .Any(s => s.Type == BulletinConstants.SanctionType.LifeImprisonment ||
                             s.Type == BulletinConstants.SanctionType.LifeImprisonmentWithoutParole ||
                             s.Type == BulletinConstants.SanctionType.Imprisonment);

            // offences end date
            var currentBullOffEndDates = currentAttachedBull.BOffences.Select(x => x.OffEndDate);

            foreach (var bull in allPersonBulletins)
            {
                var hasOffencInPeriod = currentBullOffEndDates
                 .Any(offEndDate => offEndDate >= bull.DecisionFinalDate &&
                                    offEndDate <= bull.RehabilitationDate);

                if (hasSanctionOfType && hasOffencInPeriod && bull.RehabilitationDate.HasValue)
                {
                    _rehabilitationRepository.UpdateRehabilitationData(bull.Id, bull.Version, null);
                    appliedChanges = true;
                }
            }

            return appliedChanges;
        }

        /// <summary>
        /// Apply of point 3 of the rehabilitation rules
        /// </summary>
        /// <param name="bulletins">All bulletin of the person</param>
        /// <param name="currentAttachedBull">Currently edited bulletin attached to the dbContext object</param>
        /// <returns></returns>
        private bool ApplyThirdPoint(BBulletin currentAttachedBull, List<BulletinForRehabilitationAndEventDTO> allPersonBulletins)
        {
            var appliedChanges = false;

            // if this is the first bulletin
            // we can calculate data on rehabilitation
            if (allPersonBulletins.Count == 0)
            {

                if (!currentAttachedBull.BDecisions.Any(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty)) return appliedChanges;

                var sanctionOfType = currentAttachedBull.BSanctions
                    .FirstOrDefault(x => x.SanctCategoryId == BulletinConstants.SanctionType.Fine ||
                                         x.SanctCategoryId == BulletinConstants.SanctionType.PublicDisfavor ||
                                         x.SanctCategoryId == BulletinConstants.SanctionType.DisqualificationPosition ||
                                         x.SanctCategoryId == BulletinConstants.SanctionType.DisqualificationProfession ||
                                         x.SanctCategoryId == BulletinConstants.SanctionType.DisqualificationPlace ||
                                         x.SanctCategoryId == BulletinConstants.SanctionType.DisqualificationMedal);

                if (sanctionOfType == null) return appliedChanges;

                var endDate = currentAttachedBull
                    .BDecisions
                    .Where(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty).MinBy(x => x.ChangeDate)?.ChangeDate;

                if (endDate != null)
                {
                    endDate = endDate.Value.AddYears(1);

                    SetRehabilitationData(currentAttachedBull, endDate);
                    appliedChanges = true;
                }

                return appliedChanges;
            }

            if (currentAttachedBull.CaseTypeId != BulletinConstants.CaseType.NOXD || !currentAttachedBull.BOffences.Any(x => x.OffEndDate.HasValue)) return appliedChanges;

            var bulletinWithRehabilitationDate = allPersonBulletins
                .Where(x => x.RehabilitationDate.HasValue)
                .ToList();

            foreach (var bull in bulletinWithRehabilitationDate)
            {
                // this is start date
                var decisionEndDate = bull
                    .Decisions
                    .Where(x => x.Type == BulletinConstants.DecisionType.EndOfPenalty).MinBy(x => x.ChangeDate)?.ChangeDate;

                if (!decisionEndDate.HasValue) continue;

                var endDate = decisionEndDate.Value.AddYears(1);

                var hasOffenceInPeriod = currentAttachedBull.BOffences.Select(x => x.OffEndDate)
                 .Any(d => d >= decisionEndDate && d <= endDate);

                if (hasOffenceInPeriod)
                {
                    _rehabilitationRepository.UpdateRehabilitationData(bull.Id, bull.Version, null);
                    appliedChanges = true;
                }
            }

            return appliedChanges;
        }

        /// <summary>
        /// Apply of point 4 of the rehabilitation rules
        /// </summary>
        /// <param name="bulletins">All bulletin of the person</param>
        /// <param name="currentAttachedBull">Currently edited bulletin attached to the dbContext object</param>
        /// <returns></returns>
        private bool ApplyFourthPoint(BBulletin currentAttachedBull, List<BulletinForRehabilitationAndEventDTO> allPersonBulletins)
        {
            var appliedChanges = false;

            // if this is the first bulletin
            // we can calculate data on rehabilitation
            if (allPersonBulletins.Count == 0)
            {
                if (!currentAttachedBull.BDecisions.Any(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty)) return appliedChanges;

                // todo?? more then one 
                var offenceEndDate = currentAttachedBull.BOffences.FirstOrDefault()?.OffEndDate;

                if (!offenceEndDate.HasValue || !currentAttachedBull.BirthDate.HasValue) return appliedChanges;

                int age = new DateTime((offenceEndDate.Value - currentAttachedBull.BirthDate.Value).Ticks).Year;

                if (age > 18) return appliedChanges;

                var endDate = currentAttachedBull
                    .BDecisions
                    .Where(x => x.DecisionChTypeId == BulletinConstants.DecisionType.EndOfPenalty && x.ChangeDate.HasValue)
                    .MinBy(x => x.ChangeDate)?.ChangeDate;

                if (!endDate.HasValue) return appliedChanges;

                endDate = endDate.Value.AddYears(2);

                SetRehabilitationData(currentAttachedBull, endDate);
                appliedChanges = true;

                return appliedChanges;
            }

            var check = currentAttachedBull.CaseTypeId != BulletinConstants.CaseType.NOXD ||
                !currentAttachedBull.BOffences.Any(x => x.OffEndDate.HasValue) ||
                !currentAttachedBull.BSanctions.Any(x => x.SanctCategoryId == BulletinConstants.SanctionType.Imprisonment);

            if (!check) return appliedChanges;

            var bulletinWithRehabilitationDate = allPersonBulletins
                .Where(x => x.RehabilitationDate.HasValue)
                .ToList();

            foreach (var bull in bulletinWithRehabilitationDate)
            {
                // this is start date
                var decisionEndDate = bull
                    .Decisions
                    .Where(x => x.Type == BulletinConstants.DecisionType.EndOfPenalty)
                    .MinBy(x => x.ChangeDate)?.ChangeDate;

                if (!decisionEndDate.HasValue) continue;

                var endDate = decisionEndDate.Value.AddYears(1);

                var hasOffenceInPeriod = currentAttachedBull.BOffences.Select(x => x.OffEndDate)
                 .Any(d => d >= decisionEndDate && d <= endDate);

                if (hasOffenceInPeriod)
                {
                    _rehabilitationRepository.UpdateRehabilitationData(bull.Id, bull.Version, null);
                    appliedChanges = true;
                }
            }

            return appliedChanges;
        }

        #region Helpers

        private void SetRehabilitationData(BBulletin bulletin, DateTime? rehabilitationDate)
        {
            var status = rehabilitationDate <= DateTime.Now ? BulletinConstants.Status.ForRehabilitation : null;// todo: ?

            // this entity is attached to context
            bulletin.RehabilitationDate = rehabilitationDate;
            if (string.IsNullOrEmpty(status)) return;

            var statusHistory = new BBulletinStatusH
            {
                Id = Guid.NewGuid().ToString(),
                BulletinId = bulletin.Id,
                OldStatusCode = bulletin.StatusId,
                NewStatusCode = status,
                EntityState = EntityStateEnum.Added,
                Locked = bulletin.Locked
            };

            var bulletinXmlModel = mapper.Map<BBulletin, BulletinType>(bulletin);
            var xml = XmlUtils.SerializeToXml(bulletinXmlModel);
            statusHistory.Content = xml;
            statusHistory.Version = 1;
            statusHistory.HasContent = true;

            _rehabilitationRepository.ApplyChanges(statusHistory);
            bulletin.StatusId = status;
        }

        private static bool IsInDurationInYears(DurationDTO duration, int maxYear)
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