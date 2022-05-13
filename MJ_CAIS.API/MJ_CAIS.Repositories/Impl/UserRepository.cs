using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class UserRepository : BaseAsyncRepository<GUser, CaisDbContext>, IUserRepository
    {
        public UserRepository(CaisDbContext dbContext) : base(dbContext)
        {
        }
        public virtual async Task<GUser> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<GUser>()
                .Include( u => u.GUserRoles)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public virtual IQueryable<GUser> SelectAllAsync()
        {
            var result = this._dbContext.Set<GUser>()
                .Include(u => u.CsAuthority)
                .Include(u => u.GUserRoles)
                .AsNoTracking();
            return result;
        }
    }
}
