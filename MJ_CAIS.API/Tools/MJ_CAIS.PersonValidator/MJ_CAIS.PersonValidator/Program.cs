using Microsoft.Extensions.Configuration;

namespace MJ_CAIS.PersonValidator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

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