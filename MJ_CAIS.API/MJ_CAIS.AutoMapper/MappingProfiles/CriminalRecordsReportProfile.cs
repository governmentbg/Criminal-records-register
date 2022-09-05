using AutoMapper;
using MJ_CAIS.AutoMapperContainer.Resolvers;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CriminalRecordsReportProfile : Profile
    {
        public CriminalRecordsReportProfile()
        {
            CreateMap<BBulletin, BulletinType>()
               .ForMember(d => d.Type, opt => opt.MapFrom(src => src.BulletinType == BulletinConstants.Type.Bulletin78A ? BulletinTypesType.Bulletin78A :
                    src.BulletinType == BulletinConstants.Type.ConvictionBulletin ? BulletinTypesType.ConvictionBulletin : BulletinTypesType.Unspecified))
               .ForPath(d => d.Person.NamesBg.FirstName, opt => opt.MapFrom(src => src.Firstname))
               .ForPath(d => d.Person.NamesBg.SurName, opt => opt.MapFrom(src => src.Surname))
               .ForPath(d => d.Person.NamesBg.FamilyName, opt => opt.MapFrom(src => src.Familyname))
               .ForPath(d => d.Person.NamesBg.FullName, opt => opt.MapFrom(src => src.Fullname))
               .ForPath(d => d.Person.NamesEn.FirstName, opt => opt.MapFrom(src => src.FirstnameLat))
               .ForPath(d => d.Person.NamesEn.SurName, opt => opt.MapFrom(src => src.SurnameLat))
               .ForPath(d => d.Person.NamesEn.FamilyName, opt => opt.MapFrom(src => src.FamilynameLat))
               .ForPath(d => d.Person.NamesEn.FullName, opt => opt.MapFrom(src => src.FullnameLat))
               .ForPath(d => d.Person.Sex, opt => opt.MapFrom(src => src.Sex))
               .ForPath(d => d.Person.IdentityNumber.EGN, opt => opt.MapFrom(src => src.Egn))
               .ForPath(d => d.Person.IdentityNumber.LNCh, opt => opt.MapFrom(src => src.Lnch))
               .ForPath(d => d.Person.IdentityNumber.LN, opt => opt.MapFrom(src => src.Ln))
               .ForPath(d => d.Person.IdentityNumber.SUID, opt => opt.MapFrom(src => src.Suid))
               .ForPath(d => d.Person.BirthDate, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.BirthDate, src.BirthDatePrecision)))
               .ForPath(d => d.Person.BirthPlace.Country, opt => opt.MapFrom(src => src.BirthCountry))
               .ForPath(d => d.Person.BirthPlace.City, opt => opt.MapFrom(src => src.BirthCity))
               .ForPath(d => d.Person.BirthPlace.Descr, opt => opt.MapFrom(src => src.BirthPlaceOther))
               .ForPath(d => d.Person.PersonNationality, opt => opt.MapFrom(src => src.BPersNationalities))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentCategoryReference, opt => opt.MapFrom(src => CriminalRecordsReportResolver.StringToEnum<IdentificationDocumentCategoryType>(src.IdDocCategoryId)))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentType1, opt => opt.MapFrom(src => src.IdDocTypeDescr))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentNumber, opt => opt.MapFrom(src => src.IdDocNumber))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentIssuingAuthority, opt => opt.MapFrom(src => src.IdDocIssuingAuthority))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentIssuingDate, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.IdDocIssuingDate, src.IdDocIssuingDatePrec)))
               .ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentValidUntil, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.IdDocValidDate, src.IdDocValidDatePrec)))
               .ForPath(d => d.Person.MotherNames.FirstName, opt => opt.MapFrom(src => src.MotherFirstname))
               .ForPath(d => d.Person.MotherNames.SurName, opt => opt.MapFrom(src => src.MotherSurname))
               .ForPath(d => d.Person.MotherNames.FamilyName, opt => opt.MapFrom(src => src.MotherFamilyname))
               .ForPath(d => d.Person.MotherNames.FullName, opt => opt.MapFrom(src => src.MotherFullname))
               .ForPath(d => d.Person.FatherNames.FirstName, opt => opt.MapFrom(src => src.FatherFirstname))
               .ForPath(d => d.Person.FatherNames.SurName, opt => opt.MapFrom(src => src.FatherSurname))
               .ForPath(d => d.Person.FatherNames.FamilyName, opt => opt.MapFrom(src => src.FatherFamilyname))
               .ForPath(d => d.Person.FatherNames.FullName, opt => opt.MapFrom(src => src.FatherFullname))
               .ForPath(d => d.Person.AFISNumber, opt => opt.MapFrom(src => src.AfisNumber))
               .ForPath(d => d.Person.PreviousNames, opt => opt.MapFrom(src => src.BBullPersAliases))
               .ForPath(d => d.Conviction.Decision.FileNumber, opt => opt.MapFrom(src => src.DecisionNumber))
               .ForPath(d => d.Conviction.Decision.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
               .ForPath(d => d.Conviction.Decision.DecisionFinalDate, opt => opt.MapFrom(src => src.DecisionFinalDate))
               .ForPath(d => d.Conviction.Decision.DecidingAuthority, opt => opt.MapFrom(src => src.DecidingAuth))
               .ForPath(d => d.Conviction.Decision.ECLI, opt => opt.MapFrom(src => src.DecisionEcli))
               .ForPath(d => d.Conviction.Decision.DecisionType, opt => opt.MapFrom(src => src.DecisionTypeId))
               .ForPath(d => d.Conviction.CriminalCase.CaseType, opt => opt.MapFrom(src => src.CaseTypeId))
               .ForPath(d => d.Conviction.CriminalCase.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
               .ForPath(d => d.Conviction.CriminalCase.CaseYear, opt => opt.MapFrom(src => src.CaseYear))
               .ForPath(d => d.Conviction.CriminalCase.CaseAuthority, opt => opt.MapFrom(src => src.CaseAuth))
               .ForPath(d => d.Conviction.ConvictionRemarks, opt => opt.MapFrom(src => src.ConvRemarks))
               .ForPath(d => d.Conviction.ConvictionOffence, opt => opt.MapFrom(src => src.BOffences))
               .ForPath(d => d.Conviction.WithoutSanction, opt => opt.MapFrom(src => src.NoSanction))
               .ForPath(d => d.Conviction.WithoutSanctionSpecified, opt => opt.MapFrom(src => src.NoSanction.HasValue))
               .ForPath(d => d.Conviction.ConvictionSanction, opt => opt.MapFrom(src => src.BSanctions))
               .ForPath(d => d.Conviction.ServingPrevSuspendedSentence, opt => opt.MapFrom(src => src.PrevSuspSent))
               .ForPath(d => d.Conviction.ServingPrevSuspendedSentenceSpecified, opt => opt.MapFrom(src => src.PrevSuspSent.HasValue))
               .ForPath(d => d.Conviction.ServingPrevSuspendedSentenceActNumber, opt => opt.MapFrom(src => src.PrevSuspSentDescr))
               .ForPath(d => d.Conviction.ConvictionDecisions, opt => opt.MapFrom(src => src.BDecisions))
               .ForPath(d => d.Conviction.EcrisConvictionId, opt => opt.MapFrom(src => src.EcrisConvictionId))
               .ForPath(d => d.IssuerData.BulletinCreateDate, opt => opt.MapFrom(src => src.BulletinCreateDate))
               .ForPath(d => d.IssuerData.BulletinCreatorPerson.Names.FullName, opt => opt.MapFrom(src => src.CreatedByNames))
               .ForPath(d => d.IssuerData.BulletinCreatorPerson.Position, opt => opt.MapFrom(src => src.CreatedByPosition))
               .ForPath(d => d.IssuerData.BulletinApproverPerson.Names.FullName, opt => opt.MapFrom(src => src.ApprovedByNames))
               .ForPath(d => d.IssuerData.BulletinApproverPerson.Position, opt => opt.MapFrom(src => src.ApprovedByPosition))
               .ForPath(d => d.IssuerData.BulletinCreatorAuthority, opt => opt.MapFrom(src => src.BulletinAuthority))
               .ForPath(d => d.RegistrationData.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationNumber))
               .ForPath(d => d.RegistrationData.BulletinAlphabeticalIndex, opt => opt.MapFrom(src => src.AlphabeticalIndex))
               .ForPath(d => d.RegistrationData.BulletinReceivedDate, opt => opt.MapFrom(src => src.BulletinReceivedDate))
               .ForPath(d => d.RegistrationData.BulletinReceivedDateSpecified, opt => opt.MapFrom(src => src.BulletinReceivedDate.HasValue))
               .ForPath(d => d.RegistrationData.ConvictionStatusAuthority.Name, opt => opt.MapFrom(src => src.CsAuthority.Name))
               .ForPath(d => d.RegistrationData.ConvictionStatusAuthority.Code, opt => opt.MapFrom(src => src.CsAuthority.Code));

            CreateMap<BulletinType, BBulletin>()
              .ForMember(d => d.BulletinType, opt => opt.MapFrom(src => src.Type.ToString()))
              .ForMember(d => d.Firstname, opt => opt.MapFrom(src => src.Person.NamesBg.FirstName))
              .ForMember(d => d.Surname, opt => opt.MapFrom(src => src.Person.NamesBg.SurName))
              .ForMember(d => d.Familyname, opt => opt.MapFrom(src => src.Person.NamesBg.FamilyName))
              .ForMember(d => d.Fullname, opt => opt.MapFrom(src => src.Person.NamesBg.FullName))
              .ForMember(d => d.FirstnameLat, opt => opt.MapFrom(src => src.Person.NamesEn.FirstName))
              .ForMember(d => d.SurnameLat, opt => opt.MapFrom(src => src.Person.NamesEn.SurName))
              .ForMember(d => d.FamilynameLat, opt => opt.MapFrom(src => src.Person.NamesEn.FamilyName))
              .ForMember(d => d.FullnameLat, opt => opt.MapFrom(src => src.Person.NamesEn.FullName))
              .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Person.Sex))
              .ForMember(d => d.Egn, opt => opt.MapFrom(src => src.Person.IdentityNumber.EGN))
              .ForMember(d => d.Lnch, opt => opt.MapFrom(src => src.Person.IdentityNumber.LNCh))
              .ForMember(d => d.Ln, opt => opt.MapFrom(src => src.Person.IdentityNumber.LN))
              .ForMember(d => d.Suid, opt => opt.MapFrom(src => src.Person.IdentityNumber.SUID))
              .ForMember(d => d.BirthDate, opt => opt.MapFrom(src => src.Person.BirthDate.Date))
              .ForMember(dest => dest.BirthDatePrecision, opt =>
              {
                  opt.PreCondition(src => src.Person.BirthDate.DatePrecisionSpecified);
                  opt.MapFrom(src => src.Person.BirthDate.DatePrecision.ToString());
              })
              .ForMember(d => d.BirthCityId, opt => opt.MapFrom(src => src.Person.BirthPlace.City.EKATTECode))
              .ForPath(d => d.BPersNationalities, opt => opt.MapFrom(src => src.Person.PersonNationality))
              //.ForMember(d => d.BirthCountryId, opt => opt.MapFrom(src => src.Person.BirthPlace.Country.CountryISOAlpha3)) ?? id
              .ForMember(d => d.BirthPlaceOther, opt => opt.MapFrom(src => src.Person.BirthPlace.Descr))
              .ForMember(d => d.IdDocCategoryId, opt => opt.MapFrom(src => CriminalRecordsReportResolver.EnumToString(src.Person.PersonIdentificationDocument.IdentificationDocumentCategoryReference)))
              .ForMember(d => d.IdDocTypeDescr, opt => opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentType1))
              .ForMember(d => d.IdDocNumber, opt => opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentNumber))
              .ForMember(d => d.IdDocIssuingAuthority, opt => opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentIssuingAuthority))
              .ForMember(d => d.IdDocIssuingDate, opt => opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentIssuingDate.Date))
              .ForMember(dest => dest.IdDocIssuingDatePrec, opt =>
              {
                  opt.PreCondition(src => src.Person.PersonIdentificationDocument.IdentificationDocumentIssuingDate.DatePrecisionSpecified);
                  opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentIssuingDate.DatePrecision.ToString());
              })
              .ForMember(d => d.IdDocValidDate, opt => opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentValidUntil.Date))
               .ForMember(dest => dest.IdDocValidDatePrec, opt =>
               {
                   opt.PreCondition(src => src.Person.PersonIdentificationDocument.IdentificationDocumentValidUntil.DatePrecisionSpecified);
                   opt.MapFrom(src => src.Person.PersonIdentificationDocument.IdentificationDocumentValidUntil.DatePrecision.ToString());
               })
              .ForMember(d => d.MotherFirstname, opt => opt.MapFrom(src => src.Person.MotherNames.FirstName))
              .ForMember(d => d.MotherSurname, opt => opt.MapFrom(src => src.Person.MotherNames.SurName))
              .ForMember(d => d.MotherFamilyname, opt => opt.MapFrom(src => src.Person.MotherNames.FamilyName))
              .ForMember(d => d.MotherFullname, opt => opt.MapFrom(src => src.Person.MotherNames.FullName))
              .ForMember(d => d.FatherFirstname, opt => opt.MapFrom(src => src.Person.FatherNames.FirstName))
              .ForMember(d => d.FatherSurname, opt => opt.MapFrom(src => src.Person.FatherNames.SurName))
              .ForMember(d => d.FatherFamilyname, opt => opt.MapFrom(src => src.Person.FatherNames.FamilyName))
              .ForMember(d => d.FatherFullname, opt => opt.MapFrom(src => src.Person.FatherNames.FullName))
              .ForMember(d => d.AfisNumber, opt => opt.MapFrom(src => src.Person.AFISNumber))
              .ForPath(d => d.BBullPersAliases, opt => opt.MapFrom(src => src.Person.PreviousNames))
              .ForMember(d => d.DecisionNumber, opt => opt.MapFrom(src => src.Conviction.Decision.FileNumber))
              .ForMember(d => d.DecisionDate, opt => opt.MapFrom(src => src.Conviction.Decision.DecisionDate))
              .ForMember(d => d.DecisionFinalDate, opt => opt.MapFrom(src => src.Conviction.Decision.DecisionFinalDate))
              .ForMember(d => d.DecisionEcli, opt => opt.MapFrom(src => src.Conviction.Decision.ECLI))
              .ForMember(d => d.DecisionTypeId, opt => opt.MapFrom(src => CriminalRecordsReportResolver.EnumToString(src.Conviction.Decision.DecisionType)))
              //.ForMember(d => d.DecidingAuthId, opt => opt.MapFrom(src => src.Conviction.Decision.DecidingAuthority.DecidingAuthorityCodeEIK)) //todo:? id
              //.ForMember(d => d.CaseAuthId, opt => opt.MapFrom(src => src.Conviction.CriminalCase.CaseAuthority.DecidingAuthorityCodeEIK)) // todo:? id
              .ForMember(d => d.CaseTypeId, opt => opt.MapFrom(src => src.Conviction.CriminalCase.CaseType))
              .ForMember(d => d.CaseNumber, opt => opt.MapFrom(src => src.Conviction.CriminalCase.CaseNumber))
              .ForMember(d => d.CaseYear, opt => opt.MapFrom(src => src.Conviction.CriminalCase.CaseYear))
              .ForMember(d => d.ConvRemarks, opt => opt.MapFrom(src => src.Conviction.ConvictionRemarks))
              .ForMember(d => d.BOffences, opt => opt.MapFrom(src => src.Conviction.ConvictionOffence))
              .ForPath(d => d.BDecisions, opt => opt.MapFrom(src => src.Conviction.ConvictionDecisions))
              .ForMember(dest => dest.NoSanction, opt =>
              {
                  opt.PreCondition(src => src.Conviction.WithoutSanctionSpecified);
                  opt.MapFrom(src => src.Conviction.WithoutSanction);
              })
              .ForMember(d => d.BSanctions, opt => opt.MapFrom(src => src.Conviction.ConvictionSanction))
              .ForMember(dest => dest.PrevSuspSent, opt =>
              {
                  opt.PreCondition(src => src.Conviction.ServingPrevSuspendedSentenceSpecified);
                  opt.MapFrom(src => src.Conviction.ServingPrevSuspendedSentence);
              })
              .ForMember(d => d.PrevSuspSentDescr, opt => opt.MapFrom(src => src.Conviction.ServingPrevSuspendedSentenceActNumber))
              .ForMember(d => d.EcrisConvictionId, opt => opt.MapFrom(src => src.Conviction.EcrisConvictionId))
              //.ForPath(d => d.BulletinAuthorityId, opt => opt.MapFrom(src => src. IssuerData.BulletinCreatorAuthority.DecidingAuthorityCodeEISPP))?? id            
              .ForMember(d => d.BulletinCreateDate, opt => opt.MapFrom(src => src.IssuerData.BulletinCreateDate))
              .ForMember(d => d.CreatedByNames, opt => opt.MapFrom(src => src.IssuerData.BulletinCreatorPerson.Names.FullName))
              .ForMember(d => d.CreatedByPosition, opt => opt.MapFrom(src => src.IssuerData.BulletinCreatorPerson.Position))
              .ForMember(d => d.ApprovedByNames, opt => opt.MapFrom(src => src.IssuerData.BulletinApproverPerson.Names.FullName))
              .ForMember(d => d.ApprovedByPosition, opt => opt.MapFrom(src => src.IssuerData.BulletinApproverPerson.Position))
              .ForMember(d => d.RegistrationNumber, opt => opt.MapFrom(src => src.RegistrationData.RegistrationNumber))
              .ForMember(d => d.AlphabeticalIndex, opt => opt.MapFrom(src => src.RegistrationData.BulletinAlphabeticalIndex))
              .ForMember(dest => dest.BulletinReceivedDate, opt =>
              {
                  opt.PreCondition(src => src.RegistrationData?.BulletinReceivedDateSpecified == true);
                  opt.MapFrom(src => src.RegistrationData.BulletinReceivedDate);
              })
              .ForPath(d => d.CsAuthorityId, opt => opt.MapFrom(src => src.RegistrationData.ConvictionStatusAuthority.Code));

            CreateMap<GCountry, CountryType>()
                .ForMember(d => d.CountryISONumber, opt => opt.MapFrom(src => src.Iso31662Number))
                .ForMember(d => d.CountryISOAlpha3, opt => opt.MapFrom(src => src.Iso31662Code))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Name));

            CreateMap<GCity, CityType>()
               .ForMember(d => d.EKATTECode, opt => opt.MapFrom(src => src.EkatteCode))
               .ForMember(d => d.CityName, opt => opt.MapFrom(src => src.Name));

            CreateMap<BPersNationality, CountryType>()
                .ForMember(d => d.CountryISOAlpha3, opt => opt.MapFrom(src => src.Country.Iso31662Code))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(d => d.CountryISONumber, opt => opt.MapFrom(src => src.Country.Iso31662Number));

            CreateMap<CountryType, BPersNationality>()
                .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<GDecidingAuthority, DecidingAuthorityType>()
                .ForMember(d => d.DecidingAuthorityName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.DecidingAuthorityCodeEIK, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.DecidingAuthorityCodeEISPP, opt => opt.MapFrom(src => src.EisppCode));

            CreateMap<BOffence, OffenceType>()
               //.ForMember(d => d.OffenceId, opt => opt.MapFrom(src => src.OffenceId)) // todo 
               .ForMember(d => d.NationalCategoryCode, opt => opt.MapFrom(src => src.OffenceCat.Code))
               .ForMember(d => d.NationalCategoryTitle, opt => opt.MapFrom(src => src.OffenceCat.Name))
               .ForMember(d => d.Remarks, opt => opt.MapFrom(src => src.Remarks))
               .ForPath(d => d.OffenceCommonCategoryReference.OffenceCode, opt => opt.MapFrom(src => src.EcrisOffCat.Id))
               .ForPath(d => d.OffenceCommonCategoryReference.OffenceName, opt => opt.MapFrom(src => src.EcrisOffCat.Name))
               .ForMember(d => d.OffenceApplicableLegalProvisions, opt => opt.MapFrom(src => src.LegalProvisions))
               .ForMember(d => d.OffenceStartDate, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.OffStartDate, src.OffStartDatePrec)))
               .ForMember(d => d.OffenceEndDate, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.OffEndDate, src.OffEndDatePrec)))
               .ForPath(d => d.OffencePlace.Country, opt => opt.MapFrom(src => src.OffPlaceCountry))
               .ForPath(d => d.OffencePlace.City, opt => opt.MapFrom(src => src.OffPlaceCity))
               .ForPath(d => d.OffencePlace.Descr, opt => opt.MapFrom(src => src.OffPlaceDescr))
               .ForMember(d => d.FormOfGuilt, opt => opt.MapFrom(src => src.FormOfGuiltId))
               .ForMember(d => d.FormOfGuiltSpecified, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.FormOfGuiltId)));

            CreateMap<OffenceType, BOffence>()
                .ForPath(d => d.OffenceCatId, opt => opt.MapFrom(src => src.NationalCategoryCode))
               .ForPath(d => d.EcrisOffCatId, opt => opt.MapFrom(src => src.OffenceCommonCategoryReference.OffenceCode))
               .ForMember(d => d.LegalProvisions, opt => opt.MapFrom(src => src.OffenceApplicableLegalProvisions))
               .ForMember(d => d.OffStartDate, opt => opt.MapFrom(src => src.OffenceStartDate.Date))
               .ForMember(dest => dest.OffStartDatePrec, opt =>
               {
                   opt.PreCondition(src => src.OffenceStartDate.DatePrecisionSpecified);
                   opt.MapFrom(src => src.OffenceStartDate.DatePrecision.ToString());
               })
               .ForMember(d => d.OffEndDate, opt => opt.MapFrom(src => src.OffenceEndDate.Date))
               .ForMember(dest => dest.OffEndDatePrec, opt =>
               {
                   opt.PreCondition(src => src.OffenceEndDate.DatePrecisionSpecified);
                   opt.MapFrom(src => src.OffenceEndDate.DatePrecision.ToString());
               })
               .ForMember(d => d.OffPlaceCityId, opt => opt.MapFrom(src => src.OffencePlace.City.EKATTECode))
               //.ForMember(d => d.OffPlaceCountryId, opt => opt.MapFrom(src => src.OffencePlace.Country.CountryISONumber))// todo:
               .ForMember(d => d.OffPlaceDescr, opt => opt.MapFrom(src => src.OffencePlace.Descr))
                .ForMember(dest => dest.FormOfGuiltId, opt =>
                {
                    opt.PreCondition(src => src.FormOfGuiltSpecified);
                    opt.MapFrom(src => src.FormOfGuilt.ToString());
                })
               .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
               .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<BSanction, SanctionType>()
              //.ForMember(d => d.SanctionId, opt => opt.MapFrom(src => src.SanctionId)) // todo
              .ForMember(d => d.NationalCategoryCode, opt => opt.MapFrom(src => src.SanctCategory.Code))
              .ForMember(d => d.NationalCategoryTitle, opt => opt.MapFrom(src => src.SanctCategory.Name))
              .ForPath(d => d.SanctionCommonCategoryReference.SanctionCode, opt => opt.MapFrom(src => src.EcrisSanctCateg.Category))
              .ForPath(d => d.SanctionCommonCategoryReference.SanctionText, opt => opt.MapFrom(src => src.EcrisSanctCateg.Name))
              .ForPath(d => d.Fine.SanctionAmountOfIndividualFine, opt => opt.MapFrom(src => src.FineAmount))
              .ForPath(d => d.Fine.SanctionAmountOfIndividualFineSpecified, opt => opt.MapFrom(src => src.FineAmount.HasValue))
              .ForMember(d => d.Probation, opt => opt.MapFrom(src => src.BProbations))
              .ForMember(d => d.Remarks, opt => opt.MapFrom(src => src.Descr))
              .ForPath(d => d.Prison.SanctionSentencedPeriod, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)))
              .ForPath(d => d.Prison.SanctionSuspension, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.SuspentionDurationYears, src.SuspentionDurationMonths, src.SuspentionDurationDays, src.SuspentionDurationHours)))
              .ForPath(d => d.Prison.DetentionDescription, opt => opt.MapFrom(src => src.DetenctionDescr))
              .ForPath(d => d.Other.SanctionSentencedPeriodLength, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)));

            CreateMap<SanctionType, BSanction>()
              .ForPath(d => d.SanctCategoryId, opt => opt.MapFrom(src => src.NationalCategoryCode))
              .ForPath(d => d.EcrisSanctCategId, opt => opt.MapFrom(src => src.SanctionCommonCategoryReference.SanctionCode))
              .ForPath(d => d.FineAmount, opt => opt.MapFrom(src => src.Fine.SanctionAmountOfIndividualFine))
              .ForMember(d => d.Descr, opt => opt.MapFrom(src => src.Remarks))
               .ForMember(dest => dest.FineAmount, opt =>
               {
                   opt.PreCondition(src => src.Fine.SanctionAmountOfIndividualFineSpecified);
                   opt.MapFrom(src => src.Fine.SanctionAmountOfIndividualFine);
               })
              .ForMember(d => d.BProbations, opt => opt.MapFrom(src => src.Probation))
              .ForMember(d => d.DecisionDurationYears, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationYearPattern)))
              .ForMember(d => d.DecisionDurationMonths, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationMonthPattern)))
              .ForMember(d => d.DecisionDurationDays, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationDayPattern)))
              .ForMember(d => d.DecisionDurationHours, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationHourPattern)))
              .ForMember(d => d.SuspentionDurationYears, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSuspension, CriminalRecordsReportResolver.DurationYearPattern)))
              .ForMember(d => d.SuspentionDurationMonths, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSuspension, CriminalRecordsReportResolver.DurationMonthPattern)))
              .ForMember(d => d.SuspentionDurationDays, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSuspension, CriminalRecordsReportResolver.DurationDayPattern)))
              .ForMember(d => d.SuspentionDurationHours, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Prison.SanctionSuspension, CriminalRecordsReportResolver.DurationHourPattern)))
              .ForMember(d => d.DetenctionDescr, opt => opt.MapFrom(src => src.Prison.DetentionDescription))
              .ForMember(d => d.DecisionDurationYears, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Other.SanctionSentencedPeriodLength, CriminalRecordsReportResolver.DurationYearPattern)))
              .ForMember(d => d.DecisionDurationMonths, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Other.SanctionSentencedPeriodLength, CriminalRecordsReportResolver.DurationMonthPattern)))
              .ForMember(d => d.DecisionDurationDays, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Other.SanctionSentencedPeriodLength, CriminalRecordsReportResolver.DurationDayPattern)))
              .ForMember(d => d.DecisionDurationHours, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.Other.SanctionSentencedPeriodLength, CriminalRecordsReportResolver.DurationHourPattern)))
              .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
              .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<BProbation, SanctionTypeProbation>()
                //.ForMember(d => d.SanctionId, opt => opt.MapFrom(src => src.SanctionId)) // todo
                .ForMember(d => d.ProbationCategoryCode, opt => opt.MapFrom(src => src.SanctProbCateg.Code))
                .ForMember(d => d.ProbationCategoryTitle, opt => opt.MapFrom(src => src.SanctProbCateg.Name))
                .ForMember(d => d.ProbationValue, opt => opt.MapFrom(src => src.SanctProbValue))
                .ForMember(d => d.ProbationValueSpecified, opt => opt.MapFrom(src => src.SanctProbValue.HasValue))
                .ForMember(d => d.ProbationMeasureCode, opt => opt.MapFrom(src => src.SanctProbMeasure.Code))
                .ForMember(d => d.ProbationMeasureTitle, opt => opt.MapFrom(src => src.SanctProbMeasure.Name))
                .ForMember(d => d.SanctionSentencedPeriod, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)));

            CreateMap<SanctionTypeProbation, BProbation>()
                .ForMember(d => d.SanctProbCategId, opt => opt.MapFrom(src => src.ProbationCategoryCode))
                .ForMember(d => d.SanctProbValue, opt => opt.MapFrom(src => src.ProbationValue))
                .ForMember(d => d.SanctProbMeasureId, opt => opt.MapFrom(src => src.ProbationMeasureCode))
                .ForMember(d => d.DecisionDurationYears, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationYearPattern)))
                .ForMember(d => d.DecisionDurationMonths, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationMonthPattern)))
                .ForMember(d => d.DecisionDurationDays, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationDayPattern)))
                .ForMember(d => d.DecisionDurationHours, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDurationPart(src.SanctionSentencedPeriod, CriminalRecordsReportResolver.DurationHourPattern)))
                .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<BDecision, DecisionChangeType>()
                .ForMember(d => d.DecisionChangeTypeReference, opt => opt.MapFrom(src => CriminalRecordsReportResolver.StringToEnum<DecisionChangeTypeType>(src.DecisionChTypeId)))
                .ForMember(d => d.DecisionChangeTypeReferenceSpecified, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DecisionChTypeId)))
                .ForPath(d => d.Decision.FileNumber, opt => opt.MapFrom(src => src.DecisionNumber))
                .ForPath(d => d.Decision.DecisionFinalDate, opt => opt.MapFrom(src => src.DecisionFinalDate))
                .ForPath(d => d.Decision.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
                .ForPath(d => d.Decision.DecidingAuthority, opt => opt.MapFrom(src => src.DecisionAuth))
                .ForPath(d => d.Decision.ECLI, opt => opt.MapFrom(src => src.DecisionEcli))
                .ForPath(d => d.Decision.DecisionType, opt => opt.MapFrom(src => CriminalRecordsReportResolver.StringToEnum<DecisionTypeCategories>(src.DecisionTypeId)))
                .ForMember(d => d.DecisionRemarks, opt => opt.MapFrom(src => src.Descr))
                .ForMember(d => d.ValidFrom, opt => opt.MapFrom(src => src.DecisionFinalDate))
                .ForMember(d => d.ValidFromSpecified, opt => opt.MapFrom(src => src.DecisionFinalDate.HasValue))
                .ForMember(d => d.ReceiveDate, opt => opt.MapFrom(src => src.DecisionDate))
                .ForMember(d => d.ReceiveDateSpecified, opt => opt.MapFrom(src => src.DecisionDate.HasValue));

            CreateMap<DecisionChangeType, BDecision>()
            .ForMember(dest => dest.DecisionChTypeId, opt =>
            {
                opt.PreCondition(src => src.DecisionChangeTypeReferenceSpecified);
                opt.MapFrom(src => CriminalRecordsReportResolver.EnumToString(src.DecisionChangeTypeReference));
            })
            .ForMember(d => d.DecisionTypeId, opt => opt.MapFrom(src => CriminalRecordsReportResolver.EnumToString(src.Decision.DecisionType)))
            .ForMember(d => d.DecisionNumber, opt => opt.MapFrom(src => src.Decision.FileNumber))
            .ForMember(d => d.DecisionFinalDate, opt => opt.MapFrom(src => src.Decision.DecisionFinalDate))
            .ForMember(d => d.DecisionDate, opt => opt.MapFrom(src => src.Decision.DecisionDate))
            .ForMember(d => d.DecisionEcli, opt => opt.MapFrom(src => src.Decision.ECLI))
            .ForMember(d => d.Descr, opt => opt.MapFrom(src => src.DecisionRemarks))
            .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
            //.ForPath(d => d.DecisionAuthId, opt => opt.MapFrom(src => src.Decision.DecidingAuthority.DecidingAuthorityCodeEIK)) ??
            .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<PPerson, CriminalRecordsPersonDataType>()
                .ForPath(d => d.NamesBg.FirstName, opt => opt.MapFrom(src => src.Firstname))
                .ForPath(d => d.NamesBg.SurName, opt => opt.MapFrom(src => src.Surname))
                .ForPath(d => d.NamesBg.FamilyName, opt => opt.MapFrom(src => src.Familyname))
                .ForPath(d => d.NamesBg.FullName, opt => opt.MapFrom(src => src.FullnameLat))
                .ForPath(d => d.NamesEn.FirstName, opt => opt.MapFrom(src => src.FirstnameLat))
                .ForPath(d => d.NamesEn.SurName, opt => opt.MapFrom(src => src.SurnameLat))
                .ForPath(d => d.NamesEn.FamilyName, opt => opt.MapFrom(src => src.FamilynameLat))
                .ForPath(d => d.NamesEn.FullName, opt => opt.MapFrom(src => src.FullnameLat))
                .ForMember(d => d.Sex, opt => opt.MapFrom(src => src.Sex))
                .ForPath(d => d.IdentityNumber, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPersonPids(src.PPersonIds)))
                .ForPath(d => d.BirthDate, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetDateType(src.BirthDate, src.BirthDatePrec)))
                .ForPath(d => d.BirthPlace.Country, opt => opt.MapFrom(src => src.BirthCountry))
                .ForPath(d => d.BirthPlace.City, opt => opt.MapFrom(src => src.BirthCity))
                .ForPath(d => d.BirthPlace.Descr, opt => opt.MapFrom(src => src.BirthPlaceOther))
                //.ForPath(d => d.PersonNationality, opt => opt.MapFrom(src => src.)) // todo: ? 
                .ForPath(d => d.MotherNames.FirstName, opt => opt.MapFrom(src => src.MotherFirstname))
                .ForPath(d => d.MotherNames.SurName, opt => opt.MapFrom(src => src.MotherSurname))
                .ForPath(d => d.MotherNames.FamilyName, opt => opt.MapFrom(src => src.MotherFamilyname))
                .ForPath(d => d.MotherNames.FullName, opt => opt.MapFrom(src => src.MotherFullname))
                .ForPath(d => d.FatherNames.FirstName, opt => opt.MapFrom(src => src.FatherFirstname))
                .ForPath(d => d.FatherNames.SurName, opt => opt.MapFrom(src => src.FatherSurname))
                .ForPath(d => d.FatherNames.FamilyName, opt => opt.MapFrom(src => src.FatherFamilyname))
                .ForPath(d => d.FatherNames.FullName, opt => opt.MapFrom(src => src.FatherFullname));

            CreateMap<PrevNames, BBullPersAlias>()
                .ForPath(d => d.Firstname, opt => opt.MapFrom(src => src.Names.FirstName))
                .ForPath(d => d.Surname, opt => opt.MapFrom(src => src.Names.SurName))
                .ForPath(d => d.Familyname, opt => opt.MapFrom(src => src.Names.FamilyName))
                .ForPath(d => d.Fullname, opt => opt.MapFrom(src => src.Names.FullName))
                .ForMember(dest => dest.Type, opt =>
                {
                    opt.PreCondition(src => src.NameTypeSpecified);
                    opt.MapFrom(src => CriminalRecordsReportResolver.EnumToString(src.NameType));
                })
                .ForMember(d => d.EntityState, opt => opt.MapFrom(src => EntityStateEnum.Added))
                .ForMember(d => d.Id, opt => opt.MapFrom(src => DataAccess.BaseEntity.GenerateNewId()));

            CreateMap<BBullPersAlias, PrevNames>()
              .ForPath(d => d.Names.FirstName, opt => opt.MapFrom(src => src.Firstname))
              .ForPath(d => d.Names.SurName, opt => opt.MapFrom(src => src.Surname))
              .ForPath(d => d.Names.FamilyName, opt => opt.MapFrom(src => src.Familyname))
              .ForPath(d => d.Names.FullName, opt => opt.MapFrom(src => src.Fullname))
              .ForPath(d => d.NameTypeSpecified, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.Type)))
              .ForPath(d => d.NameType, opt => opt.MapFrom(src => CriminalRecordsReportResolver.StringToEnum<NameTypesType>(src.Type)));
        }
    }
}
