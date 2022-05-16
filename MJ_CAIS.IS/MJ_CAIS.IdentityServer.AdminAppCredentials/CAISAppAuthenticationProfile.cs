using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.IdentityServer.CAISAppCredentials;
using System.ComponentModel.Composition;
using TechnoLogica.Authentication.Common;

namespace TechnoLogica.RegiX.IdentityServer.AdminAppCredentials
{
    [Export(typeof(IAuthenticationProfile))]
    public class CAISAppAuthenticationProfile : IAuthenticationProfile
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CaisDbContext>(options =>
                 options.UseOracle(configuration.GetConnectionString("CaisConnectionString"), opt => opt.UseOracleSQLCompatibility("11")));

            services.AddTransient<IProfileClientService, CAISAppProfileService>();
            services.AddTransient<IProfileClientService, LocalAdminProfileService>();

        }
    }
}
