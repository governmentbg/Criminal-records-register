using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.BulletinAdministration;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinsAdministrationProfile : Profile
    {
        public BulletinsAdministrationProfile()
        {

            CreateMap<BBulletin, BulletinAdministrationDTO>()
                .ForMember(d => d.CsAuthorityName, opt => opt.MapFrom(src => src.CsAuthority.Name))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(d => d.BulletinAuthorityName, opt => opt.MapFrom(src => src.BulletinAuthority.Name))
                .ForMember(d => d.BulletinTypeName, opt => opt.MapFrom(src =>
                    src.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                    src.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                    src.BulletinType == BulletinConstants.Type.Unspecified ? BulletinResources.Unspecified : null));

        }
    }
}
