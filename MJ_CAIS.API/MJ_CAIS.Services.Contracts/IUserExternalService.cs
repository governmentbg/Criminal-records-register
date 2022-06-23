using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;

namespace MJ_CAIS.Services.Contracts
{
    public interface IUserExternalService : IBaseAsyncService<UserExternalDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string>
    {
        Task<GUsersExt> AuthenticateExternalUserAsync(UserExternalDTO userDTO);

        Task<IQueryable<UserExternalGridDTO>> SelectExternalUsersByUserIdAsync(string userId);

        Task<string?> GetUserAdministrationNameAsync(string userId);

        Task<string?> GetUserAdministrationIdAsync(string userId);

        Task<string> SaveUserExternalAsync(UserExternalDTO aInDto, bool isAdded);

        Task<UserExternalDTO> GetUserExternalDTOAsync(string userId);
    }
}
