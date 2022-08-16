using MJ_CAIS.DTO.EcrisInbox;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisInboxService : IBaseAsyncService<EcrisInboxDTO, EcrisInboxDTO, EcrisInboxGridDTO, EEcrisInbox, string>
    {
        Task<IgPageResult<EcrisInboxGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisInboxGridDTO> aQueryOptions, string? statusId);

        Task<byte[]> GetXmlContentAsync(string aId, bool traits);
    }
}
