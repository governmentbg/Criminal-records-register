using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.WebPortal.Public.Utils.Mappings
{
    public class CommonPublicProfile : Profile
    {
        public CommonPublicProfile()
        {
            CreateMap<BaseNomenclatureDTO, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
