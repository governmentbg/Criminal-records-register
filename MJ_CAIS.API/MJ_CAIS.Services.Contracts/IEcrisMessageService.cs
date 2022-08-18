using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.AbstractMessageType;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.EcrisMessage;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IEcrisMessageService : IBaseAsyncService<EcrisMessageDTO, EcrisMessageDTO, EcrisMessageGridDTO, EEcrisMessage, string>
    {
        Task<IgPageResult<EcrisMessageGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<EcrisMessageGridDTO> aQueryOptions, string? statusId);

        Task<IQueryable<BulletinGridDTO>> GetEcrisBulletinsByIdAsync(string ecrisMessageId);

        Task<IQueryable<FbbcGridDTO>> GetEcrisFbbcsByIdAsync(string ecrisMessageId);
        Task<IQueryable<EcrisMsgNationalityDTO>> GetNationalitiesAsync(string aId);
        Task<IQueryable<EcrisMsgNameDTO>> GetNamesAsync(string aId);
        IQueryable<GraoPersonGridDTO> GetEcrisIdentifiedPeople(string aId);
        Task ChangeStatusAsync(string aInDto, string statusId);
        Task<EcrisRequestDTO> GetEcrisRequestByIdAsync(string ecrisMessageId);

        Task<EcrisResponseDTO> GetEcrisResponseByIdAsync(string ecrisMessageId);
        Task<EcrisNotificationDTO> GetEcrisNotificationByIdAsync(string ecrisMessageId);
        Task IdentifyAsync(string aInDto, string egn);
    }
}
