using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());

            CreateMap<DDocument, DocumentDTO>()
             .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.MimeType, opt => opt.MapFrom(src => src.DocContent.MimeType))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());
        }
    }
}
