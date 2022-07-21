using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IPersonHelperRepository : IBaseAsyncRepository<PPerson, string, CaisDbContext>
    {
        IQueryable<BulletinByPersonIdDTO> GetAllBulletinsByPersonId(string personId);

        IQueryable<BulletinByPersonIdForEventsDTO> GetAllBulletinsForEventsByPersonId(string personId);

        IQueryable<ApplicationsByPersonIdDTO> GetAllAplicationsByPersonId(string personId);

        IQueryable<FbbcByPersonIdDTO> GetAllFbbcsByPersonId(string personId);
    }
}
