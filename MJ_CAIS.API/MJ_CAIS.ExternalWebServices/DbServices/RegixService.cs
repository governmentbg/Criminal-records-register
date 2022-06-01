using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
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
            var result = _dbContext.EWebRequests.Include(x => x.WebService)
                .Where(x => x.Status == WebRequestStatusConstants.Pending || x.Status == WebRequestStatusConstants.Rejected)
                .Where(x => x.Attempts < 5) // TODO: from parameter
                .ToList();

            return result;
        }

        /// <summary>
        /// Извиква заявка от regix синхронно
        /// </summary>
        /// <returns></returns>
        public PersonDataResponseType CallPersonDataSearch(string egn, 
            string serviceURI,
            string? bulletinId = null,
            string? applicationId = null,
            string? ecrisMsgId = null)
        {
            var request = new PersonDataRequestType { EGN = egn };
            var requestXml = XmlUtils.SerializeToXml(request);

            var webRequestEntity = CreateWebRequest(bulletinId, applicationId, ecrisMsgId);
            webRequestEntity.RequestXml = requestXml;
            webRequestEntity.WebServiceId = GetOperation(WebServiceEnumConstants.REGIX_PersonDataSearch).Id;
            // TODO: add flag for sync

            _dbContext.SaveEntity(webRequestEntity);
            var response = ExecutePersonDataSearch(webRequestEntity, serviceURI);
            return response;
        }

        /// <summary>
        /// Изпълнява заявка от таблицата с web requests, асинхронното приложение го извиква
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string serviceURI)
        {
            string? resultXml;

            var requestDeserialized = XmlUtils.DeserializeXml<PersonDataRequestType>(request.RequestXml);
            var operation = GetOperation(WebServiceEnumConstants.REGIX_PersonDataSearch).WebServiceName;

            var cachedResponse = CheckForCachedResponse(requestDeserialized.EGN, operation);
            if (cachedResponse != null)
            {
                resultXml = cachedResponse.ResponseXml;
                request.IsFromCache = true;
            }
            else
            {
                request.Attempts += 1;
                var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, operation, serviceURI, requestDeserialized.EGN);
                resultXml = resultData.Data.Response.Any.OuterXml;
            }

            var responseObject = GetPersonDataFromResponse(resultXml);

            AddOrUpdateCache(requestDeserialized.EGN
                , request.RequestXml
                , resultXml
                , operation
                , responseObject.PersonNames.FirstName.ToString()
                , responseObject.PersonNames.SurName.ToString()
                , responseObject.PersonNames.FamilyName.ToString());

            request.ResponseXml = resultXml;
            request.Status = WebRequestStatusConstants.Accepted;

            _dbContext.SaveChanges();

            return responseObject;
        }

        private EWebRequest CreateWebRequest(string? bulletinId = null, string? applicationId = null, string? ecrisMsgId = null)
        {
            var result = new EWebRequest()
            {
                BulletinId = bulletinId,
                ApplicationId = applicationId,
                EcrisMsgId = ecrisMsgId,
                Status = WebRequestStatusConstants.Pending,
                EntityState = EntityStateEnum.Added,
            };

            return result;
        }

        private void AddOrUpdateCache(string egn
            , string requestXml
            , string responseXml
            , string operation
            , string firstName
            , string surname
            , string familyName)
        {
            var oldCachedResponse = _dbContext.ERegixCaches.FirstOrDefault(r => r.Egn == egn && r.WebServiceName == operation);
            if (oldCachedResponse == null)
            {
                _dbContext.ERegixCaches.Add(new ERegixCache()
                {
                    Id = BaseEntity.GenerateNewId(),
                    Egn = egn,
                    RequestXml = requestXml,
                    ResponseXml = responseXml,
                    Firstname = firstName,
                    Surname = surname,
                    Familyname = familyName,
                    WebServiceName = operation,
                    ExecutionDate = DateTime.Now,
                    CreatedOn = DateTime.Now,
                    CreatedBy = nameof(ExternalWebServices),
                });
            }
            else
            {
                oldCachedResponse.RequestXml = requestXml;
                oldCachedResponse.ResponseXml = responseXml;
                oldCachedResponse.ExecutionDate = DateTime.Now;
                oldCachedResponse.Firstname = firstName;
                oldCachedResponse.Surname = surname;
                oldCachedResponse.Familyname = familyName;
            }
        }

        private ERegixCache CheckForCachedResponse(string egn, string operation)
        {
            // TODO: get from system parameter
            var daysCache = _config.GetValue<int>("ProjectSettings:DaysCache");
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches
                .FirstOrDefault(r => r.Egn == egn && r.ExecutionDate > yesterday && r.WebServiceName == operation);
            return cachedResponse;
        }

        private PersonDataResponseType GetPersonDataFromResponse(string? xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.DocumentElement.SetAttribute("xmlns:xsd", "http://www.w3.org/2001/XMLSchema");
            var responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(doc.OuterXml);
            return responseObject;
        }

        private (string Id, string WebServiceName) GetOperation(string typeCode)
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
    }
}
