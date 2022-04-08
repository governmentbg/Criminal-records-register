using EcrisRIClient.EcrisService;
using Microsoft.Extensions.Configuration;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace EcrisRIClient
{
    public class Program
    {
        private static string _searchFolderName = "Inbox";
        private static string _itemsPerPage = "50";
        private static DateTime _lastSuccessfulRun = new DateTime(2021, 8, 1);
        
        static async Task Main(string[] args)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            IConfiguration config = configBuilder.Build();

            var username = config.GetValue<string>("EcrisRiSettings:username");
            var password = config.GetValue<string>("EcrisRiSettings:password");

            var client = new EcrisClient(username, password, _searchFolderName, _itemsPerPage);
            var sessionId = await client.GetActiveSessionId();
            var inboxFolderId = await client.GetInboxFolderIdentifier(sessionId);
            var msgs = await client.GetMessagesForFolder(sessionId, inboxFolderId, _lastSuccessfulRun);

            foreach (var msg in msgs)
            {
                var identifier = msg.MessageIdentifier;
                var storageClient = new storagePortv10Client();
                var xml = await ReadMessage(storageClient, sessionId, identifier);
                await File.WriteAllTextAsync(identifier + ".xml", xml);
            }
        }

        private static async Task<string> ReadMessage(storagePortv10Client client, string sessionId, string identifier)
        {
            EcrisRiIdentifierContainingWSInputType request = new EcrisRiIdentifierContainingWSInputType();
            request.WSMetaData = new SessionIdContainingWSMetaDataType() { MetaDataTimeStamp = DateTime.Now, SessionId = sessionId };
            request.WSData = new EcrisRiIdentifierContainingMessageWSInputDataType()
            {
                MessageIdentifier = identifier //"RI-NOT-000000000000633"
            };
            var resp = await client.readMessageAsync(request);
            var result = XmlUtils.SerializeToXml(resp.ReadMessageWSOutput.WSData);
            return result;
        }
    }
}
