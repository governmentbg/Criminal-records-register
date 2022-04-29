using Microsoft.AspNetCore.Mvc;
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
            builder.Services.AddControllers(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute("api"));
            });

            var app = builder.Build();

            WebSetupConfig.CustomConfigureApp(app);

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.Run();
        }
    }
}