using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Fbbc;
using MJ_CAIS.DTO.Person;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDTO, PPerson>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.BirthPlace.CityId));

            CreateMap<PPerson, PersonDTO>()
               .ForPath(d => d.Id, opt => opt.MapFrom(src => src.Id))
               .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
               .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
               .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
               .ForPath(d => d.BirthPlace.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
               .ForPath(d => d.BirthPlace.CityDisplayName, opt => opt.MapFrom(src => src.BirthCity.Name))
               .ForPath(d => d.BirthPlace.MunicipalityDisplayName, opt => opt.MapFrom(src => src.BirthCity.Municipality.Name))
               .ForPath(d => d.BirthPlace.DistrictDisplayName, opt => opt.MapFrom(src => src.BirthCity.Municipality.District.Name));

            CreateMap<PPerson, PPersonH>();
            CreateMap<PPersonId, PPersonIdsH>();
            CreateMap<PPerson, PPerson>();

            CreateMap<BBulletin, PersonDTO>()
                .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId));
        }
    }
}
