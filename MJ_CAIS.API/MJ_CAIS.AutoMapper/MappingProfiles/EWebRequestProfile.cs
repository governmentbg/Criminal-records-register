using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EWebRequest;


namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class EWebRequestProfile : Profile
    {
        public EWebRequestProfile()
        {
            CreateMap<EWebRequest, EWebRequestGridDTO>()
                .ForMember(d => d.WebServiceName, opt => opt.MapFrom(src => src.WebService.Name));
        }
    }
}
