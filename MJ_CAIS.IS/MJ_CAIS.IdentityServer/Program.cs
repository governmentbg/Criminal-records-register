using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using System.Text;
using TechnoLogica.IdentityServer;

namespace MJ_CAIS.IdentityServer
{
    public class RegiXIdentityServerProgram
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            var configuration =
                new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Program.Start<Startup>("MJ_CAIS IdentityServer", args);
        }
    }

    public class Startup : TechnoLogica.IdentityServer.Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration config, ILoggerFactory loggerFactory) 
            : base(environment, config, loggerFactory)
        {
        }
    }
}