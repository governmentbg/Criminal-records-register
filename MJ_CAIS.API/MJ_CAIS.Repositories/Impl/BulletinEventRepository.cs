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
        private readonly IUserContext _userContext;

        public BulletinEventRepository(CaisDbContext dbContext, IUserContext userContext)
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

        public IQueryable<BulletinSancttionsEventDTO> GetBulletinsByPersonId(string personId)
        {
            var query = _dbContext.BBulletins
                   .Include(x => x.EgnNavigation)
                   .Include(x => x.LnchNavigation)
                   .Include(x => x.LnNavigation)
                   .Include(x => x.IdDocNumberNavigation)
                   .Include(x => x.SuidNavigation)
                   .Where(bulletin => bulletin.StatusId == BulletinConstants.Status.Active ||
                                      bulletin.StatusId == BulletinConstants.Status.ForRehabilitation ||
                                      bulletin.StatusId == BulletinConstants.Status.NoSanction)
                   .Where(x => x.EgnNavigation.PersonId == personId ||
                             x.LnchNavigation.PersonId == personId ||
                             x.LnNavigation.PersonId == personId ||
                             x.IdDocNumberNavigation.PersonId == personId ||
                             x.SuidNavigation.PersonId == personId)
                   .Select(bulletin => new BulletinSancttionsEventDTO
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

            return query;
        }

        //public async Task<string> GetPersonIdByBulletinIdAsync(string bulletinId)
        //{
        //    var bulletin = await _dbContext.BBulletins.AsNoTracking()
        //                .Include(x => x.EgnNavigation)
        //                .Include(x => x.LnchNavigation)
        //                .Include(x => x.LnNavigation)
        //                .Include(x => x.IdDocNumberNavigation)
        //                .Include(x => x.SuidNavigation)
        //                .FirstOrDefaultAsync(x => x.Id == bulletinId);

        //    if (!string.IsNullOrEmpty(bulletin.EgnNavigation?.PersonId))
        //    {
        //       return bulletin.EgnNavigation.PersonId;
        //    }
           
        //    if (!string.IsNullOrEmpty(bulletin.LnchNavigation?.PersonId))
        //    {
        //        return bulletin.LnchNavigation.PersonId;
        //    }
           
        //    if (!string.IsNullOrEmpty(bulletin.LnNavigation?.PersonId))
        //    {
        //        return bulletin.LnNavigation.PersonId;
        //    }
            
        //    if (!string.IsNullOrEmpty(bulletin.IdDocNumberNavigation?.PersonId))
        //    {
        //        return bulletin.IdDocNumberNavigation.PersonId;
        //    }
            
        //    if (!string.IsNullOrEmpty(bulletin.SuidNavigation?.PersonId))
        //    {
        //       return bulletin.SuidNavigation.PersonId;
        //    }

        //    return null;
        //}

        public IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority()
        {
            var query = _dbContext.BBulEvents.AsNoTracking()
                .Include(x => x.Bulletin)
                .Where(x => x.Bulletin.CsAuthorityId == _userContext.CsAuthorityId && (x.StatusCode == BulletinEventConstants.Status.New &&
                          (x.EventType == BulletinEventConstants.Type.Article2211 ||
                           x.EventType == BulletinEventConstants.Type.Article2212 ||
                           x.EventType == BulletinEventConstants.Type.Article3000 ||
                           x.EventType == BulletinEventConstants.Type.NewDocument)))
                .GroupBy(x => x.EventType)
                .Select(x => new ObjectStatusCountDTO
                {
                    Status = x.Key,
                    Count = x.Count()
                });

            return query;
        }
    }
}
