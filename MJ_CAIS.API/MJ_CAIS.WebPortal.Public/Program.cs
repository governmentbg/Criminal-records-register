using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using MJ_CAIS.WebPortal.Public.Utils.Mappings;
using MJ_CAIS.WebSetup;
using MJ_CAIS.WebSetup.Utils;
using System.Security.Principal;

namespace MJ_CAIS.WebPortal.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebSetupConfig.CustomConfigureBuilder(args);
            builder.Services.AddAutoMapper(typeof(ConvictionProfile).Assembly);
            builder.Services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/Forbidden/";
                    options.LoginPath = "/Account/Login";
                    options.Cookie.Name = "CaisPublicPortal";
                });

            var app = builder.Build();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            var cookiePolicyOptions = new CookiePolicyOptions();
            app.UseCookiePolicy(cookiePolicyOptions);

            WebSetupConfig.CustomConfigureApp(app);

            app.Run();
        }
    }
}