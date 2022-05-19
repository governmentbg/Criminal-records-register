using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace MJ_CAIS.Services
{
    public class RehabilitationService<TContext> : IRehabilitationService
                where TContext : DbContext
    {
        protected IRehabilitationRepository<TContext> _rehabilitationRepository;

        public RehabilitationService(IRehabilitationRepository<TContext> rehabilitationRepository)
        {
            _rehabilitationRepository = rehabilitationRepository;
        }

        /// <summary>
        /// Algorithm for rehabilitation
        /// https://bg.wikipedia.org/wiki/%D0%A0%D0%B5%D0%B0%D0%B1%D0%B8%D0%BB%D0%B8%D1%82%D0%B0%D1%86%D0%B8%D1%8F
        /// </summary>
        public async Task ApplyRehabilitation(string bulletinId, string personId)
        {
            var bulletinsQuery = await _rehabilitationRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // 1
            if (bulletins.Count == 1)
            {
                var bulletin = bulletins.First();
                var sanction = bulletins.First().Sanctions.FirstOrDefault(x => x.Type == "nkz_lishavane_ot_svoboda");
                if (sanction != null)
                {
                    var startDate = bulletin.DecisionFinalDate.Value;// todo: ако няма дата
                    var endDate = startDate.AddYears(sanction.SuspentionDurationYears ?? 0);
                    endDate = endDate.AddMonths(sanction.SuspentionDurationMonths ?? 0);
                    endDate = endDate.AddDays(sanction.SuspentionDurationDays ?? 0);

                    var changeStatus = endDate >= DateTime.UtcNow;
                    await _rehabilitationRepository.UpdateForRehabilitationAsync(bulletinId, endDate, changeStatus);
                }
            }
        }
    }
}