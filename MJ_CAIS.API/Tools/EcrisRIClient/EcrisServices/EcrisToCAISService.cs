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
using MJ_CAIS.ExternalWebServices;
using MJ_CAIS.ExternalWebServices.Schemas.PersonValidator;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

namespace EcrisIntegrationServices
{
    public class EcrisToCAISService
    {
        const string PARAM_REQUEST_NAME = "ECRIS_REQUEST_LAST_SYNCH_DATE";
        const string PARAM_NOTIFICATION_NAME = "ECRIS_NOTIFICATION_LAST_SYNCH_DATE";
        private CaisDbContext _dbContext;
        private readonly ILogger<EcrisToCAISService> _logger;
        private readonly PersonValidatorClient _personValidatorClient;

        public EcrisToCAISService(CaisDbContext dbContext, ILogger<EcrisToCAISService> logger, PersonValidatorClient personValidatorClient)
        {
            _dbContext = dbContext;
            _logger = logger;
            _personValidatorClient = personValidatorClient;
        }

        public async Task SynchRequests(string username, string password, string searchFolderName, string itemsPerPage, string endpointAuth, string endpointStorage, string endPointAddressSearch, bool includeSubfolders, bool skipDataExtraction = false, string joinSeparator = " ", string paramRequestSynch = PARAM_REQUEST_NAME)
        {
            _logger.LogInformation($"Synchronization of requests started.Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramRequestSynch: {paramRequestSynch}.");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.REQ, skipDataExtraction, paramRequestSynch, endpointAuth, endpointStorage, endPointAddressSearch, includeSubfolders, joinSeparator);
            _logger.LogInformation("Synchronization of requests ended.");
        }
        public async Task SynchNotifications(string username, string password, string searchFolderName, string itemsPerPage, string endpointAuth, string endpointStorage, string endPointAddressSearch, bool includeSubfolders, bool skipDataExtraction = false, string joinSeparator = " ", string paramNotificationSynch = PARAM_NOTIFICATION_NAME)
        {
            _logger.LogInformation($"Synchronization of notifications started. Username: {username}; Folder: {searchFolderName}; Page size: {itemsPerPage}; skipDataExtraction: {skipDataExtraction}; joinSeparator: {joinSeparator}; paramNotificationSynch: {paramNotificationSynch}.)");
            await BaseSync(username, password, searchFolderName, itemsPerPage, EcrisMessageTypeOrAliasMessageType.NOT, skipDataExtraction, paramNotificationSynch, endpointAuth, endpointStorage, endPointAddressSearch, includeSubfolders, joinSeparator);
            _logger.LogInformation("Synchronization of notifications ended.");
        }

        private async Task BaseSync(string username, string password, string searchFolderName, string itemsPerPage, EcrisMessageTypeOrAliasMessageType messageType, bool skipDataExtraction, string paramNameForSynch, string endpointAuth, string endpointStorage, string endPointAddressSearch, bool includeSubfolders, string joinSeparator = " ")
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
                var inboxFolderId = await client.GetInboxFolderIdentifier(sessionID, searchFolderName, includeSubfolders);
                inboxFolderId = inboxFolderId.Where(p => !string.IsNullOrEmpty(p.Trim())).ToList();
                if (inboxFolderId.Count == 0)
                {
                    throw new Exception($"Folder {searchFolderName} does not exist.");
                }

