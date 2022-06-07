using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace MJ_CAIS.PersonValidator
{
    public class PersonValidatorClient
    {
        private static string Host;
        private static string ApiEndpoint;

        private readonly HttpClient _httpClient;

        public static PersonValidatorClient CreateClient(IConfiguration config)
        {
            Host = config.GetValue<string>("PersonValidator:WebServiceUrl");
            ApiEndpoint = config.GetValue<string>("PersonValidator:PersonApiEndpoint");

            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Host)
            };

            ConfigureHttpClient(httpClient, Host);

            return new PersonValidatorClient(httpClient);
        }

        private PersonValidatorClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PersonInfoResponse> GetPersonInfo(PersonInfoRequest request)
        {
            var postData = new FormUrlEncodedContent(request.GetKeyValuePairs());

            var data = await this._httpClient.PostAsync(ApiEndpoint, postData);
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
