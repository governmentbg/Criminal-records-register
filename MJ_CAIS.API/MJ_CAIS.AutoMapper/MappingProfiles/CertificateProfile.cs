using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Certificate;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CertificateProfile : Profile
    {
        public CertificateProfile()
        {
            CreateMap<ACertificate, CertificateDTO>()
                .ReverseMap();

            CreateMap<AAppBulletin, BulletinCheckDTO>()
              .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Bulletin.Id))
              .ForMember(d => d.Version, opt => opt.MapFrom(src => src.Bulletin.Version))
              .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.Bulletin.RegistrationNumber))
              .ForMember(d => d.BulletinReceivedDate, opt => opt.MapFrom(src => src.Bulletin.BulletinReceivedDate))
              .ForMember(d => d.StatusId, opt => opt.MapFrom(src => src.Bulletin.StatusId))
              .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.Bulletin.Status.Name))
              .ForMember(d => d.BulletinAuthorityId, opt => opt.MapFrom(src => src.Bulletin.CsAuthorityId))
              .ForMember(d => d.BulletinAuthorityName, opt => opt.MapFrom(src => src.Bulletin.CsAuthority.Name));

            CreateMap<ACertificate, ApplicationGridDTO>()
              .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Application.Id))
              .ForMember(d => d.Version, opt => opt.MapFrom(src => src.Application.Version))
              .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.Application.RegistrationNumber))
              .ForMember(d => d.Purpose, opt => opt.MapFrom(src => src.Application.Purpose))
              .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Application.Egn))
              .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Application.Firstname))
              .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Application.Surname))
              .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Application.Familyname))
              .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Application.BirthDate))
              .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Application.BirthPlaceOther))
              .ForMember(d => d.StatusCode, opt => opt.MapFrom(src => src.Application.StatusCode))
              .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Application.BirthCityId))
              .ForMember(d => d.CsAuthorityBirth, opt => opt.MapFrom(src => src.Application.CsAuthorityBirth.Name))
              .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.Application.CreatedOn));
        }
    }
}
