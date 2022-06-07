using AutoMapper;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Resources;
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
                           src.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinResources.Bulletin78A :
                           src.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinResources.ConvictionBulletin :
                           BulletinResources.Unspecified));

            CreateMap<BulletinAddDTO, BBulletin>()
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Person.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Person.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Person.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Person.Fullname))
                .ForMember(d => d.FirstnameLat, opt => opt.MapFrom(src => src.Person.FirstnameLat))
                .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.Person.SurnameLat))
                .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.Person.FamilynameLat))
                .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.Person.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Person.Sex))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Person.Egn))
                .ForMember(d => d.Lnch, opt => opt.MapFrom(src => src.Person.Lnch))
                .ForMember(d => d.Ln, opt => opt.MapFrom(src => src.Person.Ln))
                .ForMember(d => d.AfisNumber, opt => opt.MapFrom(src => src.Person.AfisNumber))
                .ForMember(d => d.IdDocNumber, opt => opt.MapFrom(src => src.Person.IdDocNumber))
                .ForMember(d => d.IdDocCategoryId, opt => opt.MapFrom(src => src.Person.IdDocCategoryId))
                .ForMember(d => d.IdDocTypeDescr, opt => opt.MapFrom(src => src.Person.IdDocTypeDescr))
                .ForMember(d => d.IdDocIssuingAuthority, opt => opt.MapFrom(src => src.Person.IdDocIssuingAuthority))
                .ForMember(d => d.IdDocIssuingDate, opt => opt.MapFrom(src => src.Person.IdDocIssuingDate))
                .ForMember(d => d.IdDocValidDate, opt => opt.MapFrom(src => src.Person.IdDocValidDate))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.Person.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.Person.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.Person.MotherFamilyname))
                .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.Person.MotherFullname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.Person.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.Person.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.Person.FatherFamilyname))
                .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.Person.FatherFullname))
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Person.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Person.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Person.BirthPlace.CityId))
                .ForMember(d => d.RegistrationNumber, opt => opt.Ignore());

            CreateMap<BulletinEditDTO, BBulletin>()
                .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Person.Firstname))
                .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Person.Surname))
                .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Person.Familyname))
                .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Person.Fullname))
                .ForMember(d => d.FirstnameLat, opt => opt.MapFrom(src => src.Person.FirstnameLat))
                .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.Person.SurnameLat))
                .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.Person.FamilynameLat))
                .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.Person.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Person.Sex))
                .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate))
                .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Person.Egn))
                .ForMember(d => d.Lnch, opt => opt.MapFrom(src => src.Person.Lnch))
                .ForMember(d => d.Ln, opt => opt.MapFrom(src => src.Person.Ln))
                .ForMember(d => d.AfisNumber, opt => opt.MapFrom(src => src.Person.AfisNumber))
                .ForMember(d => d.IdDocNumber, opt => opt.MapFrom(src => src.Person.IdDocNumber))
                .ForMember(d => d.IdDocCategoryId, opt => opt.MapFrom(src => src.Person.IdDocCategoryId))
                .ForMember(d => d.IdDocTypeDescr, opt => opt.MapFrom(src => src.Person.IdDocTypeDescr))
                .ForMember(d => d.IdDocIssuingAuthority, opt => opt.MapFrom(src => src.Person.IdDocIssuingAuthority))
                .ForMember(d => d.IdDocIssuingDate, opt => opt.MapFrom(src => src.Person.IdDocIssuingDate))
                .ForMember(d => d.IdDocValidDate, opt => opt.MapFrom(src => src.Person.IdDocValidDate))
                .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.Person.MotherFirstname))
                .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.Person.MotherSurname))
                .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.Person.MotherFamilyname))
                .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.Person.MotherFullname))
                .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.Person.FatherFirstname))
                .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.Person.FatherSurname))
                .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.Person.FatherFamilyname))
                .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.Person.FatherFullname))
                .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Person.BirthPlace.ForeignCountryAddress))
                .ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Person.BirthPlace.Country.Id))
                .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Person.BirthPlace.CityId))
                .ForMember(d => d.RegistrationNumber, opt => opt.Ignore());

            CreateMap<BBulletin, BulletinBaseDTO>()
                .ForMember(d => d.RegistrationNumberDisplay, opt => opt.MapFrom(src => src.RegistrationNumber))
                .ForPath(d => d.Person.Suid, opt => opt.MapFrom(src => src.Suid))
                .ForPath(d => d.Person.Firstname, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(d => d.Person.Surname, opt => opt.MapFrom(src => src.Surname))
                .ForPath(d => d.Person.Familyname, opt => opt.MapFrom(src => src.Familyname))
                .ForPath(d => d.Person.Fullname, opt => opt.MapFrom(src => src.Fullname))
                .ForPath(d => d.Person.FirstnameLat, opt => opt.MapFrom(src => src.FirstnameLat))
                .ForPath(d => d.Person.SurnameLat, opt => opt.MapFrom(src => src.SurnameLat))
                .ForPath(d => d.Person.FamilynameLat, opt => opt.MapFrom(src => src.FamilynameLat))
                .ForPath(d => d.Person.FullnameLat, opt => opt.MapFrom(src => src.FullnameLat))
                .ForPath(d => d.Person.Sex, opt => opt.MapFrom(src => src.Sex))
                .ForPath(d => d.Person.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
                .ForPath(d => d.Person.Egn, opt => opt.MapFrom(src => src.Egn))
                .ForPath(d => d.Person.Lnch, opt => opt.MapFrom(src => src.Lnch))
                .ForPath(d => d.Person.Ln, opt => opt.MapFrom(src => src.Ln))
                .ForPath(d => d.Person.AfisNumber, opt => opt.MapFrom(src => src.AfisNumber))
                .ForPath(d => d.Person.IdDocNumber, opt => opt.MapFrom(src => src.IdDocNumber))
                .ForPath(d => d.Person.IdDocCategoryId, opt => opt.MapFrom(src => src.IdDocCategoryId))
                .ForPath(d => d.Person.IdDocTypeDescr, opt => opt.MapFrom(src => src.IdDocTypeDescr))
                .ForPath(d => d.Person.IdDocIssuingAuthority, opt => opt.MapFrom(src => src.IdDocIssuingAuthority))
                .ForPath(d => d.Person.IdDocIssuingDate, opt => opt.MapFrom(src => src.IdDocIssuingDate))
                .ForPath(d => d.Person.IdDocValidDate, opt => opt.MapFrom(src => src.IdDocValidDate))
                .ForPath(d => d.Person.MotherFirstname, opt => opt.MapFrom(src => src.MotherFirstname))
                .ForPath(d => d.Person.MotherSurname, opt => opt.MapFrom(src => src.MotherSurname))
                .ForPath(d => d.Person.MotherFamilyname, opt => opt.MapFrom(src => src.MotherFamilyname))
                .ForPath(d => d.Person.MotherFullname, opt => opt.MapFrom(src => src.MotherFullname))
                .ForPath(d => d.Person.FatherFirstname, opt => opt.MapFrom(src => src.FatherFirstname))
                .ForPath(d => d.Person.FatherSurname, opt => opt.MapFrom(src => src.FatherSurname))
                .ForPath(d => d.Person.FatherFamilyname, opt => opt.MapFrom(src => src.FatherFamilyname))
                .ForPath(d => d.Person.FatherFullname, opt => opt.MapFrom(src => src.FatherFullname))
                .ForPath(d => d.Person.BirthPlace.ForeignCountryAddress, opt => opt.MapFrom(src => src.BirthPlaceOther))
                .ForPath(d => d.Person.BirthPlace.Country.Id, opt => opt.MapFrom(src => src.BirthCountryId))
                .ForPath(d => d.Person.BirthPlace.Country.DisplayName, opt => opt.MapFrom(src => src.BirthCountry.Name))
                .ForPath(d => d.Person.BirthPlace.CityId, opt => opt.MapFrom(src => src.BirthCityId))
                .ForPath(d => d.Person.BirthPlace.MunicipalityId, opt => opt.MapFrom(src => src.BirthCity != null ? src.BirthCity.MunicipalityId : null))
                .ForPath(d => d.Person.BirthPlace.DistrictId, opt => opt.MapFrom(src => src.BirthCity != null && src.BirthCity.Municipality != null ? src.BirthCity.Municipality.DistrictId : null))
                .ForPath(d => d.Person.Nationalities.SelectedPrimaryKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.Id)))
                .ForPath(d => d.Person.Nationalities.SelectedForeignKeys, opt => opt.MapFrom(src => src.BPersNationalities.Select(x => x.CountryId)));

            CreateMap<OffenceDTO, BOffence>()
                .ForMember(d => d.OffenceCatId, opt => opt.MapFrom(src => src.OffenceCategory.Id))
                .ForMember(d => d.EcrisOffCatId, opt => opt.MapFrom(src => src.EcrisOffCatId))
                .ForMember(d => d.OffPlaceDescr, opt => opt.MapFrom(src => src.OffPlace.ForeignCountryAddress))
                .ForMember(d => d.OffPlaceCountryId, opt => opt.MapFrom(src => src.OffPlace.Country.Id))
                .ForMember(d => d.OffPlaceCityId, opt => opt.MapFrom(src => src.OffPlace.CityId))                
                .ForMember(d => d.OffPlaceCountry, opt => opt.Ignore());

            CreateMap<BOffence, OffenceDTO>()
               .ForMember(d => d.EcrisOffCatName, opt => opt.MapFrom(src => src.EcrisOffCat.Name))
               .ForMember(d => d.OffenceCategory, opt => opt.MapFrom(src => src.OffenceCat))
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

            CreateMap<SanctionDTO, BSanction>();

            CreateMap<BSanction, SanctionDTO>()
               .ForMember(d => d.SanctCategoryName, opt => opt.MapFrom(src => src.SanctCategory.Name))
               .ForMember(d => d.EcrisSanctCategName, opt => opt.MapFrom(src => src.EcrisSanctCateg.Name))
               .ForMember(d => d.Probations, opt => opt.MapFrom(src => src.BProbations));


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

            CreateMap<BulletinProbationDTO, BProbation>();

            CreateMap<BProbation, BulletinProbationDTO>();

        }
    }
}
