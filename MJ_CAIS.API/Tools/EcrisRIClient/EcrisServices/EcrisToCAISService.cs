using MJ_CAIS.DataAccess;
using EcrisRIClient;
using EcrisRIClient.EcrisService;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess.Entities;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
//using Microsoft.Extensions.Logging;

namespace EcrisIntegrationServices
{
    public class EcrisToCAISService
    {
        const string PARAM_REQUEST_NAME = "ECRIS_REQUEST_LAST_SYNCH_DATE";
        const string PARAM_NOTIFICATION_NAME = "ECRIS_NOTIFICATION_LAST_SYNCH_DATE";
        private CaisDbContext _dbContext;
        private readonly ILogger<EcrisToCAISService> _logger;

        public EcrisToCAISService(CaisDbContext dbContext, ILogger<EcrisToCAISService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public void SetDbContext(CaisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SynchRequests(string username, string password, string searchFolderName, string itemsPerPage, bool skipDataExtraction = false, string joinSeparator = " ", string paramRequestSynch = PARAM_REQUEST_NAME)
        {
            _logger.LogInformation($"Synchronization of requests started.Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramRequestSynch: {paramRequestSynch}.");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.REQ, skipDataExtraction, paramRequestSynch, joinSeparator);
            _logger.LogInformation("Synchronization of requests ended.");
        }
        public async Task SynchNotifications(string username, string password, string searchFolderName, string itemsPerPage, bool skipDataExtraction = false, string joinSeparator = " ", string paramNotificationSynch = PARAM_NOTIFICATION_NAME)
        {
            _logger.LogInformation($"Synchronization of notifications started. Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramNotificationSynch: {paramNotificationSynch}.)");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.NOT, skipDataExtraction, paramNotificationSynch, joinSeparator);
            _logger.LogInformation("Synchronization of notifications ended.");
        }

        private async Task BaseSync(string username, string password, string searchFolderName, string itemsPerPage, EcrisMessageTypeOrAliasMessageType messageType, bool skipDataExtraction, string paramNameForSynch, string joinSeparator = " ")
        {
            try
            {
                EcrisClient client = new EcrisClient(username, password);
                _logger.LogTrace($"{messageType.ToString()}: EcrisClient created.");
                var sessionID = await client.GetActiveSessionId();
                _logger.LogTrace($"{messageType.ToString()}: EcrisClient logged in.");
                var inboxFolderId = await client.GetInboxFolderIdentifier(sessionID, searchFolderName);
                _logger.LogTrace($"{messageType.ToString()}: Folder {searchFolderName} identified as {inboxFolderId}.");
                EcrisRIClient.EcrisService.QueryType query;
                DateTime lastUpdatedTime;
                if (messageType == EcrisMessageTypeOrAliasMessageType.REQ)
                {
                    lastUpdatedTime = GetLastSynchDateForRequests(paramNameForSynch);
                    query = GetRequestsQuery(inboxFolderId, lastUpdatedTime);

                }
                else
                {
                    if (messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                    {
                        lastUpdatedTime = GetLastSynchDateForNotifications(paramNameForSynch);
                        query = GetNotificationsQuery(inboxFolderId, lastUpdatedTime);

                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
                _logger.LogTrace($"{messageType.ToString()}: Query created. Last updated date: {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}");

                int pageNumber = 0;
                int pageSize = Int32.Parse(itemsPerPage);

                int totalNumberOfMessages = 0;
                var storageClient = new storagePortv10Client();
                //базата пази с точност до секунди, а от екрис идва дата с по-голяма точност
                //lastUpdatedTime = lastUpdatedTime.AddSeconds(1);
                do
                {

                    var result = await client.ExecutePreparedQuery(sessionID, query, pageNumber, itemsPerPage);
                    totalNumberOfMessages = result.NumberOfResults;
                    _logger.LogTrace($"{messageType.ToString()}: Query executed. {result.MessageShortViewList.Length} rows returned (out of {totalNumberOfMessages}), page {pageNumber}.");
                    pageNumber++;
                    //заявката чете само дата, затова филтрираме само записите, за които последната промяна е след дата, която търсим
                    var filteredMessages = result.MessageShortViewList.Where(m => m.MessageVersionTimestamp > lastUpdatedTime).ToList();
                    foreach (var r in filteredMessages)
                    {
                        
                        if (r != null)
                        {
                            await AddMessageToDBContext(r, storageClient, sessionID, skipDataExtraction, joinSeparator);

                        }

                    }
                  

                    var newDate = filteredMessages.Select(r => !r.MessageVersionTimestampSpecified ? DateTime.MinValue : r.MessageVersionTimestamp).Max(d => d);
                    lastUpdatedTime = newDate!= DateTime.MinValue ? newDate : lastUpdatedTime;


                    if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.REQ)
                    {

                        SetLastSynchDateForRequests(lastUpdatedTime, paramNameForSynch);

                    };
                    if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                    {
                        SetLastSynchDateForNotifications(lastUpdatedTime, paramNameForSynch);

                    };
                    _logger.LogTrace($"{messageType.ToString()}: Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}.");
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} pre save.");
                    await _dbContext.SaveChangesAsync();
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} post save.");
                }


                while (pageNumber * pageSize < totalNumberOfMessages);

                _logger.LogTrace($"{messageType.ToString()}:Synchronization ended. Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}. ");

                var logout = await client.Logout(sessionID);

                _logger.LogTrace($"{messageType.ToString()}: Log out");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{messageType.ToString()}: {ex.Message}", ex.Data);
                throw ex;
            }
            finally
            {
                //todo: logout?
                NLog.LogManager.Flush();


            }

        }



