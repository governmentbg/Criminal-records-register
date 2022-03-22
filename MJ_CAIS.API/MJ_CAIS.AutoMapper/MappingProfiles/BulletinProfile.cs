using AutoMapper;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<BBulletin, BulletinGridDTO>();

            CreateMap<BulletinDTO, BBulletin>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Address.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.CountryId))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId));

            CreateMap<BBulletin, BulletinDTO>()
                .ForMember(d => d.CsAuthorityName, opt => opt.MapFrom(src => src.CsAuthority.Name))
                .ForPath(d => d.Address.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.Address.CountryId, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.Address.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Address.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Address.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
                .ForPath(d => d.Nationalities.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Id)))
                .ForPath(d => d.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.CountryId)));

            CreateMap<OffenceDTO, BOffence>()
                .ForMember(d => d.OffenceCatId, opt => opt.MapFrom(src => src.OffenceCatId))
                .ForMember(d => d.EcrisOffCatId, opt => opt.MapFrom(src => src.EcrisOffCatId))
                .ForMember(d => d.OffPlaceDescr, opt => opt.MapFrom(src => src.OffPlace.ForeignCountryAddress))
                .ForMember(d => d.OffPlaceCountryId, opt => opt.MapFrom(src => src.OffPlace.CountryId))
                .ForMember(d => d.OffPlaceCityId, opt => opt.MapFrom(src => src.OffPlace.CityId))
                .ForMember(d => d.OffLvlComplId, opt => opt.MapFrom(src => src.OffLvlComplId))
                .ForMember(d => d.OffLvlPartId, opt => opt.MapFrom(src => src.OffLvlPartId));
          
            CreateMap<SanctionDTO, BSanction>();

            CreateMap<BSanction, SanctionDTO>()
               .ForMember(d => d.SanctCategoryName, opt => opt.MapFrom(src => src.SanctCategory.Name))
               .ForMember(d => d.SanctProbCategName, opt => opt.MapFrom(src => src.SanctProbCateg.Name))
               .ForMember(d => d.EcrisSanctCategName, opt => opt.MapFrom(src => src.EcrisSanctCateg.Name))
               .ForMember(d => d.SanctProbMeasureName, opt => opt.MapFrom(src => src.SanctProbMeasure.Name))
               .ForMember(d => d.SanctActivityName, opt => opt.MapFrom(src => src.SanctActivity.Name));

            CreateMap<DecisionDTO, BDecision>();
            CreateMap<BDecision, DecisionDTO>()
              .ForMember(d => d.DecisionAuthName, opt => opt.MapFrom(src => src.DecisionAuth.Name))
              .ForMember(d => d.DecisionChTypeName, opt => opt.MapFrom(src => src.DecisionChType.Name))
              .ForMember(d => d.DecisionTypeName, opt => opt.MapFrom(src => src.DecisionType.Name));

            CreateMap<DocumentDTO, DDocument>()
             .ForMember(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocContent, opt => opt.Ignore());

            CreateMap<DDocument, DocumentDTO>()
             .ForMember(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());

            CreateMap<BBullPersAlias, PersonAliasDTO>()
                .ForMember(d => d.TypeCode, opt => opt.MapFrom(src => src.Type));

            CreateMap<PersonAliasDTO, BBullPersAlias>()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.TypeCode));
        }
    }
}
