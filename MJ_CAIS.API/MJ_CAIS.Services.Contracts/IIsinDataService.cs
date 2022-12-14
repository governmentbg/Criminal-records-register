using Microsoft.AspNet.OData.Query;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;
using MJ_CAIS.Services.Contracts.Utils;

namespace MJ_CAIS.Services.Contracts
{
    public interface IIsinDataService : IBaseAsyncService<IsinDataDTO, IsinDataDTO, IsinDataGridDTO, EIsinDatum, string>
    {
        Task<IgPageResult<IsinDataGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<IsinDataGridDTO> aQueryOptions, string? status, string? bulletinId);
        Task<IgPageResult<IsinBulletinGridDTO>> SelectIsinBulletinAllWithPaginationAsync(ODataQueryOptions<IsinBulletinGridDTO> aQueryOptions);
        Task SelectBulletinAsync(string aInDto, string bulletinId);
        Task<IsinDataPreviewDTO> SelectForPreviewAsync(string aIdDto);
        Task CloseAsync(string aInDto);
    }
}