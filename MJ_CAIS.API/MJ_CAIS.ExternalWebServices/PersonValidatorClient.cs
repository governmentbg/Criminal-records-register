using Microsoft.Extensions.Configuration;
using MJ_CAIS.ExternalWebServices.Schemas.PersonValidator;
using System.Text.Json;

namespace MJ_CAIS.ExternalWebServices
{
    public class PersonValidatorClient
    {
        // Singleton, HttpClient specific for person validation
        private static HttpClient _httpClient;
        private static HttpClient GetHttpClient(IConfiguration config)
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(Host)
                };

                ConfigureHttpClient(_httpClient, Host);
            }

            return _httpClient;
        }

        public static string Host;
        public static string ApiEndpoint;

        private readonly IConfiguration _configuration;

        public PersonValidatorClient(IConfiguration config)
        {
            _configuration = config;
            Host = config.GetValue<string>("PersonValidator:WebServiceUrl");
            ApiEndpoint = config.GetValue<string>("PersonValidator:PersonApiEndpoint");
        }

        public async Task<PersonInfoResponse> GetPersonInfo(PersonInfoRequest request)
        {
            var httpClient = GetHttpClient(_configuration);
            var postData = new FormUrlEncodedContent(request.GetKeyValuePairs());

            var data = await httpClient.PostAsync(ApiEndpoint, postData);
            if (!data.IsSuccessStatusCode)
            {
                var errorContent = await data.Content.ReadAsStringAsync();
                var url = _httpClient.BaseAddress + ApiEndpoint;
                var message = $"Error! Url: {url} StatusCode: {(int)data.StatusCode}, Content: {errorContent}";
                throw new InvalidOperationException(message);
            }

            var resultString = await data.Content.ReadAsStringAsync();
            var personData = JsonSerializer.Deserialize<PersonData[]>(resultString);
            var result = new PersonInfoResponse(personData);
            return result;
        }

        internal static void ConfigureHttpClient(HttpClient httpClient, string host)
        {
            var headers = httpClient.DefaultRequestHeaders;
            headers.Accept.Clear();
            headers.Accept.Add(new("application/json"));
            headers.Add("Accept-Encoding", "gzip,deflate");
            headers.Add("Connection", "Keep-Alive");
            headers.Add("Host", new Uri(host).Host);
        }
    }
}
