using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Fbbc;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class FbbcProfile : Profile
    {
        public FbbcProfile()
        {
            CreateMap<Fbbc, FbbcGridDTO>();
            CreateMap<BulletinDTO, BBulletin>();
            CreateMap<BBulletin, BulletinDTO>();
            CreateMap<DocumentDTO, DDocument>()
             .ForPath(d => d.DocType.Name, opt => opt.MapFrom(src => src.DocTypeName))
             //.ForPath(d => d.DocContent.Content, opt => opt.MapFrom(src => src.DocumentContent))
             //.ForPath(d => d.DocContent.Id, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForPath(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocType, opt => opt.Ignore())
             .ForMember(d => d.DocContent, opt => opt.Ignore())
             .ReverseMap()
             .ForPath(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());
        }
    }
}
