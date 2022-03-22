using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IBulletinRepository : IBaseAsyncRepository<BBulletin, string, CaisDbContext>
    {
        Task ChangeStatusAsync(string aInDto, string statusId);
    }
}
