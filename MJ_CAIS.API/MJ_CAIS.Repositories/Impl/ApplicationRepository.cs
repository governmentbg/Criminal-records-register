using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Repositories.Impl
{
    public class ApplicationRepository : BaseAsyncRepository<AApplication, CaisDbContext>, IApplicationRepository
    {
        public ApplicationRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }

        public override IQueryable<AApplication> SelectAllAsync()
        {
            var result = this._dbContext
                .Set<AApplication>()
                .Include(x => x.CsAuthorityBirth)
                .AsNoTracking();

            return result;
        }

       
    }
}
