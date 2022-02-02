using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Web.Setup;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using Microsoft.AspNet.OData.Extensions;

namespace MJ_CAIS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.Services.AddRazorPages();

            builder.Services.AddAutoMapper(typeof(BulletinProfile).Assembly);
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureCors();
            builder.Services.ConfigureDependencies(configuration);
            builder.Services.ConfigureOData();
            builder.Services.AddMvc(opt => opt.EnableEndpointRouting = false);

            builder.Services.AddControllers(opt =>
            {
                opt.UseCentralRoutePrefix(new RouteAttribute("api"));
            });

            var app = builder.Build();

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

            app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseRouting();

            // app.UseAuthentication(); // TODO:
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            // Make OData work with normal paths
            app.UseMvc(routeBuilder => routeBuilder.EnableDependencyInjection());

            app.Run();
        }
    }
}