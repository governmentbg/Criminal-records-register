using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinAdministrationRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        Task<BBulletin> GetBulletinByIdAsync(string id);

        IQueryable<BaseNomenclatureDTO> GetBulletinStatusesByHistory(string aId);
    }
}
