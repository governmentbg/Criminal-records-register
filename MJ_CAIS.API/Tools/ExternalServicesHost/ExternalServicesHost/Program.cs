using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.Extensions.DependencyInjection;

namespace MJ_CAIS.ExternalServicesHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            // Add CoreWCF services to the ASP.NET Core app's DI container
            builder.Services.AddServiceModelServices();

            // Add WSDL support
            builder.Services.AddServiceModelServices().AddServiceModelMetadata();
            builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

            var app = builder.Build();

            // Configure CoreWCF endpoints in the ASP.NET Core host
            app.UseServiceModel(serviceBuilder =>
            {
                serviceBuilder
                    .AddService<CriminalRecordsReportService>()
                    .AddServiceEndpoint<CriminalRecordsReportService, ICriminalRecordsReportService>(
                        new BasicHttpBinding(), 
                        "/CriminalRecordsReportService/basichttp");
                serviceBuilder
                    .AddService<EISSIntegrationService>()
                    .AddServiceEndpoint<EISSIntegrationService, IEISSIntegrationService>(
                        new BasicHttpBinding(), 
                        "/EISSIntegrationService/basichttp");

            });
            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpsGetEnabled = true;

            await app.RunAsync();
         }
    }
}
