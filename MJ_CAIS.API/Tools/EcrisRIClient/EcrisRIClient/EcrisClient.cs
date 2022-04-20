﻿using System;
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
        private readonly List<EcrisMessageType> _msgListForCais = new List<EcrisMessageType> { EcrisMessageType.REQ, EcrisMessageType.NOT };
        private string _username { get; set; }
        private string _password { get; set; }
        //private string _searchFolderName { get; set; }
        //private string _itemsPerPage { get; set; }
        private storagePortv10Client _storageClient;

        public EcrisClient(string username, string password)
        {
            _username = username;
            _password = password;
            _storageClient = new storagePortv10Client();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ServiceToDtos());
            });

            _mapper = mapperConfig.CreateMapper();
            //_searchFolderName = folder;
            //_itemsPerPage = itemsPerPage;
        }

        public async Task<string> GetActiveSessionId()
        {
            authenticationPortv10Client client = new authenticationPortv10Client();
            LoginWSInputType loginRequest = new LoginWSInputType();
            loginRequest.WSMetaData = new LoginWSInputMetaDataType() { MetaDataTimeStamp = DateTime.Now };
            loginRequest.WSData = new LoginWSInputDataType() { LoginUserName = _username, LoginUserPassword = _password };
            var resp = await client.loginAsync(loginRequest);
            return resp.LoginWSOutput.WSMetaData.SessionId;
        }

        public async Task<string> Logout(string sessionID)
        {
            authenticationPortv10Client client = new authenticationPortv10Client();
            LogoutWSInputType logoutRequest = new LogoutWSInputType();

            logoutRequest.WSMetaData = new LoginWSInputMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionID };

            var resp = await client.logoutAsync(logoutRequest);
            return ((BaseEcrisRiWSOutputDataType)resp.LogoutWSOutput.WSData).ActionExecutionInformation;
        }

        public async Task<string> GetInboxFolderIdentifier(string sessionId, string searchFolderName)
        {
            var client = new storagePortv10Client();
            var request = new BaseEcrisRiWSInputType();


            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            var resp = await client.getFoldersAsync(request);
            var foldersList = ((GetFoldersWSOutputDataType)resp.GetFoldersWSOutput.WSData).FolderList;
            for (int i = 0; i < foldersList.Length; i++)
            {
                if (foldersList[i].FolderName == searchFolderName)
                {
                    return foldersList[i].FolderIdentifier;
                }
            }

            return string.Empty;
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

        private async Task<MessageShortViewType[]> GetMessagesPageForFolder(storagePortv10Client client, int pageNumber, string sessionId, string folderId, string itemsPerPage)
        {
            var request = new GetMessagesForFolderWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            request.WSData = new GetMessagesForFolderWSInputDataType()
            {
                FolderIdentifier = folderId,
                MessagesSortedBy = MessageSortByType.VersionDesc,
                MessagesSortedBySpecified = true,
                ItemsPerPage = itemsPerPage,
                PageNumber = pageNumber,
                PageNumberSpecified = true
            };

            var resp = await client.getMessagesForFolderAsync(request);
            var wsData = (GetMessagesForFolderWSOutputDataType)resp.GetMessagesForFolderWSOutput.WSData;
            return wsData.MessageShortViewList;
        }

        public async Task<QueryType> GetPreparedQuery(string sessionID, string queryName)
        {
            var client = new searchPortv10Client();
            var request = new RetrieveStoredQueryWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionID };
            request.WSData = new RetrieveStoredQueryWSInputDataType()
            {
                QueryName = queryName

            };

            var response = await client.retrieveStoredQueryAsync(request);
            var wsData = (RetrieveStoredQueriesWSOutputDataType)response.RetrieveStoredQueriesWSOutput.WSData;
            if (wsData.QueryList.Length != 1)
                //todo:make exception
                throw new Exception("QueryNotFound");
            return wsData.QueryList[0];

        }

        public async Task SavePreparedQuery(string sessionID, QueryType query)
        {
            var client = new searchPortv10Client();

            var request = new SearchWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionID };
            request.WSData = new SearchWSInputDataType() { SearchQuery = query };

            var response = await client.storeQueryAsync(request);
            //todo:how to check success?!    


        }
        public async Task<MJ_CAIS.DTO.EcrisService.SearchWSOutputDataType> ExecutePreparedQuery(string sessionID, MJ_CAIS.DTO.EcrisService.QueryType query, int pageNumber, string itemsPerPage)
        {

            EcrisRIClient.EcrisService.QueryType serviceQuery = _mapper.Map<EcrisRIClient.EcrisService.QueryType>(query);

            var client = new searchPortv10Client();
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

    }
}
