using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()

        {
            CreateMap<AApplication, ApplicationGridDTO>()
                .ForMember(d => d.CsAuthorityBirth, opt => opt.MapFrom(src => src.CsAuthorityBirth.Name));
         
            CreateMap<AApplication, ApplicationDTO>();

            CreateMap<ApplicationDTO, AApplication>();

            CreateMap<ApplicationDocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());
            CreateMap<DDocument, ApplicationDocumentDTO>()
                .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());

        }
    }
}
