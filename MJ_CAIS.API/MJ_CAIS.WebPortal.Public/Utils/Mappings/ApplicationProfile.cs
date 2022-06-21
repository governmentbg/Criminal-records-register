using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.Public;
using MJ_CAIS.WebPortal.Public.Models.Application;
using MJ_CAIS.WebPortal.Public.Models.Conviction;

namespace MJ_CAIS.WebPortal.Public.Utils.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<AApplication, ConvictionCodeDisplayModel>()
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src => src.Egn ?? src.Lnch ?? src.Ln));

            CreateMap<ApplicationEditModel, PublicApplicationDTO>()
                .ReverseMap();

            CreateMap<PublicApplicationDTO, WApplication>()
                .ReverseMap();

            CreateMap<ApplicationPreviewDTO, ApplicationPreviewModel>();
            
        }
    }
}
