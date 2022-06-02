using Microsoft.Extensions.Configuration;
using MJ_CAIS.Common.Helpers;
using MJ_CAIS.RegiX;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Xml;

namespace MJ_CAIS.ExternalWebServices
{
    public class WebServiceClient
    {
        private readonly IConfiguration config;

        public WebServiceClient(IConfiguration config)
        {
            this.config = config;
        }

        public ServiceResultData CallRegixExecuteSynchronous(string xml, string webServiceName, CallContext callContext, string citizenEGN)
        {
            var coreUrl = config.GetValue<string>("RegiX:CoreUrl");

            var binding = new WSHttpBinding(SecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;

            var client = new RegiXEntryPointClient(binding, new EndpointAddress(coreUrl));
            client.ClientCredentials.ClientCertificate.Certificate = GetRegixCertificate();

            var doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            doc.LoadXml(xml);

            var request = new ExecuteSynchronousRequest
            {
                request = new ServiceRequestData
                {
                    Operation = webServiceName,
                    Argument = doc.DocumentElement,
                    CitizenEGN = citizenEGN,
                    EmployeeEGN = config.GetValue<string>("RegiX:EmployeeEGN"),
                    ReturnAccessMatrix = false,
                    SignResult = false,
                }
            };

            request.request.CallContext = callContext;
            var response = client.ExecuteSynchronous(request);
            if (!response.ExecuteSynchronousResult.HasError)
            {
                return response.ExecuteSynchronousResult;
            }
            else
            {
                throw new Exception(response.ExecuteSynchronousResult.Error);
            }
        }
   
        public CallContext CreateSampleCallContext(string serviceURI)
        {
            var section = config.GetSection("RegiX:CallContext");
            var callContext = new CallContext()
            {
                ServiceURI = serviceURI,
                AdministrationName = section.GetValue<string>("AdministrationName"),
                AdministrationOId = section.GetValue<string>("AdministrationOId"),
                EmployeeNames = section.GetValue<string>("EmployeeNames"),
                ServiceType = section.GetValue<string>("ServiceType"),
                EmployeeAditionalIdentifier = section.GetValue<string>("EmployeeAditionalIdentifier"),
                EmployeeIdentifier = section.GetValue<string>("EmployeeIdentifier"),
                EmployeePosition = section.GetValue<string>("EmployeePosition"),
                LawReason = section.GetValue<string>("LawReason"),
                Remark = section.GetValue<string>("Remark"),
                ResponsiblePersonIdentifier = section.GetValue<string>("ResponsiblePersonIdentifier"),
            };

            return callContext;
        }

        private X509Certificate2 GetRegixCertificate()
        {
            var section = config.GetSection("RegiX:CertificateData");

            var storeName = section.GetValue<StoreName>("storeName");
            var storeLocation = section.GetValue<StoreLocation>("storeLocation");
            var x509FindType = section.GetValue<X509FindType>("x509FindType");
            var findValue = section.GetValue<string>("findValue");

            var cert = CertificateHelper.GetX509Certificate2(storeName, storeLocation, x509FindType, findValue);
            return cert;
        }
    }
}
