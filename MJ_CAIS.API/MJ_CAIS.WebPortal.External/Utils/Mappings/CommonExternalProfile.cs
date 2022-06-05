using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MJ_CAIS.DTO.Nomenclature;

namespace MJ_CAIS.WebPortal.External.Utils.Mappings
{
    public class CommonExternalProfile : Profile
    {
        public CommonExternalProfile()
        {
            CreateMap<BaseNomenclatureDTO, SelectListItem>()
                .ForMember(d => d.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Text, opt => opt.MapFrom(src => src.Name));
        }
    }
}
