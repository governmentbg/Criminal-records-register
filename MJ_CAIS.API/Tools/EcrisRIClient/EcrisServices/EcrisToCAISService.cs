using MJ_CAIS.DataAccess;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess.Entities;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using NLog;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using EcrisRIClient;
using MJ_CAIS.DTO.EcrisService;
using MJ_CAIS.EcrisObjectsServices;
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

        public async Task SynchRequests(string username, string password, string searchFolderName, string itemsPerPage,  string endpointAuth, string endpointStorage, string endPointAddressSearch, bool skipDataExtraction = false, string joinSeparator = " ", string paramRequestSynch = PARAM_REQUEST_NAME)
        {
            _logger.LogInformation($"Synchronization of requests started.Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramRequestSynch: {paramRequestSynch}.");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.REQ, skipDataExtraction, paramRequestSynch, endpointAuth, endpointStorage, endPointAddressSearch, joinSeparator);
            _logger.LogInformation("Synchronization of requests ended.");
        }
        public async Task SynchNotifications(string username, string password, string searchFolderName, string itemsPerPage, string endpointAuth, string endpointStorage, string endPointAddressSearch, bool skipDataExtraction = false, string joinSeparator = " ", string paramNotificationSynch = PARAM_NOTIFICATION_NAME)
        {
            _logger.LogInformation($"Synchronization of notifications started. Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramNotificationSynch: {paramNotificationSynch}.)");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.NOT, skipDataExtraction, paramNotificationSynch,   endpointAuth,  endpointStorage,  endPointAddressSearch, joinSeparator);
            _logger.LogInformation("Synchronization of notifications ended.");
        }

        private async Task BaseSync(string username, string password, string searchFolderName, string itemsPerPage, EcrisMessageTypeOrAliasMessageType messageType, bool skipDataExtraction, string paramNameForSynch,string  endpointAuth, string endpointStorage, string endPointAddressSearch,string joinSeparator = " ")
        {
            bool isLoggedIn = false;
            string sessionID = "";
            EcrisClient client = null;
            try
            {
                client = new EcrisClient(username, password, endpointAuth, endpointStorage, endPointAddressSearch);
                _logger.LogTrace($"{messageType.ToString()}: EcrisClient created.");
                sessionID = await client.GetActiveSessionId();
                isLoggedIn = true;
                _logger.LogTrace($"{messageType.ToString()}: EcrisClient logged in.");
                var inboxFolderId = await client.GetInboxFolderIdentifier(sessionID, searchFolderName);
                _logger.LogTrace($"{messageType.ToString()}: Folder {searchFolderName} identified as {inboxFolderId}.");
                MJ_CAIS.DTO.EcrisService.QueryType query;
                DateTime lastUpdatedTime;
                string docTypeCode = "";
                if (messageType == MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.REQ)
                {
                    lastUpdatedTime = GetLastSynchDateForRequests(paramNameForSynch);
                    docTypeCode = await ServiceHelper.GetDocTypeCodeAsync(MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.REQ, _dbContext);
                    query = GetRequestsQuery(inboxFolderId, lastUpdatedTime);

                }
                else
                {
                    if (messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                    {
                        docTypeCode = await ServiceHelper.GetDocTypeCodeAsync(MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.NOT, _dbContext);
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
                            await AddMessageToDBContext(r, client, sessionID, skipDataExtraction, joinSeparator, docTypeCode);

                        }

                    }

                    if (filteredMessages.Count > 0)
                    {
                        var newDate = filteredMessages.Select(r => !r.MessageVersionTimestampSpecified ? DateTime.MinValue : r.MessageVersionTimestamp)?.Max(d => d);
                        lastUpdatedTime = newDate.HasValue && newDate.Value != DateTime.MinValue ? newDate.Value : lastUpdatedTime;


                        if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.REQ)
                        {

                            SetLastSynchDateForRequests(lastUpdatedTime, paramNameForSynch);

                        };
                        if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                        {
                            SetLastSynchDateForNotifications(lastUpdatedTime, paramNameForSynch);

                        };
                    }
                    _logger.LogTrace($"{messageType.ToString()}: Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}.");
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} pre save.");
                    await _dbContext.SaveChangesAsync();
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} post save.");
                }


                while (pageNumber * pageSize < totalNumberOfMessages);

                _logger.LogTrace($"{messageType.ToString()}:Synchronization ended. Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}. ");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{messageType.ToString()}: {ex.Message}", ex.Data);
                //todo: има ли нещо за почистване?!

                //throw ex;
            }
            finally
            {
                if (isLoggedIn)
                {
                    if (client != null)
                    {
                        var logout = await client.Logout(sessionID);
                    }

                    _logger.LogTrace($"{messageType.ToString()}: Log out");
                }
                NLog.LogManager.Flush();


            }

        }



        private MJ_CAIS.DTO.EcrisService.QueryType GetRequestsQuery(string searchFolderID, DateTime fromDate)
        {

            var query = GetBaseQuery(searchFolderID, fromDate);
            query.QueryParameters.MessageTypeQueryParameter = new MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAlias[]
            {
                new MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAlias(){
              Item = EcrisMessageTypeOrAliasMessageType.REQ
                }

            };

            return query;

        }
        private MJ_CAIS.DTO.EcrisService.QueryType GetNotificationsQuery(string searchFolderID, DateTime fromDate)
        {

            //EcrisMessageTypeOrAliasMessageType.REQ
            var query = GetBaseQuery(searchFolderID, fromDate);
            query.QueryParameters.MessageTypeQueryParameter = new MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAlias[]
            {
                new MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAlias(){
              Item = EcrisMessageTypeOrAliasMessageType.NOT
                }

            };

            return query;

        }
        private MJ_CAIS.DTO.EcrisService.QueryType GetBaseQuery(string searchFolderID, DateTime fromDate)
        {
            MJ_CAIS.DTO.EcrisService.QueryType query = new MJ_CAIS.DTO.EcrisService.QueryType();
            query.QueryParameters = new MJ_CAIS.DTO.EcrisService.QueryTypeQueryParameters();
            query.QueryParameters.FolderQueryParameter = new string[] { searchFolderID };

            var period = new MJ_CAIS.DTO.EcrisService.StrictDateRange();
            period.FromDate = new MJ_CAIS.DTO.EcrisService.StrictDateType()
            {

                Value = fromDate

            };
            period.ToDate = new MJ_CAIS.DTO.EcrisService.StrictDateType() { Value = DateTime.Now };

            query.QueryParameters.MessageDateQueryParameter = new MJ_CAIS.DTO.EcrisService.MessageDateQueryParameterType()
            {
                DateType = MJ_CAIS.DTO.EcrisService.MessageDateTypeEnumeration.LastUpdated,
                DateValue = new MJ_CAIS.DTO.EcrisService.DateStrictRangeQueryParameter()
                {
                    DateRangeParameter = period,
                    DateParameterType = MJ_CAIS.DTO.EcrisService.AbstractDateQueryParameterDateParameterType.Range


                }

            };

            return query;

        }
        private async Task AddMessageToDBContext(MJ_CAIS.DTO.EcrisService.MessageShortViewType msg, EcrisClient client, string sessionId, bool skipDataExtraction = false, string joinSeparator = " ", string? docTypeCode = null)
        {//functionalErrorReferenceIdentifier - да се интерпретира ли някак и как?!
            if (!msg.MessageTypeSpecified)
            {
                throw new Exception(String.Format($"Unknown type - MessageEcrisID  = {0}", msg.MessageEcrisIdentifier));
            }
            var identifier = msg.MessageIdentifier;
            var messageContent = await client.ReadMessage(sessionId, identifier);
            EEcrisInbox inbox = new EEcrisInbox();
            inbox.Id = BaseEntity.GenerateNewId();
            //todo: get from enum
            inbox.Status = ECRISConstants.EcrisInboxStatuses.Pending;
            inbox.XmlMessageTraits = msg.SerializedXMLFromService;// XmlUtils.SerializeToXml(msg);
            //var outputData = (ReadMessageWSOutputDataType)messageContent.WSData;
            inbox.XmlMessage = messageContent.SerializedXMLFromService;//XmlUtils.SerializeToXml(outputData);

            inbox.ImportedOn = DateTime.UtcNow;


            //ако съществува такъв запис, не добавяме
            if (!skipDataExtraction)
            {
                try
                {

                    var existingMSg = _dbContext.EEcrisMessages.FirstOrDefault(ee => ee.EcrisIdentifier == msg.MessageEcrisIdentifier);
                    if (existingMSg == null)
                    {
                        //todo: use methods from CommonService

                        var m = ParseMessageTraits(msg, joinSeparator);
                        m.MsgTypeId = docTypeCode;                  

                        inbox.EcrisMsg = m;
                        inbox.Status = ECRISConstants.EcrisInboxStatuses.Processed;

                        if (msg.MessageType == MJ_CAIS.DTO.EcrisService.EcrisMessageType.NOT)
                        {
                            var req = ((NotificationMessageType)((ReadMessageWSOutputDataType)messageContent).EcrisRiMessage);
                            m.EcrisMsgConvictionId = req.NotificationMessageConviction.ConvictionID;
                            if (req.RequestMessageUrgency?.Value.ToLower() == "yes")
                            {
                                m.Urgent = true;
                            };
                            if (req.RequestMessageUrgency?.Value.ToLower() == "no")
                            {
                                m.Urgent = false;
                            };

                            m.EcrisMsgConvictionId = req.NotificationMessageConviction.ConvictionID;

                        }
                        if (msg.MessageType == MJ_CAIS.DTO.EcrisService.EcrisMessageType.REQ)
                        {
                            var req = ((RequestMessageType)((ReadMessageWSOutputDataType)messageContent).EcrisRiMessage);

                            if (req.RequestMessageUrgency?.Value.ToLower() == "yes")
                            {
                                m.Urgent = true;
                            };
                            if (req.RequestMessageUrgency?.Value.ToLower() == "no")
                            {
                                m.Urgent = false;
                            };

                        }
                        var names = m.EEcrisMsgNames.FirstOrDefault(n => n.LangCode == "bg");
                        if (names == null)
                        {
                            names = m.EEcrisMsgNames.FirstOrDefault();
                        }

                        DDocument d = ServiceHelper.GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, names?.Firstname, names?.Surname, names?.Familyname, _dbContext);

                        d.EcrisMsg = m;
                        m.DDocuments.Add(d);

                        DDocContent content = ServiceHelper.GetDDocContent(XmlUtils.SerializeToXml(((ReadMessageWSOutputDataType)messageContent).EcrisRiMessage));

                        d.DocContent = content;
                        content.DDocuments.Add(d);

                        _dbContext.EEcrisMessages.Add(m);
                     
                        _dbContext.DDocuments.Add(d);
                        _dbContext.DDocContents.Add(content);
                        if (m.EEcrisMsgNationalities?.Count > 0)
                        {
                            _dbContext.EEcrisMsgNationalities.AddRange(m.EEcrisMsgNationalities);
                        }
                        if (m.EEcrisMsgNames.Count > 0)
                        {
                            _dbContext.EEcrisMsgNames.AddRange(m.EEcrisMsgNames);
                        }



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

  

        private EEcrisMessage ParseMessageTraits(MJ_CAIS.DTO.EcrisService.MessageShortViewType msg, string joinSeparator)
        {
            EEcrisMessage m = new EEcrisMessage();

            m.Id = BaseEntity.GenerateNewId();
            m.EcrisIdentifier = msg.MessageEcrisIdentifier;
            m.Identifier = msg.MessageIdentifier;//или да се пази в m.RequestMsgId???

            //има данни в msg.MessageShortViewPerson.PersonAlias - те интересуват ли ни?!
            ////има данни и за майка и баща - биха били полезни за идентификацията
            m.BirthCity = string.Join(joinSeparator, msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownName.Select(p => p.Value)); //msg.MessageShortViewPerson.PersonBirthPlace.PlaceTownReference???
            m.BirthDate = new DateTime(Int32.Parse(XmlUtils.GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateYear)),
                                       Int32.Parse(XmlUtils.GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateMonthDay.DateMonth)),
                                       Int32.Parse(XmlUtils.GetNumbersFromString(msg.MessageShortViewPerson.PersonBirthDate.DateMonthDay.DateDay)));
            m.BirthCountry = msg.MessageShortViewPerson.PersonBirthPlace.PlaceCountryReference.Value;
            m.Sex = msg.MessageShortViewPerson.PersonSex;

            var forenames = msg.MessageShortViewPerson.PersonName?.Forename?.ToList();
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

            var familyNames = msg.MessageShortViewPerson.PersonName?.SecondSurname?.ToList();
            if (familyNames!=null)
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

            var surnameNames = msg.MessageShortViewPerson.PersonName?.Surname?.ToList();
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


            m.Deadline = msg.MessageDeadline?.Value;
            m.Pin = msg.MessageShortViewPerson?.PersonIdentityNumber?.Value;

            m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForIdentification;

            return m;
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