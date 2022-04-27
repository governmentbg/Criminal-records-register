using Microsoft.AspNet.OData.Extensions;
using MJ_CAIS.WebSetup;
using MJ_CAIS.WebSetup.Setup;

namespace MJ_CAIS.WebPortal.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // When file is linked, it is not added to configuration
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var builder = WebSetupConfig.CustomConfigureBuilder(args, config);
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            WebSetupConfig.CustomConfigureApp(app);

            app.Run();
        }
    }
}