                _logger.LogTrace($"{messageType.ToString()}: Folder {searchFolderName} identified as {string.Join(", ", inboxFolderId)}.");
                MJ_CAIS.DTO.EcrisService.QueryType query;
                DateTime lastUpdatedTime;
                string docTypeCode = "";
                if (messageType == MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.REQ)
                {
                    lastUpdatedTime = await GetLastSynchDateForRequests(paramNameForSynch);
                    docTypeCode = (await ServiceHelper.GetDocTypeCodeAsync(MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.REQ, _dbContext)).FirstOrDefault(x=>!x.EndsWith("Old"));
                    query = GetRequestsQuery(inboxFolderId, lastUpdatedTime);

                }
                else
                {
                    if (messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                    {
                        docTypeCode = (await ServiceHelper.GetDocTypeCodeAsync(MJ_CAIS.DTO.EcrisService.EcrisMessageTypeOrAliasMessageType.NOT, _dbContext)).FirstOrDefault(x=>!x.EndsWith("Old"));
                        lastUpdatedTime =await GetLastSynchDateForNotifications(paramNameForSynch);
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
                    //ползваме този код ако искаме да извлечем конкретно съобщение
                    //var id = "RI-RRS-000000001382773";
                    //var messageContent = await client.ReadMessage(sessionID, id);
                    //var p = XmlUtils.SerializeToXml(((ReadMessageWSOutputDataType)messageContent).EcrisRiMessage);

                    var result = await client.ExecutePreparedQuery(sessionID, query, pageNumber, itemsPerPage);
                    totalNumberOfMessages = result.NumberOfResults;
                    _logger.LogTrace($"{messageType.ToString()}: Query executed. {result.MessageShortViewList.Length} rows returned (out of {totalNumberOfMessages}), page {pageNumber}.");
                    pageNumber++;
                    //заявката чете само дата, затова филтрираме само записите, за които последната промяна е след дата, която търсим
                    var filteredMessages = result.MessageShortViewList.Where(m => m.MessageSenderTimestampSpecified == true && m.MessageSenderTimestamp > lastUpdatedTime).ToList();
                    foreach (var r in filteredMessages)
                    {

                        if (r != null)
                        {
                            await AddMessageToDBContext(r, client, sessionID, skipDataExtraction, joinSeparator, docTypeCode);

                        }

                    }

                    if (filteredMessages.Count > 0)
                    {
                        var newDate = filteredMessages.Where(r => r.MessageSenderTimestampSpecified).Select(r => r.MessageSenderTimestamp)?.Max(d => d);
                        lastUpdatedTime = newDate.HasValue && newDate.Value != DateTime.MinValue ? newDate.Value : lastUpdatedTime;


                        if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.REQ)
                        {

                           await  SetLastSynchDateForRequests(lastUpdatedTime, paramNameForSynch);

                        };
                        if (newDate != DateTime.MinValue && messageType == EcrisMessageTypeOrAliasMessageType.NOT)
                        {
                         await   SetLastSynchDateForNotifications(lastUpdatedTime, paramNameForSynch);

                        };
                    }
                    _logger.LogTrace($"{messageType.ToString()}: Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}.");
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} pre save.");
                    await _dbContext.SaveChangesAsync();
                    _dbContext.ChangeTracker.Clear();
                    _logger.LogTrace($"{messageType.ToString()}: Page {pageNumber} post save.");
                }


                while (pageNumber * pageSize < totalNumberOfMessages);

                _logger.LogTrace($"{messageType.ToString()}:Synchronization ended. Parameter {paramNameForSynch} set to {lastUpdatedTime.ToString("yyyy-MM-dd HH:mm:ss")}. ");


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{messageType.ToString()}: {ex.Message}", ex.Data);
                _dbContext.ChangeTracker.Clear();
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



