using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinAdministrationService : IBaseAsyncService<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string>
    {
        Task UnlockBulletinAsync(UnlockBulletinModelDTO aInDto);
        IQueryable<BaseNomenclatureDTO> GetBulletinStatusesByHistory(string aId);
    }
}