        private QueryType GetRequestsQuery(string searchFolderID, DateTime fromDate)
        {

            var query = GetBaseQuery(searchFolderID, fromDate);
            query.QueryParameters.MessageTypeQueryParameter = new EcrisRIClient.EcrisService.EcrisMessageTypeOrAlias[]
            {
                new EcrisRIClient.EcrisService.EcrisMessageTypeOrAlias(){
              Item = EcrisMessageTypeOrAliasMessageType.REQ
                }

            };


            return query;

        }
        private QueryType GetNotificationsQuery(string searchFolderID, DateTime fromDate)
        {

            //EcrisMessageTypeOrAliasMessageType.REQ
            var query = GetBaseQuery(searchFolderID, fromDate);
            query.QueryParameters.MessageTypeQueryParameter = new EcrisRIClient.EcrisService.EcrisMessageTypeOrAlias[]
            {
                new EcrisRIClient.EcrisService.EcrisMessageTypeOrAlias(){
              Item = EcrisMessageTypeOrAliasMessageType.NOT
                }

            };

            return query;

        }
        private QueryType GetBaseQuery(string searchFolderID, DateTime fromDate)
        {




            EcrisRIClient.EcrisService.QueryType query = new EcrisRIClient.EcrisService.QueryType();
            query.QueryParameters = new EcrisRIClient.EcrisService.QueryTypeQueryParameters();
            query.QueryParameters.FolderQueryParameter = new string[] { searchFolderID };

            var period = new EcrisRIClient.EcrisService.StrictDateRange();
            period.FromDate = new EcrisRIClient.EcrisService.StrictDateType()
            {

                Value = fromDate

            };
            period.ToDate = new EcrisRIClient.EcrisService.StrictDateType() { Value = DateTime.Now };

            query.QueryParameters.MessageDateQueryParameter = new EcrisRIClient.EcrisService.MessageDateQueryParameterType()
            {
                DateType = EcrisRIClient.EcrisService.MessageDateTypeEnumeration.LastUpdated,
                DateValue = new EcrisRIClient.EcrisService.DateStrictRangeQueryParameter()
                {
                    DateRangeParameter = period,
                    DateParameterType = EcrisRIClient.EcrisService.AbstractDateQueryParameterDateParameterType.Range


                }



            };

            return query;

        }
        private async Task AddMessageToDBContext(EcrisRIClient.EcrisService.MessageShortViewType msg, storagePortv10Client client, string sessionId, bool skipDataExtraction = false, string joinSeparator = " ")
        {//functionalErrorReferenceIdentifier - да се интерпретира ли някак и как?!
            if (!msg.MessageTypeSpecified)
            {
                throw new Exception(String.Format($"Unknown type - MessageEcrisID  = {0}", msg.MessageEcrisIdentifier));
            }
            var identifier = msg.MessageIdentifier;
            var messageContent = await ReadMessage(client, sessionId, identifier);
            EEcrisInbox inbox = new EEcrisInbox();
            inbox.Id = BaseEntity.GenerateNewId();
            //todo: get from enum
            inbox.Status = ECRISConstants.EcrisInboxStatuses.Pending;
            inbox.XmlMessageTraits = XmlUtils.SerializeToXml(msg);
            var outputData = (ReadMessageWSOutputDataType)messageContent.WSData;
            inbox.XmlMessage = XmlUtils.SerializeToXml(outputData);

            inbox.ImportedOn = DateTime.UtcNow;


            //ако съществува такъв запис, не добавяме
            if (!skipDataExtraction)
            {
                try
                {

                    var existingMSg = _dbContext.EEcrisMessages.FirstOrDefault(ee => ee.EcrisIdentifier == msg.MessageEcrisIdentifier);
                    if (existingMSg == null)
                    {


                        var m = ParseMessageTraits(msg, joinSeparator);

                        inbox.EcrisMsg = m;
                        inbox.Status = ECRISConstants.EcrisInboxStatuses.Processed;

                        //object req = (msg.MessageType == EcrisMessageType.REQ) ? (RequestMessageType)((ReadMessageWSOutputDataType)messageContent.WSData).EcrisRiMessage : (NotificationMessageType)((ReadMessageWSOutputDataType)messageContent.WSData).EcrisRiMessage;
                        //todo: add specific if needed
                        if(msg.MessageType == EcrisMessageType.NOT)
                        {
                            var req = ((NotificationMessageType)((ReadMessageWSOutputDataType)messageContent.WSData).EcrisRiMessage);
                            m.EcrisMsgConvictionId = req.NotificationMessageConviction.ConvictionID;
                            if(req.RequestMessageUrgency?.Value.ToLower()=="yes")
                            {
                                m.Urgent = true;
                            };
                            if (req.RequestMessageUrgency?.Value.ToLower() =="no")
                            {
                                m.Urgent = false;
                            };

                        }
                        if (msg.MessageType == EcrisMessageType.REQ)
                        {
                            var req = ((RequestMessageType)((ReadMessageWSOutputDataType)messageContent.WSData).EcrisRiMessage);

                            if (req.RequestMessageUrgency?.Value.ToLower() == "yes")
                            {
                                m.Urgent = true;
                            };
                            if (req.RequestMessageUrgency?.Value.ToLower() =="no")
                            {
                                m.Urgent = false;
                            };

                        }


                        DDocument d = GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, m.Firstname, m.Surname, m.Familyname);

                        d.EcrisMsg = m;
                        m.DDocuments.Add(d);

                        DDocContent content = GetDDocContent(XmlUtils.SerializeToXml(((ReadMessageWSOutputDataType)messageContent.WSData).EcrisRiMessage));

                        d.DocContent = content;
                        content.DDocuments.Add(d);

                        _dbContext.EEcrisMessages.Add(m);
                        _dbContext.DDocuments.Add(d);
                        _dbContext.DDocContents.Add(content);



                    }
                    else
                    {
                        inbox.EcrisMsgId = existingMSg.Id;
                        inbox.Status = ECRISConstants.EcrisInboxStatuses.Processed;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex.Data, ex);
                    inbox.Status = ECRISConstants.EcrisInboxStatuses.Error;
                }


            }
            _dbContext.EEcrisInboxes.Add(inbox);

        }
        private DDocument GetDDocument(EcrisMessageType t, string name, string firstName, string surName, string familyName)
        {
            DDocument d = new DDocument();
            d.Id = BaseEntity.GenerateNewId();
            var docType = _dbContext.DDocTypes.FirstOrDefault(dt => dt.Code != null && dt.Code.ToLower() == t.ToString().ToLower());
            d.DocTypeId = docType?.Id;
            d.Name = name;

            //todo: add resources
            if (t == EcrisMessageType.REQ)
            {
                d.Descr = "Запитване за " + firstName + " " + surName + " " + familyName;
            }
            if (t == EcrisMessageType.NOT)
            {
                d.Descr = "Нотификация за " + firstName + " " + surName + " " + familyName;
            }
            return d;
        }
        private DDocContent GetDDocContent(string contentXML)
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

