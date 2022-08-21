using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.Repositories.Contracts;

namespace MJ_CAIS.Repositories.Impl
{
    public class UserExternalRepository : BaseAsyncRepository<GUsersExt, CaisDbContext>, IUserExternalRepository
    {
        public UserExternalRepository(CaisDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<string?> GetUserAdministrationIdAsync(string userId)
            => (await _dbContext.GUsersExts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId))?.AdministrationId;

        public async Task<string?> GetUserAdministrationNameAsync(string userId)
           => (await _dbContext.GUsersExts
            .Include(x=>x.Administration)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId))?.Administration?.Name;

        public IQueryable<UserExternalGridDTO> GetUsersByAdministration(string administrationId)
        {
            var query = _dbContext.GUsersExts.AsNoTracking()
                 .Where(x => x.AdministrationId == administrationId)
                 .Include(x => x.Administration)
                 .Select(x => new UserExternalGridDTO
                 {
                     Id = x.Id,
                     Active = x.Active,
                     AdministrationName = x.Administration.Name,
                     Name = x.Name,
                     Egn = x.Egn,
                     Email = x.Email,
                     IsAdmin = x.IsAdmin,
                     Position = x.Position,
                     Version = x.Version,
                     CreatedOn = x.CreatedOn,
                     UserName = x.UserName,
                     HasUserName = !string.IsNullOrEmpty(x.UserName)
                 });

            return query;
        }
    }
}
