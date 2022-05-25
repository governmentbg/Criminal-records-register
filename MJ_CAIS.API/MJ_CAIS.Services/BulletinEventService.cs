using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services.Contracts.Utils;
using static MJ_CAIS.Common.Constants.BulletinConstants;

namespace MJ_CAIS.Services
{
    public class BulletinEventService : BaseAsyncService<BulletinEventDTO, BulletinEventDTO, BulletinEventGridDTO, BBulEvent, string, CaisDbContext>, IBulletinEventService
    {
        private readonly IBulletinEventRepository _bulletinEventRepository;

        public BulletinEventService(IMapper mapper, IBulletinEventRepository bulletinEventRepository)
            : base(mapper, bulletinEventRepository)
        {
            _bulletinEventRepository = bulletinEventRepository;
        }

        public virtual async Task<IgPageResult<BulletinEventGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinEventGridDTO> aQueryOptions, string groupCode, string? statusId)
        {
            var baseQuery = await _bulletinEventRepository.SelectAllByTypeAsync(groupCode, statusId);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinEventGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var bulletinEvent = await dbContext.BBulEvents
               .FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletinEvent == null)
                throw new ArgumentException($"Bulletin event with id: {aInDto} is missing");

            bulletinEvent.StatusCode = statusId;
            bulletinEvent.EntityState = EntityStateEnum.Modified;
            bulletinEvent.ModifiedProperties = new List<string>
            {
                nameof(bulletinEvent.StatusCode),
                nameof(bulletinEvent.Version),
            };

            await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Check for events
        /// http://tfstl:8080/tfs/DefaultCollection/MJ-CAIS/_workitems/edit/45537
        /// </summary>
        /// <param name="currentAttachedBulletin">Updated bulletin attached to the context</param>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        public async Task GenereteEventWhenUpdateBullAsyn(BBulletin currentAttachedBulletin)
        {
            var personId = await _bulletinEventRepository.GetPersonIdByBulletinIdAsync(currentAttachedBulletin.Id);
            if (string.IsNullOrEmpty(personId)) return;

            var bulletinsQuery = await _bulletinEventRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // if person has one bulletin 
            // the event is not applicable
            if (bulletins.Count == 1) return;

            var existingEvents = dbContext.BBulEvents
                                .AsNoTracking()
                                .Any(x => x.BulletinId == currentAttachedBulletin.Id && x.EventType == EventType.Article2212);

            currentAttachedBulletin.BBulEvents = new List<BBulEvent>();

            if (existingEvents) return;

            CheckForArticle2212(bulletins, currentAttachedBulletin);
        }

        /// <summary>
        /// Check for events
        /// http://tfstl:8080/tfs/DefaultCollection/MJ-CAIS/_workitems/edit/45537
        /// </summary>
        /// <param name="currentAttachedBulletin">Updated bulletin attached to the context</param>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        public async Task GenereteEventWhenChangeStatusOfBullAsyn(BBulletin currentAttachedBulletin, string personId)
        {
            var bulletinsQuery = await _bulletinEventRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // if person has one bulletin 
            // the event is not applicable
            if (bulletins.Count == 1) return;

            var existingEvents = dbContext.BBulEvents
                                .AsNoTracking()
                                .Where(x => x.BulletinId == currentAttachedBulletin.Id)
                                .GroupBy(x => x.EventType)
                                .Select(x => new
                                {
                                    Type = x.Key,
                                    Any = x.Any()
                                });

            currentAttachedBulletin.BBulEvents = new List<BBulEvent>();

            var article2211 = existingEvents.FirstOrDefault(x => x.Type == EventType.Article2211);
            if (article2211 == null || !article2211.Any)
            {
                CheckForArticle2211(bulletins, currentAttachedBulletin);
            }

            var article3000 = existingEvents.FirstOrDefault(x => x.Type == EventType.Article3000);
            if (article3000 == null || !article3000.Any)
            {
                CheckForArticle3000(bulletins, currentAttachedBulletin);
            }
        }

        /// <summary>
        /// Current bulletin must has at least one offences with end date and PrevSuspSent must be false.
        /// Another bulletin must has sanction of type nkz_lishavane_ot_svoboda with date in offence period 
        /// </summary>
        /// <param name="bulletins">All bulletins of the person</param>
        /// <param name="currentBulletin">Updated bulletin attached to the context</param>
        private static void CheckForArticle2211(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            // PrevSuspSent must be false
            if (currentBulletin.PrevSuspSent == true) return;

            var currentBullOffencesEndDates = bulletins.FirstOrDefault(x => x.Id == currentBulletin.Id)?.OffencesEndDates;
            if (currentBullOffencesEndDates == null || !currentBullOffencesEndDates.Any()) return;

            var anotherBulletins = bulletins.Where(x => x.Id != currentBulletin.Id);

            // has sanction of type
            var bulletinWithSanctionOfTypeLos = anotherBulletins
                .Where(x => x.Sanctions
                .Any(x => x.Type == SanctionType.Imprisonment))
                .ToList();

            if (bulletinWithSanctionOfTypeLos.Count == 0) return;

            var mustAddEvent = false;

            foreach (var bullWithSanc in bulletinWithSanctionOfTypeLos)
            {
                // we cannot calculate the period
                if (!bullWithSanc.DecisionDate.HasValue) continue;

                var periodStart = bullWithSanc.DecisionDate.Value;

                foreach (var sanction in bullWithSanc.Sanctions)
                {
                    var periodEndDate = periodStart;
                    periodEndDate = periodEndDate.AddHours(sanction.SuspentionDurationHours ?? 0);
                    periodEndDate = periodEndDate.AddDays(sanction.SuspentionDurationDays ?? 0);
                    periodEndDate = periodEndDate.AddMonths(sanction.SuspentionDurationMonths ?? 0);
                    periodEndDate = periodEndDate.AddYears(sanction.SuspentionDurationYears ?? 0);

                    var isInPeriod = currentBullOffencesEndDates.Any(x => x >= periodStart && x <= periodEndDate);

                    if (isInPeriod)
                    {
                        mustAddEvent = true;
                    }
                }
            }

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, EventType.Article2211);
        }

        /// <summary>
        /// Is a "No Sanction" bulletin introduced for the second time
        /// </summary>
        private static void CheckForArticle2212(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            var mustAddEvent = currentBulletin.StatusId == Status.NoSanction &&
                bulletins.Any(x => x.Id != currentBulletin.Id && x.StatusId == Status.NoSanction);

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, EventType.Article2212);
        }

        private static void CheckForArticle3000(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            var mustAddEvent = currentBulletin.BulletinType == nameof(Common.Constants.BulletinConstants.Type.Bulletin78A)  &&
                bulletins.Any(x => x.Id != currentBulletin.Id &&
                (x.BulletinType == nameof(Common.Constants.BulletinConstants.Type.Bulletin78A) || x.CaseType == CaseType.NOXD));

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, EventType.Article3000);
        }

        private static void AddEventToBulletin(BBulletin currentBulletin, string eventType)
        {
            currentBulletin.BBulEvents.Add(new BBulEvent
            {
                BulletinId = currentBulletin.Id,
                Id = BaseEntity.GenerateNewId(),
                StatusCode = EventStatusType.New,
                EventType = eventType,
                EntityState = EntityStateEnum.Added
            });
        }

        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
