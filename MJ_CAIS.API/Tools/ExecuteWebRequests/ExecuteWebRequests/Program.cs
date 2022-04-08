using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.DIContainer;
using MJ_CAIS.ExternalWebServices;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services.Contracts;

namespace ExecuteWebRequests
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                .ConfigureServices(services => services.AddSingleton<RegixService>())
                .Build();

            using (host)
            {
                var regixService = host.Services.GetService<RegixService>();
                var webRequests = regixService.GetRequestsForExecution();

                foreach (var webRequest in webRequests)
                {
                    if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_PersonDataSearch)
                    {
                        regixService.ExecutePersonDataSearch(webRequest, "SERVICE_URI");
                    }
                }
            }
        }
    }
}
