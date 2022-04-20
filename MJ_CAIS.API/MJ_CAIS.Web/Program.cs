using MJ_CAIS.WebSetup;

namespace MJ_CAIS.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebSetupConfig.ConfigureBuilder(args);
            var app = builder.Build();

            WebSetupConfig.ConfigureApp(app);
            app.Run();
        }
    }
}