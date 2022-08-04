using AutoMapper;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ReportApplication;
using static MJ_CAIS.Common.Constants.ReportApplicationConstants;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class ReportApplicationProfile : Profile
    {
        public ReportApplicationProfile()
        {
            CreateMap<AReportApplication, ReportApplicationGridDTO>()
              .ForMember(d => d.StatusName, opt => opt.MapFrom(src =>
                         src.StatusCode == Status.New ? ReportApplicationResources.statusNew :
                         (src.StatusCode == Status.Approved ? ReportApplicationResources.approved :
                         (src.StatusCode == Status.Canceled ? ReportApplicationResources.canceled :
                         (src.StatusCode == Status.Delivered ? ReportApplicationResources.delivered : string.Empty)))));
        }
    }
}
