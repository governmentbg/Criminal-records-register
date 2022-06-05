using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Application.External;
using MJ_CAIS.WebPortal.External.Models.Application;

namespace MJ_CAIS.WebPortal.External.Utils.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<ApplicationEditModel, ExternalApplicationDTO>()
                .ReverseMap();

            CreateMap<ExternalApplicationDTO, WApplication>()
                .ReverseMap();
        }
    }
}
