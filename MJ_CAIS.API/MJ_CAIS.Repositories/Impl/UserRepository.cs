using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace MJ_CAIS.Repositories.Impl
{
    public class UserRepository : BaseAsyncRepository<GUser, CaisDbContext>, IUserRepository
    {
        IUserContext userContext;
        public UserRepository(CaisDbContext dbContext, IUserContext _userContext) : base(dbContext)
        {
            this.userContext = _userContext;
        }

        public override async Task<GUser> SelectAsync(string id)
        {
            var result = await this._dbContext.Set<GUser>()
                .Include( u => u.GUserRoles)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (!userContext.IsGlobalAdmin && userContext.CsAuthorityId != result?.CsAuthorityId)
            {
                return null;
            }
            else
            {
                return result;
            }
        }

        public override IQueryable<GUser> SelectAll()
        {
            var result = this._dbContext.Set<GUser>()
                .Include(u => u.CsAuthority)
                .Include(u => u.GUserRoles)
                .Include(u => u.GUserRoles).ThenInclude( ur => ur.Role)
                .AsNoTracking();
            result = userContext.FilterByAuthority(result);

            return result;
        }
    }
}
