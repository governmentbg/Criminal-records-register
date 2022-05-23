using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Application.Public;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationWebService : IBaseAsyncService<PublicApplicationDTO, PublicApplicationDTO, PublicApplicationDTO, WApplication, string>
    {
        string GetWebApplicationTypeId();
        IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId);
    }
}
