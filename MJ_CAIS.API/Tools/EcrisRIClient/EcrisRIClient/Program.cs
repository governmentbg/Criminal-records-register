using EcrisRIClient.EcrisService;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace EcrisRIClient
{
    public class Program
    {
        static string _username = "root";
        static string _password = "Lucho128";
        static string _searchFolderName = "Inbox";
        static string _itemsPerPage = "50";
        static DateTime _lastSuccessfulRun = new DateTime(2021, 8, 1);
        
        static async Task Main(string[] args)
        {
            var client = new EcrisClient(_username, _password, _searchFolderName, _itemsPerPage);
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
            
            // XmlSerialize(msgs[0]);
            // File.WriteAllTextAsync("WriteLines.txt", result);
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
            return XmlSerialize(resp.ReadMessageWSOutput.WSData);
        }

        public static string XmlSerialize(Object obj)
        {
            if (obj != null)
            {
                using (MemoryStream ms = new MemoryStream())
                using (StreamReader sr = new StreamReader(ms))
                {
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
