using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<AApplication, ApplicationDTO>()
                .ForPath(d => d.Address.Country, opt => opt.MapFrom(src => new LookupDTO
                {
                    Id = src.BirthCountry.Id,
                    DisplayName = src.BirthCountry.Name
                }))
                .ForPath(d => d.Address.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Address.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Address.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null));

            CreateMap<ApplicationDTO, AApplication>()
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId));

        }
    }
}
