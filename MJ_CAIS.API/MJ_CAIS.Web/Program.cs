using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.DataAccess.ExtUsers;
using MJ_CAIS.Services;
using MJ_CAIS.Web.Utils;
using MJ_CAIS.WebSetup;
using MJ_CAIS.WebSetup.Setup;

namespace MJ_CAIS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Most configurations are in Web.Setup project
            var builder = WebSetupConfig.CustomConfigureBuilder(args);
            builder.Services
                .AddControllers(opt =>
                {
                    if (builder.Environment.IsEnvironment("Development"))
                    {
                        opt.UseCentralRoutePrefix(new RouteAttribute("api"));
                    }
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new TrimStringJsonConverter());
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                });

            // TODO: at some point in time move back to WebSetupConfig
            // For now, different authentication for web projects
            var configuration = builder.Configuration;

            var connectionString = configuration.GetConnectionString("CaisConnectionString");
            var oracleCompatibility = configuration.GetValue<string>("OracleSQLCompatibility");
            builder
                .Services
                .AddDbContext<ExtUserDbContext>(x => x.UseOracle(connectionString, opt => opt.UseOracleSQLCompatibility(oracleCompatibility)));

            builder
                .Services
                .AddTransient(typeof(IPasswordHasher<LocalGUsersExt>), typeof(CompatibilityPasswordHasher));

            builder
                .Services
                .AddIdentityCore<LocalGUsersExt>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ExtUserDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<LocalGUsersExt>>(TokenOptions.DefaultProvider); ;

            builder.Services.AddAuthorization();
            builder.Services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration.GetValue<string>("Authentication:TokenIssuer");
                    options.ApiName = configuration.GetValue<string>("Authentication:APIName");
                    options.ApiSecret = configuration.GetValue<string>("Authentication:APISecret");
                    options.EnableCaching = false; // Do not cache credentials - always rely on identity server
                });
            IdentityModelEventSource.ShowPII = true;

            var app = builder.Build();

            WebSetupConfig.CustomConfigureApp(app, true);

            app.Run();
        }
    }
}