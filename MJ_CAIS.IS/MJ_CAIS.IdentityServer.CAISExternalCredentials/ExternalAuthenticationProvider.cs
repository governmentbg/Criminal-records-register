using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.IdentityServer.CAISExternalCredentials.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoLogica.Authentication.Common;

namespace MJ_CAIS.IdentityServer.CAISExternalCredentials
{
    [Export(typeof(IAuthenticationProvider))]
    public class ExternalAuthenticationProvider : IAuthenticationProvider
    {
        public void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPasswordHasher<GUsersExt>), typeof(CompatibilityPasswordHasher));

            services.AddIdentityCore<GUsersExt>()
                .AddSignInManager()
                .AddEntityFrameworkStores<CaisDbContext>()
                .AddDefaultTokenProviders();

        }

        public void BuildLogin(IConfiguration configuration, Controller controller, string returnURL, List<ExternalProvider> providers, ref bool externalOnly)
        {
            // Intentionally left balnk. No specific build login processing needed.
        }

        public async Task<IActionResult> Challenge(IConfiguration configuration, Controller controller, string provider, string returnUrl, string clientName)
        {
            return null;
        }

        public void ConfigureProtectedSettings(IConfiguration configuration)
        {
        }

        public async Task<AuthenticateResult> ExternalCallback(AuthenticateResult result, HttpContext httpContext, Dictionary<string, string> additionalClaims)
        {
            return result;
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            //await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            //await httpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }
}
