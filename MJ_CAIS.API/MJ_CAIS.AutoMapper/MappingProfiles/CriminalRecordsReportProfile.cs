using AutoMapper;
using MJ_CAIS.AutoMapperContainer.Resolvers;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;

namespace MJ_CAIS.AutoMapperContainer.MappingProfiles
{
    public class CriminalRecordsReportProfile : Profile
    {
        public CriminalRecordsReportProfile()
        {
            CreateMap<BBulletin, BulletinType>()
               .ForMember(d => d.Type, opt => opt.MapFrom(src => src.BulletinType == nameof(BulletinConstants.Type.Bulletin78A) ? BulletinTypesType.Bulletin78A :
                    src.BulletinType == nameof(BulletinConstants.Type.ConvictionBulletin) ? BulletinTypesType.ConvictionBulletin : BulletinTypesType.Unspecified))
               .ForPath(d => d.Person.NamesBg.FirstName, opt => opt.MapFrom(src => src.Firstname))
               .ForPath(d => d.Person.NamesBg.SurName, opt => opt.MapFrom(src => src.Surname))
               .ForPath(d => d.Person.NamesBg.FamilyName, opt => opt.MapFrom(src => src.Familyname))
               .ForPath(d => d.Person.NamesBg.FullName, opt => opt.MapFrom(src => src.Fullname))
               .ForPath(d => d.Person.NamesEn.FirstName, opt => opt.MapFrom(src => src.FirstnameLat))
               .ForPath(d => d.Person.NamesEn.SurName, opt => opt.MapFrom(src => src.Surname))
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
               //.ForPath(d => d.Person.PersonIdentificationDocument.IdentificationDocumentCategoryReference, opt => opt.MapFrom(src => src.IdDocCategoryId))
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
               .ForPath(d => d.Conviction.Decision.FileNumber, opt => opt.MapFrom(src => src.DecisionNumber))
               .ForPath(d => d.Conviction.Decision.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
               .ForPath(d => d.Conviction.Decision.DecisionFinalDate, opt => opt.MapFrom(src => src.DecisionFinalDate))
               .ForPath(d => d.Conviction.Decision.DecidingAuthority, opt => opt.MapFrom(src => src.DecidingAuth))
               .ForPath(d => d.Conviction.Decision.ECLI, opt => opt.MapFrom(src => src.DecisionEcli))
               .ForPath(d => d.Conviction.Decision.DecisionType, opt => opt.MapFrom(src =>  src.DecisionType.Name))
               .ForPath(d => d.Conviction.CriminalCase.CaseType, opt => opt.MapFrom(src => src.CaseType.Name))
               .ForPath(d => d.Conviction.CriminalCase.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
               .ForPath(d => d.Conviction.CriminalCase.CaseYear, opt => opt.MapFrom(src => src.CaseTypeId))
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
               .ForPath(d => d.IssuerData.BulletinCreateDate, opt => opt.MapFrom(src => src.CreatedOn))
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

            CreateMap<GCountry, CountryType>()
                .ForMember(d => d.CountryISONumber, opt => opt.MapFrom(src => src.Iso31662Number))
                .ForMember(d => d.CountryISOAlpha3, opt => opt.MapFrom(src => src.Iso3166Alpha2))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Name));

            CreateMap<GCity, CityType>()
               .ForMember(d => d.EKATTECode, opt => opt.MapFrom(src => src.EkatteCode))
               .ForMember(d => d.CityName, opt => opt.MapFrom(src => src.Name));

            CreateMap<BPersNationality, CountryType>()
                .ForMember(d => d.CountryISOAlpha3, opt => opt.MapFrom(src => src.Country.Iso3166Alpha2))
                .ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(d => d.CountryISONumber, opt => opt.MapFrom(src => src.Country.Iso31662Number));

            CreateMap<GDecidingAuthority, DecidingAuthorityType>()
                .ForMember(d => d.DecidingAuthorityName, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.DecidingAuthorityCodeEIK, opt => opt.MapFrom(src => src.Code))
                .ForMember(d => d.DecidingAuthorityCodeEISPP, opt => opt.MapFrom(src => src.EisppCode));

            CreateMap<BOffence, OffenceType>()
               //.ForMember(d => d.OffenceId, opt => opt.MapFrom(src => src.OffenceId)) // todo 
               .ForMember(d => d.NationalCategoryCode, opt => opt.MapFrom(src => src.OffenceCat.Id))
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
               .ForMember(d => d.FormOfGuiltSpecified, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.FormOfGuiltId)));

