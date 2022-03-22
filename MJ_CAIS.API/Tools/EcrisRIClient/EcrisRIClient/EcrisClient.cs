using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EcrisRIClient.EcrisService;

namespace EcrisRIClient
{
    public class EcrisClient
    {
        private readonly List<EcrisMessageType> _msgListForCais = new List<EcrisMessageType> { EcrisMessageType.REQ, EcrisMessageType.NOT };
        private string _username { get; set; }
        private string _password { get; set; }
        private string _searchFolderName { get; set; }
        private string _itemsPerPage { get; set; }

        public EcrisClient(string username, string password, string folder, string itemsPerPage)
        {
            _username = username;
            _password = password;
            _searchFolderName = folder;
            _itemsPerPage = itemsPerPage;
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

        public async Task<string> GetInboxFolderIdentifier(string sessionId)
        {
            storagePortv10Client client = new storagePortv10Client();
            var request = new BaseEcrisRiWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            var resp = await client.getFoldersAsync(request);
            var foldersList = ((GetFoldersWSOutputDataType)resp.GetFoldersWSOutput.WSData).FolderList;

            for (int i = 0; i < foldersList.Length; i++)
            {
                if (foldersList[i].FolderName == _searchFolderName)
                {
                    return foldersList[i].FolderIdentifier;
                }
            }
            return string.Empty;
        }

        public async Task<List<MessageShortViewType>> GetMessagesForFolder(string sessionId, string folderId, DateTime lastSuccessDate)
        {
            storagePortv10Client client = new storagePortv10Client();
            DateTime minDate = DateTime.Now;
            int pageNumber = 0;
            List<MessageShortViewType> resultList = new List<MessageShortViewType>();
            do
            {
                var pageList = await GetMessagesPageForFolder(client, pageNumber, sessionId, folderId);
                foreach (var msg in pageList)
                {
                    if (msg.MessageVersionTimestamp > lastSuccessDate && _msgListForCais.Contains(msg.MessageType))
                    {
                        resultList.Add(msg);
                    }

                    if (msg.MessageVersionTimestamp < minDate)
                    {
                        minDate = msg.MessageVersionTimestamp;
                    }
                }
                pageNumber++;

            }
            while (minDate > lastSuccessDate);

            return resultList;
        }

        private async Task<MessageShortViewType[]> GetMessagesPageForFolder(storagePortv10Client client, int pageNumber, string sessionId, string folderId)
        {
            GetMessagesForFolderWSInputType request = new GetMessagesForFolderWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            request.WSData = new GetMessagesForFolderWSInputDataType()
            {
                FolderIdentifier = folderId,
                MessagesSortedBy = MessageSortByType.VersionDesc,
                MessagesSortedBySpecified = true,
                ItemsPerPage = _itemsPerPage,
                PageNumber = pageNumber,
                PageNumberSpecified = true
            };

            var resp = await client.getMessagesForFolderAsync(request);
            var wsData = (GetMessagesForFolderWSOutputDataType)resp.GetMessagesForFolderWSOutput.WSData;
            return wsData.MessageShortViewList;

        }
    }
}