        private string GetNumbersFromString(string inputValue)
        {
            string result = "";

            for (int i = 0; i < inputValue.Length; i++)
            {
                if (Char.IsDigit(inputValue[i]))
                    result += inputValue[i];
            }

            return result;
        }
        private EEcrisMessage ParseMessageTraits(EcrisRIClient.EcrisService.MessageShortViewType msg, string joinSeparator)
        {
            EEcrisMessage m = new EEcrisMessage();

            m.Id = BaseEntity.GenerateNewId();
            m.EcrisIdentifier = msg.MessageEcrisIdentifier;
            m.Identifier = msg.MessageIdentifier;//или да се пази в m.RequestMsgId???

            //има данни в msg.MessageShortViewPerson.PersonAlias - те интересуват ли ни?!
            ////има данни и за майка и баща - биха били полезни за идентификацията
            m.BirthCity = string.Join(joinSeparator, msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownName.Select(p => p.Value)); //msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownReference???
            m.BirthDate = new DateTime(Int32.Parse(GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateYear)),
                                       Int32.Parse(GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateMonthDay.DateMonth)),
                                       Int32.Parse(GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateMonthDay.DateDay)));
            m.BirthCountry = msg.MessageShortViewPerson.PersonBirthPlace.PlaceCountryReference.Value;
            m.Sex = msg.MessageShortViewPerson.PersonSex;
            var familyName = msg.MessageShortViewPerson.PersonName?.SecondSurname?.Select(p => p.Value);
            if (familyName != null)
            {
                m.Familyname = string.Join(joinSeparator, familyName);
            }
            var firstName = msg.MessageShortViewPerson.PersonName?.Forename?.Select(p => p.Value);
            if (firstName != null)
            {
                m.Firstname = string.Join(joinSeparator, firstName);
            }
            var surname = msg.MessageShortViewPerson.PersonName?.Surname?.Select(p => p.Value);
            if (surname != null)
            {
                m.Surname = string.Join(joinSeparator, surname);
            }

            var countriesAuthorities = _dbContext.EEcrisAuthorities.Where(ea => ea.ValidFrom <= DateTime.UtcNow && ea.ValidTo >= DateTime.UtcNow
            && ea.MemberStateCode != null && (ea.MemberStateCode.ToLower() == msg.MessageSendingMemberState.ToString().ToLower()
            || msg.MessageReceivingMemberState.Select(p => p.ToString().ToLower()).Contains(ea.MemberStateCode.ToLower()))).ToList();
            if (msg.MessageSendingMemberStateSpecified)
            {
                m.FromAuthId = countriesAuthorities.FirstOrDefault(c => c.MemberStateCode?.ToLower() == msg.MessageSendingMemberState.ToString().ToLower())?.Id;
            }

            if (msg.MessageReceivingMemberState.Select(c => c.ToString().ToLower()).Contains("bg"))
            {
                m.ToAuthId = countriesAuthorities.FirstOrDefault(c => c.MemberStateCode?.ToLower() == "bg")?.Id;
            }
            else
            {
                m.ToAuthId = countriesAuthorities.Where(c => msg.MessageReceivingMemberState
                .Select(p => p.ToString().ToLower()).Contains(c.MemberStateCode?.ToLower()))
                        .FirstOrDefault()?.Id;
            }

            var nationalities = msg.MessageShortViewPerson.PersonNationalityReference?.Select(p => p.Value)?.ToList();
            if (nationalities != null)
            {
                var countries = _dbContext.GCountries.Where(c => c.EcrisTechnId != null && nationalities.Contains(c.EcrisTechnId)
                                                                && c.ValidFrom <= DateTime.Now && c.ValidTo >= DateTime.Now).ToList();
                if (nationalities.Count > 0)
                {
                    m.Nationality1Code = countries.FirstOrDefault(c => c.EcrisTechnId == msg.MessageShortViewPerson?.PersonNationalityReference[0]?.Value)?.Id;
                }
                if (nationalities.Count > 1)
                {
                    m.Nationality2Code = countries.FirstOrDefault(c => c.EcrisTechnId == msg.MessageShortViewPerson?.PersonNationalityReference[1]?.Value)?.Id;
                }
            }
            if (msg.MessageVersionTimestampSpecified)
            {
                m.MsgTimestamp = msg.MessageVersionTimestamp;
            }

           
            m.Deadline = msg.MessageDeadline?.Value;
            m.Pin = msg.MessageShortViewPerson?.PersonIdentityNumber?.Value;
            
            m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForIdentification;

            return m;
        }
        private async Task<ReadMessageWSOutputType> ReadMessage(storagePortv10Client client, string sessionId, string identifier)
        {
            EcrisRiIdentifierContainingWSInputType request = new EcrisRiIdentifierContainingWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            request.WSData = new EcrisRiIdentifierContainingMessageWSInputDataType()
            {
                MessageIdentifier = identifier //"RI-NOT-000000000000633"
            };
            var resp = await client.readMessageAsync(request);

            // var result = XmlUtils.SerializeToXml(resp.ReadMessageWSOutput);
            return resp.ReadMessageWSOutput;
        }

        private DateTime GetLastSynchDateForNotifications(string paramNameForSynch)
        {
            var parameter = _dbContext.ESynchronizationParameters.FirstOrDefault(p => p.Name == paramNameForSynch);
            if (parameter?.LastDate == null)
            {
                throw new Exception($"{paramNameForSynch} is not set.");
            }
            return parameter.LastDate.Value;
        }
        private DateTime GetLastSynchDateForRequests(string paramNameForSynch)
        {
            var parameter = _dbContext.ESynchronizationParameters.FirstOrDefault(p => p.Name == paramNameForSynch);
            if (parameter?.LastDate == null)
            {
                throw new Exception($"{paramNameForSynch} is not set.");
            }
            return parameter.LastDate.Value;

        }
        private void SetLastSynchDateForNotifications(DateTime lastUpdate, string paramNameForSynch)
        {
            var parameter = _dbContext.ESynchronizationParameters.FirstOrDefault(p => p.Name == paramNameForSynch);
            if (parameter == null)
            {
              
                throw new Exception($"{paramNameForSynch} does not exist.");
            }
            parameter.LastDate = lastUpdate;
        }
        private void SetLastSynchDateForRequests(DateTime lastUpdate, string paramNameForSynch)
        {
            var parameter = _dbContext.ESynchronizationParameters.FirstOrDefault(p => p.Name == paramNameForSynch);
            if (parameter == null)
            {
                throw new Exception($"{paramNameForSynch} does not exist.");
            }
            parameter.LastDate = lastUpdate;

        }
    }
}