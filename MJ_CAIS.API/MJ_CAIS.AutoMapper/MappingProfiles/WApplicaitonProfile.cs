using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicaiton;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicaitonProfile : Profile
    {
        public WApplicaitonProfile()
        {
            CreateMap<WApplication, WApplicaitonGridDTO>()
               .ForMember(d => d.Purpose, opt => opt.MapFrom(src => src.PurposeNavigation.Name))
               .ForMember(d => d.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod.Name));
        }
    }
}
