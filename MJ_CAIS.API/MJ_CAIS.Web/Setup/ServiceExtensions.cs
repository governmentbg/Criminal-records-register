using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace MJ_CAIS.Web.Setup
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services, string origin = null)
        {
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
