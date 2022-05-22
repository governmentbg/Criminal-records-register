using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oracle.EntityFrameworkCore.Infrastructure;
using Serilog;
using System;
using System.ComponentModel.Composition;
using TechnoLogica.Authentication.Common;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    [Export(typeof(IAuthenticationProfile))]
    public class ExternalAuthenticationProfile : IAuthenticationProfile
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CaisConnectionString");
            var oracleCompatibility = configuration.GetValue<string>("OracleSQLCompatibility");

            services.AddDbContext<CaisDbContext>(x => x.UseOracle(connectionString, opt => opt.UseOracleSQLCompatibility(oracleCompatibility)));

            services.AddTransient<IProfileClientService, ExternalProfileService>();
            services.AddTransient<IProfileClientService, LocalExternalProfileService>();

        }
    }
}
