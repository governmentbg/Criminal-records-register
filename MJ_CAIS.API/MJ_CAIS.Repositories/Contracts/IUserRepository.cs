using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IUserRepository : IBaseAsyncRepository<GUser, string, CaisDbContext>
    {
    }
}
