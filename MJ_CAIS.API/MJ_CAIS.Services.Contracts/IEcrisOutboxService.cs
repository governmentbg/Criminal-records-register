using MJ_CAIS.DTO.EcrisOutbox;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisOutboxService : IBaseAsyncService<EcrisOutboxDTO, EcrisOutboxDTO, EcrisOutboxGridDTO, EEcrisOutbox, string>
    {
        Task<IgPageResult<EcrisOutboxGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisOutboxGridDTO> aQueryOptions, string? statusId);

        Task<byte[]> GetXmlContentAsync(string aId);
    }
}
