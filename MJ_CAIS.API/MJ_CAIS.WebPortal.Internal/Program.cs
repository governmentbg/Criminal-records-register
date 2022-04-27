using MJ_CAIS.WebSetup;

namespace MJ_CAIS.WebPortal.Internal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // When file is linked, it is not added to configuration
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            
            var builder = WebSetupConfig.ConfigureBuilder(args, config);
            var app = builder.Build();
            
            WebSetupConfig.ConfigureApp(app);
            app.Run();
        }
    }
}