using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DIContainer;
using MJ_CAIS.ExternalWebServices.DbServices;

namespace ExecuteWebRequests
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                .ConfigureServices(services => services.AddTransient<IRegixService, RegixService>())
                .Build();

            using (host)
            {
                var dbContext = host.Services.GetService<CaisDbContext>();

                var regixService = host.Services.GetService<IRegixService>();
                var webRequests = regixService.GetRequestsForAsyncExecution();

                foreach (var webRequest in webRequests)
                {
                    if(webRequest.WebService != null)
                    {
                        if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_PersonDataSearch)
                        {
                            regixService.ExecutePersonDataSearch(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication.RegistrationNumber);
                        }
                        if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_RelationsSearch)
                        {
                            regixService.ExecuteRelationsSearch(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication.RegistrationNumber);
                        }
                        if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_ForeignIdentityV2)
                        {
                            regixService.ExecuteForeignIdentitySearchV2(webRequest, webRequest.WebService.WebServiceName, registrationNumber: webRequest.WApplication.RegistrationNumber);
                        }
                    }
                    dbContext.ChangeTracker.Clear();

                }
            }
        }
    }
}
