using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.WApplicationReport;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicationReportProfile : Profile
    {
        public WApplicationReportProfile()
        {
            CreateMap<WReport, WApplicationReportGridDTO>();
            CreateMap<WReport, WApplicationReportDTO>();
            CreateMap<CriminalRecordsExtendedRequestType, WReport>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()))
                .ForMember(d => d.CAdministrationName, opt => opt.MapFrom(src => src.CallContext.AdministrationName))
                .ForMember(d => d.CAdministrationOid, opt => opt.MapFrom(src => src.CallContext.AdministrationOId))
                .ForMember(d => d.CEmpAddId, opt => opt.MapFrom(src => src.CallContext.EmployeeAditionalIdentifier))
                .ForMember(d => d.CEmplId, opt => opt.MapFrom(src => src.CallContext.EmployeeIdentifier))
                .ForMember(d => d.CEmpNames, opt => opt.MapFrom(src => src.CallContext.EmployeeNames))
                .ForMember(d => d.CEmpPos, opt => opt.MapFrom(src => src.CallContext.EmployeePosition))
                .ForMember(d => d.CRespPersId, opt => opt.MapFrom(src => src.CallContext.ResponsiblePersonIdentifier))
                .ForMember(d => d.CServiceType, opt => opt.MapFrom(src => src.CallContext.ServiceType))
                .ForMember(d => d.CServiceUri, opt => opt.MapFrom(src => src.CallContext.ServiceURI))
                .ForMember(d => d.CRemark, opt => opt.MapFrom(src => src.CallContext.Remark))
                .ForMember(d => d.CLawReason, opt => opt.MapFrom(src => src.CallContext.LawReason))
                .ForMember(d => d.Pid, opt => opt.MapFrom(src => src.CriminalRecordsRequest.PID))
                .ForMember(d => d.PidType, opt => opt.MapFrom(src => src.CriminalRecordsRequest.IdentifierType));
        }
    }
}
