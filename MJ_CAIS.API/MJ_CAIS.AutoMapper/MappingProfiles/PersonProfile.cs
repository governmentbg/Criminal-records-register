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
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.SurnameLat))
               .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.FamilynameLat))
                .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Sex))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.BirthPlace.CityId))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.MotherFamilyname))
                .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.MotherFullname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.FatherFamilyname))
                .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.FatherFullname));

            CreateMap<PPerson, PPersonH>();
            CreateMap<PPersonId, PPersonIdsH>();
            CreateMap<PPerson, PPerson>();

            CreateMap<BBulletin, PersonDTO>()
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.SurnameLat))
                .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.FamilynameLat))
                .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Sex))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(d => d.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.MotherFamilyname))
                .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.MotherFullname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.FatherFamilyname))
                .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.FatherFullname));
        }
    }
}
