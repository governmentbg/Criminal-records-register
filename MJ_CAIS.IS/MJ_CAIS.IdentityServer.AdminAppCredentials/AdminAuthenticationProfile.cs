using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.IdentityServer.CAISAppCredentials;
using System.ComponentModel.Composition;
using TechnoLogica.Authentication.Common;

namespace TechnoLogica.RegiX.IdentityServer.AdminAppCredentials
{
    [Export(typeof(IAuthenticationProfile))]
    public class AdminAuthenticationProfile : IAuthenticationProfile
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CaisDbContext>(options =>
                 options.UseOracle(configuration.GetConnectionString("CaisConnectionString"), opt => opt.UseOracleSQLCompatibility("11")));

            services.AddTransient<IProfileClientService, AdminProfileService>();
            services.AddTransient<IProfileClientService, LocalAdminProfileService>();

        }
    }
}
