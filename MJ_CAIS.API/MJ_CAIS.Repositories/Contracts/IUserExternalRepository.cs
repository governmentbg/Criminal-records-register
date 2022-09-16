using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;

namespace MJ_CAIS.Repositories.Contracts
{
    public interface IUserExternalRepository : IBaseAsyncRepository<GUsersExt, string, CaisDbContext>
    {
        Task<string?> GetUserAdministrationIdAsync(string userId);

        IQueryable<UserExternalGridDTO> GetUsersByAdministration(string administrationId);

        Task<string?> GetUserAdministrationNameAsync(string userId);

        Task<string?> GetUser(string egn, string administrationId);
        
        Task<string?> GetUser(string id, string egn, string administrationId);
    }
}
