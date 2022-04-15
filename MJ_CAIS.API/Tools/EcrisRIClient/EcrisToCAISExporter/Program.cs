using EcrisIntegrationServices;
using EcrisRIClient;
using EcrisRIClient.EcrisService;
using EcrisServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DIContainer;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;
using System.Threading.Tasks;
using MJ_CAIS.DataAccess;

namespace EcrisToCAISExporter
{
    public class Program
    {
       static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            try
            {
              

                IHost host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                    .ConfigureServices(services => services.AddSingleton<EcrisToCAISService>())
                    .ConfigureLogging(logging =>
                      {
                          logging.ClearProviders();
                          logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    
                      })
                    .UseNLog()
                    .Build();
    

                var username = config.GetValue<string>("EcrisRiSettings:username");
                var password = config.GetValue<string>("EcrisRiSettings:password");
                //todo: Get from config
                string joinSeparator = config.GetValue<string>("EcrisRiSettings:joinSeparator"); 
                bool skipDataExtractionForRequests = config.GetValue<bool>("EcrisRiSettings:skipDataExtractionForRequests");
                bool skipDataExtractionForNotifications = config.GetValue<bool>("EcrisRiSettings:skipDataExtractionForNotifications");
                bool synchRequests = config.GetValue<bool>("EcrisRiSettings:synchRequests");
                bool synchNotifications = config.GetValue<bool>("EcrisRiSettings:synchNotifications");
                string pageSize = config.GetValue<string>("EcrisRiSettings:pageSize");
                string folderName = config.GetValue<string>("EcrisRiSettings:folderName");
                string paramRequestSynch = config.GetValue<string>("EcrisRiSettings:paramRequestSynch");
                string paramNotificationSynch = config.GetValue<string>("EcrisRiSettings:paramNotificationSynch");
                //todo: repeat?check if exists?
                using (host)
                {

                    //var t1 = Task.Run(async () =>
                    //{
                    if (synchRequests)
                    {
                        var ecrisTOService = host.Services.GetService<EcrisToCAISService>();

                        await ecrisTOService.SynchRequests(username, password, folderName, pageSize, skipDataExtractionForRequests, joinSeparator, paramRequestSynch);
                    }
                    //});
                    //var t2 = Task.Run(async () =>
                    //{
                    if (synchNotifications)
                    {
                        var ecrisTOService = host.Services.GetService<EcrisToCAISService>();
                        await ecrisTOService.SynchNotifications(username, password, folderName, pageSize, skipDataExtractionForNotifications, joinSeparator, paramNotificationSynch);
                    }
                    //});

                    //Task.WaitAll(t1);

                }
            }
            catch (Exception ex)

            {
                logger.Error(ex, ex.Message,ex.Data);

            }
            finally
            {
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }




        }

       
    }
}
