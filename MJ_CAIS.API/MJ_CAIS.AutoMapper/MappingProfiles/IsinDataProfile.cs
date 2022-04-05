using AutoMapper;
using MJ_CAIS.Common.Constants;
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

            CreateMap<BBulletin, IsinBulletinGridDTO>()
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src => src.Egn + " " + src.Lnch + " " + src.Ln))
                .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Country.Name)))
                .ForMember(d => d.PersonName, opt => opt.MapFrom(src => src.Firstname + " " + src.Surname + " " + src.Familyname))
                .ForMember(d => d.DecisionType, opt => opt.MapFrom(src => src.DecisionType.Name))
                .ForMember(d => d.DecisionNumber, opt => opt.MapFrom(src => src.DecisionNumber))
                .ForMember(d => d.DecisionAuthName, opt => opt.MapFrom(src => src.DecidingAuth.Name))
                .ForMember(d => d.BirthPlace, opt => opt.MapFrom(src => src.BirthCountry.Name + " " + src.BirthCity.Name))
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                            src.BulletinType == nameof(BulletinConstants.Type.Bulletin78А) ?
                            BulletinConstants.Type.Bulletin78А :
                                        src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                                        BulletinConstants.Type.Unspecified));
        }
    }
}
