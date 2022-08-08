using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IApplicationRepository : IBaseAsyncRepository<AApplication, string, CaisDbContext>
    {
        Task<IQueryable<AStatusHGridDTO>> SelectApplicationPersStatusHAsync(string aId);
       // Task<IQueryable<ACertificate>> SelectApplicationCertificateByApplicationIdAsync(string aId);
        Task<IQueryable<ACertificate>> SelectAllCertificateAsync();
    
        Task<AApplication> SelectEntityAsync(string id);
        Task<AApplication?> GetApplicationForCertificateGeneration(string id);

        Task<IQueryable<BBulletin>> SelectBulletinIdsAsync(string personId);
    }
}
