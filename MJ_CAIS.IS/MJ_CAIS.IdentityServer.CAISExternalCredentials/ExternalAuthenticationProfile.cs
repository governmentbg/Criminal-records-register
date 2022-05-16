using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Composition;
using TechnoLogica.Authentication.Common;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    [Export(typeof(IAuthenticationProfile))]
    public class ExternalAuthenticationProfile : IAuthenticationProfile
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CaisDbContext>(options =>
                 options.UseOracle(configuration.GetConnectionString("CaisConnectionString"), opt => opt.UseOracleSQLCompatibility("11")));

            services.AddTransient<IProfileClientService, ExternalProfileService>();
            services.AddTransient<IProfileClientService, LocalExternalProfileService>();

        }
    }
}
