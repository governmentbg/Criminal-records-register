using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Certificate;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CertificateProfile : Profile
    {
        public CertificateProfile()
        {
            CreateMap<CertificateSignerDTO, ACertificate>();
        }
    }
}
