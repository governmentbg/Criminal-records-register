using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;

namespace MJ_CAIS.Services.Contracts
{
    public interface IUserExternalService : IBaseAsyncService<UserExternalDTO, UserExternalDTO, UserExternalGridDTO, GUsersExt, string>
    {
        Task<GUsersExt> AuthenticateExternalUserAsync(UserExternalDTO userDTO);
    }
}
