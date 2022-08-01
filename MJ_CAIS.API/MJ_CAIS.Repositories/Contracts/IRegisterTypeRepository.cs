using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IRegisterTypeRepository : IBaseAsyncRepository<DRegisterType, string, CaisDbContext>
    {
        Task<string> GetRegisterNumber(string authorityID, string registerCode);
    }
}
