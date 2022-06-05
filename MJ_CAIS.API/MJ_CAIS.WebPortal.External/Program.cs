using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MJ_CAIS.WebSetup;

namespace MJ_CAIS.WebPortal.External
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebSetupConfig.CustomConfigureBuilder(args);
            //builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);
            builder.Services.AddControllersWithViews();

            var configuration = builder.Configuration;

            // TODO: 
            //builder.Services.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie(options =>
            //    {
            //        options.Cookie.Name = "mvccode";
            //    })
            //    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = configuration.GetValue<string>("Authentication:TokenIssuer");
            //        options.ClientId = configuration.GetValue<string>("Authentication:ClientId");
            //        options.ClientSecret = configuration.GetValue<string>("Authentication:ClientSecret");
            //        options.AuthenticationMethod = OpenIdConnectRedirectBehavior.FormPost;

            //        // code flow + PKCE (PKCE is turned on by default)
            //        options.ResponseType = "code";
            //        options.UsePkce = true;

            //        options.Scope.Clear();
            //        options.Scope.Add("openid");
            //        options.Scope.Add("profile");

            //        // keeps id_token smaller
            //        options.GetClaimsFromUserInfoEndpoint = false;
            //        options.SaveTokens = true;

            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            NameClaimType = JwtClaimTypes.Name,
            //            RoleClaimType = JwtClaimTypes.Role,
            //        };
            //    });

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