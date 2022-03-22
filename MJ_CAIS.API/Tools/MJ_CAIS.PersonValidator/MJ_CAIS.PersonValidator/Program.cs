using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MJ_CAIS.PersonValidator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args).Build();
            var config = host.Services.GetRequiredService<IConfiguration>();
            var webServiceUrl = config.GetValue<string>("WebServiceUrl");
            var client = PersonValidatorClientFactory.Create(webServiceUrl);

            try
            {
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

                var personInfo = await client.GetPersonInfo(searchCriteria);
                Console.WriteLine(personInfo.personData.Length);
            }
            catch (Exception ex)
            {
                // TODO: Log error
                throw;
            }
        }
    }
}