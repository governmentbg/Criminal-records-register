using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.WApplicationReportSearchPer;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class WApplicationReportSearchPerProfile : Profile
    {
        public WApplicationReportSearchPerProfile()
        {
            CreateMap<WReportSearchPer, WApplicationReportSearchPerGridDTO>();
            CreateMap<PersonIdentifierSearchExtendedRequestType, WReportSearchPer>()
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
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Firstame))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Fullname))
                .ForMember(d => d.Birthdate, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Birthdate))
                .ForMember(d => d.BirthdatePrec, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.BirthDatePrec))
                .ForMember(d => d.Birthplace, opt => opt.MapFrom(src => src.PersonIdentifierSearchRequest.Birthplace));
        }
    }
}
