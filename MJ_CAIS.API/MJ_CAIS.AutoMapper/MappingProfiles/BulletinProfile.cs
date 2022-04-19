﻿using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.Bulletin;
using MJ_CAIS.DTO.Common;
using MJ_CAIS.DTO.Shared;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class BulletinProfile : Profile
    {
        public BulletinProfile()
        {
            CreateMap<BBulletin, BulletinGridDTO>()
                .ForMember(d => d.BulletinAuthorityName, opt => opt.MapFrom(src => src.BulletinAuthority.Name))
                .ForMember(d => d.StatusName, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                           src.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinConstants.Type.Bulletin78A :
                           src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                           BulletinConstants.Type.Unspecified));

            CreateMap<BulletinAddDTO, BBulletin>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Address.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId));

            CreateMap<BulletinEditDTO, BBulletin>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Address.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId));

            CreateMap<BBulletin, BulletinBaseDTO>()
                .ForMember(d => d.CsAuthorityName, opt => opt.MapFrom(src => src.CsAuthority.Name))
                .ForPath(d => d.Address.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.Address.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.Address.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForPath(d => d.Address.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Address.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Address.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
                .ForPath(d => d.Nationalities.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Id)))
                .ForPath(d => d.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.CountryId)));

            CreateMap<OffenceDTO, BOffence>()
                .ForMember(d => d.OffenceCatId, opt => opt.MapFrom(src => src.OffenceCategory.Id))
                .ForMember(d => d.EcrisOffCatId, opt => opt.MapFrom(src => src.EcrisOffCatId))
                .ForMember(d => d.OffPlaceDescr, opt => opt.MapFrom(src => src.OffPlace.ForeignCountryAddress))
                .ForMember(d => d.OffPlaceCountryId, opt => opt.MapFrom(src => src.OffPlace.Country.Id))
                .ForMember(d => d.OffPlaceCityId, opt => opt.MapFrom(src => src.OffPlace.CityId))
                .ForMember(d => d.OffLvlComplId, opt => opt.MapFrom(src => src.OffLvlComplId))
                .ForMember(d => d.OffLvlPartId, opt => opt.MapFrom(src => src.OffLvlPartId))
                .ForMember(d => d.OffPlaceCountry, opt => opt.Ignore());

            CreateMap<BOffence, OffenceDTO>()
               .ForMember(d => d.EcrisOffCatName, opt => opt.MapFrom(src => src.EcrisOffCat.Name))
               .ForMember(d => d.OffenceCategory, opt => opt.MapFrom(src => src.OffenceCat))
               .ForMember(d => d.OffLvlComplName, opt => opt.MapFrom(src => src.OffLvlCompl.Name))
               .ForMember(d => d.OffLvlPartName, opt => opt.MapFrom(src => src.OffLvlPart.Name))
               .ForMember(d => d.OffPlace, opt => opt.MapFrom(src =>
                    new AddressDTO
                    {
                        CityId = src.OffPlaceCityId,
                        DistrictId = src.OffPlaceCity.Municipality.DistrictId,
                        MunicipalityId = src.OffPlaceCity.MunicipalityId,
                        ForeignCountryAddress = src.OffPlaceDescr,
                        Country = new LookupDTO
                        {
                            Id = src.OffPlaceCountryId,
                            DisplayName = src.OffPlaceCountry.Name
                        }
                    }));
            //.ForPath(d => d.OffPlace.Country, opt => opt.MapFrom(src => src.OffPlaceCountry));

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

            CreateMap<BBullPersAlias, PersonAliasDTO>()
                .ForMember(d => d.TypeCode, opt => opt.MapFrom(src => src.Type));

            CreateMap<PersonAliasDTO, BBullPersAlias>()
                .ForMember(d => d.Type, opt => opt.MapFrom(src => src.TypeCode));

            CreateMap<BOffenceCategory, LookupDTO>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.DisplayName, opt => opt.MapFrom(src => src.Name));

            CreateMap<BBulletinStatusH, BulletinStatusHistoryDTO>()
              .ForMember(d => d.NewStatus, opt => opt.MapFrom(src => src.NewStatusCodeNavigation.Name))
              .ForMember(d => d.OldStatus, opt => opt.MapFrom(src => src.OldStatusCodeNavigation.Name));
        }
    }
}
