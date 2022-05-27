using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class RegisterTypeRepository : BaseAsyncRepository<DRegisterType, CaisDbContext>, IRegisterTypeRepository
    {
        public RegisterTypeRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
    }
}
