using Microsoft.AspNet.OData.Extensions;
using MJ_CAIS.WebPortal.Public.Utils.Mappings;
using MJ_CAIS.WebSetup;
using MJ_CAIS.WebSetup.Setup;

namespace MJ_CAIS.WebPortal.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebSetupConfig.CustomConfigureBuilder(args);
            builder.Services.AddAutoMapper(typeof(ConvictionProfile).Assembly);
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