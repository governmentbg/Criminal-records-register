using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Net.Http.Headers;
using MJ_CAIS.DIContainer;

namespace MJ_CAIS.Web.Setup
{
    public static class ServiceExtensions
    {
        private const string MediaTypeHeader = "application/prs.mock-odata";

        public static void ConfigureCors(this IServiceCollection services, string origin = null)
        {
            // TODO: use AllowedHosts in configuration

            services.AddCors(options =>
            {
                options.AddPolicy("DevCorsPolicy",
                    builder =>
                    {
                        builder.AllowAnyMethod().AllowAnyHeader();

                        if (string.IsNullOrEmpty(origin))
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            builder.WithOrigins(origin);
                        }
                    });
            });
        }

        public static void ConfigureDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpContextAccessor();

            // services.AddSingleton<JwtManager>(); // TODO: 

            ContainerExtension.Initialize(services, configuration);

            // services.AddTransient<IAuthenticationService, AuthenticationService<User>>(); // TODO: 
        }

        public static void ConfigureOData(this IServiceCollection services)
        {
            services.AddOData();
            services.AddODataQueryFilter();

            // Configuration to make Swagger work with OData
            services.AddMvc(op =>
            {
                var outputFormatters = op.OutputFormatters.OfType<ODataOutputFormatter>().Where(it => !it.SupportedMediaTypes.Any());
                foreach (var formatter in outputFormatters)
                {
                    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypeHeader));
                }

                var inputFormatters = op.InputFormatters.OfType<ODataInputFormatter>().Where(it => !it.SupportedMediaTypes.Any());
                foreach (var formatter in inputFormatters)
                {
                    formatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue(MediaTypeHeader));
                }
            });
        }

        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Insert(0, new CentralizedPrefixConvention(routeAttribute));
        }

        public static void UseCentralRoutePrefix(this MvcOptions opts, string prefix)
        {
            opts.UseCentralRoutePrefix(new RouteAttribute(prefix));
        }
    }
}
