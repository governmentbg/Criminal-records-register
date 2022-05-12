using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcrisRIClient.EcrisService;
using AutoMapper;
using EcrisRIClient.Mappers;

namespace EcrisRIClient
{
    public class EcrisClient

    {
        private IMapper _mapper;
        //private readonly List<EcrisMessageType> _msgListForCais = new List<EcrisMessageType> { EcrisMessageType.REQ, EcrisMessageType.NOT };
        private string _username { get; set; }
        private string _password { get; set; }
        private string _endPointAddressAuthentication { get; set; }
        private string _endPointAddressMessageStorage { get; set; } 
        private string _endPointAddressSearch { get; set; }
        //private string _searchFolderName { get; set; }
        //private string _itemsPerPage { get; set; }
        private storagePortv10Client _storageClient;

        public EcrisClient(string username, string password, string  endPointAddressAuthentication, string endPointAddressMessageStorage, string endPointAddressSearch)
        {
            _username = username;
            _password = password;
            //todo: use configuration
            _endPointAddressAuthentication = endPointAddressAuthentication;
            _endPointAddressMessageStorage = endPointAddressMessageStorage;
            _endPointAddressSearch = endPointAddressSearch;
            _storageClient = new storagePortv10Client(storagePortv10Client.EndpointConfiguration.storageServicePort, _endPointAddressMessageStorage);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ServiceToDtos());
            });

            _mapper = mapperConfig.CreateMapper();
           
        }

        public async Task<string> GetActiveSessionId()
        {
            authenticationPortv10Client client = new authenticationPortv10Client(authenticationPortv10Client.EndpointConfiguration.authenticationPort, _endPointAddressAuthentication);
            LoginWSInputType loginRequest = new LoginWSInputType();
            loginRequest.WSMetaData = new LoginWSInputMetaDataType() { MetaDataTimeStamp = DateTime.Now };
            loginRequest.WSData = new LoginWSInputDataType() { LoginUserName = _username, LoginUserPassword = _password };
            var resp = await client.loginAsync(loginRequest);
            return resp.LoginWSOutput.WSMetaData.SessionId;
        }

        public async Task<string> Logout(string sessionID)
        {
            authenticationPortv10Client client = new authenticationPortv10Client(authenticationPortv10Client.EndpointConfiguration.authenticationPort, _endPointAddressAuthentication);
            LogoutWSInputType logoutRequest = new LogoutWSInputType();

            logoutRequest.WSMetaData = new LoginWSInputMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionID };

            var resp = await client.logoutAsync(logoutRequest);
            return ((BaseEcrisRiWSOutputDataType)resp.LogoutWSOutput.WSData).ActionExecutionInformation;
        }

        public async Task<List<string>> GetInboxFolderIdentifier(string sessionId, string searchFolderName, bool includeSubfolders)
        {
            var client = new storagePortv10Client(storagePortv10Client.EndpointConfiguration.storageServicePort, _endPointAddressMessageStorage);
            var request = new BaseEcrisRiWSInputType();


            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            var resp = await client.getFoldersAsync(request);
            var foldersList = ((GetFoldersWSOutputDataType)resp.GetFoldersWSOutput.WSData).FolderList;
            List<string> result = new List<string>();
            for (int i = 0; i < foldersList.Length; i++)
            {
                if (foldersList[i].FolderName == searchFolderName)
                {
                    if (includeSubfolders)
                    {
                        result = GetFolderIdsFromParentFolder(foldersList[i]);
                    }
                    else
                    {
                        result.Add(foldersList[i].FolderIdentifier);
                    }
                    break;
                }
            }

            return result;
        }

        private List<string> GetFolderIdsFromParentFolder(FolderType f)
        {
            List<string> result = new List<string>();
            result.Add(f.FolderIdentifier);
            if (f.FolderContainedFolders != null)
            {
                foreach (var folder in f.FolderContainedFolders)
                {
                    result.AddRange(GetFolderIdsFromParentFolder(folder));
                }
            }
            return result;
        }

        //public async Task<List<MessageShortViewType>> GetMessagesForFolder(string sessionId, string folderId, DateTime lastSuccessDate)
        //{
        //    var client = new storagePortv10Client();
        //    var minDate = DateTime.Now;
        //    var pageNumber = 0;
        //    var resultList = new List<MessageShortViewType>();
        //    do
        //    {
        //        var pageList = await GetMessagesPageForFolder(client, pageNumber, sessionId, folderId, itemsPerPage);
        //        foreach (var msg in pageList)
        //        {
        //            if (msg.MessageVersionTimestamp > lastSuccessDate && _msgListForCais.Contains(msg.MessageType))
        //            {
        //                resultList.Add(msg);
        //            }

        //            if (msg.MessageVersionTimestamp < minDate)
        //            {
        //                minDate = msg.MessageVersionTimestamp;
        //            }
        //        }
        //        pageNumber++;

        //    }
        //    while (minDate > lastSuccessDate);

        //    return resultList;
        //}

        //private async Task<MessageShortViewType[]> GetMessagesPageForFolder(storagePortv10Client client, int pageNumber, string sessionId, string folderId, string itemsPerPage)
        //{
        //    var request = new GetMessagesForFolderWSInputType();
        //    request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
        //    request.WSData = new GetMessagesForFolderWSInputDataType()
        //    {
        //        FolderIdentifier = folderId,
        //        MessagesSortedBy = MessageSortByType.VersionDesc,
        //        MessagesSortedBySpecified = true,
        //        ItemsPerPage = itemsPerPage,
        //        PageNumber = pageNumber,
        //        PageNumberSpecified = true
        //    };

        //    var resp = await client.getMessagesForFolderAsync(request);
        //    var wsData = (GetMessagesForFolderWSOutputDataType)resp.GetMessagesForFolderWSOutput.WSData;
        //    return wsData.MessageShortViewList;
        //}


        public async Task<MJ_CAIS.DTO.EcrisService.SearchWSOutputDataType> ExecutePreparedQuery(string sessionID, MJ_CAIS.DTO.EcrisService.QueryType query, int pageNumber, string itemsPerPage)
        {

            EcrisRIClient.EcrisService.QueryType serviceQuery = _mapper.Map<EcrisRIClient.EcrisService.QueryType>(query);

            var client = new searchPortv10Client(searchPortv10Client.EndpointConfiguration.searchPort, _endPointAddressSearch);
            var request = new SearchWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionID };
            request.WSData = new SearchWSInputDataType()
            {
                SearchQuery = serviceQuery,
                ItemsPerPage = itemsPerPage,
                PageNumber = pageNumber,
                PageNumberSpecified = true,
                MessagesSortedBy = MessageSortByType.ReceivedSendDateAsc,
                MessagesSortedBySpecified = true
            };

            var response = await client.performSearchAsync(request);

            var result = _mapper.Map<EcrisRIClient.EcrisService.SearchWSOutputDataType, MJ_CAIS.DTO.EcrisService.SearchWSOutputDataType>((SearchWSOutputDataType)response.PerformSearchWSOutput.WSData);
            return result;

        }

        public async Task<MJ_CAIS.DTO.EcrisService.ReadMessageWSOutputDataType> ReadMessage(string sessionId, string identifier)
        {
            EcrisRiIdentifierContainingWSInputType request = new EcrisRiIdentifierContainingWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            request.WSData = new EcrisRiIdentifierContainingMessageWSInputDataType()
            {
                MessageIdentifier = identifier //"RI-NOT-000000000000633"
            };
            var resp = await _storageClient.readMessageAsync(request);

            var result = _mapper.Map<EcrisRIClient.EcrisService.ReadMessageWSOutputDataType,
                MJ_CAIS.DTO.EcrisService.ReadMessageWSOutputDataType>((ReadMessageWSOutputDataType)resp.ReadMessageWSOutput.WSData);
            return result;
        }

        public async Task<MJ_CAIS.DTO.EcrisService.AbstractMessageType>  InsertRequestResponse(MJ_CAIS.DTO.EcrisService.RequestResponseMessageType incommingMessage, string sessionID, string folderID)
        {
            var msg = CreateRequestResponseMessage(incommingMessage);
            EcrisRIClient.EcrisService.RequestResponseMessageType msgResult = (EcrisRIClient.EcrisService.RequestResponseMessageType) await InsertMessage(msg, folderID, sessionID);
           
            var result = _mapper.Map<EcrisRIClient.EcrisService.RequestResponseMessageType,
                MJ_CAIS.DTO.EcrisService.RequestResponseMessageType>(msgResult);

            return result;
        }
        public async Task<MJ_CAIS.DTO.EcrisService.AbstractMessageType> InsertNotification(MJ_CAIS.DTO.EcrisService.NotificationMessageType incommingMessage, string sessionID, string folderID)
        {
            var msg = CreateNotificationMessage(incommingMessage);
            EcrisRIClient.EcrisService.NotificationMessageType msgResult = (EcrisRIClient.EcrisService.NotificationMessageType)await InsertMessage(msg, folderID, sessionID);

            var result = _mapper.Map<EcrisRIClient.EcrisService.NotificationMessageType,
                MJ_CAIS.DTO.EcrisService.NotificationMessageType>(msgResult);

            return result;
        }
        private AbstractMessageType CreateRequestResponseMessage(MJ_CAIS.DTO.EcrisService.RequestResponseMessageType incommingMessage)
        {
            var result = _mapper.Map<MJ_CAIS.DTO.EcrisService.RequestResponseMessageType,
                EcrisRIClient.EcrisService.RequestResponseMessageType>(incommingMessage);
            return result;
        }

        private AbstractMessageType CreateNotificationMessage(MJ_CAIS.DTO.EcrisService.NotificationMessageType incommingMessage)
        {
            var result = _mapper.Map<MJ_CAIS.DTO.EcrisService.NotificationMessageType,
                EcrisRIClient.EcrisService.NotificationMessageType>(incommingMessage);
            return result;
        }

        private async Task<AbstractMessageType> InsertMessage(AbstractMessageType message,string folderId, string sessionId)
        {
            StoreMessageWSInputDataType reqData = new StoreMessageWSInputDataType();
            reqData.EcrisRiMessage = message;
            reqData.TargetFolderIdentifier = folderId;           
            
            StoreMessageWSInputType req = new StoreMessageWSInputType();
            req.WSData = reqData;
            req.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            
            storeMessageResponse result = await _storageClient.storeMessageAsync(req);

           return ((StoreMessageWSOutputDataType)result.StoreMessageWSOutput.WSData).EcrisMessage;
                     
        }


    }
}
