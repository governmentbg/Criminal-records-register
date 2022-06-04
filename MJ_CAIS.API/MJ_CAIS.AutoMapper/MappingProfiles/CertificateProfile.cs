using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
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
        }
    }
}
