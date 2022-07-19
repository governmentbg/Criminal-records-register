using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.AspNet.OData.Query;
using MJ_CAIS.Services.Contracts.Utils;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisMessageService : IBaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
        Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string statusId);

        Task<IQueryable<BulletinGridDTO>> GetEcrisBulletinsByIdAsync(string ecrisMessageId);

        Task<IQueryable<FbbcGridDTO>> GetEcrisFbbcsByIdAsync(string ecrisMessageId);
        Task<IQueryable<EcrisMsgNationalityDTO>> GetNationalitiesAsync(string aId);
        Task<IQueryable<EcrisMsgNameDTO>> GetNamesAsync(string aId);
        Task<IQueryable<GraoPersonGridDTO>> GetGraoPeopleAsync(string aId);
        Task IdentifyAsync(string aInDto, string statusId);
    }
}
