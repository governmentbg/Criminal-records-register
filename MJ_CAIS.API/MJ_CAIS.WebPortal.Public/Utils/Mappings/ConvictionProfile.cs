using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.WebPortal.Public.Models.Conviction;

namespace MJ_CAIS.WebPortal.Public.Utils.Mappings
{
    public class ConvictionProfile : Profile
    {
        public ConvictionProfile()
        {
            // TODO: change entity
            CreateMap<AApplication, ConvictionCodeDisplayModel>()
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src => src.Egn ?? src.Lnch ?? src.Ln));
        }
    }
}
