using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IApplicationService : IBaseAsyncService<ApplicationDTO, ApplicationDTO, ApplicationDTO, AApplication, string>
    {
        IQueryable<PublicApplicationGridDTO> SelectPublicApplications(string userId);

        Task<IgPageResult<ApplicationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<ApplicationGridDTO> aQueryOptions, string? statusId);
    }
}
