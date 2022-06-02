﻿using Microsoft.EntityFrameworkCore;
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
                var cache = AddOrUpdateCache(request, citizenEgn);
                PopulateObjects(request, cache);
            }
            
            _dbContext.SaveChanges();
            return responseObject;
        }

        private async void PopulateObjects(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.ApplicationId))
            {
              var application = await _dbContext.AApplications.FirstOrDefaultAsync(a => a.Id == request.ApplicationId);
                if (application != null)
                {
                    application.Firstname = cache.Firstname;
                    application.FirstnameLat = cache.FirstnameLat; ;
                    application.Surname = cache.Surname;
                    application.SurnameLat = cache.SurnameLat;
                    application.Familyname = cache.Familyname;
                    application.FamilynameLat = cache.FamilynameLat;
                    application.Egn= cache.Egn;
                    application.Lnch = cache.Lnch;
                    application.BirthDate = cache.BirthDate;
                    application.BirthPlaceOther = cache.BirthDistrictName + " " + cache.BirthMunName + " "+ cache.BirthCityName + " " +cache.BirthPlace;
                    var country = await _dbContext.GCountries.FirstOrDefaultAsync(x => x.Iso3166Alpha2 == cache.BirthCountryCode.ToUpper());
                    if (country != null)
                    {
                        application.BirthCountry = country;
                        application.BirthCountryId = country.Id;
                    }
                    _dbContext.AApplications.Update(application);
                      
                }
            }
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

        private ERegixCache AddOrUpdateCache(EWebRequest request, string egn)
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

            return regixCache;
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
    }
}
