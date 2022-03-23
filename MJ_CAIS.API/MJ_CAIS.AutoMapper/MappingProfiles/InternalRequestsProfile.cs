using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class InternalRequestsProfile : Profile
    {
        public InternalRequestsProfile()
        {
            CreateMap<BInternalRequest, InternalRequestGridDTO>()
               .ForMember(d => d.BulletinNumber, opt => opt.MapFrom(src => src.Bulletin.RegistrationNumber))
               .ForMember(d => d.ReqStatus, opt => opt.MapFrom(src => src.ReqStatusCodeNavigation.Name))
               .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Bulletin.Firstname))
               .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Bulletin.Surname))
               .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Bulletin.Familyname));

            CreateMap<BInternalRequest, InternalRequestDTO>();
            CreateMap<InternalRequestDTO, BInternalRequest>();

        }
    }
}