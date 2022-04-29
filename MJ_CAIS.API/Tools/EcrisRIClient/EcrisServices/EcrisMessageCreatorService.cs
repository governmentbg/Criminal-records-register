using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

namespace EcrisIntegrationServices
{
    public class EcrisMessageCreatorService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<EcrisMessageCreatorService> _logger;
        const string REQUEST_SUCCESSFUL = "RRT-00-00";
        const string REQUEST_DENIAL = "RRT-00-01";
        const string REQUEST_NOT_FROM_MEMBER_STATE = "RRT-00-02";
        const string REQUEST_DEAD_PERSON = "RRT-00-03";
        const string REQUEST_NIST_NOT_MATCH = "RRT-00-04";
        const string REQUEST_MULTIPLE_PEOPLE_FOUND = "RRT-00-05";
        const string ECRIS_REQUEST_CODE = "EcrisReq";
        const string ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON = "Identified";
        const string ECRIS_MESSAGE_STATUS_UNIDENTIFIED_PERSON = "Unidentified";
        public EcrisMessageCreatorService(CaisDbContext dbContext, ILogger<EcrisMessageCreatorService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task CreateResponsesToRequests(int pageSize, string joinSeparator)
        {
            _logger.LogInformation($"Creation of responces started. PageSize: {pageSize}; joinSeparator: {joinSeparator}.");
            try
            {
                var pageNumber = 0;
                var numberOfMessages = 0;
                do
                {
                    _logger.LogTrace($"Page {pageNumber}.");
                    var messages = await GetRequestForReplyingIdentifiedPeople(pageNumber, pageSize);
                    pageNumber++;
                    numberOfMessages = messages.Count();
                    _logger.LogTrace($"{numberOfMessages} messages selected.");
                    if (numberOfMessages != 0)
                    {
                        foreach (var request in messages)
                        {
                            var reqResp = await GenerateResponseToRequest(request);

                            //todo: what is the value of convictionID?!
                            AddMessageToDBContext(reqResp, "", joinSeparator);

                        }

                        int insertedMessages = await _dbContext.SaveChangesAsync();
                        _logger.LogTrace($"{insertedMessages / 3} messages inserted to db.");
                    }
                }
                while (numberOfMessages > 0);


            }
            catch (Exception ex)
            {
                //todo: транзакции и промени в базата?!
                _logger.LogError(ex.Message, ex.Data, ex);
                throw ex;
            }

            _logger.LogInformation($"Creation of responces ended.");

        }

        public async Task<RequestResponseMessageType> GenerateResponseToRequest(RequestMessageType request)
        {
            var reqResp = CreateRequestResponseNoConvictionSuccessful(request);
            var personID = await CommonService.GetPersonIDForEcrisMessages(request.EcrisMsgId, _dbContext);
            if (string.IsNullOrEmpty(personID))
            {
                throw new Exception("Person is not identified.");
            }
            //todo: find convictions
            var fbbcs = await CheckFBBCAsync(request, personID);
            _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of find fbbc records: {fbbcs.Count()}.");

            foreach (var fbbc in fbbcs)
            {
                await AddFBBCToResponce(reqResp, fbbc);
                _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, FBBC with ID {fbbc.Id} added.");
            }

            var buletins = await CheckBuletinsAsync(request, personID);
            _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of buletins fbbc records: {buletins.Count()}.");
            foreach (var buletin in buletins)
            {
                await AddBulletinToResponce(reqResp, buletin);
                _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, BBulletin with ID {buletin.Id} added.");
            }

            return reqResp;
        }




        private async Task AddBulletinToResponce(RequestResponseMessageType reqResp, BBulletin buletin)
        {
            throw new NotImplementedException();
            List<BBulletin> personBuletins = new List<BBulletin>();

            if (personBuletins.Count > 0)
            {
                List<ConvictionType> convictions = reqResp.RequestResponseMessageConviction?.ToList();
                if (convictions == null)
                {
                    convictions = new List<ConvictionType>();

                }
                string bgCode = _dbContext.GCountries.FirstOrDefault(c => c.Iso3166Alpha2 == "BG").EcrisTechnId;
                foreach (var personBuletin in personBuletins)
                {
                    ConvictionType conv = GetConvictionFromBuletin(personBuletin, bgCode);

                    convictions.Add(conv);
                }
                reqResp.RequestResponseMessageConviction = convictions.ToArray();
            }

        }


        private ConvictionType GetConvictionFromBuletin(BBulletin buletin, string bgCode)
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

            conv.ConvictionIsTransmittable = CommonService.GetYesNoType(buletin.ConvIsTransmittable == 1);

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
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[0].Value = buletin.DecidingAuth.Name;
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[1].Value = buletin.DecidingAuth.NameEn;
                conv.ConvictionDecidingAuthority.DecidingAuthorityName[2].Value = buletin.DecidingAuth.DisplayName;
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



        private OffenceType[] GetOffences(BBulletin personBuletin)
        {
            throw new NotImplementedException();
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



        private DecisionType[] GetDecisions(BBulletin personBuletin)
        {
            throw new NotImplementedException();
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

        private SanctionType[] GetSanctions(BBulletin personBuletin)
        {
            throw new NotImplementedException();
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

        private async Task AddFBBCToResponce(RequestResponseMessageType reqResp, Fbbc fbbc)
        {
            ConvictionType conv = new ConvictionType();

            var docContents = _dbContext.DDocContents.Where(cont => cont.DDocuments.Where(d => d.FbbcId == fbbc.Id && d.EcrisMsgId != null).Any()
                                                                     && cont.MimeType == "application/xml").Select(cont => cont.Content);


            var notifications = await docContents?.Select(c => XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(c)) as NotificationMessageType)?.ToListAsync();

            var listOfConvictions = reqResp.RequestResponseMessageConviction?.ToList();
            if (listOfConvictions == null)
            {
                listOfConvictions = new List<ConvictionType>();
            }

            listOfConvictions.AddRange(notifications.Select(n => n.NotificationMessageConviction).ToList());
            reqResp.RequestResponseMessageConviction = listOfConvictions.ToArray();

        }

        private async Task<List<Fbbc>> CheckFBBCAsync(RequestMessageType request, string personID)
        {
                      
            //всички ekris msg за човека, за които има фббц запис
            var ecrisMsgs = _dbContext.EEcrisMessages.Where(e => e.FbbcId != null && e.EEcrisIdentifications.Any(ei => ei.Approved == 1 && ei.PersonId == personID))
                 .Select(e => e.Id).ToList();

            //имат връзка с ecris msg, първи получен fbbc за convictionID,fbbc - не е изтрито
            return await _dbContext.Fbbcs.Where(f => //f.PersonId == personID &&
                                 f.StatusCode == FbbcConstants.FBBCStatus.Active
                                && ecrisMsgs.Contains(f.Id)).GroupBy(f => f.EcrisConvId).Select(g => g.OrderBy(f => f.ReceiveDate).First()).ToListAsync();

        }


        private async Task<List<BBulletin>> CheckBuletinsAsync(RequestMessageType request, string personID)
        {
  

            return new List<BBulletin>();

        }


        private RequestResponseMessageType CreateRequestResponseNoConvictionBase(RequestMessageType request, string responseType)
        {

            RequestResponseMessageType reqResp = new RequestResponseMessageType();
            // reqResp.AuthoringLanguage = "bg";
            reqResp.MessageSendingMemberState = MemberStateCodeType.BG;
            reqResp.MessageSendingMemberStateSpecified = true;
            reqResp.MessagePerson = request.MessagePerson;
            reqResp.MessageReceivingMemberState = new MemberStateCodeType[] {
                    request.MessageSendingMemberState
            };
            reqResp.MessageSendingMemberStateSpecified = true;
            reqResp.MessageType = EcrisMessageType.RRS;
            reqResp.MessageTypeSpecified = true;
            reqResp.MessageResponseTo = new RestrictedIdentifiableMessageType()
            {
                MessageEcrisIdentifier = request.MessageEcrisIdentifier,
                MessageIdentifier = request.MessageIdentifier
            };

            reqResp.RequestResponseMessageOtherMemberState = new MemberStateCodeType[1] {
                    request.MessageSendingMemberState
                };
            reqResp.RequestResponseMessageRequestResponseTypeReference = new RequestResponseTypeExternalReferenceType()
            {
                Value = responseType// for successful - "RRT-00-00"

            };
            return reqResp;
        }

        public RequestResponseMessageType CreateRequestResponseNoConvictionSuccessful(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_SUCCESSFUL);

        }
        public RequestResponseMessageType CreateRequestResponseNoConvictionDenial(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_DENIAL);

        }
        public RequestResponseMessageType CreateRequestResponseNoConvictionMultiplePeopleFound(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_MULTIPLE_PEOPLE_FOUND);

        }
        public RequestResponseMessageType CreateRequestResponseNoConvictionNISTNotMatch(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_NIST_NOT_MATCH);

        }
        public RequestResponseMessageType CreateRequestResponseNoConvictionNotFromMemberState(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_NOT_FROM_MEMBER_STATE);

        }
        public RequestResponseMessageType CreateRequestResponseNoConvictionDeadPerson(RequestMessageType request)
        {
            return CreateRequestResponseNoConvictionBase(request, REQUEST_DEAD_PERSON);

        }

        private async Task<List<RequestMessageType>> GetRequestForReplyingIdentifiedPeople(int pageNumber, int pageSize)
        {
            var contents = await _dbContext.DDocContents.Where(cont => cont.DDocuments
                                 .Where(dd => dd.EcrisMsgId != null
                                 && dd.EcrisMsg.EcrisMsgStatus == ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON
                                 && dd.EcrisMsg.MsgTypeId == ECRIS_REQUEST_CODE
                                 //todo: дали да гледаме само тези с EEcrisInboxes.Count > 0 ?
                                 ).Any() && cont.Content != null).Select(cc => new { cc.Content, cc.DDocuments.First().EcrisMsgId, cc.CreatedOn })
                                 .OrderBy(c => c.CreatedOn)
                                 .Skip(pageNumber * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            var result = contents?.Select(c => new { message = XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(c.Content)) as RequestMessageType, c.EcrisMsgId })
                  .ToList();
            result?.ForEach(c => { c.message.EcrisMsgId = c.EcrisMsgId; });
            return result?.Select(c => c.message)?.ToList();
        }
        private void AddMessageToDBContext(AbstractMessageType msg, string convictionID, string joinSeparator)
        {

            EEcrisMessage m = CreateEcrisMessage(msg, convictionID, joinSeparator);


            DDocument d = CommonService.GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, m.Firstname, m.Surname, m.Familyname, _dbContext);

            d.EcrisMsg = m;
            m.DDocuments.Add(d);
            DDocContent content = CommonService.GetDDocContent(XmlUtils.SerializeToXml(msg));

            d.DocContent = content;
            content.DDocuments.Add(d);

            _dbContext.EEcrisMessages.Add(m);
            _dbContext.DDocuments.Add(d);
            _dbContext.DDocContents.Add(content);
        }
        private EEcrisMessage CreateEcrisMessage(MJ_CAIS.DTO.EcrisService.AbstractMessageType msg, string convictionID, string joinSeparator, string requestMsgId = "")
        {
            EEcrisMessage m = new EEcrisMessage();

            m.Id = BaseEntity.GenerateNewId();
            m.EcrisIdentifier = msg.MessageEcrisIdentifier;
            m.Identifier = msg.MessageIdentifier;//или да се пази в m.RequestMsgId???
            m.EcrisMsgConvictionId = convictionID;
            PersonType person = null;
            if (msg.MessageType == EcrisMessageType.RRS)
            {
                person = ((RequestResponseMessageType)msg).MessagePerson;
                m.MsgTypeId = CommonService.GetDocTypeCode(EcrisMessageTypeOrAliasMessageType.RRS, _dbContext);
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
                m.MsgTypeId = CommonService.GetDocTypeCode(EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
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
            m.BirthCity = string.Join(joinSeparator, person.PersonBirthPlace.PlaceTownName.Select(p => p.Value)); //msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownReference???
            m.BirthDate = new DateTime(Int32.Parse(XmlUtils.GetNumbersFromString(person.PersonBirthDate.DateYear)),
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

            var countriesAuthorities = _dbContext.EEcrisAuthorities.Where(ea => ea.ValidFrom <= DateTime.UtcNow && ea.ValidTo >= DateTime.UtcNow
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
                var countries = _dbContext.GCountries.Where(c => c.EcrisTechnId != null && nationalities.Contains(c.EcrisTechnId)
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

        public async Task CreateNotificationFromBulletin(BBulletin bulletin, string joinSeparator = " ")
        {
            string bgCode = (await _dbContext.GCountries.FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG"))?.EcrisTechnId;
            NotificationMessageType msg = new NotificationMessageType();

            msg.NotificationMessageConviction = GetConvictionFromBuletin(bulletin, bgCode);

            LoadCommonDataFromBulletin(msg, bulletin);

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageOtherAffectedConviction = new ConvictionToConvictionsRelationshipType();
            //msg.NotificationMessageOtherAffectedConviction.SourceConviction = "";
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction = new StructuredConvictionReferenceType[1];
            //msg.NotificationMessageOtherAffectedConviction.DestinationConviction[0].Item =?

            //дали може да се вземе от някъде?!
            //msg.NotificationMessageUpdatedConvictionReference = new UpdateConvictionReferenceType();
            //msg.NotificationMessageUpdatedConvictionReference.Item = ?



            AddMessageToDBContext(msg, bulletin.EcrisConvictionId, joinSeparator);

            await _dbContext.SaveChangesAsync();

        }

        private void LoadCommonDataFromBulletin(NotificationMessageType msg, BBulletin bulletin)
        {
            msg.MessageType = EcrisMessageType.NOT;
            msg.MessageTypeSpecified = true;

            msg.MessageSendingMemberStateSpecified = true;
            msg.MessageSendingMemberState = MemberStateCodeType.BG;
            var notBGNacionality = bulletin.BPersNationalities.Where(nacionality => nacionality.Country.Iso3166Alpha2 != "BG");
            List<MemberStateCodeType> notBGNacionalityInEU = new List<MemberStateCodeType>();
            foreach (var nacionality in notBGNacionality)
            {
                object res;
                if (Enum.TryParse(typeof(MemberStateCodeType), nacionality.Country.Iso3166Alpha2.ToUpper(), out res))
                {
                    notBGNacionalityInEU.Add((MemberStateCodeType)res);
                }

            }
            if (notBGNacionalityInEU.Count() > 0)
            {
                msg.MessageReceivingMemberState = new MemberStateCodeType[notBGNacionalityInEU.Count()];
                for (int i = 0; i < notBGNacionalityInEU.Count(); i++)
                {
                    msg.MessageReceivingMemberState[i] = notBGNacionalityInEU[i];

                }
            }
            else
            {
                throw new Exception("No receiving country in EU.");
            }

            msg.MessagePerson = new PersonType();
            //todo: add adress from somewhere
            //msg.MessagePerson.PersonAddress;

            //todo: да се добавят ли от групата?!
            //msg.MessagePerson.PersonAlias;
            msg.MessagePerson.PersonBirthPlace = new AliasBirthPlaceType();
            if (bulletin.BirthCity != null)
            {
                msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[2];
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthCity.Name;
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].languageCode = "BG";
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].Value = bulletin.BirthCity.NameEn;
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[1].languageCode = "EN";
                msg.MessagePerson.PersonBirthPlace.PlaceTownReference = new CityExternalReferenceType();
                msg.MessagePerson.PersonBirthPlace.PlaceTownReference.Value = bulletin.BirthCity.EcrisTechnId;
            }
            else
            {
                msg.MessagePerson.PersonBirthPlace.PlaceTownName = new MultilingualTextType200CharsMultilingualTextLinguisticRepresentation[1];
                msg.MessagePerson.PersonBirthPlace.PlaceTownName[0].Value = bulletin.BirthPlaceOther;
            }
            msg.MessagePerson.PersonBirthPlace.PlaceCountryReference = new CountryExternalReferenceType();
            msg.MessagePerson.PersonBirthPlace.PlaceCountryReference.Value = bulletin.BirthCountry.EcrisTechnId;


            if (bulletin.BirthDate.HasValue)
            {
                msg.MessagePerson.PersonBirthDate = CommonService.GetDateTypeFromDateAndPrecission(bulletin.BirthDate.Value, bulletin.BirthDatePrecision);
            }



            msg.MessagePerson.PersonFatherForename = new NameTextType[1];
            msg.MessagePerson.PersonFatherForename[0].Value = bulletin.FatherFirstname;
            msg.MessagePerson.PersonFatherSecondSurname = new NameTextType[1];
            msg.MessagePerson.PersonFatherSecondSurname[0].Value = bulletin.FatherFamilyname;
            msg.MessagePerson.PersonFatherSurname = new NameTextType[1];
            msg.MessagePerson.PersonFatherSurname[0].Value = bulletin.FatherSurname;
            //msg.MessagePerson.PersonFormerForename;
            //msg.MessagePerson.PersonFormerSecondSurname;
            //msg.MessagePerson.PersonFormerSurname;

            msg.MessagePerson.PersonIdentificationDocument = new IdentificationDocumentType[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentNumber = bulletin.IdDocNumber;
            if (bulletin.IdDocIssuingDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingDate = CommonService.GetDateTypeFromDateAndPrecission(bulletin.IdDocIssuingDate.Value, bulletin.IdDocIssuingDatePrec);
            }


            if (bulletin.IdDocValidDate.HasValue)
            {
                msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentValidUntil = CommonService.GetDateTypeFromDateAndPrecission(bulletin.IdDocValidDate.Value, bulletin.IdDocValidDatePrec);
            }
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentType1 = new MultilingualTextType50CharsMultilingualTextLinguisticRepresentation[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentType1[0].Value = bulletin.IdDocTypeDescr;
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentCategoryReference = new IdentificationDocumentCategoryExternalReferenceType();
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentCategoryReference.Value = bulletin.IdDocCategory.EcrisTechnId;
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingAuthority = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
            msg.MessagePerson.PersonIdentificationDocument[1].IdentificationDocumentIssuingAuthority[0].Value = bulletin.IdDocIssuingAuthority;


            //egn, lnch или лн?!
            msg.MessagePerson.PersonIdentityNumber = new RestrictedStringType50Chars();
            msg.MessagePerson.PersonIdentityNumber.Value = string.Join(';', bulletin.Ln, bulletin.Lnch, bulletin.Egn);

            msg.MessagePerson.PersonMotherForename = new NameTextType[1];
            msg.MessagePerson.PersonMotherForename[0].Value = bulletin.MotherFirstname;
            msg.MessagePerson.PersonMotherSecondSurname = new NameTextType[1];
            msg.MessagePerson.PersonMotherSecondSurname[0].Value = bulletin.MotherFamilyname;
            msg.MessagePerson.PersonMotherSurname = new NameTextType[1];
            msg.MessagePerson.PersonMotherSurname[0].Value = bulletin.MotherSurname;



            msg.MessagePerson.PersonName = new PersonNameType();
            msg.MessagePerson.PersonName.Forename = new NameTextType[2];
            msg.MessagePerson.PersonName.Forename[0].Value = bulletin.Firstname;
            msg.MessagePerson.PersonName.Forename[0].languageCode = "BG";
            msg.MessagePerson.PersonName.Forename[1].Value = bulletin.FirstnameLat;
            msg.MessagePerson.PersonName.Forename[1].languageCode = "EN";

            msg.MessagePerson.PersonName.SecondSurname = new NameTextType[2];
            msg.MessagePerson.PersonName.SecondSurname[0].Value = bulletin.Familyname;
            msg.MessagePerson.PersonName.SecondSurname[0].languageCode = "BG";
            msg.MessagePerson.PersonName.SecondSurname[1].Value = bulletin.FamilynameLat;
            msg.MessagePerson.PersonName.SecondSurname[1].languageCode = "EN";

            msg.MessagePerson.PersonName.Surname = new NameTextType[2];
            msg.MessagePerson.PersonName.Surname[0].Value = bulletin.Surname;
            msg.MessagePerson.PersonName.Surname[0].languageCode = "BG";
            msg.MessagePerson.PersonName.Surname[1].Value = bulletin.SurnameLat;
            msg.MessagePerson.PersonName.Surname[1].languageCode = "EN";

            msg.MessagePerson.PersonName.FullName = new FullNameTextType[2];
            msg.MessagePerson.PersonName.FullName[0].Value = bulletin.Fullname;
            msg.MessagePerson.PersonName.FullName[0].languageCode = "BG";
            msg.MessagePerson.PersonName.FullName[1].Value = bulletin.FullnameLat;
            msg.MessagePerson.PersonName.FullName[1].languageCode = "EN";


            msg.MessagePerson.PersonNationalityReference = bulletin.BPersNationalities.Select(n => new CountryExternalReferenceType() { Value = n.Country.Iso3166Alpha2 }).ToArray();

            //msg.MessagePerson.PersonRemarks ;
            //1-мъж, 2 - жена
            msg.MessagePerson.PersonSex = (int)bulletin.Sex;
            msg.MessagePerson.PersonSexSpecified = bulletin.Sex == 0;


        }


    }
}
