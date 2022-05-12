using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.User;

namespace MJ_CAIS.WebPortal.Public.Utils.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, GUser>();
            CreateMap<GUser, UserDTO>();
        }
    }
}
