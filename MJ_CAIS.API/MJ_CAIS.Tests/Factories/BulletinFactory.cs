using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using System;
using System.Linq;

namespace MJ_CAIS.Tests.Factories
{
    internal class BulletinFactory
    {
        public static BulletinType GetFilledInBulletinXSD(bool includeNomenclature)
        {
            var decisionDate = DateTime.Now.AddDays(1);
            var decisionFinalDate = DateTime.Now.AddYears(1);

            var result = new BulletinType
            {
                Type = BulletinTypesType.Bulletin78A,
                RegistrationData = new RegistrationData
                {
                    BulletinAlphabeticalIndex = "1",
                    BulletinReceivedDate = DateTime.Now,
                    BulletinReceivedDateSpecified = true,
                    ConvictionStatusAuthority = new ConvictionStatusAuthorityType(),
                    RegistrationNumber = "220605660301000000164",
                },
                Conviction = new ConvictionType
                {
                    ConvictionDecisions = new DecisionChangeType[]
                    {
                        new DecisionChangeType
                        {
                            Decision = new DecisionActType
                            {
                                DecisionDate = decisionDate,
                                DecisionFinalDate = decisionFinalDate,
                                DecisionType = DecisionTypeCategories.dkp_reshenie,
                                ECLI = "454497987",
                                FileNumber = "23212454"
                            },
                            DecisionChangeTypeReference = DecisionChangeTypeType.DCH00N,
                            DecisionChangeTypeReferenceSpecified = true,
                            DecisionRemarks = "remarkss..",
                            ReceiveDate = decisionFinalDate,
                            ReceiveDateSpecified = true,
                            ValidFrom =decisionDate,
                            ValidFromSpecified = true,
                        },
                    },
                    ConvictionOffence = new OffenceType[]
                    {
                        new OffenceType
                        {
                            FormOfGuilt = FormOfGuiltType.intentionally,
                            FormOfGuiltSpecified = true,
                            OffenceApplicableLegalProvisions = "off app",
                            OffenceCommonCategoryReference = new OffenceCommonCategoryType(),
                            OffenceEndDate = new DateType
                            {
                                Date = DateTime.Now.AddYears(1),
                                DatePrecision = DatePrecisionEnum.YMD,
                                DatePrecisionSpecified = true,
                            },
                            //OffenceId = "13148545",
                            OffencePlace = new PlaceType
                            {
                                Descr = "info",
                            },
                            OffenceStartDate = new DateType
                            {
                                Date = DateTime.Now.AddYears(-1),
                                DatePrecisionSpecified = false,
                            },
                            Remarks = "remarks"
                        },
                    },
                    ConvictionRemarks = "convicaiton remarks info......",
                    ConvictionSanction = new SanctionType[]
                    {
                        new SanctionType
                        {
                            Remarks = "Sanction remarks..",
                            Fine = new SanctionTypeFine
                            {
                                SanctionAmountOfIndividualFine = 20,
                                SanctionAmountOfIndividualFineSpecified = true
                            },
                            Other = new SanctionTypeOther
                            {
                                SanctionSentencedPeriodLength = "P6Y3M4DT120H"
                            },
                            Prison = new SanctionTypePrison
                            {
                                DetentionDescription = "P4Y3M4DT120H",
                                SanctionSentencedPeriod = "P6Y3M4DT120H",
                                SanctionSuspension = "P7Y3M4DT120H",
                            },
                            Probation = new SanctionTypeProbation[]
                            {
                                new SanctionTypeProbation
                                {
                                    SanctionSentencedPeriod = "P1Y2M3DT10H",
                                    ProbationValue = 159,
                                    ProbationValueSpecified = true,
                                },
                            },
                            SanctionCommonCategoryReference = new SanctionCommonCategoryType()
                           // SanctionId = "sanc type id",
                        },
                    },
                    CriminalCase = new CriminalCaseType
                    {
                        CaseNumber = "12378",
                        CaseType = CaseType.sign_ncd,
                        CaseYear = "2022",
                    },
                    Decision = new DecisionActType
                    {
                        DecisionDate = DateTime.Now.AddDays(1).AddMonths(1),
                        DecisionFinalDate = DateTime.Now.AddDays(2).AddMonths(2),
                        DecisionType = DecisionTypeCategories.dkp_sporazumenie,
                        ECLI = "ecli1456",
                        FileNumber = "2648",
                    },
                    EcrisConvictionId = "",
                    ServingPrevSuspendedSentence = true,
                    ServingPrevSuspendedSentenceActNumber = "act num",
                    ServingPrevSuspendedSentenceSpecified = true,
                    WithoutSanction = false,
                    WithoutSanctionSpecified = true,
                },
                IssuerData = new IssuerData
                {
                    BulletinApproverPerson = new OfficialPersonType
                    {
                        Names = new PersonNameType
                        {
                            FullName = "Approved FullName name",
                        },
                        Position = "approv position",
                    },
                    BulletinCreateDate = DateTime.Now.AddDays(-6),
                    BulletinCreatorPerson = new OfficialPersonType
                    {
                        Names = new PersonNameType
                        {
                            FullName = "creator FullName name",
                        },
                        Position = "creator position",
                    },
                },
                Person = new PersonType
                {
                    AFISNumber = "afis num",
                    BirthDate = new DateType
                    {
                        Date = DateTime.Now.AddDays(30).AddMinutes(60).AddYears(-20),
                        DatePrecision = DatePrecisionEnum.YMD,
                        DatePrecisionSpecified = true
                    },
                    BirthPlace = new PlaceType
                    {
                        Descr = "infoBirthPlace",
                    },
                    FatherNames = new PersonNameType
                    {
                        FirstName = "FatherNames FirstName",
                        FamilyName = "FatherNames FamilyName",
                        FullName = "FatherNames FullName",
                        SurName = "FatherNames SurName"
                    },
                    IdentityNumber = new PersonIdentityNumberType
                    {
                        EGN = "1014141415",
                        LN = "5641354",
                        LNCh = "98646164",
                        SUID = "54894dfg89dfgd1",
                    },
                    MotherNames = new PersonNameType
                    {
                        FirstName = "MotherNames FirstName",
                        FamilyName = "MotherNames FamilyName",
                        FullName = "MotherNames FullName",
                        SurName = "MotherNames SurName"
                    },
                    NamesBg = new PersonNameType
                    {
                        FirstName = "NamesBg FirstName",
                        FamilyName = "NamesBg FamilyName",
                        FullName = "NamesBg FullName",
                        SurName = "NamesBg SurName",
                    },
                    NamesEn = new PersonNameType
                    {
                        FirstName = "NamesEn FirstName",
                        FamilyName = "NamesEn FamilyName",
                        FullName = "NamesEn FullName",
                        SurName = "NamesEn SurName",
                    },
                    PersonIdentificationDocument = new IdentificationDocumentType
                    {
                        IdentificationDocumentCategoryReference = IdentificationDocumentCategoryType.ID00006,
                        IdentificationDocumentIssuingAuthority = "identity auth",
                        IdentificationDocumentIssuingDate = new DateType
                        {
                            Date = DateTime.Now.AddYears(-9),
                            DatePrecision = DatePrecisionEnum.Y,
                            DatePrecisionSpecified = true
                        },
                        IdentificationDocumentNumber = "identific num",
                        IdentificationDocumentType1 = "doc type 1",
                        IdentificationDocumentValidUntil = new DateType
                        {
                            Date = DateTime.Now.AddYears(10),
                            DatePrecision = DatePrecisionEnum.Y,
                            DatePrecisionSpecified = true
                        },
                    },
                    PersonNationality = new CountryType[]
                    {
                        new CountryType(),
                        new CountryType(),
                    },
                    PreviousNames = new PrevNames[]
                    {
                        new PrevNames
                        {
                            Names= new PersonNameType
                            {
                                 FirstName = "prevname FirstName",
                                 FamilyName = "prevname FamilyName",
                                 FullName = "prevname FullName",
                                 SurName = "prevname SurName",
                            },
                            NameType = NameTypesType.maiden,
                            NameTypeSpecified = true,
                        },
                    },
                    Sex = 1,
                },
            };

            if (includeNomenclature)
            {
                result.RegistrationData.ConvictionStatusAuthority.Code = "660";
                result.RegistrationData.ConvictionStatusAuthority.Name = "Министерство на правосъдието";
                result.Conviction.ConvictionDecisions[0].Decision.DecidingAuthority = new DecidingAuthorityType
                {
                    DecidingAuthorityCodeEIK = "210",
                    DecidingAuthorityCodeEISPP = "СВБ",
                    DecidingAuthorityName = "Окръжен съд - Бургас"
                };

                result.Conviction.ConvictionOffence[0].NationalCategoryCode = "11494";
                result.Conviction.ConvictionOffence[0].NationalCategoryTitle = "   чл.118-119, към група Убийство, при раздразнение, превишаване неизбежната отбрана";
                result.Conviction.ConvictionOffence[0].OffenceCommonCategoryReference.OffenceCode = "O-00-090900";
                result.Conviction.ConvictionOffence[0].OffenceCommonCategoryReference.OffenceName = "Квалифицирани случаи на изнудване";
                result.Conviction.ConvictionOffence[0].OffencePlace.City = new CityType
                {
                    EKATTECode = "14814",
                    CityName = "с. Гергини"
                };

                result.Conviction.ConvictionOffence[0].OffencePlace.Country = new CountryType
                {
                    CountryISONumber = "280",
                    CountryISOAlpha3 = "FD",
                    CountryName = "(Западна Германия)Федерална Рпублика Германия"
                };

                result.Conviction.ConvictionSanction[0].NationalCategoryCode = "nkz_globa";
                result.Conviction.ConvictionSanction[0].NationalCategoryTitle = "Глоба";
                result.Conviction.ConvictionSanction[0].Probation[0].ProbationCategoryCode = "pbc_bvz_trud";
                result.Conviction.ConvictionSanction[0].Probation[0].ProbationCategoryTitle = "безвъзмезден труд";
                result.Conviction.ConvictionSanction[0].Probation[0].ProbationMeasureCode = "pbcu_broi_vnoski";
                result.Conviction.ConvictionSanction[0].Probation[0].ProbationMeasureTitle = "брой вноски";
                result.Conviction.ConvictionSanction[0].SanctionCommonCategoryReference.SanctionCode = "1000";
                result.Conviction.ConvictionSanction[0].SanctionCommonCategoryReference.SanctionText = "Лишаване от свобода";

                result.Conviction.CriminalCase.CaseAuthority = new DecidingAuthorityType
                {
                    DecidingAuthorityCodeEIK = "211",
                    DecidingAuthorityCodeEISPP = "СДА",
                    DecidingAuthorityName = "Районен съд - Айтос",
                };

                result.Conviction.Decision.DecidingAuthority = new DecidingAuthorityType
                {
                    DecidingAuthorityCodeEIK = "214",
                    DecidingAuthorityCodeEISPP = "СИИ",
                    DecidingAuthorityName = "Районен съд - Малко Търново",
                };

                result.IssuerData.BulletinCreatorAuthority = new DecidingAuthorityType
                {
                    DecidingAuthorityCodeEIK = "218",
                    DecidingAuthorityCodeEISPP = "СЛМ",
                    DecidingAuthorityName = "Районен съд - Царево",
                };


                result.Person.BirthPlace.City = new CityType
                {
                    EKATTECode = "29324",
                    CityName = "с. Живко"
                };

                result.Person.BirthPlace.Country = new CountryType
                {
                    CountryISONumber = "31",
                    CountryISOAlpha3 = "AZE",
                    CountryName = "Азербайджан"
                };

                result.Person.PersonNationality[0].CountryISONumber = "624";
                result.Person.PersonNationality[0].CountryISOAlpha3 = "GNB";
                result.Person.PersonNationality[0].CountryName = "Гвинея Бисау";
                result.Person.PersonNationality[1].CountryISONumber = "100";
                result.Person.PersonNationality[1].CountryISOAlpha3 = "BGR";
                result.Person.PersonNationality[1].CountryName = "България";
            }
            return result;
        }

