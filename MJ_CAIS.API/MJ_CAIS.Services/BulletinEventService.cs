using AutoMapper;
using Microsoft.AspNet.OData.Query;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.Exceptions;
using MJ_CAIS.Common.Resources;
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
        protected override bool IsChildRecord(string aId, List<string> aParentsList) => false;

        public BulletinEventService(IMapper mapper, IBulletinEventRepository bulletinEventRepository)
            : base(mapper, bulletinEventRepository)
        {
            _bulletinEventRepository = bulletinEventRepository;
        }

        public virtual async Task<IgPageResult<BulletinEventGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinEventGridDTO> aQueryOptions, string groupCode, string? statusId, string? bulletinId)
        {
            var baseQuery = await _bulletinEventRepository.SelectAllByTypeAsync(groupCode, statusId, bulletinId);
            var resultQuery = await this.ApplyOData(baseQuery, aQueryOptions);
            var pageResult = new IgPageResult<BulletinEventGridDTO>();
            this.PopulatePageResultAsync(pageResult, aQueryOptions, baseQuery, resultQuery);
            return pageResult;
        }

        public async Task ChangeStatusAsync(string aInDto, string statusId)
        {
            var bulletinEvent = await _bulletinEventRepository.SelectAsync(aInDto);
               // await dbContext.BBulEvents
               //.FirstOrDefaultAsync(x => x.Id == aInDto);

            if (bulletinEvent == null)
                throw new BusinessLogicException(string.Format(BusinessLogicExceptionResources.bulletinDoesNotExist, aInDto));

            bulletinEvent.StatusCode = statusId;
            bulletinEvent.EntityState = EntityStateEnum.Modified;
            bulletinEvent.ModifiedProperties = new List<string>
            {
                nameof(bulletinEvent.StatusCode),
                nameof(bulletinEvent.Version),
            };
            //todo: ���� �� � saveEntity?!
            await _bulletinEventRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Check for events
        /// http://tfstl:8080/tfs/DefaultCollection/MJ-CAIS/_workitems/edit/45537
        /// </summary>
        /// <param name="currentAttachedBulletin">Updated bulletin attached to the context</param>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        public async Task GenerateEventWhenUpdateBullAsync(BBulletin currentAttachedBulletin, string personId)
        {
            bool existingEvents = _bulletinEventRepository.GetExistingEvents(currentAttachedBulletin);
            if (existingEvents) return;

            var bulletinsQuery = _bulletinEventRepository.GetBulletinsByPersonId(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // if person has one bulletin 
            // the event is not applicable
            if (bulletins.Count == 1) return;

            currentAttachedBulletin.BBulEvents = new List<BBulEvent>();
            CheckForArticle2212(bulletins, currentAttachedBulletin);
        }

 

        /// <summary>
        /// Check for events
        /// http://tfstl:8080/tfs/DefaultCollection/MJ-CAIS/_workitems/edit/45537
        /// </summary>
        /// <param name="currentAttachedBulletin">Updated bulletin attached to the context</param>
        /// <param name="personId">Person identifier</param>
        /// <returns></returns>
        public async Task GenerateEventWhenChangeStatusOfBullAsync(BBulletin currentAttachedBulletin, string personId)
        {
            //var existingEvents = dbContext.BBulEvents
            //                    .AsNoTracking()
            //                    .Where(x => x.BulletinId == currentAttachedBulletin.Id)
            //                    .GroupBy(x => x.EventType)
            //                    .Select(x => new
            //                    {
            //                        Type = x.Key,
            //                        Any = x.Any()
            //                    });

            var existingEvents = _bulletinEventRepository.GetExistingEventsByType(currentAttachedBulletin);

            currentAttachedBulletin.BBulEvents = new List<BBulEvent>();

            var article2211 = existingEvents.FirstOrDefault(x => x.Type == BulletinEventConstants.Type.Article2211);
            var article3000 = existingEvents.FirstOrDefault(x => x.Type == BulletinEventConstants.Type.Article3000);

            var checkForEvent = article2211 == null || !article2211.Any || article3000 == null || !article3000.Any;
            if (!checkForEvent) return;

            var bulletinsQuery = _bulletinEventRepository.GetBulletinsByPersonId(personId);
            var bulletins = await bulletinsQuery.ToListAsync();

            // if person has one bulletin 
            // the event is not applicable
            if (bulletins.Count == 1) return;

            if (article2211 == null || !article2211.Any)
            {
                CheckForArticle2211(bulletins, currentAttachedBulletin);
            }

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
        private async void CheckForArticle2211(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            // PrevSuspSent must be false
            if (currentBulletin.PrevSuspSent == true) return;

            var currentBullOffencesEndDates = await _bulletinEventRepository.GetOffencesEndDatesByBulletinId(currentBulletin.Id).ToListAsync();
            if (currentBullOffencesEndDates == null || !currentBullOffencesEndDates.Any()) return;

            var anotherBulletins = bulletins.Where(x => x.Id != currentBulletin.Id);

            var allSanctionByBulletins = _bulletinEventRepository.GetSanctionsSuspentionByBulletinId(bulletins.Select(x => x.Id).ToList());
            // has sanction of type
            var bulletinWithSanctionOfTypeLos = allSanctionByBulletins
                .Where(x => x.Type == SanctionType.Imprisonment)
                .ToList();

            if (bulletinWithSanctionOfTypeLos.Count == 0) return;

            var mustAddEvent = false;

            foreach (var bullWithSanc in bulletinWithSanctionOfTypeLos)
            {
                // we cannot calculate the period
                var periodStart = bulletins.FirstOrDefault(x => x.Id == bullWithSanc.BulletinId)?.DecisionDate;
                if (!periodStart.HasValue) continue;

                var periodStartVal = periodStart.Value;
                var periodEndDate = periodStartVal;
                periodEndDate = periodEndDate.AddHours(bullWithSanc.SuspentionDurationHours ?? 0);
                periodEndDate = periodEndDate.AddDays(bullWithSanc.SuspentionDurationDays ?? 0);
                periodEndDate = periodEndDate.AddMonths(bullWithSanc.SuspentionDurationMonths ?? 0);
                periodEndDate = periodEndDate.AddYears(bullWithSanc.SuspentionDurationYears ?? 0);

                var isInPeriod = currentBullOffencesEndDates.Any(x => x >= periodStart && x <= periodEndDate);

                if (isInPeriod)
                {
                    mustAddEvent = true;
                }
            }

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, BulletinEventConstants.Type.Article2211);
        }

        /// <summary>
        /// Is a "No Sanction" bulletin introduced for the second time
        /// </summary>
        private static void CheckForArticle2212(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            var mustAddEvent = currentBulletin.StatusId == Status.NoSanction &&
                bulletins.Any(x => x.Id != currentBulletin.Id && x.StatusId == Status.NoSanction);

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, BulletinEventConstants.Type.Article2212);
        }

        private static void CheckForArticle3000(List<BulletinSancttionsEventDTO> bulletins, BBulletin currentBulletin)
        {
            var mustAddEvent = currentBulletin.BulletinType == BulletinConstants.Type.Bulletin78A &&
                bulletins.Any(x => x.Id != currentBulletin.Id &&
                (x.BulletinType == BulletinConstants.Type.Bulletin78A || x.CaseType == CaseType.NOXD));

            if (!mustAddEvent) return;

            AddEventToBulletin(currentBulletin, BulletinEventConstants.Type.Article3000);
        }

        private static void AddEventToBulletin(BBulletin currentBulletin, string eventType)
        {
            //todo: ���� �� �� � � ���� - ����� EntityState?!
            currentBulletin.BBulEvents.Add(new BBulEvent
            {
                BulletinId = currentBulletin.Id,
                Id = BaseEntity.GenerateNewId(),
                StatusCode = BulletinEventConstants.Status.New,
                EventType = eventType,
                EntityState = EntityStateEnum.Added
            });
        }
    }
}
