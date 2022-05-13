using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.UserCitizen;
using MJ_CAIS.DTO.UserExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class UserCitizenProfile : Profile
    {
        public UserCitizenProfile()
        {
            CreateMap<GUsersCitizen, UserCitizenDTO>();
            CreateMap<UserCitizenDTO, GUsersCitizen>();
            CreateMap<GUsersCitizen, UserCitizenGridDTO>();
        }
    }
}
