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

namespace EcrisMessageCreator
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
                    .ConfigureServices(services => services.AddSingleton<EcrisMessageCreatorService>())
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()
                    .Build();

                int pageSize = config.GetValue<int>("EcrisCreatorSettings:PageSize"); ;
                string joinSeparator = config.GetValue<string>("EcrisCreatorSettings:joinSeparator");
                bool createReply = config.GetValue<bool>("EcrisCreatorSettings:createReply");
                bool mergeNotificationWithFBBC = config.GetValue<bool>("EcrisCreatorSettings:mergeNotificationWithFBBC");
                //todo: repeat?check if exists?
                using (host)
                {


                    var msgCreatorService = host.Services.GetService<EcrisMessageCreatorService>();
                    if (createReply)
                    {
                        await msgCreatorService.CreateResponsesToRequests(pageSize, joinSeparator);
                    }
                    if (mergeNotificationWithFBBC)
                    {
                        await msgCreatorService.ProcessIdentifiedNotificationsAsync();
                    }



                }
            }
            catch (Exception ex)

            {
                logger.Error(ex, ex.Message, ex.Data);

            }
            finally
            {
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }




        }


    }
}
