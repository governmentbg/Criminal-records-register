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

            //CreateMap<WApplication, AApplication>()
            //    .ForMember(d => d.WApplicationId, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(d => d.ApplicationTypeId, opt => opt.MapFrom(src => src.ApplicationType.Id))
            //    .ForMember(d => d.ApplicationType, opt => opt.Ignore());

            CreateMap<WApplication, PersonDTO>();
        }
    }
}
