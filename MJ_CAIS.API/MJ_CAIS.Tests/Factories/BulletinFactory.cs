using MJ_CAIS.DTO.ExternalServicesHost;
using System;

namespace MJ_CAIS.Tests.Factories
{
    internal class BulletinFactory
    {
        public static BulletinType GetFilledInBulletinXSD()
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
                    ConvictionStatusAuthority = new ConvictionStatusAuthorityType
                    {
                        //Code = "660",
                        //Name = "",
                    },
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
                                //DecidingAuthority = new DecidingAuthorityType
                                //{
                                //    DecidingAuthorityCodeEIK = "eik123",
                                //    DecidingAuthorityCodeEISPP = "eisp11",
                                //    DecidingAuthorityName = "deciding authority test name",
                                //},
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
                            //NationalCategoryCode = "bg1",
                            //NationalCategoryTitle = "bulgaria",
                            OffenceApplicableLegalProvisions = "off app",
                            OffenceCommonCategoryReference = new OffenceCommonCategoryType
                            {
                               // OffenceCode = "offCode",
                                //OffenceName = "off name"
                            },
                            OffenceEndDate = new DateType
                            {
                                Date = DateTime.Now.AddYears(1),
                                DatePrecision = DatePrecisionEnum.YMD,
                                DatePrecisionSpecified = true,
                            },
                            //OffenceId = "13148545",
                            OffencePlace = new PlaceType
                            {
                                //City = new CityType
                                //{
                                //    CityName = "sofia",
                                //    EKATTECode = "s5465",
                                //},
                                //Country = new CountryType
                                //{
                                //    CountryISOAlpha3 = "79874",
                                //    CountryISONumber = "113131",
                                //    CountryName = "country name12",
                                //},
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
                            //NationalCategoryCode = "bg888",
                            //NationalCategoryTitle = "nat type",
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
                                    //ProbationCategoryCode = "procCOde",
                                    //ProbationCategoryTitle = "prob title",
                                    //ProbationMeasureCode = "prob mes code",
                                    //ProbationMeasureTitle = "prob mes titile",
                                    ProbationValue = 159,
                                    ProbationValueSpecified = true,
                                },
                            },
                            SanctionCommonCategoryReference = new SanctionCommonCategoryType
                            {
                                //SanctionCode = "sanc code1",
                                //SanctionText = "sanction code"
                            },
                           // SanctionId = "sanc type id",
                        },
                    },
                    CriminalCase = new CriminalCaseType
                    {
                        //CaseAuthority = new DecidingAuthorityType
                        //{
                        //    DecidingAuthorityCodeEIK = "dec eik1",
                        //    DecidingAuthorityCodeEISPP = "dec eispp1",
                        //    DecidingAuthorityName = "dec name",
                        //},
                        CaseNumber = "12378",
                        CaseType = CaseType.sign_ncd,
                        CaseYear = "2022",
                    },
                    Decision = new DecisionActType
                    {
                        //DecidingAuthority = new DecidingAuthorityType
                        //{
                        //    DecidingAuthorityCodeEIK = "dec eik2",
                        //    DecidingAuthorityCodeEISPP = "dec eispp2",
                        //    DecidingAuthorityName = "dec name22",
                        //},
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
                            // bulletin has one column
                            //FamilyName = "Approved famili name",
                            //FirstName = "Approved FirstName name",
                            FullName = "Approved FullName name",
                            //SurName = "Approved SurName name"
                        },
                        Position = "approv position",
                    },
                    BulletinCreateDate = DateTime.Now.AddDays(-6),
                    //BulletinCreatorAuthority = new DecidingAuthorityType
                    //{
                    //    DecidingAuthorityCodeEIK = "bull dec eik",
                    //    DecidingAuthorityCodeEISPP = "bull dec eispp",
                    //    DecidingAuthorityName = "bull dec name"
                    //},
                    BulletinCreatorPerson = new OfficialPersonType
                    {
                        Names = new PersonNameType
                        {
                            //FamilyName = "creator famili name",
                            //FirstName = "creator FirstName name",
                            FullName = "creator FullName name",
                            //SurName = "creator SurName name",
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
                        // не може да се тества защото тези обекти не се мапят към пропъртитата на бюлетин от базата
                        //City = new CityType
                        //{
                        //    CityName = "sofia BirthPlace",
                        //    EKATTECode = "s5465BirthPlace",
                        //},
                        //Country = new CountryType // todo? mapping
                        //{
                        //    CountryISOAlpha3 = "79874BirthPlace",
                        //    CountryISONumber = "113131BirthPlace",
                        //    CountryName = "country name12BirthPlace",
                        //},
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
                        //new CountryType{
                        //    CountryISOAlpha3 = "co1 iso",
                        //    CountryISONumber = "co1 ison",
                        //     CountryName = "co1 name"
                        //},
                        // new CountryType{
                        //    CountryISOAlpha3 = "co2 iso",
                        //    CountryISONumber = "co2 ison",
                        //    CountryName = "co2 name"
                        //}
                    },
                    PreviousNames = new PrevNames[]
                    {
                        new PrevNames
                        {
                            // bulletin in db does not contains this properties: todo?
                            //BirthDate =  new DateType
                            //{
                            //    Date = DateTime.Now.AddYears(10),
                            //    DatePrecision = DatePrecisionEnum.Y,
                            //    DatePrecisionSpecified = true
                            //},
                            //BirthPlace =new PlaceType
                            //{
                            //     City = new CityType
                            //     {
                            //         CityName = "sofia PrevNames",
                            //         EKATTECode = "s5465PrevNames",
                            //     },
                            //     Country = new CountryType
                            //     {
                            //         CountryISOAlpha3 = "79874BPrevNames",
                            //         CountryISONumber = "113131PrevNames",
                            //         CountryName = "country name12PrevNames",
                            //     },
                            //     Descr = "infoPrevNames",
                            //},
                            //IdentityNumber =  new PersonIdentityNumberType
                            //{
                            //    EGN = "1014141415prev",
                            //    LN = "5641354prev",
                            //    LNCh = "98646164prev",
                            //    SUID = "54894dfg89dfgd1prev",
                            //},
                            Names= new PersonNameType
                            {
                                 FirstName = "prevname FirstName",
                                 FamilyName = "prevname FamilyName",
                                 FullName = "prevname FullName",
                                 SurName = "prevname SurName",
                            },
                            NameType = NameTypesType.maiden,
                            NameTypeSpecified = true,
                            //Sex = 1, 
                            //SexSpecified = true,
                        },
                    },
                    Sex = 1,
                },
            };

            return result;
        }
    }
}
