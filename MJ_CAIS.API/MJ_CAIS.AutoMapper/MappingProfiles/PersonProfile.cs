using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
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
               .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
               .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
               .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId));

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
