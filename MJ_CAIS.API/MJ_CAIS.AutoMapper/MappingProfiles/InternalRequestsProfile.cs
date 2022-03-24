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
               .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Bulletin.Firstname))
               .ForMember(d => d.SurName, opt => opt.MapFrom(src => src.Bulletin.Surname))
               .ForMember(d => d.FamilyName, opt => opt.MapFrom(src => src.Bulletin.Familyname));

            CreateMap<BInternalRequest, InternalRequestDTO>()
                .ForMember(d => d.ReqStatusCode, opt => opt.Ignore());

            CreateMap<InternalRequestDTO, BInternalRequest>();

        }
    }
}