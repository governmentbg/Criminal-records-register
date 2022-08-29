using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.DTO.Application.Public;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IApplicationWebRepository : IBaseAsyncRepository<WApplication, string, CaisDbContext>
    {
        Task<DTO.Application.External.ApplicationPreviewDTO> GetExternalForPreviewAsync(string id);
        IQueryable<ExternalApplicationGridDTO> SelectExternalApplications(string userId);
        Task<DTO.Application.Public.ApplicationPreviewDTO> GetPublicForPreviewAsync(string id);
        IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId);
        Task<bool> HasDublicates(string egn, string purposeId, string applicationTypeId, string applicantId);
    }
}
