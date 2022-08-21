using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WCertificate;


namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WCertificateProfile : Profile
    {
        public WCertificateProfile()
        {
            CreateMap<WCertificate, WCertificateDTO>();
            //.ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
            //.ForMember(d => d.AccessCode1, opt => opt.MapFrom(src => src.AccessCode1))
            //.ForMember(d => d.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
            //.ForMember(d => d.ValidTo, opt => opt.MapFrom(src => src.ValidTo));

        }
    }
}
