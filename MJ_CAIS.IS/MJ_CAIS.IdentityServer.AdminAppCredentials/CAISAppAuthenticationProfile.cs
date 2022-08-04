using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.IdentityServer.CAISAppCredentials;
using Oracle.EntityFrameworkCore.Infrastructure;
using Serilog;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using TechnoLogica.Authentication.Common;

namespace TechnoLogica.RegiX.IdentityServer.AdminAppCredentials
{
    [Export(typeof(IAuthenticationProfile))]
    public class CAISAppAuthenticationProfile : IAuthenticationProfile
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CaisConnectionString");
            var oracleCompatibility = configuration.GetValue<string>("OracleSQLCompatibility");

            services.AddDbContext<CaisDbContext>(x => x.UseOracle(connectionString, opt => opt.UseOracleSQLCompatibility(oracleCompatibility)));

            services.AddTransient<IProfileClientService, CAISAppProfileService>();

        }
    }
}
