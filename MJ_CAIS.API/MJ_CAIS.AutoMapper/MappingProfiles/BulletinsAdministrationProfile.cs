using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinsAdministrationProfile : Profile
    {
        public BulletinsAdministrationProfile()
        {
            CreateMap<BBulletin, BulletinAdministrationGridDTO>()
                .ForMember(d => d.BulletinAuthorityName, opt => opt.MapFrom(src => src.BulletinAuthority.Name))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                    src.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinConstants.Type.Bulletin78A :
                    src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                    src.BulletinType == nameof(BulletinConstants.Type.Unspecified) ? BulletinConstants.Type.Unspecified : null));
        }
    }
}
