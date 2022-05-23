using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MJ_CAIS.WebSetup.Utils
{
    public class RequestUtils
    {
        private readonly IConfiguration _config;

        public RequestUtils(IConfiguration config)
        {
            _config = config;
        }

        public bool IsHostedBehindLoadBalancer()
        {
            var data = _config.GetValue<string>("HostedBehindLoadBalancer");
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }

            var result = Convert.ToBoolean(data);
            return result;
        }

        public string? GetClientIpAddress(HttpContext httpContext)
        {
            string? userIP = httpContext.Connection.RemoteIpAddress?.ToString();
            bool isLoadBalancer = IsHostedBehindLoadBalancer();
            if (isLoadBalancer)
            {
                var header = httpContext.Request.Headers["X-Forwarded-For"];
                if (!string.IsNullOrEmpty(header))
                {
                    userIP = header;
                }
            }

            return userIP;
        }
    }
}
