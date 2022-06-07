using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MJ_CAIS.PersonValidator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => services.AddSingleton(PersonValidatorClient.CreateClient(config)))
                .Build();

            using (host)
            {
                var client = host.Services.GetService<PersonValidatorClient>();

                var searchCriteria = new PersonInfoRequest
                {
                    year = "2001",
                    month = "1",
                    day = "1",
                    gender = GenderType.male.ToString(),
                    fname = "tsanko",
                    sname = "tsankov",
                    lname = "cankov",
                    threshold = "0.6",
                };

                try
                {
                    var personInfo = await client.GetPersonInfo(searchCriteria);
                    Console.WriteLine(personInfo.personData.Length);
                }
                catch (Exception ex)
                {
                    // TODO: log error
                }
            }
        }
    }
}