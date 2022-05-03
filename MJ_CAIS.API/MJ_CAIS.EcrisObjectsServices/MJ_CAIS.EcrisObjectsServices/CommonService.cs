using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.EcrisObjectsServices
{
    public class CommonService
    {
        public static async Task<string> GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType rEQ, CaisDbContext dbContext)
        {
            var code = await dbContext.DDocTypes.FirstOrDefaultAsync(d => d.Code == rEQ.ToString());
            if (code == null)
            {
                throw new Exception($"D_DOC_TYPES does not contain record with code {rEQ.ToString()}");
            }
            return code.Id;
        }

        public static DDocument GetDDocument(MJ_CAIS.DTO.EcrisService.EcrisMessageType t, string? name, string? firstName, string? surName, string? familyName, CaisDbContext dbContext)
        {
            DDocument d = new DDocument();
            d.Id = BaseEntity.GenerateNewId();
            var docType = dbContext.DDocTypes.FirstOrDefault(dt => dt.Code != null && dt.Code.ToLower() == t.ToString().ToLower());
            d.DocTypeId = docType?.Id;
            d.Name = name;

            //todo: add resources
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.REQ)
            {
                d.Descr = "Запитване за " + firstName + " " + surName + " " + familyName;
            }
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.NOT)
            {
                d.Descr = "Нотификация за " + firstName + " " + surName + " " + familyName;
            }
            if (t == MJ_CAIS.DTO.EcrisService.EcrisMessageType.RRS)
            {
                d.Descr = "Отговор на запитване  за " + firstName + " " + surName + " " + familyName;
            }
            return d;
        }
        public static DDocContent GetDDocContent(string contentXML)
        {
            DDocContent content = new DDocContent();
            content.Id = BaseEntity.GenerateNewId();
            content.MimeType = "application/xml";
            content.Content = Encoding.UTF8.GetBytes(contentXML);
            content.Bytes = content.Content.Length;
            //byte[] hashBytes = (new MD5CryptoServiceProvider()).ComputeHash(content.Content);
            // content.Md5Hash = Convert.ToHexString(hashBytes);
            // content.Sha1Hash = Convert.ToHexString(new SHA1CryptoServiceProvider().ComputeHash(content.Content));
            return content;
        }

        public static async Task<string?> GetPersonIDForEcrisMessages(string ecrisMsgID, CaisDbContext dbContext)
        {
            return (await dbContext.EEcrisIdentifications.FirstOrDefaultAsync(p => p.EcrisMsgId == ecrisMsgID && p.Approved == 1))?.PersonId;

        }
        public static YesNoUnknownStringEnumerationType? GetYesNoType(bool? value)
        {
            if (value == true)
            {
                YesNoUnknownStringEnumerationType res = new YesNoUnknownStringEnumerationType();
                res.Value = "yes";
                return res;
            }
            if (value == false)
            {
                YesNoUnknownStringEnumerationType res = new YesNoUnknownStringEnumerationType();
                res.Value = "no";
                return res;
            }
            return null;
        }

        public static string GetPeriodFromNumbers(int? years, int? months, int? days, int? hours)
        {
            return "P" +
                  ((years == null || years == 0) ? "" : (years.ToString() + "Y"))
                  + ((months == null || months == 0) ? "" : (months.ToString() + "M"))
                    + ((days == null || days == 0) ? "" : (days.ToString() + "D"))
                  + ((hours == null || hours == 0) ? "" : ("T" + hours.ToString() + "H"));
        }

        public static DateType GetDateTypeFromDateAndPrecission(DateTime date, string? prec)
        {
            DateType res = new DateType();
            string prec1;
            if (prec == null) prec1 = "ymd";
            else prec1 = prec;
            if (prec1.Contains('y') || prec1.Contains('Y'))
            {
                res.DateYear = date.Year.ToString();

            }
            if (prec1.Contains('m') || prec1.Contains('M'))
            {
                res.DateMonthDay = new MonthDayType();
                res.DateMonthDay.DateMonth = date.Month.ToString();
                if (prec1.Contains('d') || prec1.Contains('D'))
                {
                    res.DateMonthDay.DateDay = date.Day.ToString();
                }

            }

            return res;
        }

        public static List<NameTextType>? GetNameTextType(List<string?> names, List<string> langCodes)
        {
            if (names?.Where(x => x != null).Count() == 0)
            {
                return null;
            }
            List<NameTextType> res = new List<NameTextType>();
            for (int i = 0; i < names?.Count(); i++)
            {
                if (names[i] != null)
                {
                    NameTextType name = new NameTextType();
                    name.Value = names[i];
                    if (i < langCodes.Count)
                    {
                        name.languageCode = langCodes[i];
                    }
                    res.Add(name);
                }
            }

            return res;
        }
        public static List<FullNameTextType>? GetFullNameTextType(List<string?> names, List<string> langCodes)
        {
            if (names?.Where(x => x != null).Count() == 0)
            {
                return null;
            }
            List<FullNameTextType> res = new List<FullNameTextType>();
            for (int i = 0; i < names?.Count(); i++)
            {
                if (names[i] != null)
                {
                    FullNameTextType name = new FullNameTextType();
                    name.Value = names[i];
                    if (i < langCodes.Count)
                    {
                        name.languageCode = langCodes[i];
                    }
                    res.Add(name);
                }
            }

            return res;
        }

        public static DateTime? GetDateTime(StrictDateType date)
        {
            if (date == null)
            {
                return null;
            }
            else
            {
                return date.Value;
            }
        }



        public static ConvictionType GetConvictionFromBuletin(BBulletin buletin, string? bgCode)
        {
            ConvictionType conv = new ConvictionType();
            conv.ConvictionSanction = GetSanctions(buletin);
            conv.ConvictionDecision = GetDecisions(buletin); ;
            conv.ConvictionOffence = GetOffences(buletin); ;
            if (buletin.DecisionDate.HasValue)
            {
                conv.ConvictionDecisionDate = new StrictDateType();
                conv.ConvictionDecisionDate.Value = buletin.DecisionDate.Value;
            }
            if (buletin.DecisionFinalDate.HasValue)
            {
                conv.ConvictionDecisionFinalDate = new StrictDateType();
                conv.ConvictionDecisionFinalDate.Value = buletin.DecisionFinalDate.Value;
            }
            if (CommonService.GetYesNoType(buletin.ConvIsTransmittable == 1) != null)
            {
                conv.ConvictionIsTransmittable = CommonService.GetYesNoType(buletin.ConvIsTransmittable == 1);
            }

            if (buletin.ConvRetPeriodEndDate.HasValue)
            {
                conv.ConvictionRetentionPeriodEndDate = new StrictDateType();
                conv.ConvictionRetentionPeriodEndDate.Value = buletin.ConvRetPeriodEndDate.Value;
            }
            if (!string.IsNullOrEmpty(buletin.ConvRemarks))
            {
                conv.ConvictionRemarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                conv.ConvictionRemarks[0].Value = buletin.ConvRemarks;

            }
            if (!string.IsNullOrEmpty(buletin.DecidingAuthId))
            {

                conv.ConvictionDecidingAuthority = new DecidingAuthorityType();

                conv.ConvictionDecidingAuthority.DecidingAuthorityCode = new RestrictedStringType50Chars();
                //todo: кой идентификатор да се пише тук
                conv.ConvictionDecidingAuthority.DecidingAuthorityCode.Value = buletin.DecidingAuth.Id;

                conv.ConvictionDecidingAuthority.DecidingAuthorityName = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[3];
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[0].Value = buletin.DecidingAuth?.Name;
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[1].Value = buletin.DecidingAuth?.NameEn;
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[2].Value = buletin.DecidingAuth?.DisplayName;
            }
            conv.ConvictionID = buletin.EcrisConvictionId;
            //todo: винаги ли е бг?
            conv.ConvictionConvictingCountryReference = new CountryExternalReferenceType();
            conv.ConvictionConvictingCountryReference.Value = bgCode;
            //todo: какво да пиша в тези полета?
            //conv.ConvictionRelationship;                
            //conv.ConvictionFileNumber;
            //conv.ConvictionNonCriminalRuling;
            return conv;

        }



        private static  OffenceType[] GetOffences(BBulletin personBuletin)
        {

            List<OffenceType> result = new List<OffenceType>();
            foreach (var offence in personBuletin.BOffences)
            {
                OffenceType o = new OffenceType();
                o.NationalCategoryCode = offence.OffenceCat.Code;
                o.NationalCategoryTitle = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                o.NationalCategoryTitle[0].Value = offence.OffenceCat.Name;
                //todo: how to set these values
                //o.OffenceCommonCategoryReference;
                //o.OffenceID;

                o.OffencePlace = new PlaceType();
                o.OffencePlace.PlaceCountryReference = new CountryExternalReferenceType();
                o.OffencePlace.PlaceCountryReference.Value = offence.OffPlaceCountry.EcrisTechnId;
                o.OffencePlace.PlaceCountrySubdivisionReference = new CountrySubdivisionExternalReferenceType();
                //  o.OffencePlace.PlaceCountrySubdivisionReference.Value = offence.OffPlaceSubdiv.EcrisTechnId;
                o.OffencePlace.PlaceTownReference = new CityExternalReferenceType();
                o.OffencePlace.PlaceTownReference.Value = offence.OffPlaceCity.EcrisTechnId;
                o.OffencePlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[2];
                o.OffencePlace.PlaceTownName[0].Value = offence.OffPlaceCity.Name;
                o.OffencePlace.PlaceTownName[1].Value = offence.OffPlaceCity.NameEn;

                if (offence.OffEndDate.HasValue)
                {
                    o.OffenceEndDate = new DateType();
                    o.OffenceEndDate.DateYear = offence.OffEndDate.Value.Year.ToString();
                    o.OffenceEndDate.DateMonthDay = new MonthDayType();
                    o.OffenceEndDate.DateMonthDay.DateDay = offence.OffEndDate.Value.Day.ToString();
                    o.OffenceEndDate.DateMonthDay.DateMonth = offence.OffEndDate.Value.Month.ToString();

                }
                o.OffenceLevelOfCompletionReference = new OffenceLevelOfCompletionExternalReferenceType();
                // o.OffenceLevelOfCompletionReference.Value = offence.OffLvlCompl.EcrisTechnId;
                o.OffenceLevelOfParticipationReference = new OffenceLevelOfParticipationExternalReferenceType();
                // o.OffenceLevelOfParticipationReference.Value = offence.OffLvlPart.EcrisTechnId;
                //o.OffenceResponsibilityExemption = CommonService.GetYesNoType(offence.RespExemption);
                if (offence.OffStartDate.HasValue)
                {
                    o.OffenceStartDate = new DateType();
                    o.OffenceStartDate.DateYear = offence.OffStartDate.Value.Year.ToString();
                    o.OffenceStartDate.DateMonthDay = new MonthDayType();
                    o.OffenceStartDate.DateMonthDay.DateDay = offence.OffStartDate.Value.Day.ToString();
                    o.OffenceStartDate.DateMonthDay.DateMonth = offence.OffStartDate.Value.Month.ToString();
                }
                //o.OffenceNumberOfOccurrences = offence.Occurrences?.ToString();
                o.OffenceApplicableLegalProvisions = offence.LegalProvisions;
                //  o.OffenceIsContinuous = CommonService.GetYesNoType(offence.IsContiniuous);
                //  o.OffenceRecidivism = CommonService.GetYesNoType(offence.Recidivism);
                o.Remarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                o.Remarks[0].Value = offence.Remarks;

                result.Add(o);
            }

            return result.ToArray();
        }



        private static DecisionType[] GetDecisions(BBulletin personBuletin)
        {

            List<DecisionType> result = new List<DecisionType>();
            foreach (var decision in personBuletin.BDecisions)
            {
                DecisionType d = new DecisionType();
                d.DecisionID = decision.DecisionNumber;

                if (decision.DecisionFinalDate.HasValue)
                {
                    d.DecisionFinalDate = new StrictDateType();
                    d.DecisionFinalDate.Value = decision.DecisionFinalDate.Value;
                }

                d.DecisionRemarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[0];
                d.DecisionRemarks[0].Value = decision.Descr;
                //todo: каква е стойността на това поле
                d.DecisionDeleteConvictionFromRegister = CommonService.GetYesNoType(false);

                d.DecisionChangeTypeReference = new DecisionChangeTypeExternalReferenceType[1];
                d.DecisionChangeTypeReference[0].Value = decision.DecisionChType.EcrisTechnId;

                d.DecisionDecidingAuthority = new DecidingAuthorityType();
                d.DecisionDecidingAuthority.DecidingAuthorityCode = new RestrictedStringType50Chars();
                d.DecisionDecidingAuthority.DecidingAuthorityCode.Value = decision.DecisionAuth?.Code;
                d.DecisionDecidingAuthority.DecidingAuthorityName = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[3];
                d.DecisionDecidingAuthority.DecidingAuthorityName[0].Value = decision.DecisionAuth?.Name;
                d.DecisionDecidingAuthority.DecidingAuthorityName[1].Value = decision.DecisionAuth?.NameEn;
                d.DecisionDecidingAuthority.DecidingAuthorityName[2].Value = decision.DecisionAuth?.DisplayName;

                if (decision.DecisionDate.HasValue)
                {
                    d.DecisionDate = new StrictDateType();
                    d.DecisionDate.Value = decision.DecisionDate.Value;
                }

                result.Add(d);
            }




            return result.ToArray();
        }

        private static SanctionType[] GetSanctions(BBulletin personBuletin)
        {
            List<SanctionType> result = new List<SanctionType>();
            foreach (var sanction in personBuletin.BSanctions)
            {
                SanctionType s = new SanctionType();
                //в този обект има и данни за пробация?!
                s.SanctionSuspension = new SanctionSuspensionType();
                s.SanctionSuspension.PeriodDuration = CommonService.GetPeriodFromNumbers(sanction.SuspentionDurationYears, sanction.SuspentionDurationMonths,
                                                                                              sanction.SuspentionDurationDays, sanction.SuspentionDurationHours);
                s.SanctionSuspension.SanctionSuspensionRemarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                //s.SanctionSuspension.SanctionSuspensionRemarks[0].Value = sanction.ProbationDescr;
                s.SanctionAmountOfIndividualFine = new PositiveDecimalType();
                s.SanctionAmountOfIndividualFine.PositiveDecimalUnit = sanction.FineAmount?.ToString();

                //s.SanctionID;
                //s.SanctionMultiplier;            
                //s.SanctionCurrencyOfFineReference;
                //s.SanctionNumberOfFines;
                //s.SanctionAlternativeTypeReference;
                //s.SanctionCommonCategoryReference;
                //s.SanctionInterruption = new SanctionInterruptionType();

                s.SanctionSentencedPeriod = new SanctionSentencedPeriodType();
                //PnYnMnDTnHnMnS
                s.SanctionSentencedPeriod.PeriodDuration = CommonService.GetPeriodFromNumbers(sanction.DecisionDurationYears, sanction.DecisionDurationMonths,
                    sanction.DecisionDurationDays, sanction.DecisionDurationHours);


                s.NationalCategoryCode = sanction.SanctCategory.Code;
                s.NationalCategoryTitle = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                s.NationalCategoryTitle[0].Value = sanction.SanctCategory.Name;
                s.Remarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                s.Remarks[0].Value = sanction.Descr;


                s.SanctionTypeReference = new SanctionNatureExternalReferenceType();
                s.SanctionTypeReference.Value = sanction.EcrisSanctCateg.EcrisTechnId;


                result.Add(s);
            }




            return result.ToArray();
        }

        public static async Task AddMessageToDBContextAsync(AbstractMessageType msg, string? convictionID, string joinSeparator, CaisDbContext dbContext)
        {

            EEcrisMessage m = await CreateEcrisMessageAsync(msg, convictionID, joinSeparator, dbContext);


            DDocument d = CommonService.GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, m.Firstname, m.Surname, m.Familyname, dbContext);

            d.EcrisMsg = m;
            m.DDocuments.Add(d);
            DDocContent content = CommonService.GetDDocContent(XmlUtils.SerializeToXml(msg));

            d.DocContent = content;
            content.DDocuments.Add(d);

            dbContext.EEcrisMessages.Add(m);
            dbContext.DDocuments.Add(d);
            dbContext.DDocContents.Add(content);
        }


        private static async Task<EEcrisMessage> CreateEcrisMessageAsync(MJ_CAIS.DTO.EcrisService.AbstractMessageType msg, string? convictionID, string joinSeparator, CaisDbContext dbContext, string requestMsgId = "")
        {
            EEcrisMessage m = new EEcrisMessage();

            m.Id = BaseEntity.GenerateNewId();
            m.EcrisIdentifier = msg.MessageEcrisIdentifier;
            m.Identifier = msg.MessageIdentifier;//или да се пази в m.RequestMsgId???
            m.EcrisMsgConvictionId = convictionID;
            PersonType? person = null;
            if (msg.MessageType == EcrisMessageType.RRS)
            {
                person = ((RequestResponseMessageType)msg).MessagePerson;
                m.MsgTypeId = await CommonService.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.RRS, dbContext);
                if (((RequestResponseMessageType)msg).RequestMessageUrgency?.Value.ToLower() == "yes")
                {
                    m.Urgent = true;
                };
                if (((RequestResponseMessageType)msg).RequestMessageUrgency?.Value.ToLower() == "no")
                {
                    m.Urgent = false;
                };
                if (((RequestResponseMessageType)msg).RequestResponseMessageConviction.Length > 0)
                {
                    m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ResponceCreated;
                }
                else
                {
                    m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForSending;
                }
                m.RequestMsgId = requestMsgId;
            }
            if (msg.MessageType == EcrisMessageType.NOT)
            {
                person = ((NotificationMessageType)msg).MessagePerson;
                m.MsgTypeId = await CommonService.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.NOT, dbContext);
                if (((NotificationMessageType)msg).RequestMessageUrgency?.Value.ToLower() == "yes")
                {
                    m.Urgent = true;
                };
                if (((NotificationMessageType)msg).RequestMessageUrgency?.Value.ToLower() == "no")
                {
                    m.Urgent = false;
                };
                m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.NotificationCreated;
            }
            //има данни в msg.MessageShortViewPerson.PersonAlias - те интересуват ли ни?!
            ////има данни и за майка и баща - биха били полезни за идентификацията
            var city = person?.PersonBirthPlace?.PlaceTownName?.Select(p => p.Value);
            if (city != null)
            {
                m.BirthCity = string.Join(joinSeparator, city);
            }//msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownReference???
            m.BirthDate = new DateTime(Int32.Parse(XmlUtils.GetNumbersFromString(person?.PersonBirthDate?.DateYear)),
                                       Int32.Parse(XmlUtils.GetNumbersFromString(person.PersonBirthDate.DateMonthDay.DateMonth)),
                                       Int32.Parse(XmlUtils.GetNumbersFromString(person.PersonBirthDate.DateMonthDay.DateDay)));
            m.BirthCountry = person.PersonBirthPlace.PlaceCountryReference.Value;
            m.Sex = person.PersonSex;
            var familyName = person.PersonName?.SecondSurname?.Select(p => p.Value);
            if (familyName != null)
            {
                m.Familyname = string.Join(joinSeparator, familyName);
            }
            var firstName = person.PersonName?.Forename?.Select(p => p.Value);
            if (firstName != null)
            {
                m.Firstname = string.Join(joinSeparator, firstName);
            }
            var surname = person.PersonName?.Surname?.Select(p => p.Value);
            if (surname != null)
            {
                m.Surname = string.Join(joinSeparator, surname);
            }

            var countriesAuthorities = dbContext.EEcrisAuthorities.Where(ea => ea.ValidFrom <= DateTime.UtcNow && ea.ValidTo >= DateTime.UtcNow
            && ea.MemberStateCode != null && (ea.MemberStateCode.ToLower() == msg.MessageSendingMemberState.ToString().ToLower()
            || msg.MessageReceivingMemberState.Select(p => p.ToString().ToLower()).Contains(ea.MemberStateCode.ToLower()))).ToList();
            if (msg.MessageSendingMemberStateSpecified)
            {
                m.FromAuthId = countriesAuthorities.FirstOrDefault(c => c.MemberStateCode?.ToLower() == "bg")?.Id;
            }


            m.ToAuthId = countriesAuthorities.Where(c => msg.MessageReceivingMemberState
            .Select(p => p.ToString().ToLower()).Contains(c.MemberStateCode?.ToLower()))
                    .FirstOrDefault()?.Id;


            var nationalities = person?.PersonNationalityReference?.Select(p => p.Value)?.ToList();
            if (nationalities != null)
            {
                var countries = dbContext.GCountries.Where(c => c.EcrisTechnId != null && nationalities.Contains(c.EcrisTechnId)
                                                                && c.ValidFrom <= DateTime.Now && c.ValidTo >= DateTime.Now).ToList();
                if (nationalities.Count > 0)
                {
                    m.Nationality1Code = countries.FirstOrDefault(c => c.EcrisTechnId == person?.PersonNationalityReference[0]?.Value)?.Id;
                }
                if (nationalities.Count > 1)
                {
                    m.Nationality2Code = countries.FirstOrDefault(c => c.EcrisTechnId == person?.PersonNationalityReference[1]?.Value)?.Id;
                }
            }
            if (msg.MessageVersionTimestampSpecified)
            {
                m.MsgTimestamp = msg.MessageVersionTimestamp;
            }


            //m.Deadline = msg.MessageDeadline?.Value;
            m.Pin = person?.PersonIdentityNumber?.Value;



            return m;
        }




    }
}
