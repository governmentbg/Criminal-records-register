using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<BBulletin, BulletinGridDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Convert.ToInt32(src.Id)));

            CreateMap<BulletinDTO, BBulletin>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<BBulletin, BulletinDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Convert.ToInt32(src.Id)));
        }
    }
}
