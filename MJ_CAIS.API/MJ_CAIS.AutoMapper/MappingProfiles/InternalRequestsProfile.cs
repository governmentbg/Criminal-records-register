using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.InternalRequest;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class InternalRequestsProfile : Profile
    {
        public InternalRequestsProfile()
        {
            CreateMap<NInternalRequest, InternalRequestGridDTO>()
               .ForMember(d => d.ReqStatusName, opt => opt.MapFrom(src => src.ReqStatusCodeNavigation.Name))
               .ForMember(d => d.ReqestType, opt => opt.MapFrom(src => src.NIntReqType.Name))
               .ForMember(d => d.FromAuthorityName, opt => opt.MapFrom(src => src.FromAuthority.Name))
               .ForMember(d => d.ToAuthorityName, opt => opt.MapFrom(src => src.ToAuthority.Name))
               .ForMember(d => d.Pid, opt => opt.MapFrom(src => src.PPersId.Pid));

            CreateMap<NInternalRequest, InternalRequestForJudgeGridDTO>()
              .ForMember(d => d.ReqStatusName, opt => opt.MapFrom(src => src.ReqStatusCodeNavigation.Name))
              .ForMember(d => d.ReqestType, opt => opt.MapFrom(src => src.NIntReqType.Name))
              .ForMember(d => d.FromAuthorityName, opt => opt.MapFrom(src => src.FromAuthority.Name))
              .ForMember(d => d.Pid, opt => opt.MapFrom(src => src.PPersId.Pid));

            CreateMap<InternalRequestDTO, NInternalRequest>()
                .ForMember(d => d.PPersIdId, opt => opt.MapFrom(src => src.PPersIdId));

            CreateMap<NInternalRequest, InternalRequestDTO>()
             .ForPath(d => d.PPersIdId, opt => opt.MapFrom(src => src.PPersIdId))
             .ForPath(d => d.PPidTypeCode, opt => opt.MapFrom(src => src.PPersId.PidType.Code))
             .ForPath(d => d.PPersIdIdDisplay, opt => opt.MapFrom(src => src.PPersId.Pid));

            CreateMap<BBulletin, BulletinPersonInfoModelDTO>()
                .ForMember(d => d.BulletinId, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.BulletinReceivedDate, opt => opt.MapFrom(src => src.BulletinReceivedDate))
                .ForMember(d => d.CsAuthorityName, opt => opt.MapFrom(src => src.CsAuthority.Name))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.RegistrationNumber))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.BirthCity.Name))
                .ForMember(d => d.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForMember(d => d.MunicipalityName, opt => opt.MapFrom(src => src.BirthCity.Municipality.Name))
                .ForMember(d => d.Districtname, opt => opt.MapFrom(src => src.BirthCity.Municipality.District.Name))
                .ForMember(d => d.DecidingAuthName, opt => opt.MapFrom(src => src.DecidingAuth.Name))
                .ForMember(d => d.PersonAliases, opt => opt.MapFrom(src => src.BBullPersAliases))
                .ForMember(d => d.Nationalities, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Country.Name)))
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                           src.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                           src.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                           BulletinResources.Unspecified));

            CreateMap<SelectedPersonBulletinGridDTO, NInternalReqBulletin>();            
        }
    }
}