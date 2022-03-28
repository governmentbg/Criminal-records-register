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
            CreateMap<FbbcDTO, Fbbc>();
            CreateMap<Fbbc, FbbcDTO>();
            CreateMap<FbbcDocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());
            CreateMap<DDocument, FbbcDocumentDTO>()
                .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());
        }
    }
}
