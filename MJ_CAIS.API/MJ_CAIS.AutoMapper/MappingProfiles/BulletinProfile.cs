using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<BBulletin, BulletinGridDTO>();

            CreateMap<BulletinDTO, BBulletin>();
            CreateMap<BBulletin, BulletinDTO>();

            CreateMap<BOffenceDTO, BOffence>()
                .ForPath(d => d.OffenceCat.Name, opt => opt.MapFrom(src => src.OffenceCatName))
                .ForPath(d => d.EcrisOffCat.Name, opt => opt.MapFrom(src => src.EcrisOffCatName))
                .ForPath(d => d.OffPlaceCountry.Name, opt => opt.MapFrom(src => src.OffPlaceCountryId))
                .ForPath(d => d.OffPlaceSubdiv.Name, opt => opt.MapFrom(src => src.OffPlaceSubdivName))
                .ForPath(d => d.OffPlaceCity.Name, opt => opt.MapFrom(src => src.OffPlaceCityName))
                .ForPath(d => d.OffLvlCompl.Name, opt => opt.MapFrom(src => src.OffLvlComplName))
                .ForPath(d => d.OffLvlPart.Name, opt => opt.MapFrom(src => src.OffLvlPartName))
                .ReverseMap();
        }
    }
}
