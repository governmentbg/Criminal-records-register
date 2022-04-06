using Microsoft.Extensions.Configuration;

namespace DBProject
{
    public class Program
    {
        public static void Main()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            IConfiguration config = configBuilder.Build();

            var fbbcConnString = config.GetConnectionString("FBBCConnectionString");
            var dbConnString = config.GetConnectionString("DefaultConnectionString");
            Fbbc.GetData(fbbcConnString, dbConnString);
        }
    }
}