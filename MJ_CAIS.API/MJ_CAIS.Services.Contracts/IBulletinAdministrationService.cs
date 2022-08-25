using MJ_CAIS.DTO.BulletinAdministration;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Nomenclature;
using MJ_CAIS.Services.Contracts.Utils;
using Microsoft.AspNet.OData.Query;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinAdministrationService : IBaseAsyncService<BulletinAdministrationDTO, BulletinAdministrationDTO, BulletinAdministrationGridDTO, BBulletin, string>
    {
        Task<IgPageResult<BulletinAdministrationGridDTO>> SelectAllWithPaginationAsync(ODataQueryOptions<BulletinAdministrationGridDTO> aQueryOptions, BulletinAdministrationSearchParamDTO searchParams);
        Task UnlockBulletinAsync(UnlockBulletinModelDTO aInDto);
        IQueryable<BaseNomenclatureDTO> GetBulletinStatusesByHistory(string aId);
    }
}
