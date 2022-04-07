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
            CreateMap<BBulletin, IsinBulletinGridDTO>()
                .ForMember(d => d.Identifier, opt => opt.MapFrom(src =>
                        src.Egn +
                        (!string.IsNullOrEmpty(src.Egn) ? " / " + src.Lnch : src.Lnch) +
                        (!string.IsNullOrEmpty(src.Egn) || !string.IsNullOrEmpty(src.Lnch) ? " / " + src.Ln : src.Ln)))
                .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Country.Name)))
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
        }
    }
}
