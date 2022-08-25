using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class FbbcProfile : Profile
    {
        public FbbcProfile()
        {
            CreateMap<Fbbc, FbbcGridDTO>()
                .ForMember(d => d.DocType, opt => opt.MapFrom(src => src.DocType.Name));

            CreateMap<FbbcDTO, Fbbc>()
                .ForMember(d => d.CountryId, opt => opt.MapFrom(src => src.CountryLookup.Id))
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Person.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Person.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Person.Familyname))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Person.Egn))
                .ForMember(d => d.Suid, opt => opt.MapFrom(src => src.Person.Suid))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.Person.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.Person.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.Person.MotherFamilyname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.Person.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.Person.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.Person.FatherFamilyname))
                .ForMember(d => d.BirthPlace, opt => opt.MapFrom(src => src.Person.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Person.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Person.BirthPlace.CityId))
                .ForMember(d => d.Person, opt => opt.Ignore()); //this is PersonId property

            CreateMap<Fbbc, FbbcDTO>()
                 .ForPath(d => d.CountryLookup, opt => opt.MapFrom(src => new LookupDTO
                 {
                     Id = src.CountryId,
                     DisplayName = src.Country.Name
                 }))
                .ForPath(d => d.Person.Suid, opt => opt.MapFrom(src => src.Suid))
                .ForPath(d => d.Person.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(d => d.Person.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(d => d.Person.Familyname, opt => opt.MapFrom(src => src.Familyname))
                .ForPath(d => d.Person.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(d => d.Person.Egn, opt => opt.MapFrom(src => src.Egn))
                .ForPath(d => d.Person.MotherFirstname, opt => opt.MapFrom(src => src.MotherFirstname))
                .ForPath(d => d.Person.MotherSurname, opt => opt.MapFrom(src => src.MotherSurname))
                .ForPath(d => d.Person.MotherFamilyname, opt => opt.MapFrom(src => src.MotherFamilyname))
                .ForPath(d => d.Person.FatherFirstname, opt => opt.MapFrom(src => src.FatherFirstname))
                .ForPath(d => d.Person.FatherSurname, opt => opt.MapFrom(src => src.FatherSurname))
                .ForPath(d => d.Person.FatherFamilyname, opt => opt.MapFrom(src => src.FatherFamilyname))
                .ForPath(d => d.Person.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlace))
                .ForPath(d => d.Person.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.Person.BirthPlace.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForPath(d => d.Person.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Person.BirthPlace.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Person.BirthPlace.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null));

            CreateMap<FbbcDocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());
            CreateMap<DDocument, FbbcDocumentDTO>()
                .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());
        }
    }
}
