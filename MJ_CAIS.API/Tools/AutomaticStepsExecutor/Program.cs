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
using MJ_CAIS.DataAccess;

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
                    .ConfigureServices(services => services.AddAutoMapper(typeof(ApplicationProfile).Assembly))
                    .ConfigureServices(services => services.AddSingleton<IUserContext>(new UserContext() {
                        UserId = config.GetValue<string>("ContextUser:UserId"),
                        UserName = config.GetValue<string>("ContextUser:UserName")
                    }))
                    .ConfigureServices(services => services.AddAutoMapper(typeof(RegixProfile).Assembly))
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()
               
                    .Build();


                var pageSize = config.GetValue<int>("AutomaticStepsExecutor:PageSize");
                var repeatTillEnd = config.GetValue<bool?>("AutomaticStepsExecutor:RepeatTillEnd");

                using (host)
                {
                   // CertificateGenerationService s = (CertificateGenerationService)host.Services.GetService<ICertificateGenerationService>();
                    //var fileArray = await s.PrintApplication("aaaa-bbbb-cccc");
                    //var _pdfSigner = host.Services.GetService<IPdfSigner>();
                    //fileArray = _pdfSigner.SignPdf(fileArray, "cais.mjs.bg", new Dictionary<string, string>()
                    //{ { "application_id", "aaaa-bbbb-cccc"} });

                    //System.IO.File.WriteAllBytes("hello.pdf", fileArray);
                    // IRegisterTypeService s = (RegisterTypeService)host.Services.GetService<IRegisterTypeService>();
                    //var regNum = await  s.GetRegisterNumberForApplicationWeb("532");
                    //ICertificateService s = (ICertificateService)host.Services.GetService<ICertificateService>();
                    //var cert = await s.GetCertificateDocumentContent("e818612c-8262-4fb5-af49-c38c586ab830");
                    //var cert = await s.CreateCertificate("e818612c-8262-4fb5-af49-c38c586ab830");
                    //System.IO.File.WriteAllBytes("cert_e818612c-8262-4fb5-af49-c38c586ab830.pdf", cert);

                    IAutomaticStepService service = (IAutomaticStepService)host.Services.GetService(typeofExecutor);


                    //IPrintDocumentService s = (IPrintDocumentService)host.Services.GetService<IPrintDocumentService>();
                    //var fileArray = await s.PrintDailyCertificates(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), "");
                    //System.IO.File.WriteAllBytes("testDailyCertificatesDelivered.pdf", fileArray);

                    if (service != null)
                    {
                        var numberOfEntities = 0;
                        int numberOfPage = 0;
                        do
                        {
                            logger.Trace("PreSelect started.");
                            await service.PreSelectAsync(config);
                            logger.Trace("PreSelect ended.");
                            logger.Trace("Select started.");
                            var entities = await service.SelectEntitiesAsync(pageSize, config, numberOfPage);
                            numberOfPage++;
                            numberOfEntities = entities.Count;
                            logger.Trace($"Select ended. {numberOfEntities} selected.");
                            logger.Trace("PostSelect started.");
                            await service.PostSelectAsync(config);
                            logger.Trace("PostSelect ended.");
                            logger.Trace("PreProcess started.");
                            await service.PreProcessAsync(config);
                            logger.Trace("PreProcess ended.");
                            if (numberOfEntities > 0)
                            {
                                logger.Trace("ProcessEntitiesAsync started.");
                                var result = await service.ProcessEntitiesAsync(entities, config);
                                logger.Trace($"ProcessEntitiesAsync ended. {result.GetLogInfo()}");
                            }
                            logger.Trace("PostProcess started.");
                            await service.PostProcessAsync(config);
                            logger.Trace("PostProcess ended.");
                        }
                        while (repeatTillEnd.HasValue && repeatTillEnd == true && numberOfEntities > 0);
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
