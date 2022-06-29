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

            var cscMainConnString = config.GetConnectionString("CSCMAIN_ConnectionString");

            //CSCMain.GetData(cscMainConnString, dbConnString);
            //Fbbc.GetData(fbbcConnString, dbConnString);
            Esgraon.GetData(cscMainConnString, dbConnString);
        }
    }
}