using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DTO.Home;

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
            var query = (from bulletin in _dbContext.BBulletins.AsNoTracking()

                         join bulletinPersonId in _dbContext.PBulletinIds.AsNoTracking() on bulletin.Id equals bulletinPersonId.BulletinId
                                   into bulletinPersonLeft
                         from bulletinPersonId in bulletinPersonLeft.DefaultIfEmpty()

                         join personIds in _dbContext.PPersonIds.AsNoTracking() on bulletinPersonId.PersonId equals personIds.Id
                                     into personIdsLeft
                         from personIds in personIdsLeft.DefaultIfEmpty()

                         where personIds.PersonId == personId && (bulletin.StatusId == BulletinConstants.Status.Active ||
                                bulletin.StatusId == BulletinConstants.Status.ForRehabilitation || bulletin.StatusId == BulletinConstants.Status.NoSanction)
                         select
                        new BulletinSancttionsEventDTO
                        {
                            Id = bulletin.Id,
                            DecisionDate = bulletin.DecisionDate,
                            PrevSuspSent = bulletin.PrevSuspSent,
                            StatusId = bulletin.StatusId,
                            Version = bulletin.Version,
                            BulletinType = bulletin.BulletinType,
                            CaseType = bulletin.CaseTypeId,
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
                        }).GroupBy(x => x.Id).Select(x => x.FirstOrDefault());

            return await Task.FromResult(query);
        }

        public async Task<string> GetPersonIdByBulletinIdAsync(string bulleintId)
        {
            var result = await _dbContext.PBulletinIds.AsNoTracking()
                        .Include(x => x.Person)
                        .FirstOrDefaultAsync(x => x.BulletinId == bulleintId);

            return result?.Person?.PersonId;
        }

        public async Task<IQueryable<ObjectStatusCountDTO>> GetStatusCountAsync()
        {
            var query = _dbContext.BBulEvents.AsNoTracking()
                .Where(x=>x.StatusCode == BulletinEventConstants.Status.New)
                .GroupBy(x => x.EventType )
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return await Task.FromResult(query);
        }
    }
}
