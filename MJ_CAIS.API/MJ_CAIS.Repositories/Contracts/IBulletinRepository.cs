using MJ_CAIS.DataAccess;
using MJ_CAIS.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinRepository : IBaseAsyncRepository<Bulletin, string, CaisDbContext>
    {
    }
}
