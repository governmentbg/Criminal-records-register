using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IViewsCountsRepository : IBaseAsyncRepository<VBulletin, string, CaisDbContext>
    {
        IQueryable<ObjectStatusCountDTO> GetApplicationsCountByCurrentAuthority();
        IQueryable<ObjectStatusCountDTO> GetCentralAuthorityCounts();
        IQueryable<ObjectStatusCountDTO> GetBulletinsStatusCountByCurrentAuthority();

    }
}
