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
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Services;

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
                    //PrintDocumentService s = (PrintDocumentService)host.Services.GetService<IPrintDocumentService>();
                    //var fileArray = await s.PrintApplication("aaaa-bbbb-cccc");
                    //var _pdfSigner = host.Services.GetService<IPdfSigner>();
                    //fileArray = _pdfSigner.SignPdf(fileArray, "cais.mjs.bg", new Dictionary<string, string>()
                    //{ { "application_id", "aaaa-bbbb-cccc"} });

                    //System.IO.File.WriteAllBytes("hello.pdf", fileArray);
                    // IRegisterTypeService s = (RegisterTypeService)host.Services.GetService<IRegisterTypeService>();
                    //var regNum = await  s.GetRegisterNumberForApplicationWeb("532");


                    IAutomaticStepService service = (IAutomaticStepService)host.Services.GetService(typeofExecutor);

                    if (service != null)
                    {

                        logger.Trace("PreSelect started.");
                        await service.PreSelectAsync(config);
                        logger.Trace("PreSelect ended.");
                        logger.Trace("Select started.");
                        var entities = await service.SelectEntitiesAsync(pageSize, config);
                        logger.Trace($"Select ended. {entities.Count} selected.");
                        logger.Trace("PostSelect started.");
                        await service.PostSelectAsync(config);
                        logger.Trace("PostSelect ended.");
                        logger.Trace("PreProcess started.");
                        await service.PreProcessAsync(config);
                        logger.Trace("PreProcess ended.");
                        if (entities.Count > 0)
                        {
                            logger.Trace("ProcessEntitiesAsync started.");
                            var result = await service.ProcessEntitiesAsync(entities, config);
                            logger.Trace($"ProcessEntitiesAsync ended. {result.GetLogInfo()}");
                        }
                        logger.Trace("PostProcess started.");
                        await service.PostProcessAsync(config);
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
