using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.ExternalWebServices.Schemas.PersonValidator;
using System.Net.Http.Headers;
using System.Text;
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

        private readonly ILogger<PersonValidatorClient> _logger;
        public PersonValidatorClient(IConfiguration config, ILogger<PersonValidatorClient> logger)
        {
            _configuration = config;
            _logger = logger;
            Host = config.GetValue<string>("PersonValidator:WebServiceUrl");
            ApiEndpoint = config.GetValue<string>("PersonValidator:PersonApiEndpoint");
        }

        private async Task<PersonInfoResponse> GetPersonInfo(PersonInfoRequest request)
        {
            if (string.IsNullOrEmpty(request.fname))
            {
                throw new Exception("Първо име не е въведено.");
            }

            if(string.IsNullOrEmpty(request.year) || string.IsNullOrEmpty(request.month) || string.IsNullOrEmpty(request.day))
            {
                throw new Exception("Рожденната дата не е коректна.");
            }
            try
            {
                var date = new DateTime(Int32.Parse(request.year), Int32.Parse(request.month), Int32.Parse(request.day));
            }
            catch (Exception ex)
            {
                throw new Exception("Рожденната дата не е коректна.");
            }

            var httpClient = GetHttpClient(_configuration);
            HttpResponseMessage data;
            var dict = request.GetKeyValuePairs();
            //if (!String.IsNullOrEmpty(request.fname) && !String.IsNullOrEmpty(request.sname) && !String.IsNullOrEmpty(request.lname))
            //{
              
            //    dict.Remove("fullname");
            //    var postData = new FormUrlEncodedContent(dict);
            //    data = await httpClient.PostAsync(ApiEndpoint, postData);
            //}
            //else
            //{
              
            //    dict.Remove("fname");
            //    dict.Remove("sname");
            //    dict.Remove("lname");
                var postData = new FormUrlEncodedContent(dict);
                data = await httpClient.PostAsync(ApiEndpoint, postData);
            //}

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

        public async Task<List<PersonData>> GetPersonInfo(string firstname, string? surname, string? familyname, PersonInfoGenderType sex, DateTime birthDate, string matchTreshold = "0.6")
        {
            try
            {
                PersonInfoRequest reqidentifiacition = new PersonInfoRequest();
                //if (String.IsNullOrEmpty(firstname) || String.IsNullOrEmpty(surname) || String.IsNullOrEmpty(familyname))
                //{
                //    string fullname = String.Join(' ', firstname?.Trim(), surname?.Trim(), familyname?.Trim());
                //    var separatedNames = fullname.Split(' ');
                //    if (separatedNames.Length > 0)
                //    {
                //        reqidentifiacition.fname = separatedNames[0].Trim();
                //        reqidentifiacition.sname = separatedNames.Length>2 ? separatedNames[1].Trim() : throw new Exception("Невалидни параметри");
                //        reqidentifiacition.lname = separatedNames.Length > 3 ? string.Join(' ',separatedNames.Skip(2)).Trim() : throw new Exception("Невалидни параметри");
                //    }
                //    else
                //    {
                //        throw new Exception("Невалидни параметри");
                //    }
                //}
                //else
                //{
                    reqidentifiacition.fname = firstname;
                    reqidentifiacition.sname = surname;
                    reqidentifiacition.lname = familyname;

               // }
                reqidentifiacition.threshold = matchTreshold;
                reqidentifiacition.gender = sex;
                reqidentifiacition.month = birthDate.Month.ToString();
                reqidentifiacition.year = birthDate.Year.ToString();
                reqidentifiacition.day = birthDate.Day.ToString();

                var result = await GetPersonInfo(reqidentifiacition);

                return result.personData.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"PersonIdentification: " + ex.Message, Host, ApiEndpoint, firstname,  surname, familyname,  sex, birthDate, matchTreshold);
                throw ex;
            }

        }
    }
}
