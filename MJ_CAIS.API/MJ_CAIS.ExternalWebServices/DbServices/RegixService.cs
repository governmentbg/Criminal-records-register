using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.RegiX;
using System.Xml;
using TechnoLogica.RegiX.GraoNBDAdapter;

namespace MJ_CAIS.ExternalWebServices.DbServices
{
    public class RegixService : IRegixService
    {
        private readonly WebServiceClient _client;
        private readonly IConfiguration _config;
        private readonly CaisDbContext _dbContext;
        private Dictionary<string, (string Id, string WebServiceName)> webServiceTypes;

        public RegixService(CaisDbContext dbContext, IConfiguration config)
        {
            _dbContext = dbContext;
            _config = config;
            _client = new WebServiceClient(config);
        }

        public List<EWebRequest> GetRequestsForAsyncExecution()
        {
            int attempts = GetRegixAttempts();
            var result = _dbContext.EWebRequests.Include(x => x.WebService)
                .Where(x => x.IsAsync == true || x.IsAsync == null)
                .Where(x => x.Status == WebRequestStatusConstants.Pending || x.Status == WebRequestStatusConstants.Rejected)
                .Where(x => x.Attempts < attempts)
                .ToList();

            return result;
        }

        public (PersonDataResponseType, EWebRequest) SyncCallPersonDataSearch(string egn,
            string? bulletinId = null,
            string? applicationId = null,
            string? ecrisMsgId = null)
        {
            var isAsync = false;
            var operation = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);

            var webRequestEntity = FactoryRegix.CreatePersonWebRequest(egn, isAsync, operation.Id, bulletinId, applicationId, ecrisMsgId);
            _dbContext.SaveEntity(webRequestEntity);
            var response = ExecutePersonDataSearch(webRequestEntity, operation.WebServiceName);
            return (response, webRequestEntity);
        }

        /// <summary>
        /// Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string webServiceName)
        {
            var requestDeserialized = XmlUtils.DeserializeXml<PersonDataRequestType>(request.RequestXml);
            var citizenEgn = requestDeserialized.EGN;

            CallRegix(request, webServiceName, citizenEgn);

            PersonDataResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);
                AddOrUpdateCache(request, citizenEgn);
            }
            
            _dbContext.SaveChanges();
            return responseObject;
        }

        private void CallRegix(EWebRequest request, string webServiceName, string citizenEgn)
        {
            var cachedResponse = CheckForCachedResponse(citizenEgn, webServiceName);
            if (cachedResponse != null)
            {
                request.ResponseXml = cachedResponse.ResponseXml;
                request.IsFromCache = true;
            }
            else
            {
                request.Attempts += 1;
                request.IsFromCache = false;

                try
                {
                    var callContext = CreateCallContext(request);
                    var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, webServiceName, callContext, citizenEgn);
                    request.ResponseXml = resultData.Data.Response.Any.OuterXml;
                    request.ResponseXml = AddXmlSchema(request.ResponseXml);
                }
                catch (Exception ex)
                {
                    request.HasError = true;
                    request.Error = ex.Message;
                    request.StackTrace = ex.StackTrace;
                }
            }

            request.ExecutionDate = DateTime.Now;
            request.Status = request.HasError == true ?
                WebRequestStatusConstants.Rejected :
                WebRequestStatusConstants.Accepted;
        }

        private ERegixCache CheckForCachedResponse(string egn, string webServiceName)
        {
            var daysCache = GetRegixDaysCache();
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches
                .FirstOrDefault(r => r.Egn == egn && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
            return cachedResponse;
        }

        private void AddOrUpdateCache(EWebRequest request, string egn)
        {
            var operation = request.WebService.WebServiceName;
            var regixCache = _dbContext.ERegixCaches.FirstOrDefault(r => r.Egn == egn && r.WebServiceName == operation);
            if (regixCache == null)
            {
                regixCache = new ERegixCache()
                {
                    Id = BaseEntity.GenerateNewId(),
                    Egn = egn,
                };

                _dbContext.ERegixCaches.Add(regixCache);
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.WebServiceName = operation;
            regixCache.ExecutionDate = DateTime.Now;

            // TODO: based on operation, parse different objects and fill data
        }
        
        private (string Id, string WebServiceName) GetOperationByType(string typeCode)
        {
            // Singleton pattern
            if (webServiceTypes == null)
            {
                var types = _dbContext.EWebServices.AsNoTracking().ToList();

                webServiceTypes = new Dictionary<string, (string id, string webServiceName)>();
                foreach (var type in types)
                {
                    webServiceTypes.Add(type.TypeCode, (type.Id, type.WebServiceName));
                }
            }

            return webServiceTypes[typeCode];
        }

        private int GetRegixAttempts()
        {
            var maxNumberOfAttempts = _dbContext.GSystemParameters.AsNoTracking()
                .FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.REGIX_NUMBER_OF_ATTEMPTS)
                ?.ValueNumber;

            int result = maxNumberOfAttempts != null ? (int)maxNumberOfAttempts : 5;
            return result;
        }

        private int GetRegixDaysCache()
        {
            var maxNumberOfAttempts = _dbContext.GSystemParameters.AsNoTracking()
                .FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.REGIX_NUMBER_OF_ATTEMPTS)
                ?.ValueNumber;

            int result = maxNumberOfAttempts != null ? (int)maxNumberOfAttempts : 5;
            return result;
        }

        private CallContext CreateCallContext(EWebRequest request)
        {
            // Default value
            string serviceURI = "ЦАИС_" + DateTime.Now.Date.ToString("dd-MM-yyyy");

            if (request.ApplicationId != null)
            {
                serviceURI = "Във връзка със заявление: " + request.ApplicationId;
            }
            else if (request.BulletinId != null)
            {
                serviceURI = "Във връзка с бюлетин: " + request.BulletinId;
            }
            else if (request.EcrisMsgId != null)
            {
                serviceURI = "Във връзка със съобщение от ЕКРИС: " + request.EcrisMsgId;
            }

            var callContext = _client.CreateSampleCallContext(serviceURI);
            // TODO: fill other data for user that created the web request

            return callContext;
        }

        private string AddXmlSchema(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }

            try
            {
                // Fixing bug with schema validation
                var doc1 = new XmlDocument();
                doc1.LoadXml(xml);
                doc1.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
                var resultXml = doc1.OuterXml;

                return resultXml;
            }
            catch (Exception)
            {
                return xml;
            }
        }

    }
}
