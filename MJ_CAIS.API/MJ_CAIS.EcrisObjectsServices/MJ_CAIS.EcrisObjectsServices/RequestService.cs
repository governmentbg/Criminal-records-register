using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.EcrisObjectsServices
{
    public class RequestService : IRequestService
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
        public async Task RecreateResponseToRequest(string responseId)
        {
            var emsg = await _dbContext.EEcrisMessages.AsNoTracking()
                .Include(m=>m.EEcrisReferences).AsNoTracking().FirstOrDefaultAsync(x => x.Id == responseId);
            if (emsg == null)
            {
                throw new Exception($"Съобщение {responseId} не съществува.");
            }
            if (string.IsNullOrEmpty(emsg.RequestMsgId))
            {
                throw new Exception($"Няма отговор за съобщение {responseId}");
            }

            var reqEM = await _dbContext.EEcrisMessages.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == emsg.RequestMsgId);

            var content = await _dbContext.DDocContents.AsNoTracking().Where(cont => cont.DDocuments
                              .Where(dd => dd.EcrisMsgId == emsg.RequestMsgId).Any()
                              && cont.Content != null 
                              && cont.MimeType == "application/xml")
                .Select(cc => new { cc.Content, cc.DDocuments.First().EcrisMsgId, cc.CreatedOn })
                              .FirstOrDefaultAsync();
            if(content== null)
            {
                throw new Exception("Няма съдържание на запитване.");
            }

            var request = XmlUtils.DeserializeXml<AbstractMessageType>(
                Encoding.UTF8.GetString(content.Content)
                ) as RequestMessageType;
                           

            var reqResp = await GenerateResponseToRequest(request);                   

            var contentXML = XmlUtils.SerializeToXml(reqResp);
            
            var doc = await _dbContext.DDocContents.AsNoTracking().Where(c => c.MimeType == "application/xml" &&
            c.DDocuments.Any(x => x.EcrisMsgId ==responseId)).ToListAsync();
           
            doc.ForEach(x => {
                x.MimeType = "application/xml";
                x.Content = Encoding.UTF8.GetBytes(contentXML);
                x.Bytes = x.Content.Length;
            });

            emsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForSending;
            if (emsg.EEcrisReferences.Count > 0)
            {
                _dbContext.RemoveRange(emsg.EEcrisReferences);
                
            }
            emsg.EEcrisReferences = new List<EEcrisReference>();
            foreach (var conv in reqResp.RequestResponseMessageConviction)
            {
                
                if (!string.IsNullOrEmpty(conv.BuletinId) || !string.IsNullOrEmpty(conv.FbbcId))
                {
                    EEcrisReference eref = new EEcrisReference();
                    eref.Id = BaseEntity.GenerateNewId();
                    eref.EcrisMsgId = emsg.Id;
                    eref.BulletinId = conv.BuletinId;
                    eref.FbbcId = conv.FbbcId;
                    emsg.EEcrisReferences.Add(eref);

                }
            }
            if (emsg.EEcrisReferences?.Count > 0)
            {
                _dbContext.EEcrisReferences.AddRange(emsg.EEcrisReferences);
            }
            _dbContext.Update(emsg);
            
            _dbContext.UpdateRange(doc);

            await _dbContext.SaveChangesAsync();
        }

        public async Task GenerateUnsuccessfulResponce(string requestId, string reasonCode, string joinSeparator=" ")
        {
            var reqMsg = await _dbContext.EEcrisMessages.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == requestId);

            var content = await _dbContext.DDocContents.AsNoTracking().Where(cont => cont.DDocuments
                              .Where(dd => dd.EcrisMsgId == requestId).Any()
                              && cont.Content != null
                              && cont.MimeType == "application/xml")
                .Select(cc => new { cc.Content, cc.DDocuments.First().EcrisMsgId, cc.CreatedOn })
                              .FirstOrDefaultAsync();
            if (content == null)
            {
                throw new Exception("Няма съдържание на запитване.");
            }

            var request = XmlUtils.DeserializeXml<AbstractMessageType>(
                Encoding.UTF8.GetString(content.Content)
                ) as RequestMessageType;


            if (request != null)
            {
                var reqResp = CreateRequestResponseNoConvictionBase(request, reasonCode);

                //var reqMsg = _dbContext.EEcrisMessages.First(mes => mes.Id == request.EcrisMsgId);
                reqMsg.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ReplyCreated;
                
                await ServiceHelper.AddMessageToDBContextAsync(reqResp, "", "", joinSeparator, _dbContext, requestId);

                _dbContext.EEcrisMessages.Update(reqMsg);

            }
            int insertedMessages = await _dbContext.SaveChangesAsync();
        }
        public async Task<RequestResponseMessageType> GenerateResponseToRequest(RequestMessageType request)
        {
            var reqResp = CreateRequestResponseNoConvictionSuccessful(request);
            var graoPerson = await ServiceHelper.GetPersonIDForEcrisMessages(request.EcrisMsgId, _dbContext);
            if (graoPerson == null)
            {
                throw new Exception("Person is not identified.");
            }
            string graoIssuer = PersonConstants.IssuerType.GRAO;
            string countryBGcode = (await _dbContext.GCountries.AsNoTracking().FirstOrDefaultAsync(c => c.Iso3166Alpha2.ToUpper() == "BG"))?.Id;
            if (string.IsNullOrEmpty(countryBGcode))
            {
                throw new Exception("Country c.Iso3166Alpha2 == \"BG\" does not exist.");
            }
            string egnType = (await _dbContext.PPersonIdTypes.AsNoTracking().FirstOrDefaultAsync(c => c.Code.ToUpper() == PersonConstants.PidType.Egn.ToUpper()))?.Id;
            if (string.IsNullOrEmpty(egnType))
            {
                throw new Exception("Person Id type  Code== \"EGN\" does not exist.");
            }
            var personIds = await ServiceHelper.GetPersonIDsByEGN(graoPerson.Egn, _dbContext, graoIssuer, countryBGcode, egnType);
            if (personIds != null)
            {

                var fbbcs = await CheckFBBCAsync(request, personIds.Select(p => p.Id).ToList());

                _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of find fbbc records: {fbbcs.Count()}.");

                foreach (var fbbc in fbbcs)
                {
                    await AddFBBCToResponce(reqResp, fbbc);
                    _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, FBBC with ID {fbbc.Id} added.");

                }

                var buletins = await CheckBuletinsAsync(request, personIds.Select(p => p.Id).ToList());
                _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, number of buletins fbbc records: {buletins.Count()}.");
                string? bgCode = (await _dbContext.GCountries.FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG"))?.EcrisTechnId;

                //todo: if needed load nomenclatures for all vallues here
                foreach (var buletin in buletins)
                {
                    await AddBulletinToResponce(reqResp, buletin, bgCode);
                    _logger.LogTrace($"EcrisMessageID: {request.EcrisMsgId}, BBulletin with ID {buletin.Id} added.");
                }
            }

            return reqResp;
        }


        private async Task AddBulletinToResponce(RequestResponseMessageType reqResp, BBulletin buletin, string? bgCode)
        {

            List<ConvictionType> convictions = reqResp.RequestResponseMessageConviction?.ToList();
            if (convictions == null)
            {
                convictions = new List<ConvictionType>();

            }

            ConvictionType conv = await ServiceHelper.GetConvictionFromBuletin(buletin, bgCode, _dbContext);
            conv.BuletinId = buletin.Id;
            if (!convictions.Where(c => c.ConvictionID == conv.ConvictionID).Any())
            {
                convictions.Add(conv);
            }
          

            reqResp.RequestResponseMessageConviction = convictions.ToArray();


        }
        private async Task AddFBBCToResponce(RequestResponseMessageType reqResp, Fbbc fbbc)
        {
            //ConvictionType conv = new ConvictionType();

            //conv.FbbcId= fbbc.Id;

            var docContents = _dbContext.DDocContents.Where(cont => cont.DDocuments.Where(d => d.FbbcId == fbbc.Id //&& d.EcrisMsgId != null
                                                                     ).Any()
                                                                     && cont.MimeType == "application/xml").Select(cont => cont.Content);
            if (docContents != null)
            {

                var notifications = await docContents.Where(c => c != null).Select(c => XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(c)) as NotificationMessageType).ToListAsync();
                if (notifications != null)
                {
                    var notificationsConv = notifications.Where(n => n.NotificationMessageConviction != null).ToList();
                    notificationsConv.ForEach(n => n.NotificationMessageConviction.FbbcId = fbbc.Id);

                    var listOfConvictions = reqResp.RequestResponseMessageConviction?.ToList();
                    if (listOfConvictions == null)
                    {
                        listOfConvictions = new List<ConvictionType>();
                    }
                    foreach (var conviction in notificationsConv.Select(c => c.NotificationMessageConviction).ToList())
                    {
                        if (!listOfConvictions.Where(c => c.ConvictionID == conviction.ConvictionID).Any())
                        {
                            listOfConvictions.Add(conviction);
                        }
                    }
                    reqResp.RequestResponseMessageConviction = listOfConvictions.ToArray();
                }
            }

        }

        private async Task<List<Fbbc>> CheckFBBCAsync(RequestMessageType request, List<string> personIDs)
        {

            //всички ekris msg за човека, за които има фббц запис
            //var ecrisMsgs = _dbContext.EEcrisMessages.Where(e => e.FbbcId != null && e.EEcrisIdentifications.Any(ei => ei.Approved == 1 && ei.GraoPersonId == personID))
            //     .Select(e => e.Id).ToList();

            //имат връзка с ecris msg, първи получен fbbc за convictionID,fbbc - не е изтрито
            return await _dbContext.Fbbcs.Where(f => //f.PersonId == personID &&
                                  personIDs.Contains(f.PersonId) &&
                                 f.StatusCode == FbbcConstants.FBBCStatus.Active
                                //дали да се гледат и по ConvictionID?
                                //&& ecrisMsgs.Contains(f.Id)
                                )
                  //EcrisConvId е уникално => само по 1 запис ще има 
                  //.GroupBy(f => f.EcrisConvId)
                  //.Select(g => g.OrderBy(f => f.ReceiveDate).FirstOrDefault())
                  .ToListAsync();

        }


        private async Task<List<BBulletin>> CheckBuletinsAsync(RequestMessageType request, List<string> personIDs)
        {


            return await _dbContext.BBulletins
                                .Include(b => b.BOffences)
                                .Include(b => b.BSanctions)
                                .Include(b => b.BDecisions)
                                .Include(b => b.BPersNationalities)
                                .Include(b => b.BulletinAuthority)
                                .Include(b => b.CsAuthority)
                                .Include(b => b.BBullPersAliases)
                                .Include(b => b.BirthCountry)
                                .Include(b => b.BirthCity)
                                .Include(b => b.CaseAuth)
                                .Include(b => b.DecidingAuth)
                                .Include(b => b.IdDocCategory)
                                //todo: id или код?! дали е този код
                                .Where(b => b.StatusId == BulletinConstants.Status.Active
                               && /*b.PBulletinIds.Where(pb => personIDs.Contains(pb.PersonId)).Any()*/
                               (personIDs.Contains(b.EgnId) ||
                               personIDs.Contains(b.LnchId) ||
                               personIDs.Contains(b.LnId) ||
                               personIDs.Contains(b.IdDocNumberId) ||
                               personIDs.Contains(b.SuidId)
                               ))
                               .ToListAsync();

        }


        private RequestResponseMessageType CreateRequestResponseNoConvictionBase(RequestMessageType request, string responseType)
        {

            RequestResponseMessageType reqResp = new RequestResponseMessageType();
            // reqResp.AuthoringLanguage = "bg";
            reqResp.MessageSendingMemberState = MemberStateCodeType.BG;
            reqResp.MessageSendingMemberStateSpecified = true;
            reqResp.MessagePerson = request.MessagePerson;
            reqResp.MessageReceivingMemberState = new MemberStateCodeType[1] {
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
