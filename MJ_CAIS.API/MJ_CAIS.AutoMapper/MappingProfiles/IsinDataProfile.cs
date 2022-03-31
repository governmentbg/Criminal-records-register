using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.IsinData;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class IsinDataProfile : Profile
    {
        public IsinDataProfile()
        {
            // todo: remove
            //CreateMap<EIsinDatum, IsinDataGridDTO>()
            //    .ForMember(d => d.MsgDateTime, opt => opt.MapFrom(src => src.IsinMsg.MsgDatetime))
            //    .ForMember(d => d.PersonName, opt => opt.MapFrom(src => src.Firstname + " " + src.Surname + " " + src.Familyname))
            //    .ForMember(d => d.BirthCountryName, opt => opt.MapFrom(src => src.BirthcountryName))
            //    .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.Country1Name + " " + src.Country2Name))
            //    .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Birthdate))
            //    .ForMember(d => d.DecisionInfo, opt => opt.MapFrom(src =>
            //            src.DecisionTypeId + " " + 
            //            src.DecisionNumber + " " + 
            //            src.DecisionDate + " " + 
            //            src.DecisionAuthName))
            //    .ForMember(d => d.CaseInfo, opt => opt.MapFrom(src => 
            //            src.CaseTypeId + " " + 
            //            src.CaseNumber + " " + 
            //            src.CaseYear + " " + 
            //            src.CaseAuthName));

            //CreateMap<EIsinDatum, IsinDataDTO>()
            //   .ForMember(d => d.MsgDateTime, opt => opt.MapFrom(src => src.IsinMsg.MsgDatetime));
        }
    }
}
