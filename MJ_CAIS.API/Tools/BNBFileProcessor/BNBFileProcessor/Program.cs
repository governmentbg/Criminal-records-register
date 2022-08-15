using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MJ_CAIS.DIContainer;
using MJ_CAIS.DataAccess;
using Microsoft.EntityFrameworkCore;
using MJ_CAIS.Common.Constants;
using System.Text.RegularExpressions;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.Extensions.Hosting.WindowsServices;
using System.ServiceProcess;
using BNBFileProcessor.Configuration;
using BNBFileProcessor.Configurations.Elements;
using BNBFileProcessor.Helpers;

namespace BNBFileProcessor
{
    public class Program
    {
        private static Logger logger;
        private static string regex1Str;
        private static string regex2Str;
        //static void Main(string[] args)
        //{
        //    DIHelper.SetDIContainer();

        //    //CaseEventProcessor caseEventProcessor = new CaseEventProcessor();

        //    EventListenerSetting eventListenerConfiguration = EventListenerSetting.EventListenerSettings;

        //    ServicePointConfigurationManager.ConfirateServicePointManager();
        //    ServicePointConfigurationManager.ConfigurateServicePoint();

        //    if (Environment.UserInteractive)
        //    {
        //        FileMonitoring fileMonitoring = new FileMonitoring(eventListenerConfiguration);
        //        fileMonitoring.StartupAndStop(args);
        //    }
        //    else
        //    {
        //        ServiceBase[] ServicesToRun = new ServiceBase[]
        //        {
        //            new FileMonitoring(eventListenerConfiguration)
        //        };
        //        ServiceBase.Run(ServicesToRun);
        //    }

        //}
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info($"Execution started.");



                IHost host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                    .ConfigureServices(services => services.AddTransient(typeof(IBNBEventProcessor),typeof(BNBFileProcessor)))
                    .UseWindowsService()
                    .ConfigureServices((hostContext, services) =>
                     {
                         if (WindowsServiceHelpers.IsWindowsService())
                             services.AddSingleton<IHostLifetime, FileMonitoring>();
                     })
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()
                    .Build();

                DIContainer.Container = host;

                EventListenerSetting eventListenerConfiguration = new EventListenerSetting(config);
                //EventListenerSetting.EventListenerSettings;
                WatcherSetting  watcherS = new WatcherSetting();
                watcherS.CompleatePath = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:compleatePath");
                Console.WriteLine(config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:compleatePath"));
                watcherS.ErrorPath = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:errorPath");
                watcherS.ProcessingPath = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:processingPath");
                watcherS.SourcePath = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:sourcePath");
                watcherS.WatcherKey = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:key");
                watcherS.ElapsedTime = config.GetValue<int>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:elapsedTimeInSeconds");
                watcherS.FileFilter = config.GetValue<string>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:fileFilter");
                watcherS.DeleteCompletedFiles = config.GetValue<bool>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:deleteCompletedFiles");
                watcherS.MaxFileProcess = config.GetValue<int>("EventListenerConfiguration:WatcherSettingsList:WatcherSettings:maxFileProcess");

                eventListenerConfiguration.WatcherSettingsList = new WatcherSettingscollection(config);
                eventListenerConfiguration.WatcherSettingsList[0] = watcherS;

             regex1Str = config.GetValue<string>("BNBPayments:regex1");
                regex2Str = config.GetValue<string>("BNBPayments:regex1");

                using (host)
                {

                    if (Environment.UserInteractive)
                    {
                        FileMonitoring fileMonitoring = new FileMonitoring(eventListenerConfiguration);
                        fileMonitoring.StartupAndStop(args);
                    }
                    else
                    {
                        ServiceBase[] ServicesToRun = new ServiceBase[]
                        {
                                new FileMonitoring(eventListenerConfiguration)
                        };
                        ServiceBase.Run(ServicesToRun);
                }
            
                }

            }
            catch (Exception ex)

            {
                logger.Error(ex, ex.Message, ex.Data);

            }
            finally
            {
                logger.Info($"Execution ended.");
                NLog.LogManager.Flush();
                NLog.LogManager.Shutdown();
            }




        }



    }
}


