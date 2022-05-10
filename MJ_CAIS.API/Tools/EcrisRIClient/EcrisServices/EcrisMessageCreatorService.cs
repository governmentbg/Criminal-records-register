using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.EcrisObjectsServices;
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
        const string ECRIS_REQUEST_CODE = "EcrisReq";
        const string ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON = "Identified";
        RequestService _requestService;
        public EcrisMessageCreatorService(CaisDbContext dbContext, ILogger<EcrisMessageCreatorService> logger, RequestService requestService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _requestService = requestService;
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
                            if (request != null)
                            {
                                var reqResp = await _requestService.GenerateResponseToRequest(request);

                                //todo: what is the value of convictionID?!
                                await ServiceHelper.AddMessageToDBContextAsync(reqResp, "","", joinSeparator,_dbContext);
                            }

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

              
        private async Task<List<RequestMessageType?>> GetRequestForReplyingIdentifiedPeople(int pageNumber, int pageSize)
        {
            //todo: кога сменяме статуса и дали не трябва винаги да връщаме първа страница?!
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
            if (result != null)
            {
                result.ForEach(c => { if (c.EcrisMsgId != null && c.message != null) c.message.EcrisMsgId = c.EcrisMsgId; });
                return result.Select(c => c.message).ToList();
            }
            else return new List<RequestMessageType?>();
        }
      
      

        public async Task ProcessIdentifiedNotificationsAsync()
        {
            _logger.LogInformation($"ProcessIdentifiedNotificationsAsync started.");
            var notificationType = await ServiceHelper.GetDocTypeCodeAsync(EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
            _logger.LogTrace($"Notification type: {notificationType}.");
            var ecrisMsgs = _dbContext.EEcrisMessages.Where(em => em.EcrisMsgStatus == ECRIS_MESSAGE_STATUS_IDENTIFIED_PERSON && em.MsgTypeId == notificationType && em.FbbcId==null);
           
            //todo: тук каква е стойността и от къде се взема?!
            string graoIssuer = "GRAO";
            string countryBGcode = (await _dbContext.GCountries.FirstOrDefaultAsync(c => c.Iso3166Alpha2 == "BG"))?.Id;
            if (string.IsNullOrEmpty(countryBGcode))
            {
                throw new Exception("Country c.Iso3166Alpha2 == \"BG\" does not exist.");
            }
            string egnType = (await _dbContext.PPersonIdTypes.FirstOrDefaultAsync(c => c.Code== "EGN"))?.Id;
            if (string.IsNullOrEmpty(egnType))
            {
                throw new Exception("Person Id type  Code== \"EGN\" does not exist.");
            }
            foreach (var msg in ecrisMsgs)
            {
                //променливи, за да можем да върнем състоянието в catch ако се счупи
                Fbbc f = null;
                Fbbc stateF = null;
                bool isNewF = false;
                List<DDocument> stateD = new List<DDocument>();
                try
                {
                    _logger.LogTrace($"EcrisMessageID: {msg.Id}.");
                    var graoPerson = await ServiceHelper.GetPersonIDForEcrisMessages(msg.Id, _dbContext);
                    if (graoPerson == null)
                    {
                        throw new Exception($"{msg.Id} : Person not identified.");
                    }
                    _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}");
                    List<Fbbc> fbbcs = new List<Fbbc>();

                   var personIds = await ServiceHelper.GetPersonIDsByEGN(graoPerson.Egn, _dbContext, graoIssuer, countryBGcode, egnType);

                    string pidId = "";
                    if (personIds != null && personIds.Count>0)
                    {
                        var personIdsId = personIds.Select(x => x.Id).ToList();
                        pidId = personIds.FirstOrDefault(p => p.Pid == graoPerson.Egn && p.Issuer == graoIssuer && p.CountryId == countryBGcode && p.PidTypeId == egnType)?.Id;

                        fbbcs = await _dbContext.Fbbcs.Where(fbbc => fbbc.EcrisConvId == msg.EcrisMsgConvictionId
                                                                                    && personIdsId.Contains(fbbc.PersonId)
                                                                                    && fbbc.StatusCode == "Active").ToListAsync();
                    }
                    else
                    {
                        //todo: create person
                        PPersonId pid =  new PPersonId();
                        pid.Id = BaseEntity.GenerateNewId();
                        pid.Pid = graoPerson.Egn;
                        pid.Issuer = graoIssuer;
                        pid.CountryId = countryBGcode;
                        pid.PidTypeId = egnType;
                        pidId = pid.Id;


                        PPerson person = new PPerson();
                        person.Id = BaseEntity.GenerateNewId();
                        person.Firstname = graoPerson.Firstname;
                        person.Surname = graoPerson.Surname;
                        person.Familyname = graoPerson.Familyname;
                        person.BirthDate = graoPerson.BirthDate;
                        person.FatherFullname = graoPerson.FathersNames;
                        person.MotherFullname = graoPerson.MothersNames;
                        person.Sex = graoPerson.Sex;
                        //todo: birth place?!
                        person.PPersonIds = new List<PPersonId>();
                        person.PPersonIds.Add(pid);

                        pid.PersonId = person.Id;

                        _dbContext.PPeople.Add(person);
                        _dbContext.PPersonIds.Add(pid);

                    }
                    if (fbbcs.Count() == 0)
                    {
                        isNewF=true;
                        _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, fbbc does not exist. It will be created.");
                        //todo: validate entries; check codes
                        //todo: getfromSomewhere
                        
                         f = new Fbbc();
                        f.Id = BaseEntity.GenerateNewId();
                        f.Surname = graoPerson.Surname;
                        f.EcrisConvId = msg.EcrisMsgConvictionId;
                        f.Familyname = graoPerson.Familyname;
                        f.Firstname = graoPerson.Firstname;
                        f.BirthDate = msg.BirthDate;
                        f.BirthDatePrec = "YMD";
                        f.BirthPlace = msg.BirthCity;
                        f.BirtyCountryDescr = msg.BirthCountry;
                        f.DocTypeId = msg.MsgTypeId;
                        f.Egn = msg.Pin;
                        f.PersonId = pidId;
                        f.ReceiveDate = msg.MsgTimestamp;
                        //toso: какво е това?
                        f.EcrisUpdConvTypeId = "1";
                        f.StatusCode = "Active";

                        var content = msg.DDocuments.Where(dd => dd.DocContent.MimeType == "application/xml").Select(d => d.DocContent.Content).FirstOrDefault();
                        NotificationMessageType notification = XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(content)) as NotificationMessageType;

                        f.ConvDecisionDate = ServiceHelper.GetDateTime(notification.NotificationMessageConviction.ConvictionDecisionDate);
                        f.ConvDecFinalDate = ServiceHelper.GetDateTime(notification.NotificationMessageConviction.ConvictionDecisionFinalDate);
                        bool isFordelete = false;
                        foreach (var d in notification.NotificationMessageConviction.ConvictionDecision)
                        {
                            if (d.DecisionDeleteConvictionFromRegister?.Value?.ToLower() == "yes")
                            {
                                isFordelete = true;

                            }
                        }
                        if (isFordelete)
                        {
                            f.StatusCode = "ForDelete";
                            _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, fbbc {f.Id} will be marked as deleted.");

                        }
                        else
                        {
                            f.StatusCode = "Active";
                        }
                        msg.FbbcId = f.Id;
                        foreach (var d in msg.DDocuments)
                        {
                            stateD.Add(d);
                            d.FbbcId = f.Id;
                            _dbContext.DDocuments.Update(d);
                        }
                        _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, number of documents which will be updated: {msg.DDocuments.Count()} .");
                        _dbContext.Fbbcs.Add(f);
                        _dbContext.EEcrisMessages.Update(msg);
                        _logger.LogTrace($"EcrisMessageID: {msg.Id} updated.");
                    }
                    else
                    {
                         f = fbbcs.First();
                         stateF = f;
                        _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, linked fbbc: {f.Id} ");
                        var content = msg.DDocuments.Where(dd => dd.DocContent.MimeType == "application/xml").Select(d => d.DocContent.Content).FirstOrDefault();
                        NotificationMessageType notification = XmlUtils.DeserializeXml<AbstractMessageType>(Encoding.UTF8.GetString(content)) as NotificationMessageType;
                        bool isFordelete = false;
                        foreach (var d in notification.NotificationMessageConviction.ConvictionDecision)
                        {
                            if (d.DecisionDeleteConvictionFromRegister?.Value?.ToLower() == "yes")
                            {
                                isFordelete = true;

                            }
                        }
                        if (isFordelete)
                        {
                            f.StatusCode = "ForDelete";
                            _dbContext.Fbbcs.Update(f);
                            _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, fbbc {f.Id} will be marked as deleted.");
                        }

                        msg.FbbcId = f.Id;

                        foreach (var d in msg.DDocuments)
                        {
                            stateD.Add(d);
                            d.FbbcId = f.Id;
                            _dbContext.DDocuments.Update(d);
                        }
                        _logger.LogTrace($"EcrisMessageID: {msg.Id}, person identified: {graoPerson.Egn}, number of documents which will be updated: {msg.DDocuments.Count()} .");
                        _dbContext.EEcrisMessages.Update(msg);
                        _logger.LogTrace($"EcrisMessageID: {msg.Id} updated.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message, ex.Data);
                    var entry = _dbContext.Entry(msg);
                    entry.State = EntityState.Unchanged;
                    if (isNewF)
                    {
                        var fEntry = _dbContext.Entry(f);
                        fEntry.State = EntityState.Unchanged;
                    }
                    else
                    {
                        f = stateF;
                        _dbContext.Fbbcs.Update(f);
                    }
                    _dbContext.DDocuments.UpdateRange(stateD);
                    NLog.LogManager.Flush();
                }

                            }
            _logger.LogTrace($"Pre - save changes.");
            await _dbContext.SaveChangesAsync();
            _logger.LogTrace($"Save changes to DB.");

            _logger.LogInformation($"ProcessIdentifiedNotificationsAsync ended.");
            NLog.LogManager.Flush();
        }


    }
}
