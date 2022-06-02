using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using MJ_CAIS.DTO.IsinData;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class IsinDataProfile : Profile
    {
        public IsinDataProfile()
        {
            CreateMap<BBulletin, IsinBulletinGridDTO>()
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src =>
                        src.Egn +
                        (!string.IsNullOrEmpty(src.Egn) ? " / " + src.Lnch : src.Lnch) +
                        (!string.IsNullOrEmpty(src.Egn) || !string.IsNullOrEmpty(src.Lnch) ? " / " + src.Ln : src.Ln)))
                .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Country.Name)))// todo: filter is not working
                .ForMember(d => d.PersonName, opt => opt.MapFrom(src => src.Firstname + " " + src.Surname + " " + src.Familyname))
                .ForMember(d => d.DecisionType, opt => opt.MapFrom(src => src.DecisionType.Name))
                .ForMember(d => d.DecisionNumber, opt => opt.MapFrom(src => src.DecisionNumber))
                .ForMember(d => d.DecisionAuthName, opt => opt.MapFrom(src => src.DecidingAuth.Name))
                .ForMember(d => d.BirthPlace, opt => opt.MapFrom(src => src.BirthCountry.Name + ", " + src.BirthCity.Name))
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                            src.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ?
                            BulletinConstants.Type.Bulletin78A :
                                        src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                                        BulletinConstants.Type.Unspecified));

            CreateMap<FineData, EIsinDatum>()
                .ForMember(d => d.IdentifierType, opt => opt.MapFrom(src => src.PersonData.IdentifierType.ToString()))
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src => src.PersonData.Identifier))
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.PersonData.FirstName))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.PersonData.SurName))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.PersonData.FamilyName))
                .ForMember(d => d.Birthdate, opt => opt.MapFrom(src => src.PersonData.BirthDate))
                 .ForMember(dest => dest.Sex, opt =>
                 {
                     opt.PreCondition(src => src.PersonData.SexSpecified);
                     opt.MapFrom(src => src.PersonData.Sex);
                 })
                .ForMember(d => d.Country1Code, opt => opt.MapFrom(src => src.PersonData.CountryCode1))
                .ForMember(d => d.Country1Name, opt => opt.MapFrom(src => src.PersonData.CountryName1))
                .ForMember(d => d.Country2Code, opt => opt.MapFrom(src => src.PersonData.CountryCode2))
                .ForMember(d => d.Country2Name, opt => opt.MapFrom(src => src.PersonData.CountryName2))
                .ForMember(d => d.BirthcountryCode, opt => opt.MapFrom(src => src.PersonData.BirthCountryCode))
                .ForMember(d => d.BirthcountryName, opt => opt.MapFrom(src => src.PersonData.BirthCountryName))
                .ForMember(d => d.BirthPlace, opt => opt.MapFrom(src => src.PersonData.BirthPlace))
                .ForMember(dest => dest.DecisionTypeId, opt =>
                {
                    opt.PreCondition(src => src.ConvictionData.ActTypeCodeSpecified);
                    opt.MapFrom(src => src.ConvictionData.ActTypeCode);
                })
                .ForMember(d => d.DecisionNumber, opt => opt.MapFrom(src => src.ConvictionData.ActNumber))
                .ForMember(dest => dest.DecisionDate, opt =>
                 {
                     opt.PreCondition(src => src.ConvictionData.ActDateSpecified);
                     opt.MapFrom(src => src.ConvictionData.ActDate);
                 })
                 .ForMember(dest => dest.DecisionFinalDate, opt =>
                 {
                     opt.PreCondition(src => src.ConvictionData.ActFinalDateSpecified);
                     opt.MapFrom(src => src.ConvictionData.ActFinalDate);
                 })
                .ForMember(d => d.DecisionAuthCode, opt => opt.MapFrom(src => src.ConvictionData.ActDecidingAuthorityCode))
                .ForMember(d => d.DecisionAuthName, opt => opt.MapFrom(src => src.ConvictionData.ActDecidingAuthorityName))
                .ForMember(d => d.CaseNumber, opt => opt.MapFrom(src => src.ConvictionData.CaseNumber))
                .ForMember(d => d.CaseYear, opt => opt.MapFrom(src => src.ConvictionData.CaseYear))
                .ForMember(dest => dest.CaseTypeId, opt =>
                {
                    opt.PreCondition(src => src.ConvictionData.CaseTypeCodeSpecified);
                    opt.MapFrom(src => src.ConvictionData.CaseTypeCode);
                })
                .ForMember(d => d.CaseAuthCode, opt => opt.MapFrom(src => src.ConvictionData.CaseDecidingAuthorityCode))
                .ForMember(d => d.CaseAuthName, opt => opt.MapFrom(src => src.ConvictionData.CaseDecidingAuthorityName))
                .ForMember(dest => dest.SanctionType, opt =>
                {
                    opt.PreCondition(src => src.ConvictionData.SanctionTypeSpecified);
                    opt.MapFrom(src => src.ConvictionData.SanctionType);
                });
                //.ForMember(d => d.LegalProvisions, opt => opt.MapFrom(src => src.ConvictionData.LegalProvisions))
                //.ForMember(d => d.Remarks, opt => opt.MapFrom(src => src.ConvictionData.Remarks))
                //.ForMember(dest => dest.ExcecutionEndDate, opt =>
                //{
                //    opt.PreCondition(src => src.ConvictionData.ExcecutionEndDateSpecified);
                //    opt.MapFrom(src => src.ConvictionData.ExcecutionEndDate);
                //});
        }
    }
}
