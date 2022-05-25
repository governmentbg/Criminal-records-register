using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.DIContainer;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using TL.JasperReports.Integration;
using TL.Signer;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.ExternalWebServices;

namespace AutomaticStepsExecutor
{
    public class Program
    {
       
        static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
       
            try
            {
                logger.Info($"Execution started.");
                var executorClass = config.GetValue<string>("AutomaticStepsExecutor:ExecutorClass");
                var typeofExecutor = Type.GetType(executorClass);

                IHost host = Host.CreateDefaultBuilder()
                    .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                    .ConfigureServices(services => services.AddSingleton(typeofExecutor))
                    .ConfigureServices(services=>services.AddAutoMapper(typeof(ApplicationProfile).Assembly))              
                  
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()
               
                    .Build();


                var pageSize = config.GetValue<int>("AutomaticStepsExecutor:PageSize");


                using (host)
                {
                   // IPrintDocumentService s = host.Services.GetService<IPrintDocumentService>();
                   // var resultPdf = await s.PrintApplication("aaaa-bbbb-cccc");
                   // System.IO.File.WriteAllBytes("hello.pdf", resultPdf);


                    IAutomaticStepService service = (IAutomaticStepService)host.Services.GetService(typeofExecutor);

                    if (service != null)
                    {

                        logger.Trace("PreSelect started.");
                        await service.PreSelectAsync();
                        logger.Trace("PreSelect ended.");
                        logger.Trace("Select started.");
                        var entities = await service.SelectEntitiesAsync(pageSize);
                        logger.Trace($"Select ended. {entities.Count} selected.");
                        logger.Trace("PostSelect started.");
                        await service.PostSelectAsync();
                        logger.Trace("PostSelect ended.");
                        logger.Trace("PreProcess started.");
                        await service.PreProcessAsync();
                        logger.Trace("PreProcess ended.");
                        logger.Trace("ProcessEntitiesAsync started.");
                        var result = await service.ProcessEntitiesAsync(entities);
                        logger.Trace($"ProcessEntitiesAsync ended. {result.GetLogInfo()}");
                        logger.Trace("PostProcess started.");
                        await service.PostProcessAsync();
                        logger.Trace("PostProcess ended.");
                    }
                    else
                    {
                        throw new Exception($"{executorClass} could not be loaded");
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
