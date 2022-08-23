using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinEvent;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class BulletinEventRepository : BaseAsyncRepository<BBulEvent, CaisDbContext>, IBulletinEventRepository
    {
        private readonly IUserContext _userContext;

        public BulletinEventRepository(CaisDbContext dbContext,
            IUserContext userContext)
            : base(dbContext)
        {
            _userContext = userContext;
        }

        public async Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string groupCode, string? statusId, string? bulletinId)
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

                        where (string.IsNullOrEmpty(statusId) || bullEvents.StatusCode == statusId) && eventType.GroupCode == groupCode &&
                        ((string.IsNullOrEmpty(bulletinId) && bulletin.CsAuthorityId == _userContext.CsAuthorityId) || bullEvents.BulletinId == bulletinId)
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
                            Version = bulletin.Version
                        };

            return await Task.FromResult(query);
        }

        public IQueryable<DateTime?> GetOffencesEndDatesByBulletinId(string bulletinId)
        {
            var query = _dbContext.BOffences.AsNoTracking()
                .Where(x => x.BulletinId == bulletinId)
                .Select(x => x.OffEndDate);

            return query;
        }

        public IQueryable<SanctionEventDTO> GetSanctionsSuspentionByBulletinId(List<string> bulletinIds)
        {
            var query = _dbContext.BSanctions.AsNoTracking()
                .Where(x => bulletinIds.Contains(x.BulletinId))
                .Select(x => new SanctionEventDTO
                {
                    Id = x.Id,
                    SuspentionDurationDays = x.SuspentionDurationDays,
                    SuspentionDurationHours = x.SuspentionDurationHours,
                    SuspentionDurationMonths = x.SuspentionDurationMonths,
                    SuspentionDurationYears = x.SuspentionDurationYears,
                    Type = x.SanctCategoryId,
                    BulletinId = x.BulletinId
                });

            return query;
        }

        public async Task<bool> GetExistingEventsAsync(string bulletinId)
        {
            return await _dbContext.BBulEvents
                                .AsNoTracking()
                                .AnyAsync(x => x.BulletinId == bulletinId && x.EventType == BulletinEventConstants.Type.Article2212);

        }

        public IQueryable<BuletinEventTypeDTO> GetExistingEventsByType(BBulletin currentAttachedBulletin)
        {
            return _dbContext.BBulEvents
                                .AsNoTracking()
                                .Where(x => x.BulletinId == currentAttachedBulletin.Id)
                                .GroupBy(x => x.EventType)
                                .Select(x => new BuletinEventTypeDTO
                                {
                                    Type = x.Key,
                                    Any = x.Any()
                                });
        }
    }
}
