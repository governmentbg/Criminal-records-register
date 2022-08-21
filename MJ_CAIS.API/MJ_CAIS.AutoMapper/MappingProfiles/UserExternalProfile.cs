using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class UserExternalProfile : Profile
    {
        public UserExternalProfile()
        {
            CreateMap<GUsersExt, UserExternalDTO>().ReverseMap();
            CreateMap<UserExternalInDTO, GUsersExt>();
            CreateMap<GUsersExt, UserExternalGridDTO>()
                .ForMember(d => d.AdministrationName, opt => opt.MapFrom(src => src.Administration.Name))
                .ForMember(d => d.HasRegRegCertSubject, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.RegCertSubject)))
                .ForMember(d => d.HasUserName, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.UserName)));
        }
    }
}
