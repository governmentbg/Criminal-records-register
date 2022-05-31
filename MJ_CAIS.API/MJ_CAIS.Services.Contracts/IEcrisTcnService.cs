using MJ_CAIS.DTO.EcrisTcn;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisTcnService : IBaseAsyncService<EcrisTcnDTO, EcrisTcnDTO, EcrisTcnGridDTO, EEcrisTcn, string>
    {
        Task<IgPageResult<EcrisTcnGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisTcnGridDTO> aQueryOptions, string statusId);
        Task ChangeStatusAsync(string aInDto, string statusId);
    }
}
