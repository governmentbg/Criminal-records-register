using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReport;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicationReportProfile : Profile
    {
        public WApplicationReportProfile()
        {
            CreateMap<WReport, WApplicationReportGridDTO>();
            CreateMap<WReport, WApplicationReportDTO>();
        }
    }
}
