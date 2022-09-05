using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DataAccess.ExtUsers;
using MJ_CAIS.DTO.UserExternal;
using MJ_CAIS.WebPortal.External.Models.Account;
using MJ_CAIS.WebPortal.External.Models.UserExternal;

namespace MJ_CAIS.WebPortal.External.Utils.Mappings
{
    public class UserExternalProfile : Profile
    {
        public UserExternalProfile()
        {
            CreateMap<UserExternalEditModel, UserExternalDTO>()
                .ReverseMap();

            CreateMap<UserExternalInDTO, GUsersExt>()
                .ReverseMap();

            CreateMap<InactiveViewModel, UserExternalInDTO>()
                .ReverseMap();
        }
    }
}
