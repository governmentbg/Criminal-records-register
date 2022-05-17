using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExtAdministration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class ExtAdministrationProfile : Profile
    {
        public ExtAdministrationProfile()
        {
            CreateMap<GExtAdministration, ExtAdministrationDTO>();
            CreateMap<ExtAdministrationDTO, GExtAdministration>();
            CreateMap<GExtAdministration, ExtAdministrationGridDTO>();
        }
    }
}
