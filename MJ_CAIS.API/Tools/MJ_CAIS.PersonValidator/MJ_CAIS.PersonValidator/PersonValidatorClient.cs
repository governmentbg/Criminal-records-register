using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MJ_CAIS.PersonValidator
{
    public class PersonValidatorClient
    {
        private readonly HttpClient httpClient;
        private const string ApiEndpoint = "ApiServlet";

        public PersonValidatorClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PersonInfoResponse> GetPersonInfo(PersonInfoRequest request)
        {
            var postData = new FormUrlEncodedContent(request.GetKeyValuePairs());

            var data = await this.httpClient.PostAsync(ApiEndpoint, postData);
            if (!data.IsSuccessStatusCode)
            {
                var errorContent = await data.Content.ReadAsStringAsync();
                var url = httpClient.BaseAddress + ApiEndpoint;
                var message = $"Error! Url: {url} StatusCode: {(int)data.StatusCode}, Content: {errorContent}";
                throw new InvalidOperationException(message);
            }

            var resultString = await data.Content.ReadAsStringAsync();
            var personData = JsonSerializer.Deserialize<PersonData[]>(resultString);
            var result = new PersonInfoResponse(personData);
            return result;
        }
    }
}
