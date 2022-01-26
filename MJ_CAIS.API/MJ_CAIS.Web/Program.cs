using Microsoft.AspNetCore.Mvc;
using MJ_CAIS.Web.Setup;

namespace MJ_CAIS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureCors();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}