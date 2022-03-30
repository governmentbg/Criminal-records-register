using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IIsinDataRepository : IBaseAsyncRepository<EIsinDatum, string, CaisDbContext>
    {
    }
}
