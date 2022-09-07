using FluentValidation.AspNetCore;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.FluentValidators.Bulletin;
using MJ_CAIS.WebSetup.Setup;
using MJ_CAIS.WebSetup.Utils.CustomModelBinders;

namespace MJ_CAIS.WebSetup
{
    public class WebSetupConfig
    {
        public static WebApplicationBuilder CustomConfigureBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddRazorPages();

            builder.Services.AddAutoMapper(typeof(BulletinProfile).Assembly);
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureDependencies(configuration);
            builder.Services.ConfigureOData();
            builder.Services.ConfigureUserContext();
            builder.Services.AddMvc(opt =>
            {
                opt.ModelBinderProviders.Insert(0, new CustomDateTimeModelBinderProvider());
                opt.EnableEndpointRouting = false;
            }
            );

            builder.Services.AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssembly(typeof(DocumentValidator).Assembly);
                conf.ImplicitlyValidateChildProperties = true;
            });

            // Temporary moved to main Web project until authentication is the same
            //builder.Services.AddAuthorization();
            //builder.Services
            //    .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = configuration.GetValue<string>("Authentication:TokenIssuer");
            //        options.ApiName = configuration.GetValue<string>("Authentication:APIName");
            //        options.ApiSecret = configuration.GetValue<string>("Authentication:APISecret"); ;
            //        options.EnableCaching = false; // Do not cache credentials - always rely on identity server
            //     });
            //IdentityModelEventSource.ShowPII = true;

            return builder;
        }

        public static void CustomConfigureApp(WebApplication app, bool useMiddlewareForException = false)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseConfiguredSwagger();
                app.UseCors("DevCorsPolicy");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            });

            if (useMiddlewareForException)
            {
                app.UseMiddleware<ErrorHandlingMiddleware>();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            // Make OData work with normal paths
            app.UseMvc(routeBuilder => routeBuilder.EnableDependencyInjection());
        }
    }
}