            CreateMap<BSanction, SanctionType>()
              //.ForMember(d => d.SanctionId, opt => opt.MapFrom(src => src.SanctionId)) // todo
              .ForMember(d => d.NationalCategoryCode, opt => opt.MapFrom(src => src.SanctCategory.Code))
              .ForMember(d => d.NationalCategoryTitle, opt => opt.MapFrom(src => src.SanctCategory.Name))
              .ForPath(d => d.SanctionCommonCategoryReference.SanctionCode, opt => opt.MapFrom(src => src.EcrisSanctCateg.Category))
              .ForPath(d => d.SanctionCommonCategoryReference.SanctionText, opt => opt.MapFrom(src => src.EcrisSanctCateg.Name))
              .ForPath(d => d.Fine.SanctionAmountOfIndividualFine, opt => opt.MapFrom(src => src.FineAmount))
              .ForPath(d => d.Fine.SanctionAmountOfIndividualFineSpecified, opt => opt.MapFrom(src => src.FineAmount.HasValue))
              .ForMember(d => d.Probation, opt => opt.MapFrom(src => src.BProbations))
              .ForPath(d => d.Prison.SanctionSentencedPeriod, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)))
              .ForPath(d => d.Prison.SanctionSuspension, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.SuspentionDurationYears, src.SuspentionDurationMonths, src.SuspentionDurationDays, src.SuspentionDurationHours)))
              .ForPath(d => d.Prison.DetentionDescription, opt => opt.MapFrom(src => src.DetenctionDescr))
              .ForPath(d => d.Other.SanctionSentencedPeriodLength, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)));

            CreateMap<BProbation, SanctionTypeProbation>()
                //.ForMember(d => d.SanctionId, opt => opt.MapFrom(src => src.SanctionId)) // todo
                .ForMember(d => d.ProbationCategoryCode, opt => opt.MapFrom(src => src.SanctProbCateg.Code))
                .ForMember(d => d.ProbationCategoryTitle, opt => opt.MapFrom(src => src.SanctProbCateg.Name))
                .ForMember(d => d.ProbationValue, opt => opt.MapFrom(src => src.SanctProbValue))
                .ForMember(d => d.ProbationValueSpecified, opt => opt.MapFrom(src => src.SanctProbValue.HasValue))
                .ForMember(d => d.ProbationMeasureCode, opt => opt.MapFrom(src => src.SanctProbMeasure.Code))
                .ForMember(d => d.ProbationMeasureTitle, opt => opt.MapFrom(src => src.SanctProbMeasure.Name))
                .ForMember(d => d.SanctionSentencedPeriod, opt => opt.MapFrom(src => CriminalRecordsReportResolver.GetPeriodFromNumbers(src.DecisionDurationYears, src.DecisionDurationMonths, src.DecisionDurationDays, src.DecisionDurationHours)));

            CreateMap<BDecision, DecisionChangeType>()
                .ForMember(d => d.DecisionChangeTypeReference, opt => opt.MapFrom(src => CriminalRecordsReportResolver.StringToEnum<DecisionChangeTypeType>(src.DecisionChTypeId)))
                .ForMember(d => d.DecisionChangeTypeReferenceSpecified, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DecisionChTypeId)))
                .ForPath(d => d.Decision.FileNumber, opt => opt.MapFrom(src => src.DecisionNumber))
                .ForPath(d => d.Decision.DecisionFinalDate, opt => opt.MapFrom(src => src.DecisionFinalDate))
                .ForPath(d => d.Decision.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
                .ForPath(d => d.Decision.DecidingAuthority, opt => opt.MapFrom(src => src.DecisionAuth))
                .ForPath(d => d.Decision.ECLI, opt => opt.MapFrom(src => src.DecisionEcli))
                .ForMember(d => d.DecisionRemarks, opt => opt.MapFrom(src => src.Descr))
                .ForMember(d => d.ValidFrom, opt => opt.MapFrom(src => src.DecisionDate))
                .ForMember(d => d.ValidFromSpecified, opt => opt.MapFrom(src => src.DecisionDate.HasValue))
                .ForMember(d => d.ReceiveDate, opt => opt.MapFrom(src => src.DecisionFinalDate))
                .ForMember(d => d.ReceiveDateSpecified, opt => opt.MapFrom(src => src.DecisionFinalDate.HasValue));

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
        }
    }
}
