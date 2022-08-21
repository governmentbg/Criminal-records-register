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
            CreateMap<GExtAdministration, ExtAdministrationDTO>()
                .ForPath(d => d.ExtAdministrationUics, opt => opt.MapFrom(src => src.GExtAdministrationUics));
            CreateMap<ExtAdministrationDTO, GExtAdministration>();
            CreateMap<ExtAdministrationInDTO, GExtAdministration>();
            CreateMap<GExtAdministration, ExtAdministrationGridDTO>();
            CreateMap<GExtAdministrationUic, ExtAdministrationUicDTO>();
            CreateMap<ExtAdministrationUicDTO, GExtAdministrationUic>();
        }
    }
}
