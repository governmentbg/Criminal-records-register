using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IEWebRequestsRepository : IBaseAsyncRepository<EWebRequest, string, CaisDbContext>
    {

        Task<IQueryable<EWebRequest>> SelectAllByApplicationId(string aId);
    }
}
