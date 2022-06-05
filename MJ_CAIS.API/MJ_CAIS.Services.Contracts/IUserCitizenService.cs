using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserCitizen;

namespace MJ_CAIS.Services.Contracts
{
    public interface IUserCitizenService : IBaseAsyncService<UserCitizenDTO, UserCitizenDTO, UserCitizenGridDTO, GUsersCitizen, string>
    {
        Task<GUsersCitizen> AuthenticatePublicUserAsync(UserCitizenDTO userDTO);
        Task<GUsersCitizen> GetUserCitizenByEgnAsync(string egn);
    }
}
