using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IBulletinService : IBaseAsyncService<BulletinDTO, BulletinDTO, BulletinGridDTO, Bulletin, string>
    {
    }
}
