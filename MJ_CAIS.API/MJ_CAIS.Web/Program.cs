using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
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
            if (!builder.Environment.IsEnvironment("tl"))
            {
                builder.Services
                    .AddControllers(opt =>
                    {
                        opt.UseCentralRoutePrefix(new RouteAttribute("api"));
                    })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new TrimStringJsonConverter());
                        //options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                    });
            }

            // TODO: at some point in time move back to WebSetupConfig
            // For now, different authentication for web projects
            var configuration = builder.Configuration;
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

            WebSetupConfig.CustomConfigureApp(app);

            app.Run();
        }
    }
}