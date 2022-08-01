using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Home;
using MJ_CAIS.DTO.IsinData;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IIsinDataRepository : IBaseAsyncRepository<EIsinDatum, string, CaisDbContext>
    {
        IQueryable<ObjectStatusCountDTO> GetStatusCountByCurrentAuthority();
        Task<bool> HasBulletin(string bulletinId);

        Task<IsinDataPreviewDTO> SelectIsinDataAsync(string aId);
        IQueryable<IsinDataGridDTO> SelectAll(string? status, string? bulletinId);
        IQueryable<BBulletin> SelectAllBulletin();
    }
}
