using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.IO;
using TechnoLogica.IdentityServer;

namespace MJ_CAIS.IdentityServer
{
    public class RegiXIdentityServerProgram
    {
        public static void Main(string[] args)
        {
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