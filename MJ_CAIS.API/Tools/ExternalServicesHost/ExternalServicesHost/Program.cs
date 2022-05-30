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
using MJ_CAIS.DIContainer;
using Microsoft.Extensions.Configuration;
using System.IO;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.DataAccess;
using EO.Pdf;

namespace MJ_CAIS.ExternalServicesHost
{
    public class Program
    {
        public static IServiceProvider Services { get; set; }

        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder();

            var config =
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            Runtime.AddLicense(config.GetValue<string>("EOPDFLicenseKey"));

            ContainerExtension.Initialize(builder.Services, config);
            builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);
            builder.Services.AddSingleton<IUserContext>(new UserContext()
            {
                UserId = config.GetValue<string>("ContextUser:UserId"),
                UserName = config.GetValue<string>("ContextUser:UserName")
            });


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
                    .AddService<CriminalRecordsReportService>(
                    opt =>
                    {
                        opt.DebugBehavior.IncludeExceptionDetailInFaults = true;
                    })
                    .AddServiceEndpoint<CriminalRecordsReportService, ICriminalRecordsReportService>(
                        new BasicHttpBinding(), 
                        "/CriminalRecordsReportService/basichttp");

                serviceBuilder
                    .AddService<EISSIntegrationService>(
                    opt =>
                    {
                        opt.DebugBehavior.IncludeExceptionDetailInFaults = true;
                    })
                    .AddServiceEndpoint<EISSIntegrationService, IEISSIntegrationService>(
                        new BasicHttpBinding(), 
                        "/EISSIntegrationService/basichttp");

            });
            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpsGetEnabled = true;

            Services = app.Services;

            await app.RunAsync();
         }
    }
}
