﻿using AutoMapper;
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
                .ForPath(d => d.Address.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.Address.CountryId, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.Address.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Address.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Address.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
                .ForPath(d => d.Nationalities.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Id)))
                .ForPath(d => d.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.CountryId)));

            CreateMap<OffenceDTO, BOffence>()
                .ForPath(d => d.OffenceCat.Name, opt => opt.MapFrom(src => src.OffenceCatName))
                .ForPath(d => d.EcrisOffCat.Name, opt => opt.MapFrom(src => src.EcrisOffCatName))
                .ForPath(d => d.OffPlaceCountry.Name, opt => opt.MapFrom(src => src.OffPlaceCountryId))
                .ForPath(d => d.OffPlaceSubdiv.Name, opt => opt.MapFrom(src => src.OffPlaceSubdivName))
                .ForPath(d => d.OffPlaceCity.Name, opt => opt.MapFrom(src => src.OffPlaceCityName))
                .ForPath(d => d.OffLvlCompl.Name, opt => opt.MapFrom(src => src.OffLvlComplName))
                .ForPath(d => d.OffLvlPart.Name, opt => opt.MapFrom(src => src.OffLvlPartName))
                .ReverseMap();

            CreateMap<SanctionDTO, BSanction>()
               .ForPath(d => d.SanctCategory.Name, opt => opt.MapFrom(src => src.SanctCategoryName))
               .ForPath(d => d.SanctProbCateg.Name, opt => opt.MapFrom(src => src.SanctProbCategName))
               .ForPath(d => d.EcrisSanctCateg.Name, opt => opt.MapFrom(src => src.EcrisSanctCategName))
               .ForPath(d => d.SanctProbMeasure.Name, opt => opt.MapFrom(src => src.SanctProbMeasureName))
               .ForPath(d => d.SanctActivity.Name, opt => opt.MapFrom(src => src.SanctActivityName))
               .ReverseMap();

            CreateMap<DecisionDTO, BDecision>()
              .ForPath(d => d.DecisionAuth.Name, opt => opt.MapFrom(src => src.DecisionAuthName))
              .ForPath(d => d.DecisionChType.Name, opt => opt.MapFrom(src => src.DecisionChTypeName))
              .ForPath(d => d.DecisionType.Name, opt => opt.MapFrom(src => src.DecisionTypeName))
              .ForMember(d => d.DecisionAuth, opt => opt.Ignore())
              .ForMember(d => d.DecisionChType, opt => opt.Ignore())
              .ForMember(d => d.DecisionType, opt => opt.Ignore())
              .ReverseMap();

            CreateMap<DocumentDTO, DDocument>()
             .ForPath(d => d.DocType.Name, opt => opt.MapFrom(src => src.DocTypeName))
             .ForPath(d => d.DocContentId, opt => opt.MapFrom(src => src.DocumentContentId))
             .ForMember(d => d.DocType, opt => opt.Ignore())
             .ForMember(d => d.DocContent, opt => opt.Ignore())
             .ReverseMap()
             .ForPath(d => d.DocumentContentId, opt => opt.MapFrom(src => src.DocContent.Id))
             .ForMember(d => d.DocumentContent, opt => opt.Ignore());

            CreateMap<BBullPersAlias, PersonAliasDTO>()
                .ForMember(d => d.TypeCode, opt => opt.MapFrom(src => src.Type))
                .ReverseMap()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.TypeCode));

        }
    }
}
