using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.AStatusH;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()

        {
            CreateMap<AApplication, ApplicationGridDTO>()
                .ForMember(d => d.CsAuthorityBirth, opt => opt.MapFrom(src => src.CsAuthorityBirth.Name));
         
            CreateMap<AApplication, ApplicationDTO>();

            CreateMap<ApplicationDTO, AApplication>()
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Person.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Person.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Person.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Person.Fullname))
                .ForMember(d => d.FirstnameLat, opt => opt.MapFrom(src => src.Person.FirstnameLat))
                .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.Person.SurnameLat))
                .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.Person.FamilynameLat))
                .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.Person.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Person.Sex))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Person.Egn))
                .ForMember(d => d.Lnch, opt => opt.MapFrom(src => src.Person.Lnch))
                .ForMember(d => d.Ln, opt => opt.MapFrom(src => src.Person.Ln))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.Person.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.Person.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.Person.MotherFamilyname))
                .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.Person.MotherFullname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.Person.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.Person.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.Person.FatherFamilyname))
                .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.Person.FatherFullname))
                .ForMember(d => d.BirthPlaceOther,
                    opt => opt.MapFrom(src => src.Person.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Person.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Person.BirthPlace.CityId));
                

            CreateMap<AApplication, ApplicationDTO>()
             .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
             .ForPath(d => d.Person.Suid, opt => opt.MapFrom(src => src.Suid))
             .ForPath(d => d.Person.Firstname, opt => opt.MapFrom(src => src.Firstname))
             .ForPath(d => d.Person.Surname, opt => opt.MapFrom(src => src.Surname))
             .ForPath(d => d.Person.Familyname, opt => opt.MapFrom(src => src.Familyname))
             .ForPath(d => d.Person.Fullname, opt => opt.MapFrom(src => src.Fullname))
             .ForPath(d => d.Person.FirstnameLat, opt => opt.MapFrom(src => src.FirstnameLat))
             .ForPath(d => d.Person.SurnameLat, opt => opt.MapFrom(src => src.SurnameLat))
             .ForPath(d => d.Person.FamilynameLat, opt => opt.MapFrom(src => src.FamilynameLat))
             .ForPath(d => d.Person.FullnameLat, opt => opt.MapFrom(src => src.FullnameLat))
             .ForPath(d => d.Person.Sex, opt => opt.MapFrom(src => src.Sex))
             .ForPath(d => d.Person.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
             .ForPath(d => d.Person.Egn, opt => opt.MapFrom(src => src.Egn))
             .ForPath(d => d.Person.Lnch, opt => opt.MapFrom(src => src.Lnch))
             .ForPath(d => d.Person.Ln, opt => opt.MapFrom(src => src.Ln))
             .ForPath(d => d.Person.MotherFirstname, opt => opt.MapFrom(src => src.MotherFirstname))
             .ForPath(d => d.Person.MotherSurname, opt => opt.MapFrom(src => src.MotherSurname))
             .ForPath(d => d.Person.MotherFamilyname, opt => opt.MapFrom(src => src.MotherFamilyname))
             .ForPath(d => d.Person.MotherFullname, opt => opt.MapFrom(src => src.MotherFullname))
             .ForPath(d => d.Person.FatherFirstname, opt => opt.MapFrom(src => src.FatherFirstname))
             .ForPath(d => d.Person.FatherSurname, opt => opt.MapFrom(src => src.FatherSurname))
             .ForPath(d => d.Person.FatherFamilyname, opt => opt.MapFrom(src => src.FatherFamilyname))
             .ForPath(d => d.Person.FatherFullname, opt => opt.MapFrom(src => src.FatherFullname))
             .ForPath(d => d.Person.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
             .ForPath(d => d.Person.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
             .ForPath(d => d.Person.BirthPlace.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
             .ForPath(d => d.Person.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
             .ForPath(d => d.Person.BirthPlace.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
             .ForPath(d => d.Person.BirthPlace.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
             .ForPath(d => d.Person.Nationalities.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.AAppCitizenships.Select(x => x.Id)))
             .ForPath(d => d.Person.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.AAppCitizenships.Select(x => x.CountryId)));


            CreateMap<AAppPersAlias, PersonAliasDTO>()
                .ForMember(d => d.TypeCode, opt => opt.MapFrom(src => src.Type));


            CreateMap<ApplicationDocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());
            CreateMap<DDocument, ApplicationDocumentDTO>()
                .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());

        }
    }
}
