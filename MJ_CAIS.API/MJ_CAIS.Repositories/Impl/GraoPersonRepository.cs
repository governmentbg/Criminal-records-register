using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DTO.Home;

namespace MJ_CAIS.Repositories.Impl
{
    public class GraoPersonRepository : BaseAsyncRepository<GraoPerson, CaisDbContext>, IGraoPersonRepository
    {
        public GraoPersonRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

    }
}
