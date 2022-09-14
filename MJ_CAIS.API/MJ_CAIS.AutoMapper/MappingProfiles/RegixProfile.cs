using AutoMapper;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.RegixIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class RegixProfile : Profile
    {
        public RegixProfile()
        {
            CreateMap<AAppCitizenshipValues, AAppCitizenship>()
             .ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.CountryId));
            CreateMap<AAppCitizenshipValues, WAppCitizenship>()
           .ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.CountryId));
            CreateMap<AAppCitizenshipValues, ARepCitizenship>()
           .ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.CountryId));
                

            CreateMap<PersonAliasesValues, AAppPersAlias>();
            CreateMap<PersonAliasesValues, WAppPersAlias>();


        }
    }
}
