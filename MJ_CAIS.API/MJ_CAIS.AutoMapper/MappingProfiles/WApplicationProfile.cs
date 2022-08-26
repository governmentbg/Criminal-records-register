using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.DTO.WApplicaiton;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicationProfile : Profile
    {
        public WApplicationProfile()
        {
            CreateMap<WApplication, WApplicaitonGridDTO>()
                .ForMember(d => d.Purpose, opt => opt.MapFrom(src => src.PurposeNavigation.Name))
                .ForMember(d => d.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.StatusCodeNavigation.Name));

            CreateMap<WApplication, WApplicaitonDTO>()
                .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
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
                .ForPath(d => d.Person.BirthPlace.MunicipalityId,
                    opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Person.BirthPlace.DistrictId,
                    opt => opt.MapFrom(src =>
                        src.BirthCity != null && src.BirthCity.Municipality != null
                            ? src.BirthCity.Municipality.DistrictId
                            : null))
                .ForPath(d => d.Person.Nationalities.SelectedPrimaryKeys,
                    opt => opt.MapFrom(src => src.WAppCitizenships.Select(x => x.Id)))
                .ForPath(d => d.Person.Nationalities.SelectedForeignKeys,
                    opt => opt.MapFrom(src => src.WAppCitizenships.Select(x => x.CountryId)));

            //CreateMap<WApplication, AApplication>()
            //    .ForMember(d => d.WApplicationId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(d => d.ApplicationTypeId, opt => opt.MapFrom(src => src.ApplicationType.Id))
            //    .ForMember(d => d.ApplicationType, opt => opt.Ignore());

            CreateMap<WApplication, PersonDTO>();

            CreateMap<WCertificate, WCertificateDTO>().ReverseMap();
        }
    }
}