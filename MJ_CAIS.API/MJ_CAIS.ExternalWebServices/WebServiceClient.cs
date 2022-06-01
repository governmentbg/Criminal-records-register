using Microsoft.Extensions.Configuration;
using MJ_CAIS.RegiX;
using System.Xml;
using static MJ_CAIS.RegiX.RegiXEntryPointClient;

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
            var client = new RegiXEntryPointClient(EndpointConfiguration.WSHttpBinding_IRegiXEntryPoint, coreUrl);

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
    }
}
