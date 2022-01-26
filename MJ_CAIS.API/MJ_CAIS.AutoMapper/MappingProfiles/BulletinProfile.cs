using AutoMapper;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.Entities;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<Bulletin, BulletinGridDTO>();

            CreateMap<BulletinDTO, Bulletin>();
            CreateMap<Bulletin, BulletinDTO>();
        }
    }
}
