using AutoMapper;
using MJ_CAIS.AutoMapperContainer.Resolvers;
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
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.StatusCodeNavigation.Name))
                .ForMember(d => d.DocType, opt => opt.MapFrom(src => src.Doc.DocContent.MimeType))
                .ForMember(d => d.DocName, opt => opt.MapFrom(src => src.Doc.Name));

            CreateMap<ACertificate, CertificateGridDTO>()
                .ForMember(d => d.FirstSigner,
                    opt => opt.MapFrom(src => CertificateResolver.GetFullName(src.FirstSigner.Firstname,
                        src.FirstSigner.Surname, src.FirstSigner.Familyname)))
                .ForMember(d => d.SecondSigner, opt => opt.MapFrom(src => CertificateResolver.GetFullName(src.SecondSigner.Firstname,
                    src.SecondSigner.Surname, src.SecondSigner.Familyname)));

            CreateMap<CertificateDTO, ACertificate>()
                .ForMember(d => d.StatusCodeNavigation, opt => opt.Ignore())
                .ForMember(d => d.Doc, opt => opt.Ignore())
                .ForMember(d => d.RegistrationNumber, opt => opt.Ignore())
                .ForMember(d => d.AccessCode1, opt => opt.Ignore())
                .ForMember(d => d.AccessCode2, opt => opt.Ignore());

            CreateMap<AAppBulletin, BulletinCheckDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.BulletinId, opt => opt.MapFrom(src => src.BulletinId))
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
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.Application.CreatedOn))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.StatusCodeNavigation.Name));
        }
    }
}