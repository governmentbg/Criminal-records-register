using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IOffenceCategoryRepository : IBaseAsyncRepository<BOffenceCategory, string, CaisDbContext>
    {
    }
}
