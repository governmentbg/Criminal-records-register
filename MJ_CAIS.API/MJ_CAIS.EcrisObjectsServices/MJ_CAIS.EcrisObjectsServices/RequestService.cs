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

namespace MJ_CAIS.EcrisObjectsServices
{
    public class RequestService
    {
        const string REQUEST_SUCCESSFUL = "RRT-00-00";
        const string REQUEST_DENIAL = "RRT-00-01";
        const string REQUEST_NOT_FROM_MEMBER_STATE = "RRT-00-02";
        const string REQUEST_DEAD_PERSON = "RRT-00-03";
        const string REQUEST_NIST_NOT_MATCH = "RRT-00-04";
        const string REQUEST_MULTIPLE_PEOPLE_FOUND = "RRT-00-05";
        const string ECRIS_REQUEST_CODE = "EcrisReq";
        const string ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON = "Identified";
        const string ECRIS_MESSAGE_STATUS_UNIDENTIFIED_PERSON = "Unidentified";
        private CaisDbContext _dbContext;
        private readonly ILogger<RequestService> _logger;
        public RequestService(CaisDbContext dbContext, ILogger<RequestService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
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

            List<BBulletin> personBuletins = new List<BBulletin>();

            if (personBuletins.Count > 0)
            {
                List<ConvictionType> convictions = reqResp.RequestResponseMessageConviction?.ToList();
                if (convictions == null)
                {
                    convictions = new List<ConvictionType>();

                }
                string bgCode = (await _dbContext.GCountries.FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG")).EcrisTechnId;
                foreach (var personBuletin in personBuletins)
                {
                    ConvictionType conv = CommonService.GetConvictionFromBuletin(personBuletin, bgCode);

                    convictions.Add(conv);
                }
                reqResp.RequestResponseMessageConviction = convictions.ToArray();
            }

        }
        private async Task AddFBBCToResponce(RequestResponseMessageType reqResp, Fbbc fbbc)
        {
            ConvictionType conv = new ConvictionType();

            var docContents = _dbContext.DDocContents.Where(cont => cont.DDocuments.Where(d => d.FbbcId == fbbc.Id && d.EcrisMsgId != null).Any()
                                                                     && cont.MimeType == "application/xml").Select(cont => cont.Content);
            if (docContents != null)
            {

                var notifications = await docContents.Where(c => c != null).Select(c => XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(c)) as NotificationMessageType).ToListAsync();
                if (notifications != null)
                {
                    var listOfConvictions = reqResp.RequestResponseMessageConviction?.ToList();
                    if (listOfConvictions == null)
                    {
                        listOfConvictions = new List<ConvictionType>();
                    }

                    listOfConvictions.AddRange(notifications.Where(n => n != null).Select(n => n.NotificationMessageConviction).ToList());
                    reqResp.RequestResponseMessageConviction = listOfConvictions.ToArray();
                }
            }

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


    }
}
