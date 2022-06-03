using MJ_CAIS.WebSetup;

namespace MJ_CAIS.WebPortal.External
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebSetupConfig.CustomConfigureBuilder(args);
            var app = builder.Build();
            
            WebSetupConfig.CustomConfigureApp(app);
            app.Run();
        }
    }
}