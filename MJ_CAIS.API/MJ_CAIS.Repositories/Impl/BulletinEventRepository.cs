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

        public async Task<IQueryable<BulletinEventGridDTO>> SelectAllByTypeAsync(string type)
        {
            var query = from bullEvents in _dbContext.BBulEvents.AsNoTracking()
                        join eventTypes in _dbContext.BEventTypes.AsNoTracking() on bullEvents.EventType equals eventTypes.Code
                               into eventTypesLeft
                        from eventType in eventTypesLeft.DefaultIfEmpty()
                        join bulletin in _dbContext.BBulletins.AsNoTracking() on bullEvents.BulletinId equals bulletin.Id
                                into bulletinLeft
                        from bulletin in bulletinLeft.DefaultIfEmpty()
                        where eventType.Code == type
                        select new BulletinEventGridDTO
                        {
                            Id = bullEvents.Id,
                            RegistrationNumber = bulletin.RegistrationNumber,
                            BirthDate = bulletin.BirthDate,
                            CreatedOn = bullEvents.CreatedOn,
                            Description = bullEvents.Description,
                            Egn = bulletin.Egn,
                            Ln = bulletin.Ln,
                            Lnch = bulletin.Lnch,
                            EventType = eventType.Name,
                            PersonName = bulletin.Firstname + " " + bulletin.Surname + " " + bulletin.Familyname
                        };

            return await Task.FromResult(query);
        }
    }
}