        public static BBulletin GetFilledInBulletinDb(bool includeNomenclature)
        {
            var result = new BBulletin
            {
                BulletinType = BulletinTypesType.Bulletin78A.ToString(),
                AlphabeticalIndex = "1",
                BulletinReceivedDate = DateTime.Now.AddDays(-1),
                RegistrationNumber = "846598456165198",
                BDecisions = new BDecision[] {
                    new BDecision
                    {
                        DecisionDate = DateTime.Now.AddDays(9),
                        DecisionFinalDate = DateTime.Now.AddDays(12),
                        DecisionTypeId = DecisionTypeCategories.dkp_reshenie.ToString(),
                        DecisionEcli = "454497987",
                        DecisionNumber =     "23212454",
                        DecisionChTypeId = "DCH-00-N",
                        Descr = "remarkss..",
                    }
                },
                BOffences = new BOffence[]
                {
                    new BOffence
                    {
                        FormOfGuiltId=FormOfGuiltType.intentionally.ToString(),
                        LegalProvisions = "off app",
                        OffEndDate = DateTime.Now.AddYears(3),
                        OffEndDatePrec = "YMD",
                        OffPlaceDescr ="desc",
                        OffStartDate = DateTime.Now.AddMonths(6),
                        Remarks = "remarks"
                    }
                },
                ConvRemarks = "desc conv",
                BSanctions = new BSanction[]
                {
                    new BSanction
                    {
                        Descr = "desc",
                        FineAmount = 123,
                        DecisionDurationYears = 1,
                        DecisionDurationDays = 2,
                        DecisionDurationMonths = 3,
                        DecisionDurationHours = 4,
                        DetenctionDescr = "desc det",
                        SuspentionDurationYears = 4,
                        SuspentionDurationMonths = 5,
                        SuspentionDurationDays = 6,
                        SuspentionDurationHours = 7,
                        BProbations = new BProbation[]
                        {
                            new BProbation
                            {
                                DecisionDurationDays = 1,
                                DecisionDurationYears = 2,
                                DecisionDurationMonths = 1,
                                DecisionDurationHours = 3,
                                SanctProbValue = 23,
                            }
                        }
                    }
                },
                CaseTypeId = "sign_noxd",
                CaseNumber = "1223165",
                CaseYear = 2022,
                DecisionDate = DateTime.Now.AddDays(1).AddMonths(1),
                DecisionFinalDate = DateTime.Now.AddDays(2).AddMonths(2),
                DecisionTypeId = DecisionTypeCategories.dkp_sporazumenie.ToString(),
                DecisionNumber = "122",
                DecisionEcli = "ecli1456",
                EcrisConvictionId = "1321316dsf",
                PrevSuspSent = true,
                PrevSuspSentDescr = "prev sus desc",
                NoSanction = false,
                BulletinCreateDate = DateTime.Now.AddHours(3),
                ApprovedByNames = "test appr",
                ApprovedByPosition = "position",
                CreatedByNames = "Test 123",
                CreatedByPosition = "created posioton",
                AfisNumber = "",
                Firstname = "Stefka",
                Surname = "Yonkova",
                Familyname = "Vasileva",
                Fullname = "Stefka Yonkova Vasileva",
                FirstnameLat = "Stef",
                SurnameLat = "Yon",
                FamilynameLat = "Vas",
                Sex = 2,
                Egn = "9309083636",
                Ln = "16445464650",
                Lnch = "49846165465",
                BirthDate = DateTime.Now.AddYears(-20),
                BirthDatePrecision = "YMD",
                BirthPlaceOther = "desc birth place",
                FullnameLat = "Stef vas",
                IdDocNumber = "2",
                IdDocCategoryId = "ID-00-001",
                IdDocTypeDescr = "12",
                IdDocIssuingAuthority = "mwr",
                IdDocIssuingDate = DateTime.Now.AddMonths(1).AddDays(4),
                IdDocValidDate = DateTime.Now.AddMonths(10).AddDays(4).AddYears(10),
                IdDocValidDatePrec = "YM",
                MotherFirstname = "mother first name",
                MotherFamilyname = "mother family name",
                MotherSurname = "mother sur name",
                MotherFullname = "mother full name",
                FatherFirstname = "father first name",
                FatherSurname = "father sur name",
                FatherFamilyname = "father family name",
                FatherFullname = "father full name",
            };

            if (includeNomenclature)
            {
                result.CsAuthority = new GCsAuthority
                {
                    Code = "660",
                    Name = "cs name"
                };

                result.BOffences.First().OffenceCat = new BOffenceCategory { Code = "11494", Name = "off name" };
                result.BOffences.First().EcrisOffCat = new BEcrisOffCategory { Id = "O-00-090900", Name = "ecrisof name" };
                result.BOffences.First().OffPlaceCity = new GCity { Id = "14814" };

                result.BSanctions.First().SanctCategory = new BSanctionCategory { Code = "78", Name = "sanction name" };
                result.BSanctions.First().EcrisSanctCateg = new BEcrisStanctCateg { Category = "1000", Name = "sanction ecris name" };
                
                result.BSanctions.First().BProbations.First().SanctProbCateg = new BSanctProbCategory { Code = "pbc_bvz_trud" , Name = "prob name"};
                result.BSanctions.First().BProbations.First().SanctProbMeasure = new BSanctProbMeasure { Code = "pbcu_broi_vnoski", Name = "sanc prob mesure name" };
                result.BirthCity = new GCity { Id = "14814" };
            }

            return result;
        }
    }
}
