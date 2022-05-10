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

        public static async Task<GraoPerson?> GetPersonIDForEcrisMessages(string ecrisMsgID, CaisDbContext dbContext)
        {
            return (await dbContext.EEcrisIdentifications.FirstOrDefaultAsync(p => p.EcrisMsgId == ecrisMsgID && p.Approved == 1))?.GraoPerson;

        }
        public static YesNoUnknownStringEnumerationType? GetYesNoType(bool? value)
        {
            //Yes, No, UNKNOWN
            YesNoUnknownStringEnumerationType res = new YesNoUnknownStringEnumerationType();
            if (value == true)
            {

                res.Value = "Yes";
                return res;
            }
            if (value == false)
            {

                res.Value = "No";
                return res;
            }

            res.Value = "UNKNOWN";
            return res;

        }

        public static string GetPeriodFromNumbers(int? years, int? months, int? days, int? hours)
        {
            return "P" +
                  ((years == null || years == 0) ? "0Y" : (years.ToString() + "Y"))
                  + ((months == null || months == 0) ? "0M" : (months.ToString() + "M"))
                    + ((days == null || days == 0) ? "0D" : (days.ToString() + "D"))
                  + ((hours == null || hours == 0) ? "T0H" : ("T" + hours.ToString() + "H"));
        }

        public static DateType GetDateTypeFromDateAndPrecission(DateTime date, string? prec)
        {
            DateType res = new DateType();
            string prec1;
            if (prec == null) prec1 = "ymd";
            else prec1 = prec;
            if (prec1.Contains('y') || prec1.Contains('Y'))
            {
                res.DateYear = date.ToString("yyyy");

            }
            if (prec1.Contains('m') || prec1.Contains('M'))
            {
                res.DateMonthDay = new MonthDayType();
                res.DateMonthDay.DateMonth = date.ToString("--MM");
                if (prec1.Contains('d') || prec1.Contains('D'))
                {
                    res.DateMonthDay.DateDay = date.ToString("---dd");
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



        public static async Task<ConvictionType> GetConvictionFromBuletin(BBulletin buletin, string? bgCode, CaisDbContext dbContext)
        {

            ConvictionType conv = new ConvictionType();
            //load nomenclatures:
            var sancIDs = buletin.BSanctions.Select(s => s.SanctCategoryId).ToList();
            var sanctCategories = await dbContext.BSanctionCategories.Where(cat => sancIDs.Contains(cat.Id)).ToListAsync();
            var ecrisSancIDs = buletin.BSanctions.Select(s => s.EcrisSanctCategId).ToList();
            var ecrisSanctCategories = await dbContext.BEcrisStanctCategs.Where(cat => ecrisSancIDs.Contains(cat.Id)).ToListAsync();
            var offenceCatIDs = buletin.BOffences.Select(s => s.OffenceCatId).ToList();
            var offenceCategories = await dbContext.BOffenceCategories.Where(cat => offenceCatIDs.Contains(cat.Id)).ToListAsync();
            var offenceCountriesIDs = buletin.BOffences.Select(s => s.OffPlaceCountryId).ToList();
            var offenceCountries = await dbContext.GCountries.Where(cat => offenceCountriesIDs.Contains(cat.Id)).ToListAsync();
            var offenceCitiesIDs = buletin.BOffences.Select(s => s.OffPlaceCityId).ToList();
            var offenceCities = await dbContext.GCities.Where(cat => offenceCitiesIDs.Contains(cat.Id)).ToListAsync();
            var decisionChType = buletin.BDecisions.Select(s => s.DecisionChTypeId).ToList();
            var decisionChTypes = await dbContext.BDecisionChTypes.Where(cat => decisionChType.Contains(cat.Id)).ToListAsync();
            var desidingAuthoritiesIDs = buletin.BDecisions.Select(s => s.DecisionAuthId).ToList();
            desidingAuthoritiesIDs.Add(buletin.DecidingAuthId);
            var desidingAuthorities = await dbContext.GDecidingAuthorities.Where(cat => desidingAuthoritiesIDs.Contains(cat.Id)).ToListAsync();
            var offenceCategoriesIDs = buletin.BOffences.Select(s => s.EcrisOffCatId).ToList();
            var ecrisOffenceCategories = await dbContext.BEcrisOffCategories.Where(cat => offenceCategoriesIDs.Contains(cat.Id)).ToListAsync();

            conv.ConvictionSanction = GetSanctions(buletin, sanctCategories, ecrisSanctCategories);
            conv.ConvictionDecision = GetDecisions(buletin, decisionChTypes, desidingAuthorities);
            conv.ConvictionOffence = GetOffences(buletin, offenceCategories, offenceCountries, offenceCities, ecrisOffenceCategories);
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
                conv.ConvictionRemarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[2];
                conv.ConvictionRemarks[0] = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation();
                conv.ConvictionRemarks[0].Value = buletin.ConvRemarks;


            }
            if (!string.IsNullOrEmpty(buletin.DecidingAuthId))
            {
                var dAuth = desidingAuthorities.FirstOrDefault(d => d.Id == buletin.DecidingAuthId);
                if (dAuth != null)
                {
                    conv.ConvictionDecidingAuthority = new DecidingAuthorityType();

                    conv.ConvictionDecidingAuthority.DecidingAuthorityCode = new RestrictedStringType50Chars();
                    //todo: кой идентификатор да се пише тук
                    conv.ConvictionDecidingAuthority.DecidingAuthorityCode.Value = dAuth.Code;

                    conv.ConvictionDecidingAuthority.DecidingAuthorityName = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                    conv.ConvictionDecidingAuthority.DecidingAuthorityName[0] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                
                    conv.ConvictionDecidingAuthority.DecidingAuthorityName[0].Value = dAuth.Name;
      
                }
            }

            //todo: винаги ли е бг?
            conv.ConvictionConvictingCountryReference = new CountryExternalReferenceType();
            conv.ConvictionConvictingCountryReference.Value = bgCode;
            //todo: какво да пиша в тези полета?
            //conv.ConvictionRelationship;
            conv.ConvictionID = string.IsNullOrEmpty(buletin.EcrisConvictionId) ? "BG-C-000000000000000" : buletin.EcrisConvictionId;
            //"The reference number of the decision in the national judicial system."
            conv.ConvictionFileNumber = new RestrictedStringType50Chars();
            conv.ConvictionFileNumber.Value = buletin.CaseYear + "-" + buletin.CaseNumber;

            conv.ConvictionNonCriminalRuling = CommonService.GetYesNoType(buletin.NoSanction);

            return conv;

        }



        private static OffenceType[] GetOffences(BBulletin personBuletin, List<BOffenceCategory> categories, List<GCountry> countries, List<GCity> cities, List<BEcrisOffCategory> ecrisCategoies)
        {

            List<OffenceType> result = new List<OffenceType>();
            int i = 0;
            foreach (var offence in personBuletin.BOffences)
            {

                OffenceType o = new OffenceType();
                i++;
                if (offence.OffenceCatId != null)
                {
                    var category = categories.FirstOrDefault(cat => cat.Id == offence.OffenceCatId);
                    if (category != null)
                    {
                        o.NationalCategoryCode = category.Code;
                        o.NationalCategoryTitle = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                        o.NationalCategoryTitle[0] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                        o.NationalCategoryTitle[0].Value = category.Name;
                    }
                }

                //todo: how to set these values
                //o.OffenceCommonCategoryReference;
                if (offence.EcrisOffCatId != null) {
                    var category = ecrisCategoies.FirstOrDefault(cat => cat.Id == offence.EcrisOffCatId);
                    if (category != null && category.EcrisTechnId!= null) {
                        o.OffenceCommonCategoryReference = new OffenceExternalReferenceType();
                        o.OffenceCommonCategoryReference.Value = category.EcrisTechnId;
                    }
                }
                //todo: 'O-[0-9]{5}'
                o.OffenceID = "O-" + i.ToString("00000");
                o.OffenceIsContinuous = CommonService.GetYesNoType(null);
                o.OffenceNumberOfOccurrences = "1";
                o.OffenceLevelOfCompletionReference = new OffenceLevelOfCompletionExternalReferenceType();
                //'LC-[0-9]{2}-[0A-Z]' 
                o.OffenceLevelOfCompletionReference.Value = "LC-00-0";
                o.OffenceLevelOfParticipationReference = new OffenceLevelOfParticipationExternalReferenceType();
                //'LP-[0-9]{2}-[0A-Z]' 
                o.OffenceLevelOfParticipationReference.Value = "LP-00-0";
                o.OffenceResponsibilityExemption = CommonService.GetYesNoType(null);
                o.OffenceRecidivism = CommonService.GetYesNoType(null);
                ////////

                o.OffencePlace = new PlaceType();
                if (offence.OffPlaceCountryId != null)
                {
                    var country = countries.FirstOrDefault(cat => cat.Id == offence.OffPlaceCountryId);
                    if (country != null)
                    {
                        o.OffencePlace.PlaceCountryReference = new CountryExternalReferenceType();
                        o.OffencePlace.PlaceCountryReference.Value = offence.OffPlaceCountry.EcrisTechnId;
                    }
                }

                if (offence.OffPlaceCityId != null)
                {
                    var city = cities.FirstOrDefault(cat => cat.Id == offence.OffPlaceCityId);
                    if (city != null)
                    {
                        if (!string.IsNullOrEmpty(city.EcrisTechnId))
                        {
                            o.OffencePlace.PlaceTownReference = new CityExternalReferenceType();
                            o.OffencePlace.PlaceTownReference.Value = city.EcrisTechnId;
                        }
                        if (!string.IsNullOrEmpty(city.NameEn))
                        {
                            o.OffencePlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[2];
                            o.OffencePlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                            o.OffencePlace.PlaceTownName[1] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                            o.OffencePlace.PlaceTownName[0].Value = city.Name;
                            o.OffencePlace.PlaceTownName[1].Value = city.NameEn;
                        }
                        else
                        {
                            o.OffencePlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                            o.OffencePlace.PlaceTownName[0] = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation();
                            o.OffencePlace.PlaceTownName[0].Value = city.Name;


                        }
                    }
                }


                if (offence.OffEndDate.HasValue)
                {
                    o.OffenceEndDate = CommonService.GetDateTypeFromDateAndPrecission(offence.OffEndDate.Value, offence.OffEndDatePrec);


                }
             
                if (offence.OffStartDate.HasValue)
                {
                    o.OffenceStartDate = CommonService.GetDateTypeFromDateAndPrecission(offence.OffStartDate.Value, offence.OffStartDatePrec);

                }
               
                o.OffenceApplicableLegalProvisions = offence.LegalProvisions;
               
        
                if (!string.IsNullOrEmpty(offence.Remarks))
                {
                    o.Remarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                    o.Remarks[0] = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation();
                    o.Remarks[0].Value = offence.Remarks;
                }

                result.Add(o);
            }

            return result.ToArray();
        }



        private static DecisionType[] GetDecisions(BBulletin personBuletin, List<BDecisionChType> decisionChTypes, List<GDecidingAuthority> authorities)
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
                d.DecisionRemarks[0] = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation();
                d.DecisionRemarks[0].Value = decision.Descr;
                //todo: каква е стойността на това поле
                d.DecisionDeleteConvictionFromRegister = CommonService.GetYesNoType(false);
                if (string.IsNullOrEmpty(decision.DecisionChTypeId))
                {
                    var changeType = decisionChTypes.FirstOrDefault(d => d.Id == decision.DecisionChTypeId);
                    if (changeType != null)
                    {
                        d.DecisionChangeTypeReference = new DecisionChangeTypeExternalReferenceType[1];
                        d.DecisionChangeTypeReference[0] = new DecisionChangeTypeExternalReferenceType();
                        d.DecisionChangeTypeReference[0].Value = changeType.EcrisTechnId;
                    }
                }

                if (decision.DecisionAuthId != null)
                {
                    var authority = authorities.FirstOrDefault(d => d.Id == decision.DecisionAuthId);
                    if (authority != null)
                    {
                        d.DecisionDecidingAuthority = new DecidingAuthorityType();
                        d.DecisionDecidingAuthority.DecidingAuthorityCode = new RestrictedStringType50Chars();
                        d.DecisionDecidingAuthority.DecidingAuthorityCode.Value = authority.Code;
                        d.DecisionDecidingAuthority.DecidingAuthorityName = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[3];
                        d.DecisionDecidingAuthority.DecidingAuthorityName[0] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                        d.DecisionDecidingAuthority.DecidingAuthorityName[1] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                        d.DecisionDecidingAuthority.DecidingAuthorityName[2] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();

                        d.DecisionDecidingAuthority.DecidingAuthorityName[0].Value = authority.Name;
                        d.DecisionDecidingAuthority.DecidingAuthorityName[1].Value = authority.NameEn;
                        d.DecisionDecidingAuthority.DecidingAuthorityName[2].Value = authority.DisplayName;
                    }
                }

                if (decision.DecisionDate.HasValue)
                {
                    d.DecisionDate = new StrictDateType();
                    d.DecisionDate.Value = decision.DecisionDate.Value;
                }

                result.Add(d);
            }




            return result.ToArray();
        }

        private static SanctionType[] GetSanctions(BBulletin personBuletin, List<BSanctionCategory> categories, List<BEcrisStanctCateg> ecrisStanctCategs)
        {
            List<SanctionType> result = new List<SanctionType>();
            int i = 0;
            foreach (var sanction in personBuletin.BSanctions)
            {
                SanctionType s = new SanctionType();
                i++;
               
                s.SanctionAmountOfIndividualFine = new PositiveDecimalType();
                s.SanctionAmountOfIndividualFine.PositiveDecimalUnit = sanction.FineAmount?.ToString();

                //todo: какви стойности да добавя тук?!
                s.SanctionID = "S-"+ i.ToString("00000");
                s.SanctionTypeReference = new SanctionNatureExternalReferenceType();
                //'N-[0-9]{2}-[0A-Z]{1}'
                s.SanctionTypeReference.Value = "N-00-0";
                s.SanctionMultiplier = "1";
                s.SanctionAlternativeTypeReference = new SanctionAlternativeTypeExternalReferenceType();
                //SAT-[0-9]{2}-[A-Z]{1}
                s.SanctionAlternativeTypeReference.Value = "SAT-00-Z";
                s.SanctionIsSpecificToMinor = CommonService.GetYesNoType(null);

              
              

                var category = categories.Where(cat => cat.Id == sanction.SanctCategoryId).FirstOrDefault();
                if (category != null)
                {
                    s.NationalCategoryCode = category.Code;
                    s.NationalCategoryTitle = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                    s.NationalCategoryTitle[0] = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation();
                    s.NationalCategoryTitle[0].Value = category.Name;
                }
                s.Remarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                s.Remarks[0] = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation();
                s.Remarks[0].Value = sanction.Descr;


                var ecrisCategory = ecrisStanctCategs.Where(cat => cat.Id == sanction.EcrisSanctCategId).FirstOrDefault();
                if (ecrisCategory != null&& ecrisCategory.EcrisTechnId!=null)
                {

                    s.SanctionCommonCategoryReference = new SanctionExternalReferenceType(); 
                    s.SanctionCommonCategoryReference.Value = ecrisCategory.EcrisTechnId;
                }

                      result.Add(s);
            }




            return result.ToArray();
        }

        public static async Task AddMessageToDBContextAsync(AbstractMessageType msg, string? convictionID, string buletinID, string joinSeparator, CaisDbContext dbContext)
        {

            EEcrisMessage m = await CreateEcrisMessageAsync(msg, convictionID, joinSeparator, dbContext);

            var names = m.EEcrisMsgNames.FirstOrDefault(n => n.LangCode == "bg");
            if (names == null)
            {
                names = m.EEcrisMsgNames.FirstOrDefault();
            }
            m.BulletinId = buletinID;
            DDocument d = CommonService.GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, names?.Firstname, names?.Surname, names?.Familyname, dbContext);
            if (!string.IsNullOrEmpty(buletinID))
            {
                d.BulletinId = buletinID;
            }
            d.EcrisMsg = m;
            m.DDocuments.Add(d);
            DDocContent content = CommonService.GetDDocContent(XmlUtils.SerializeToXml(msg));

            d.DocContent = content;
            content.DDocuments.Add(d);

            dbContext.EEcrisMessages.Add(m);
            dbContext.DDocuments.Add(d);
            dbContext.DDocContents.Add(content);
            if (m.EEcrisMsgNationalities?.Count > 0)
            {
                dbContext.EEcrisMsgNationalities.AddRange(m.EEcrisMsgNationalities);
            }
            if (m.EEcrisMsgNames.Count > 0)
            {
                dbContext.EEcrisMsgNames.AddRange(m.EEcrisMsgNames);
            }
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

            var forenames = person?.PersonName?.Forename?.ToList();
            if (forenames != null)
            {
                foreach (var forename in forenames)
                {
                    EEcrisMsgName name = new EEcrisMsgName();
                    name.Id = BaseEntity.GenerateNewId();
                    name.LangCode = forename.languageCode;
                    name.Firstname = forename.Value;
                    name.EEcrisMsgId = m.Id;
                    m.EEcrisMsgNames.Add(name);
                }
            }

            var familyNames = person?.PersonName?.SecondSurname?.ToList();
            if (familyNames != null)
            {
                foreach (var familyName in familyNames)
                {
                    var nameByLang = m.EEcrisMsgNames.FirstOrDefault(n => n.LangCode == familyName.languageCode);
                    if (nameByLang == null)
                    {
                        EEcrisMsgName name = new EEcrisMsgName();
                        name.Id = BaseEntity.GenerateNewId();
                        name.LangCode = familyName.languageCode;
                        name.Firstname = familyName.Value;
                        name.EEcrisMsgId = m.Id;
                        m.EEcrisMsgNames.Add(name);
                    }
                    else
                    {
                        nameByLang.Familyname = familyName.Value;
                    }
                }
            }

            var surnameNames = person?.PersonName?.Surname?.ToList();
            if (surnameNames != null)
            {
                foreach (var surname in surnameNames)
                {
                    var nameByLang = m.EEcrisMsgNames.FirstOrDefault(n => n.LangCode == surname.languageCode);
                    if (nameByLang == null)
                    {
                        EEcrisMsgName name = new EEcrisMsgName();
                        name.Id = BaseEntity.GenerateNewId();
                        name.LangCode = surname.languageCode;
                        name.Firstname = surname.Value;
                        name.EEcrisMsgId = m.Id;
                        m.EEcrisMsgNames.Add(name);
                    }
                    else
                    {
                        nameByLang.Surname = surname.Value;
                    }
                }
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
                foreach (var nationality in nationalities)
                {
                    var countryID = countries.FirstOrDefault(c => c.EcrisTechnId == nationality)?.Id;
                    if (countryID != null)
                    {
                        EEcrisMsgNationality nat = new EEcrisMsgNationality();
                        nat.Id = BaseEntity.GenerateNewId();
                        nat.EEcrisMsgId = m.Id;
                        nat.CountryId = countryID;
                        m.EEcrisMsgNationalities.Add(nat);
                    }
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

        public static async Task<List<PPersonId>> GetPersonIDsByEGN(string Egn, CaisDbContext dbContext, string? graoIssuer, string? countryBGcode, string? egnType)
        {
            var pid = await dbContext.PPersonIds.FirstOrDefaultAsync(pp => pp.Pid == Egn && pp.Issuer == graoIssuer && pp.CountryId == countryBGcode && pp.PidTypeId == egnType);
            if (pid != null)
            {
                var personIds = await dbContext.PPersonIds.Where(pp => pp.PersonId == pid.PersonId).ToListAsync();
                return personIds;
            }
            else
            {
                return null;
            }
        }




    }
}
