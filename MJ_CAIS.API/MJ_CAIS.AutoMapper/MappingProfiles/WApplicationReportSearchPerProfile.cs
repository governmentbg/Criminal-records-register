using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.WApplicationReportSearchPer;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicationReportSearchPerProfile : Profile
    {
        public WApplicationReportSearchPerProfile()
        {
            CreateMap<WReport, WApplicationReportSearchPerGridDTO>();
            CreateMap<WReport, WApplicationReportSearchPerDTO>();
        }
    }
}