        private MJ_CAIS.DTO.EcrisService.QueryType GetRequestsQuery(List<string> searchFolderID, DateTime fromDate)
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
        private MJ_CAIS.DTO.EcrisService.QueryType GetNotificationsQuery(List<string> searchFolderID, DateTime fromDate)
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
        private MJ_CAIS.DTO.EcrisService.QueryType GetBaseQuery(List<string> searchFolderID, DateTime fromDate)
        {
            MJ_CAIS.DTO.EcrisService.QueryType query = new MJ_CAIS.DTO.EcrisService.QueryType();
            query.QueryParameters = new MJ_CAIS.DTO.EcrisService.QueryTypeQueryParameters();
            query.QueryParameters.FolderQueryParameter = searchFolderID.Where(p => !string.IsNullOrEmpty(p)).ToArray();


            var period = new MJ_CAIS.DTO.EcrisService.StrictDateRange();
            period.FromDate = new MJ_CAIS.DTO.EcrisService.StrictDateType()
            {

                Value = fromDate

            };
            period.ToDate = new MJ_CAIS.DTO.EcrisService.StrictDateType() { Value = DateTime.Now };

            query.QueryParameters.MessageDateQueryParameter = new MJ_CAIS.DTO.EcrisService.MessageDateQueryParameterType()
            {
                DateType = MJ_CAIS.DTO.EcrisService.MessageDateTypeEnumeration.SentReceived,                 
                DateValue = new MJ_CAIS.DTO.EcrisService.DateStrictRangeQueryParameter()
                {
                    DateRangeParameter = period,
                    DateParameterType = MJ_CAIS.DTO.EcrisService.AbstractDateQueryParameterDateParameterType.Range


                }

            };

            query.QueryParameters.MemberStateQueryParameter = new QueryTypeQueryParametersMemberStateQueryParameter();
            query.QueryParameters.MemberStateQueryParameter.DestinationSpecified = true;
            query.QueryParameters.MemberStateQueryParameter.Destination = QueryTypeQueryParametersMemberStateQueryParameterDestination.SENT;
            query.QueryParameters.MemberStateQueryParameter.MemberStateCodes = new MemberStateCodeType[1];
            query.QueryParameters.MemberStateQueryParameter.MemberStateCodes[0] = MemberStateCodeType.BG;


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

            //var messageContent = await client.ReadMessage(sessionId, identifier);
            EEcrisInbox inbox = new EEcrisInbox();
            inbox.Id = BaseEntity.GenerateNewId();
            
            inbox.Status = ECRISConstants.EcrisInboxStatuses.Pending;
            inbox.XmlMessageTraits = msg.SerializedXMLFromService;// XmlUtils.SerializeToXml(msg);
            //var outputData = (ReadMessageWSOutputDataType)messageContent.WSData;
            inbox.XmlMessage = messageContent.SerializedXMLFromService;//XmlUtils.SerializeToXml(outputData);

            inbox.ImportedOn = DateTime.Now;
            inbox.Error = null;
            inbox.StackTrace = null;
            inbox.HasError = false;


            //ако съществува такъв запис, не добавяме
            if (!skipDataExtraction)
            {
                try
                {

                    var existingMSg = await _dbContext.EEcrisMessages.AsNoTracking().FirstOrDefaultAsync(ee => ee.EcrisIdentifier == msg.MessageEcrisIdentifier);
                    if (existingMSg == null)
                    {
                        //todo: use methods from CommonService

                        var m = await ParseMessageTraits(msg, joinSeparator);
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

                        DDocument d = await ServiceHelper.GetDDocument(msg.MessageType, msg.MessageEcrisIdentifier, names?.Firstname, names?.Surname, names?.Familyname, _dbContext);

                        d.EcrisMsg = m;
                        m.DDocuments.Add(d);

                        DDocContent content = ServiceHelper.GetDDocContent(XmlUtils.SerializeToXml(((ReadMessageWSOutputDataType)messageContent).EcrisRiMessage));

                        d.DocContent = content;
                        content.DDocuments.Add(d);
                        m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.ForIdentification;
                        //опит за идентификация
                        if (!string.IsNullOrEmpty(m.Pin))
                        {
                            var graoPersonId = (await _dbContext.GraoPeople.AsNoTracking().FirstOrDefaultAsync(p => p.Egn == m.Pin))?.Id;
                            if (!string.IsNullOrEmpty(graoPersonId))
                            {
                                m.EEcrisIdentifications = new List<EEcrisIdentification>(){ new EEcrisIdentification()
                                       {
                                           Id = BaseEntity.GenerateNewId(),
                                           EcrisMsgId = m.Id,
                                           EcrisMsg = m,
                                           GraoPersonId = graoPersonId,
                                           Approved = 1
                                           
                                       } };
                                m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Identified;
                            }
                        }
                            
                        if ( m.EcrisMsgStatus != ECRISConstants.EcrisMessageStatuses.Identified
                            &&m.Sex != 0 && m.Sex != null
                            && !string.IsNullOrEmpty(m.EEcrisMsgNames.FirstOrDefault()?.Firstname)
                            && m.BirthDate.HasValue 
                            )
                        {
                            //при тези условия има смисъл да включваме идентификацията, иначе остава за ръчна обработка

                            try
                            {
                                var gender = m.Sex == 1 ? PersonInfoGenderType.male : PersonInfoGenderType.female ;
                                var graoPersons = await _personValidatorClient.GetPersonInfo(m.EEcrisMsgNames.FirstOrDefault().Firstname,
                                    m.EEcrisMsgNames.FirstOrDefault()?.Surname, m.EEcrisMsgNames.FirstOrDefault()?.Familyname, gender, m.BirthDate.Value, PersonIdentificationConstants.Tresholds.PartialMatch);


                                if (graoPersons.Count > 0)
                                {
                                    var egns = graoPersons.Select(p => p.person.personalNumber).ToList();
                                    m.EEcrisIdentifications = await _dbContext.GraoPeople.AsNoTracking().Where(p => egns.Contains(p.Egn))
                                        .Select(x => new EEcrisIdentification
                                        {
                                            Id = BaseEntity.GenerateNewId(),
                                            EcrisMsgId = m.Id,
                                            EcrisMsg = m,
                                            GraoPersonId = x.Id
                                        })
                                        .ToListAsync();
                                    if (m.EEcrisIdentifications.Count == 1)
                                    {
                                        m.EcrisMsgStatus = ECRISConstants.EcrisMessageStatuses.Identified;
                                        m.EEcrisIdentifications.First().Approved = 1;
                                    }
                                    _dbContext.EEcrisIdentifications.AddRange(m.EEcrisIdentifications);
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError("Person identification failed: " + ex.Message, ex.Data, ex);
                            }


                        }
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
                    _logger.LogError($"{identifier} : {ex.Message}", ex.Data, ex);
                    inbox.Status = ECRISConstants.EcrisInboxStatuses.Error;
                    inbox.Error = $"{identifier} : {ex.Message}";
                    inbox.StackTrace = ex.StackTrace;
                    inbox.HasError = true;
                }


            }
            _dbContext.EEcrisInboxes.Add(inbox);

        }



        private async Task<EEcrisMessage> ParseMessageTraits(MJ_CAIS.DTO.EcrisService.MessageShortViewType msg, string joinSeparator)
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

            m.Sex = msg.MessageShortViewPerson.PersonSex;// == 0 ? 1 : 2;

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


            var countriesAuthorities = await _dbContext.EEcrisAuthorities.AsNoTracking().Where(ea => ea.ValidFrom <= DateTime.Now && ea.ValidTo >= DateTime.Now
            && ea.MemberStateCode != null && (ea.MemberStateCode.ToLower() == msg.MessageSendingMemberState.ToString().ToLower()
            || msg.MessageReceivingMemberState.Select(p => p.ToString().ToLower()).Contains(ea.MemberStateCode.ToLower()))).ToListAsync();
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
                var countries = await _dbContext.GCountries.AsNoTracking().Where(c => c.EcrisTechnId != null && nationalities.Contains(c.EcrisTechnId)
                                                               && c.ValidFrom <= DateTime.Now && c.ValidTo >= DateTime.Now).ToListAsync();
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


        private async Task< DateTime> GetLastSynchDateForNotifications(string paramNameForSynch)
        {
            var parameter = await _dbContext.ESynchronizationParameters.AsNoTracking().FirstOrDefaultAsync(p => p.Name == paramNameForSynch);
            if (parameter?.LastDate == null)
            {
                throw new Exception($"{paramNameForSynch} is not set.");
            }
            return parameter.LastDate.Value;
        }
        private async Task<DateTime> GetLastSynchDateForRequests(string paramNameForSynch)
        {
            var parameter = await _dbContext.ESynchronizationParameters.AsNoTracking().FirstOrDefaultAsync(p => p.Name == paramNameForSynch);
            if (parameter?.LastDate == null)
            {
                throw new Exception($"{paramNameForSynch} is not set.");
            }
            return parameter.LastDate.Value;

        }
        private async Task SetLastSynchDateForNotifications(DateTime lastUpdate, string paramNameForSynch)
        {
            var parameter = await _dbContext.ESynchronizationParameters.AsNoTracking().FirstOrDefaultAsync(p => p.Name == paramNameForSynch);
            if (parameter == null)
            {

                throw new Exception($"{paramNameForSynch} does not exist.");
            }
            parameter.LastDate = lastUpdate;
        }
        private async Task SetLastSynchDateForRequests(DateTime lastUpdate, string paramNameForSynch)
        {
            var parameter = await _dbContext.ESynchronizationParameters.AsNoTracking().FirstOrDefaultAsync(p => p.Name == paramNameForSynch);
            if (parameter == null)
            {
                throw new Exception($"{paramNameForSynch} does not exist.");
            }
            parameter.LastDate = lastUpdate;

        }
    }
}