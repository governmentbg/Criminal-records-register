using MJ_CAIS.DTO.User;
using MJ_CAIS.DataAccess.Entities;

namespace MJ_CAIS.Services.Contracts
{
    public interface IUserService : IBaseAsyncService<UserDTO, UserDTO, UserGridDTO, GUser, string>
    {
        Task<UserDTO> AuthenticatePublicUser(UserDTO userDTO);
    }
}
