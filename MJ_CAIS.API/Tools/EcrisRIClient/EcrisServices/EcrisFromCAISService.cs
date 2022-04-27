using EcrisIntegrationServices;
using EcrisRIClient;
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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace EcrisServices
{
    public class EcrisFromCAISService
    {
        private CaisDbContext _dbContext;
        private readonly ILogger<EcrisFromCAISService> _logger;
        const string REQUEST_SUCCESSFUL = "RRT-00-00";
        const string REQUEST_DENIAL = "RRT-00-01";
        const string REQUEST_NOT_FROM_MEMBER_STATE = "RRT-00-02";
        const string REQUEST_DEAD_PERSON = "RRT-00-03";
        const string REQUEST_NIST_NOT_MATCH = "RRT-00-04";
        const string REQUEST_MULTIPLE_PEOPLE_FOUND = "RRT-00-05";
        const string ECRIS_REQUEST_CODE = "EcrisReq";
        const string ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON = "Identified";
        const string ECRIS_MESSAGE_STATUS_UNIDENTIFIED_PERSON = "Unidentified";
        public EcrisFromCAISService(CaisDbContext dbContext, ILogger<EcrisFromCAISService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        //public async Task InsertReplyToRequests(string username, string password, string folderName, string joinSeparator)
        //{
        //    _logger.LogInformation($"Insert of replies to requests started. Username: {username}; Folder: {folderName}.");
        //    try
        //    {
        //        EcrisClient client = new EcrisClient(username, password);
        //        _logger.LogTrace($" EcrisClient created.");
        //        var sessionID = await client.GetActiveSessionId();
        //        _logger.LogTrace($" EcrisClient logged in.");
        //        var folderId = await client.GetInboxFolderIdentifier(sessionID, folderName);
        //        _logger.LogTrace($" Folder {folderName} identified as {folderId}.");

        //        var messages = await GetRequestForReplyingIdentifiedPeople();

        //        foreach (var request in messages)
        //        {
        //            var reqResp = CreateRequestResponseNoConvictionSuccessful(request);

        //            //todo: find convictions


        //            var fbbcs = await CheckFBBCAsync(request);
        //            foreach(var fbbc in fbbcs)
        //            {
        //                AddFBBCToResponce(reqResp, fbbc);
        //            }
        //            var buletins = await CheckBuletinsAsync(request);
        //            foreach (var buletin in buletins)
        //            {
        //                AddBuletinToResponce(reqResp, buletin);
        //            }

        //            //todo: what is the value of convictionID?!
        //            AddMessageToDBContext(reqResp, "", joinSeparator);

        //            //var resp = await client.InsertRequestResponse(reqResp, sessionID, folderId);


        //        }

        //        _dbContext.SaveChanges();


        //    }
        //    catch (Exception ex)
        //    {
        //        //todo: транзакции и промени в базата?!
        //        _logger.LogError(ex.Message, ex.Data, ex);
        //        throw ex;
        //    }




        //    _logger.LogInformation($"Insert of replies to requests ended.");

        //}

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

            //todo: find convictions
            var fbbcs = await CheckFBBCAsync(request);
            _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of find fbbc records: {fbbcs.Count()}.");

            foreach (var fbbc in fbbcs)
            {
                await AddFBBCToResponce(reqResp, fbbc);
            }
            var buletins = await CheckBuletinsAsync(request);
            _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of buletins fbbc records: {buletins.Count()}.");
            foreach (var buletin in buletins)
            {
                await AddBuletinToResponce(reqResp, buletin);
            }

            return reqResp;
        }

        private async Task AddBuletinToResponce(RequestResponseMessageType reqResp, BBulletin buletin)
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
                    ConvictionType conv = new ConvictionType();

                    conv.ConvictionSanction = GetSanctions(personBuletin);
                    conv.ConvictionDecision = GetDecisions(personBuletin); ;
                    conv.ConvictionOffence = GetOffences(personBuletin); ;
                    if (personBuletin.DecisionDate.HasValue)
                    {
                        conv.ConvictionDecisionDate = new StrictDateType();
                        conv.ConvictionDecisionDate.Value = personBuletin.DecisionDate.Value;
                    }
                    if (personBuletin.DecisionFinalDate.HasValue)
                    {
                        conv.ConvictionDecisionFinalDate = new StrictDateType();
                        conv.ConvictionDecisionFinalDate.Value = personBuletin.DecisionFinalDate.Value;
                    }

                    conv.ConvictionIsTransmittable = CommonService.GetYesNoType(personBuletin.ConvIsTransmittable == 1);

                    if (personBuletin.ConvRetPeriodEndDate.HasValue)
                    {
                        conv.ConvictionRetentionPeriodEndDate = new StrictDateType();
                        conv.ConvictionRetentionPeriodEndDate.Value = personBuletin.ConvRetPeriodEndDate.Value;
                    }
                    if (!string.IsNullOrEmpty(personBuletin.ConvRemarks))
                    {
                        conv.ConvictionRemarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                        conv.ConvictionRemarks[0].Value = personBuletin.ConvRemarks;

                    }
                    if (!string.IsNullOrEmpty(personBuletin.DecidingAuthId))
                    {
                        conv.ConvictionDecidingAuthority = new DecidingAuthorityType();
                        conv.ConvictionDecidingAuthority.DecidingAuthorityCode = new RestrictedStringType50Chars();
                        //todo: кой идентификатор да се пише тук
                        conv.ConvictionDecidingAuthority.DecidingAuthorityCode.Value = personBuletin.DecidingAuth.Id;
                        conv.ConvictionDecidingAuthority.DecidingAuthorityName = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[3];
                        conv.ConvictionDecidingAuthority.DecidingAuthorityName[0].Value = personBuletin.DecidingAuth.Name;
                        conv.ConvictionDecidingAuthority.DecidingAuthorityName[1].Value = personBuletin.DecidingAuth.NameEn;
                        conv.ConvictionDecidingAuthority.DecidingAuthorityName[2].Value = personBuletin.DecidingAuth.DisplayName;
                    }
                    conv.ConvictionID = personBuletin.EcrisConvictionId;
                    //todo: винаги ли е бг?
                    conv.ConvictionConvictingCountryReference = new CountryExternalReferenceType();
                    conv.ConvictionConvictingCountryReference.Value = bgCode;
                    //todo: какво да пиша в тези полета?
                    //conv.ConvictionRelationship;                
                    //conv.ConvictionFileNumber;
                    //conv.ConvictionNonCriminalRuling;





                    convictions.Add(conv);
                }
                reqResp.RequestResponseMessageConviction = convictions.ToArray();
            }

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
                s.SanctionSuspension.PeriodDuration = "Години: " + sanction.SuspentionDurationYears
                  + " Месеци: " + sanction.SuspentionDurationMonths
                  + " Дни: " + sanction.SuspentionDurationDays
                  + " Часове: " + sanction.SuspentionDurationHours;
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
                ////if (sanction.DecisionStartDate.HasValue)
                ////{
                ////    s.SanctionSentencedPeriod.PeriodStartDate = new StrictDateType();
                ////    s.SanctionSentencedPeriod.PeriodStartDate.Value = sanction.DecisionStartDate.Value;
                ////}
                //if (sanction.DecisionEndDate.HasValue)
                //{
                //    s.SanctionSentencedPeriod.PeriodEndDate = new StrictDateType();
                //    s.SanctionSentencedPeriod.PeriodEndDate.Value = sanction.DecisionEndDate.Value;
                //}
                s.SanctionSentencedPeriod.PeriodDuration =
                  "Години: " + sanction.DecisionDurationYears
                  + " Месеци: " + sanction.DecisionDurationMonths
                  + " Дни: " + sanction.DecisionDurationDays
                  + " Часове: " + sanction.DecisionDurationHours;

                s.NationalCategoryCode = sanction.SanctCategory.Code;
                s.NationalCategoryTitle = new MultilingualTextType400CharsMultilingualTextLinguisticRepresentation[1];
                s.NationalCategoryTitle[0].Value = sanction.SanctCategory.Name;
                s.Remarks = new UncollapsedMultilingualTextTypeMultilingualTextLinguisticRepresentation[1];
                s.Remarks[0].Value = sanction.Descr;






                s.SanctionTypeReference = new SanctionNatureExternalReferenceType();
                s.SanctionTypeReference.Value = sanction.EcrisSanctCateg.EcrisTechnId;

                s.SanctionExecutionPeriod = new SanctionPeriodType();
                //if (sanction.ExecutionStartDate.HasValue)
                //{
                //    s.SanctionExecutionPeriod.PeriodStartDate = new StrictDateType();
                //    s.SanctionExecutionPeriod.PeriodStartDate.Value = sanction.ExecutionStartDate.Value;

                //}
                //if (sanction.ExecutionEndDate.HasValue)
                //{
                //    s.SanctionExecutionPeriod.PeriodEndDate = new StrictDateType();
                //    s.SanctionExecutionPeriod.PeriodEndDate.Value = sanction.ExecutionEndDate.Value;

                //}
                //s.SanctionExecutionPeriod.PeriodDuration =
                //    "Години: " + sanction.ExecutionDurationYears
                //    + " Месеци: " + sanction.ExecutionDurationMonths
                //    + " Дни: " + sanction.ExecutionDurationDays
                //    + " Часове: " + sanction.ExecutionDurationHours;




                //s.SanctionIsSpecificToMinor = CommonService.GetYesNoType(sanction.SpecificToMinor);


                result.Add(s);
            }




            return result.ToArray();
        }

        private async Task AddFBBCToResponce(RequestResponseMessageType reqResp, Fbbc fbbc)
        {
            ConvictionType conv = new ConvictionType();

            var docContents = _dbContext.DDocContents.Where(cont => cont.DDocuments.Where(d => d.FbbcId == fbbc.Id).Any()
                                       && cont.MimeType == "application/xml").Select(cont => cont.Content);


            var notifications = await docContents?.Select(c => XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(c)) as NotificationMessageType)
                      ?.ToListAsync();

            var listOfConvictions = reqResp.RequestResponseMessageConviction?.ToList();
            if (listOfConvictions == null)
            {
                listOfConvictions = new List<ConvictionType>();
            }

            listOfConvictions.AddRange(notifications.Select(n => n.NotificationMessageConviction).ToList());
            reqResp.RequestResponseMessageConviction = listOfConvictions.ToArray();

        }

        private async Task<List<Fbbc>> CheckFBBCAsync(RequestMessageType request)
        {

            var personID = await CommonService.GetPersonIDForEcrisMessages(request.EcrisMsgId, _dbContext);
            if (string.IsNullOrEmpty(personID))
            {
                throw new Exception("Person is not identified.");
            }
            return await _dbContext.Fbbcs.Where(f => f.PersonId == personID).ToListAsync();

        }

        private async Task<List<BBulletin>> CheckBuletinsAsync(RequestMessageType request)
        {
            var personID = await CommonService.GetPersonIDForEcrisMessages(request.EcrisMsgId, _dbContext);
            if (string.IsNullOrEmpty(personID))
            {
                throw new Exception("Person is not identified.");
            }
            //todo: how to find buletins for person?!
            //return await _dbContext.BBulletins.Where(b => b.PersonId == personID).ToListAsync();

            return new List<BBulletin>();

        }

        public async Task SendMessagesToEcris(string username, string password, string folderName)
        {
            _logger.LogInformation($"SendMessagesToEcris started. Username: {username}; Folder: {folderName}.");
            string sessionID = null;
            EcrisClient client = null;
            try
            {
                var contents = await _dbContext.DDocContents.Where(cont => cont.DDocuments
                                    .Where(dd => dd.EcrisMsg != null && dd.EcrisMsg.EcrisMsgStatus == ECRISConstants.EcrisMessageStatuses.ForSending).Any()).ToListAsync();
                if (contents.Count > 0)
                {
                    client = new EcrisClient(username, password);
                    _logger.LogTrace($" EcrisClient created.");
                    sessionID = await client.GetActiveSessionId();
                    _logger.LogTrace($" EcrisClient logged in.");
                    var folderId = await client.GetInboxFolderIdentifier(sessionID, folderName);
                    _logger.LogTrace($" Folder {folderName} identified as {folderId}.");
                    var notificationTypeId = CommonService.GetDocTypeCode(EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
                    var responseTypeId = CommonService.GetDocTypeCode(EcrisMessageTypeOrAliasMessageType.RRS, _dbContext);
                    foreach (var content in contents)
                    {
                        var ecrisMsg = content.DDocuments.First().EcrisMsg;
                        var ecrisOutbox = ecrisMsg.EEcrisOutboxes.FirstOrDefault();
                        bool isNew = false;
                        if (ecrisOutbox == null)
                        {
                            _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : New outbox created.");
                            ecrisOutbox = new EEcrisOutbox();
                            ecrisOutbox.Id = BaseEntity.GenerateNewId();
                            ecrisOutbox.CreatedOn = DateTime.UtcNow;
                            isNew = true;
                        }

                        string xml = Encoding.UTF8.GetString(content.Content);
                        ecrisOutbox.XmlObject = xml;
                        //todo:add constants
                        ecrisOutbox.Status = "PENDING";
                        ecrisOutbox.Error = null;
                        ecrisOutbox.HasError = false;
                        ecrisOutbox.Attempts = ecrisOutbox.Attempts == null ? 1 : ecrisOutbox.Attempts + 1;
                        ecrisOutbox.EcrisMsgId = ecrisMsg.Id;
                        ecrisOutbox.StackTrace = null;
                        ecrisOutbox.ExecutionDate = DateTime.UtcNow;



                        AbstractMessageType msg = XmlUtils.DeserializeXml<AbstractMessageType>(xml);
                        //msg.MessageEcrisIdentifier = null;
                        //msg.MessageIdentifier = null;
                        AbstractMessageType resp = null;

                        var msgTypeID = ecrisMsg.MsgTypeId;
                        try
                        {
                            if (msgTypeID == notificationTypeId)
                            {
                                var not = (NotificationMessageType)msg;
                                ecrisOutbox.Operation = "STORE_NOTIFICATION";
                                if (isNew)
                                {

                                    _dbContext.EEcrisOutboxes.Add(ecrisOutbox);
                                }
                                else
                                {
                                    _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                                }
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox for pending pre-save.");
                                await _dbContext.SaveChangesAsync();
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox for pending saved.");
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Notification pre-insert.");
                                resp = await client.InsertNotification(not, sessionID, folderId);
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Notification post-insert.");
                            }
                            if (msgTypeID == responseTypeId)
                            {
                                var reqResp = (RequestResponseMessageType)msg;
                                ecrisOutbox.Operation = "STORE_RESPONSE";
                                if (isNew)
                                {
                                    _dbContext.EEcrisOutboxes.Add(ecrisOutbox);
                                }
                                else
                                {
                                    _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                                }
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox for pending pre-save.");
                                await _dbContext.SaveChangesAsync();
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox for pending saved.");
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Response pre-insert.");
                                resp = await client.InsertRequestResponse(reqResp, sessionID, folderId);
                                _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Response post-insert.");
                            }

                            ecrisMsg.EcrisIdentifier = resp.MessageEcrisIdentifier;
                            ecrisMsg.Identifier = resp.MessageIdentifier;
                            ecrisMsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Sent;
                            _dbContext.EEcrisMessages.Update(ecrisMsg);
                            ecrisOutbox.Status = "SENT";
                            _dbContext.EEcrisOutboxes.Update(ecrisOutbox);
                            _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox and ecris message pre-update.");
                            await _dbContext.SaveChangesAsync();
                            _logger.LogTrace($"EcrisMessage({ecrisMsg.Id}) : Outbox and ecris message post-update.");
                        }
                        catch (Exception ex)
                        {
                            //continue with next
                            _logger.LogError(ex.Message, ex.Data, ex);
                            ecrisOutbox.Status = "ERROR";
                            ecrisOutbox.HasError = true;
                            ecrisOutbox.StackTrace = ex.StackTrace;
                            ecrisOutbox.Error = ex.Message;
                            await _dbContext.SaveChangesAsync();

                        }



                    }

                }

            }
            catch (Exception ex)
            {
                //todo: транзакции и промени в базата, logout?!
                _logger.LogError(ex.Message, ex.Data, ex);
                throw ex;
            }
            finally
            {
                if (string.IsNullOrEmpty(sessionID))
                {
                    _logger.LogTrace($" EcrisClient logged out.");
                    client?.Logout(sessionID);
                }
            }

            _logger.LogInformation($"SendMessagesToEcris ended.");

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




    }
}
