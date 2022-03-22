namespace MJ_CAIS.PersonValidator
{
    public static class PersonValidatorClientFactory
    {
        public static PersonValidatorClient Create(string host)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri(host)
            };
        
            ConfigureHttpClient(httpClient, host);

    	    return new PersonValidatorClient(httpClient);
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
