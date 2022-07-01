using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Inquiry;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class InquiryProfile : Profile
    {
        public InquiryProfile()
        {
            CreateMap<VBulletinsFull, ExportInquiryBulletinGridDTO>()
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                           src.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                           src.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                           BulletinResources.Unspecified));
        }
    }
}
