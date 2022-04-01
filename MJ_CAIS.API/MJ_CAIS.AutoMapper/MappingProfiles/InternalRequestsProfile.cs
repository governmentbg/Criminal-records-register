using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class InternalRequestsProfile : Profile
    {
        public InternalRequestsProfile()
        {
            CreateMap<BInternalRequest, InternalRequestGridDTO>()
               .ForMember(d => d.BulletinNumber, opt => opt.MapFrom(src => src.Bulletin.RegistrationNumber))
               .ForMember(d => d.ReqStatus, opt => opt.MapFrom(src => src.ReqStatusCodeNavigation.Name))
               .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Bulletin.Firstname))
               .ForMember(d => d.SurName, opt => opt.MapFrom(src => src.Bulletin.Surname))
               .ForMember(d => d.FamilyName, opt => opt.MapFrom(src => src.Bulletin.Familyname));

            CreateMap<BInternalRequest, InternalRequestDTO>()
             .ForMember(d => d.ReqStatusName, opt => opt.MapFrom(src => src.ReqStatusCodeNavigation.Name));

            CreateMap<BBulletin, BulletinPersonInfoModelDTO>()
                .ForMember(d => d.BulletinId, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.RegistrationNumber))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.BirthCity.Name))
                .ForMember(d => d.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForMember(d => d.MunicipalityName, opt => opt.MapFrom(src => src.BirthCity.Municipality.Name))
                .ForMember(d => d.Districtname, opt => opt.MapFrom(src => src.BirthCity.Municipality.District.Name))
                .ForMember(d => d.DecisionTypeName, opt => opt.MapFrom(src => src.DecisionType.Name))
                .ForMember(d => d.DecidingAuthName, opt => opt.MapFrom(src => src.DecidingAuth.Name))
                .ForMember(d => d.PersonAliases, opt => opt.MapFrom(src => src.BBullPersAliases))
                .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Country.Name)));

            CreateMap<InternalRequestDTO, BInternalRequest>();
        }
    }
}