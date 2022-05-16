using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<GUser, UserDTO>()
                .ForPath(d => d.Roles.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.GUserRoles.Select(x => x.Id)))
                .ForPath(d => d.Roles.SelectedForeignKeys, opt => opt.MapFrom(src => src.GUserRoles.Select(x => x.RoleId)));
            CreateMap<UserDTO, GUser>();
            CreateMap<GUser, UserGridDTO>()
                .ForMember( d => d.AuthorityName, opt => opt.MapFrom( src => src.CsAuthority.Name))
                .ForMember( d => d.Roles, opt => opt.MapFrom( src => src.GUserRoles.Select( s=> s.Role.Name)));
        }
    }
}
