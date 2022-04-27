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

namespace EcrisFromCAISExporter
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
                    .ConfigureServices(services => services.AddSingleton<EcrisFromCAISService>())
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
               // bool insertRepliesToReqests = config.GetValue<bool>("EcrisRiSettings:insertRepliesToReqests");
               // bool insertNotifications = config.GetValue<bool>("EcrisRiSettings:insertNotifications");
                string folderName = config.GetValue<string>("EcrisRiSettings:folderName");
                //todo: repeat?check if exists?
                using (host)
                {

                    
                     var ecrisFromService = host.Services.GetService<EcrisFromCAISService>();
                     await ecrisFromService.SendMessagesToEcris(username, password, folderName);
                   
                   

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
