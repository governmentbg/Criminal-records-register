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
                nameof(bulletinEvent.StatusCode)
            };

            await dbContext.SaveChangesAsync();
        }

        public async Task GenereteEventAsyn(string personId)
        {
            var bulletinsQuery = await _bulletinEventRepository.GetBulletinByPersonIdAsync(personId);
            var bulletins = await bulletinsQuery.ToListAsync();
            var uniqueBulletins = bulletins.GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).ToList();

            // if person has one or zero bulletin 
            // the event is not applicable
            if (uniqueBulletins.Count < 2) return;

            //nkz_lishavane_ot_svoboda
            var bulletinWithSanctionOfTypeLos = bulletins
                .Where(x => x.Sanctions
                .Any(x => x.Type == "nkz_lishavane_ot_svoboda"))
                .ToList();

            // todo: какво се случва ако имаме повече от един бюлетин
            // в който има наказание лишаване от свобода 
            if (bulletinWithSanctionOfTypeLos.Count != 1) return;

            var bulletin = bulletinWithSanctionOfTypeLos.First();

            //todo: възможно ли е да има повече от едно наказание в бюлетин
            // което да е от тип ЛОС
            var sanction = bulletin.Sanctions.FirstOrDefault(x => x.Type == "nkz_lishavane_ot_svoboda");

            if (!bulletin.DecisionDate.HasValue) return;

            // крайната дата преди която трябва да е вписан бюлетин
            // в който да не е чекнат Постановено изтърпяване на предходна условна присъда
            var periodEndDate = bulletin.DecisionDate.Value;
            periodEndDate = periodEndDate.AddHours(sanction.DecisionDurationHours ?? 0);
            periodEndDate = periodEndDate.AddDays(sanction.DecisionDurationDays ?? 0);
            periodEndDate = periodEndDate.AddMonths(sanction.DecisionDurationMonths ?? 0);
            periodEndDate = periodEndDate.AddYears(sanction.DecisionDurationYears ?? 0);

            // Има бюлетин в периода на изпитателния срок на предходния бюлетин и не е маркирано
            // "Постановено изтърпяване на предходна условна присъда" в осъждането
            var bulletinForEvents = uniqueBulletins.Where(x => x.DecisionDate.HasValue && x.DecisionDate.Value >= bulletin.DecisionDate &&
            x.DecisionDate.Value <= periodEndDate && x.Id != bulletin.Id &&
             (!x.PrevSuspSent.HasValue || x.PrevSuspSent == false));

            var bulletinIds = uniqueBulletins.Select(x => x.Id).ToList();

            var existingEvents = await dbContext.BBulEvents
                .AsNoTracking()
                .Where(x => bulletinIds.Contains(x.BulletinId) && x.EventType == "Article2211")
                .Select(x => x.BulletinId)
                .ToListAsync();

            var events = new List<BBulEvent>();
            foreach (var bullId in bulletinIds)
            {
                // вече има добавен евент или това е бюлетина с наказанието
                if (existingEvents.Any(x => x == bullId) || bullId == bulletin.Id)
                {
                    continue;
                }

                events.Add(new BBulEvent
                {
                    BulletinId = bullId,
                    Id = BaseEntity.GenerateNewId(),
                    CreatedOn = DateTime.UtcNow,
                    StatusCode = "New",
                    EventType = "Article2211",
                    EntityState = EntityStateEnum.Added
                });
            }

            dbContext.BBulEvents.AddRange(events);
            await dbContext.SaveChangesAsync();
        }


        protected override bool IsChildRecord(string aId, List<string> aParentsList)
        {
            return false;
        }
    }
}
