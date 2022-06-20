using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IApplicationRepository : IBaseAsyncRepository<AApplication, string, CaisDbContext>
    {
        IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority();
        Task<IQueryable<AAppPersAlias>> SelectApplicationPersAliasByApplicationIdAsync(string aId);
        Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId);
        Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId);
        Task<IQueryable<ACertificate>> SelectAllCertificateAsync();
        IQueryable<ObjectStatusCountDTO> GetForJudgeCountByCurrentAuthority();
    }
}
