using AutoMapper;
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
                .ForMember(d => d.BulletinType, opt => opt.MapFrom(src =>
                           src.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinConstants.Type.Bulletin78A :
                           src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinConstants.Type.ConvictionBulletin :
                           BulletinConstants.Type.Unspecified));

            CreateMap<BulletinAddDTO, BBulletin>()
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Address.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Address.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Address.CityId));

            CreateMap<BulletinEditDTO, BBulletin>()
               .ForMember(dest => dest.RegistrationNumber, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice || src.StatusId == BulletinConstants.Status.NewEISS);
                   opt.MapFrom(src => src.RegistrationNumber);
               })
               .ForMember(dest => dest.SequentialIndex, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice || src.StatusId == BulletinConstants.Status.NewEISS);
                   opt.MapFrom(src => src.SequentialIndex);
               })
               .ForMember(dest => dest.AlphabeticalIndex, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice || src.StatusId == BulletinConstants.Status.NewEISS);
                   opt.MapFrom(src => src.AlphabeticalIndex);
               })
               .ForMember(dest => dest.EcrisConvictionId, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice || src.StatusId == BulletinConstants.Status.NewEISS);
                   opt.MapFrom(src => src.EcrisConvictionId);
               })
               .ForMember(dest => dest.BulletinType, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice || src.StatusId == BulletinConstants.Status.NewEISS);
                   opt.MapFrom(src => src.BulletinType);
               })
               .ForMember(dest => dest.DecisionNumber, opt =>
               {
                   opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                   opt.MapFrom(src => src.DecisionNumber);
               })
                .ForMember(dest => dest.DecisionDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.DecisionDate);
                })
                .ForMember(dest => dest.DecisionFinalDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.DecisionFinalDate);
                })
                .ForMember(dest => dest.DecidingAuthId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.DecidingAuthId);
                })
                .ForMember(dest => dest.DecisionTypeId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.DecisionTypeId);
                })
                .ForMember(dest => dest.CaseTypeId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.CaseTypeId);
                })
                .ForMember(dest => dest.CaseNumber, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.CaseNumber);
                })
                .ForMember(dest => dest.CaseYear, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.CaseYear);
                })
                .ForMember(dest => dest.ConvRemarks, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.ConvRemarks);
                })
                .ForMember(dest => dest.DecisionEcli, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.DecisionEcli);
                })
                .ForMember(dest => dest.BulletinCreateDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BulletinCreateDate);
                })
                .ForMember(dest => dest.BulletinReceivedDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BulletinReceivedDate);
                })
                .ForMember(dest => dest.BulletinAuthorityId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BulletinAuthorityId);
                })
                .ForMember(dest => dest.CreatedByNames, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.CreatedByNames);
                })
                .ForMember(dest => dest.ApprovedByNames, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.ApprovedByNames);
                })
                .ForMember(dest => dest.ApprovedByPosition, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.ApprovedByPosition);
                })
                .ForMember(dest => dest.Firstname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Firstname);
                })
                .ForMember(dest => dest.Surname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Surname);
                })
                .ForMember(dest => dest.Familyname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Familyname);
                })
                .ForMember(dest => dest.Fullname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Fullname);
                })
                .ForMember(dest => dest.FirstnameLat, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FirstnameLat);
                })
                .ForMember(dest => dest.SurnameLat, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.SurnameLat);
                })
                .ForMember(dest => dest.FamilynameLat, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FamilynameLat);
                })
                .ForMember(dest => dest.Sex, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Sex);
                })
                .ForMember(dest => dest.Egn, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Egn);
                })
                .ForMember(dest => dest.Lnch, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Lnch);
                })
                .ForMember(dest => dest.Ln, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Ln);
                })
                .ForMember(dest => dest.BirthDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BirthDate);
                })
                .ForMember(dest => dest.BirthDatePrecision, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BirthDatePrecision);
                })
                .ForMember(dest => dest.BirthCityId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BirthCityId);
                })
                .ForMember(dest => dest.BirthCountryId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BirthCountryId);
                })
                .ForMember(dest => dest.BirthPlaceOther, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.BirthPlaceOther);
                })
                .ForMember(dest => dest.FullnameLat, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FullnameLat);
                })
                .ForMember(dest => dest.IdDocNumber, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocNumber);
                })
                .ForMember(dest => dest.IdDocCategoryId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocCategoryId);
                })
                .ForMember(dest => dest.IdDocTypeDescr, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocTypeDescr);
                })
                .ForMember(dest => dest.IdDocIssuingAuthority, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocIssuingAuthority);
                })
                .ForMember(dest => dest.IdDocIssuingDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocIssuingDate);
                })
                .ForMember(dest => dest.IdDocIssuingDatePrec, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocIssuingDatePrec);
                })
                .ForMember(dest => dest.IdDocValidDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocValidDate);
                })
                .ForMember(dest => dest.IdDocValidDatePrec, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.IdDocValidDatePrec);
                })
                .ForMember(dest => dest.MotherFirstname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.MotherFirstname);
                })
                .ForMember(dest => dest.MotherFamilyname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.MotherFamilyname);
                })
                .ForMember(dest => dest.MotherFullname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.MotherFullname);
                })
                .ForMember(dest => dest.FatherFirstname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FatherFirstname);
                })
                .ForMember(dest => dest.FatherSurname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FatherSurname);
                })
                .ForMember(dest => dest.FatherFamilyname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FatherFamilyname);
                })
                .ForMember(dest => dest.FatherFullname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.FatherFullname);
                })
                .ForMember(dest => dest.MotherSurname, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.MotherSurname);
                })
                .ForMember(dest => dest.AfisNumber, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.AfisNumber);
                })
                .ForMember(dest => dest.ConvIsTransmittable, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.ConvIsTransmittable);
                })
                .ForMember(dest => dest.ConvRetPeriodEndDate, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.ConvRetPeriodEndDate);
                })
                .ForMember(dest => dest.CreatedByPosition, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.CreatedByPosition);
                })
                .ForMember(dest => dest.BirthPlaceOther, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Address.ForeignCountryAddress);
                })
                .ForMember(dest => dest.BirthCountryId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Address.Country.Id);
                })
                .ForMember(dest => dest.BirthCityId, opt =>
                {
                    opt.PreCondition(src => src.StatusId == BulletinConstants.Status.NewOffice);
                    opt.MapFrom(src => src.Address.CityId);
                });

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
        }
    }
}
