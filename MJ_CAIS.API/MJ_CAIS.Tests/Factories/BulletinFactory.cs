using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.ExternalServicesHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Tests.Factories
{
    internal class BulletinFactory
    {
        public static BulletinType GetFilledInBulletinXSD()
        {
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
                        Code = "660",
                        Name = "Министерство на Правосъдието",
                    },
                    RegistrationNumber = "220605660301000000164",
                },
                Conviction = new ConvictionType
                {
                    ConvictionDecisions = new DecisionChangeType[]
                    {
                        new DecisionChangeType
                        {
                            Decision =new DecisionActType
                            {
                                DecidingAuthority = new DecidingAuthorityType
                                {
                                     DecidingAuthorityCodeEIK = "eik123",
                                     DecidingAuthorityCodeEISPP = "eisp11",
                                     DecidingAuthorityName = "deciding authority test name",
                                },
                                DecisionDate = DateTime.Now.AddDays(1),
                                DecisionFinalDate = DateTime.Now.AddYears(1),
                                DecisionType = DecisionTypeCategories.dkp_reshenie,
                                ECLI = "454497987",
                                FileNumber = "23212454"
                            },
                            DecisionChangeTypeReference = DecisionChangeTypeType.DCH00N,
                            DecisionChangeTypeReferenceSpecified = true,
                            DecisionRemarks = "remarkss..",
                            ReceiveDate = DateTime.Now.AddMonths(1),
                            ReceiveDateSpecified = true,
                            ValidFrom = DateTime.Now.AddDays(4),
                            ValidFromSpecified = true,
                        },
                       new DecisionChangeType
                        {
                            Decision =new DecisionActType
                            {
                                DecidingAuthority = new DecidingAuthorityType
                                {
                                     DecidingAuthorityCodeEIK = "eik8978",
                                     DecidingAuthorityCodeEISPP = "eisp89798",
                                     DecidingAuthorityName = "deciding authority test name 222",
                                },
                                DecisionDate = DateTime.Now.AddDays(6),
                                DecisionFinalDate = DateTime.Now.AddYears(9),
                                DecisionType = DecisionTypeCategories.dkp_prisada,

                            },
                            DecisionChangeTypeReferenceSpecified = false,
                            DecisionRemarks = "remarkss..",
                            ReceiveDateSpecified = false,
                            ValidFromSpecified = false,
                        }
                    },
                    ConvictionOffence = new OffenceType[]
                    {
                        new OffenceType
                        {
                            FormOfGuilt = FormOfGuiltType.intentionally,
                            FormOfGuiltSpecified = true,
                            NationalCategoryCode ="bg1",
                            NationalCategoryTitle = "bulgaria",
                            OffenceApplicableLegalProvisions = "off app",
                            OffenceCommonCategoryReference = new OffenceCommonCategoryType
                            {
                                OffenceCode = "offCode",
                                OffenceName ="off name"
                            },
                            OffenceEndDate = new DateType
                            {
                                Date =  DateTime.Now.AddYears(1),
                                DatePrecision = DatePrecisionEnum.YMD,
                                DatePrecisionSpecified = true,
                            },
                            OffenceId = "13148545",
                            OffencePlace = new PlaceType
                            {
                                City = new CityType
                                {
                                    CityName = "sofia",
                                    EKATTECode = "s5465",
                                },
                                Country = new CountryType
                                {
                                    CountryISOAlpha3 = "79874",
                                    CountryISONumber="113131",
                                    CountryName = "country name12",
                                },
                                Descr = "info",
                            },
                            OffenceStartDate = new DateType
                            {
                                Date =  DateTime.Now.AddYears(-1),
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
                                SanctionAmountOfIndividualFine =20,
                                SanctionAmountOfIndividualFineSpecified= true
                            },
                            NationalCategoryCode = "bg888",
                            NationalCategoryTitle = "nat type",
                            Other = new SanctionTypeOther
                            {
                                SanctionSentencedPeriodLength = "26323"
                            },
                            Prison = new SanctionTypePrison
                            {
                                DetentionDescription= "date12",
                                SanctionSentencedPeriod = "spec4",
                                SanctionSuspension= "susp78", 
                            },
                            Probation = new SanctionTypeProbation[]
                            {
                                new SanctionTypeProbation
                                {
                                    SanctionSentencedPeriod = "12",
                                    ProbationCategoryCode = "procCOde",
                                    ProbationCategoryTitle="prob title",
                                    ProbationMeasureCode="prob mes code",
                                    ProbationMeasureTitle  = "prob mes titile",
                                    ProbationValue = 159,
                                    ProbationValueSpecified = true,
                                },
                            },
                            SanctionCommonCategoryReference = new SanctionCommonCategoryType
                            {
                                SanctionCode = "sanc code1",
                                SanctionText = "sanction code"
                            },
                            SanctionId = "sanc type id",                          
                        },                        
                    },
                    CriminalCase = new CriminalCaseType
                    {
                        CaseAuthority = new DecidingAuthorityType
                        {
                            DecidingAuthorityCodeEIK ="dec eik1",
                            DecidingAuthorityCodeEISPP ="dec eispp1",
                            DecidingAuthorityName = "dec name",
                        },
                        CaseNumber = "12378",
                        CaseType = CaseType.sign_ncd,
                        CaseYear = "2022",
                    },
                    Decision = new DecisionActType
                    {
                        DecidingAuthority = new DecidingAuthorityType
                        {
                            DecidingAuthorityCodeEIK = "dec eik2",
                            DecidingAuthorityCodeEISPP = "dec eispp2",
                            DecidingAuthorityName = "dec name22",
                        },
                        DecisionDate  = DateTime.Now.AddDays(1).AddMonths(1),
                        DecisionFinalDate = DateTime.Now.AddDays(2).AddMonths(2),
                        DecisionType = DecisionTypeCategories.dkp_sporazumenie,
                        ECLI = "ecli1456",
                        FileNumber = "2648",
                    },
                    EcrisConvictionId = ""
                }
            };

            return result;
        }
    }
}
