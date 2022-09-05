using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DIContainer;
using MJ_CAIS.ExternalWebServices.DbServices;
using NLog;
using NLog.Web;

namespace ExecuteWebRequests
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                logger.Info($"Execution started.");
                IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                .ConfigureServices(services => services.AddTransient<IRegixService, RegixService>())
                 .ConfigureServices(services => services.AddSingleton<IUserContext>(new UserContext()
                 {
                     UserId = config.GetValue<string>("ContextUser:UserId"),
                     UserName = config.GetValue<string>("ContextUser:UserName")
                 }))

                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);

                    })
                    .UseNLog()

                    .Build();


                using (host)
                {
                    var dbContext = host.Services.GetService<CaisDbContext>();

                    var regixService = host.Services.GetService<IRegixService>();
                    var webRequests = regixService.GetRequestsForAsyncExecution();

                    logger.Info($"Number of requests for execution: {webRequests.Count}");
                    foreach (var webRequest in webRequests)
                    {
                        if (webRequest.WebService != null)
                        {
                            logger.Trace($"RequstID: {webRequest.Id} - Execute {webRequest.WebService.TypeCode } started.");
                            switch (webRequest.WebService.TypeCode)
                            {
                                case WebServiceEnumConstants.REGIX_PersonDataSearch:

                                    if (webRequest.WApplication != null)
                                    {
                                        regixService.ExecutePersonDataSearch(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication.RegistrationNumber);

                                    }
                                    break;
                                case WebServiceEnumConstants.REGIX_RelationsSearch:
                                    if (webRequest.WApplication != null)
                                    {
                                        regixService.ExecuteRelationsSearch(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication.RegistrationNumber);
                                    }
                                    break;
                                case WebServiceEnumConstants.REGIX_ForeignIdentityV2:
                                    if (webRequest.WApplication != null)
                                    {
                                        regixService.ExecuteForeignIdentitySearchV2(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication?.RegistrationNumber);
                                    }
                                    break;
                                default:
                                    logger.Warn($"RequstID: {webRequest.Id} - Execute {webRequest.WebService.TypeCode } : type is not expected.");
                                    break;

                            }
                            logger.Trace($"RequstID: {webRequest.Id} - Execute {webRequest.WebService.TypeCode } ended.");
                        }
                        else
                        {
                            logger.Info($"RequstID: {webRequest.Id} - WebService is null.");
                        }
                        dbContext.ChangeTracker.Clear();

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
