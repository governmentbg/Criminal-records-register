using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.BulletinEvent;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinEventRepository : BaseAsyncRepository<BBulEvent, CaisDbContext>, IBulletinEventRepository
    {
        public BulletinEventRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId)
        {
            var query = from bullEvents in _dbContext.BBulEvents.AsNoTracking()
                        join eventTypes in _dbContext.BEventTypes.AsNoTracking() on bullEvents.EventType equals eventTypes.Code
                               into eventTypesLeft
                        from eventType in eventTypesLeft.DefaultIfEmpty()
                        join bulletin in _dbContext.BBulletins.AsNoTracking() on bullEvents.BulletinId equals bulletin.Id
                                into bulletinLeft
                        from bulletin in bulletinLeft.DefaultIfEmpty()

                        join eventStatus in _dbContext.BEventStatuses.AsNoTracking() on bullEvents.StatusCode equals eventStatus.Code
                              into eventStatusLeft
                        from eventStatus in eventStatusLeft.DefaultIfEmpty()

                        where (string.IsNullOrEmpty(statusId) || bullEvents.StatusCode == statusId) &&
                        eventType.GroupCode == groupCode
                        select new BulletinEventGridDTO
                        {
                            Id = bullEvents.Id,
                            EventType = eventType.Name,
                            StatusCode = eventStatus.Code,
                            StatusName = eventStatus.Name,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            BulletinId = bulletin.Id,
                            BirthDate = bulletin.BirthDate,
                            CreatedOn = bullEvents.CreatedOn,
                            Description = bullEvents.Description,
                            Identifier = bulletin.Egn + " / " + bulletin.Lnch + " / " + bulletin.Ln,
                            PersonName = bulletin.Firstname + " " + bulletin.Surname + " " + bulletin.Familyname,
                        };

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<BulletinSancttionsEventDTO>> GetBulletinByPersonIdAsync(string personId)
        {
            var query = from bulletin in _dbContext.BBulletins.AsNoTracking()

                        join bulletinPersonId in _dbContext.PBulletinIds.AsNoTracking() on bulletin.Id equals bulletinPersonId.BulletinId
                                  into bulletinPersonLeft
                        from bulletinPersonId in bulletinPersonLeft.DefaultIfEmpty()

                        join personIds in _dbContext.PPersonIds.AsNoTracking() on bulletinPersonId.PersonId equals personIds.Id
                                    into personIdsLeft
                        from personIds in personIdsLeft.DefaultIfEmpty()

                        where personIds.PersonId == personId
                        select
                        new BulletinSancttionsEventDTO
                        {
                            Id = bulletin.Id,
                            DecisionDate = bulletin.DecisionDate,
                            PrevSuspSent = bulletin.PrevSuspSent,
                            Sanctions = bulletin.BSanctions.Select(x => new SanctionEventDTO
                            {
                                Id = x.Id,
                                SuspentionDurationDays = x.SuspentionDurationDays,
                                SuspentionDurationHours = x.SuspentionDurationHours,
                                SuspentionDurationMonths = x.SuspentionDurationMonths,
                                SuspentionDurationYears = x.SuspentionDurationYears,
                                Type = x.SanctCategoryId
                            }),
                            OffencesEndDates = bulletin.BOffences.Select(x => x.OffEndDate)
                        };

            return await Task.FromResult(query);
        }
    }
}
