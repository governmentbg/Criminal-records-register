using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
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
        /// Algorithm for rehabilitation
        /// https://bg.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B0%D0%B1%D0%B8%D0%BB%D0%B8%D1%82%D0%B0%D1%86%D0%B8%D1%8F
        /// </summary>
        public async Task ApplyRehabilitation(string bulletinId, string personId)
        {
            var bulletinsQuery = await _rehabilitationRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            var firstPoinIsApplyed = await ApplyFirstPoinAsync(bulletins, bulletinId);
            if (firstPoinIsApplyed) return;
        }

        private async Task<bool> ApplyFirstPoinAsync(List<BulletinForRehabilitationDTO> bulletins, string currentBulletinId)
        {
            var isProposedForRehabilitation = false;

            var currentBulletin = bulletins.FirstOrDefault(x => x.Id == currentBulletinId);
            if (currentBulletin == null || !currentBulletin.DecisionFinalDate.HasValue) return isProposedForRehabilitation;

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
                if (sanction == null) return false;

                var endDate = startDate.AddYears(sanction.SuspentionDurationYears ?? 0);
                endDate = endDate.AddMonths(sanction.SuspentionDurationMonths ?? 0);
                endDate = endDate.AddDays(sanction.SuspentionDurationDays ?? 0);

                var status = endDate <= DateTime.UtcNow ? BulletinConstants.Status.ForRehabilitation : null;
                _rehabilitationRepository.UpdateRehabilitationData(currentBulletinId, endDate, status);
                await _rehabilitationRepository.SaveChangesAsync();
                return true;
            }

            var currentBullOffEndDates = currentBulletin.OffencesEndDates;

            var bulletinWithRehabilitationDate = bulletins
                .Where(x => x.Id != currentBulletinId && x.RehabilitationDate.HasValue)
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
                    _rehabilitationRepository.UpdateRehabilitationData(currentBull.Id, null, null);
                }
            }

            await _rehabilitationRepository.SaveChangesAsync();
            return false;
        }

    }
}