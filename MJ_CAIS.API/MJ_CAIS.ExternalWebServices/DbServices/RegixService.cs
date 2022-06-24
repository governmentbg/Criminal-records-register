using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.RegiX;
using System.Xml;
using TechnoLogica.RegiX.GraoNBDAdapter;
using TechnoLogica.RegiX.MVRERChAdapterV2;

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
            string? wApplicationId = null,
            string? ecrisMsgId = null,
            bool isAsync = false)
        {
            var operation = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);

            var webRequestEntity = FactoryRegix.CreatePersonWebRequest(egn, isAsync, operation.Id, bulletinId, applicationId, wApplicationId, ecrisMsgId);
            _dbContext.SaveEntity(webRequestEntity);

            PersonDataResponseType response = null;
            if (!isAsync)
            {
                response = ExecutePersonDataSearch(webRequestEntity, operation.WebServiceName);
            }
            return (response, webRequestEntity);
        }
     
        public (ForeignIdentityInfoResponseType, EWebRequest) SyncCallPersonDataSearchByLNCH(string egn,
            string? bulletinId = null,
            string? applicationId = null,
            string? wApplicationId = null,
            string? ecrisMsgId = null)
        {
            var isAsync = false;
            var operation = GetOperationByType(WebServiceEnumConstants.REGIX_ForeignIdentityV2);

            var webRequestEntity = FactoryRegix.CreateForeignPersonWebRequest(egn, isAsync, operation.Id, bulletinId, applicationId, wApplicationId, ecrisMsgId);
            _dbContext.SaveEntity(webRequestEntity);
            var response = ExecutePersonDataSearchByLNCH(webRequestEntity, operation.WebServiceName);
            return (response, webRequestEntity);
        }

        /// <summary>
        /// Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string webServiceName)
        {
            var empty = new PersonDataResponseType();
            var emptyXml = XmlUtils.SerializeToXml(empty);

            var requestDeserialized = XmlUtils.DeserializeXml<PersonDataRequestType>(request.RequestXml);
            var citizenEgn = requestDeserialized.EGN;

            CallRegix(request, webServiceName, citizenEgn);

            PersonDataResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);
                var cache = AddOrUpdateCache(request, webServiceName, citizenEgn);
                PopulateObjects(request, cache);
            }

            _dbContext.SaveChanges();
            return responseObject;
        }

        /// <summary>
        /// Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public ForeignIdentityInfoResponseType ExecutePersonDataSearchByLNCH(EWebRequest request, string webServiceName)
        {
            var empty = new ForeignIdentityInfoResponseType();
            var emptyXml = XmlUtils.SerializeToXml(empty);

            var requestDeserialized = XmlUtils.DeserializeXml<ForeignIdentityInfoRequestType>(request.RequestXml);
            var citizenLNCH = requestDeserialized.Identifier;

            CallRegixLNCH(request, webServiceName, citizenLNCH);

            ForeignIdentityInfoResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<ForeignIdentityInfoResponseType>(request.ResponseXml);
                var cache = AddOrUpdateCacheLNCH(request, webServiceName, citizenLNCH);
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
                    application.FirstnameLat = cache.FirstnameLat;
                    application.Surname = cache.Surname;
                    application.SurnameLat = cache.SurnameLat;
                    application.Familyname = cache.Familyname;
                    application.FamilynameLat = cache.FamilynameLat;
                    application.Egn = cache.Egn;
                    application.Lnch = cache.Lnch;
                    application.BirthDate = cache.BirthDate;
                    //application.Sex = cache.s; TODO: ADD sex column to ERegixCache
                    application.BirthPlaceOther = cache.BirthDistrictName + " " + cache.BirthMunName + " " + cache.BirthCityName + " " + cache.BirthPlace;
                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        var country = await _dbContext.GCountries.FirstOrDefaultAsync(x => x.Iso3166Alpha2 == cache.BirthCountryCode.ToUpper());
                        if (country != null)
                        {
                            application.BirthCountry = country;
                            application.BirthCountryId = country.Id;
                        }
                    }
                    //foreach (var v in wapplication.AAppCitizenships)
                    //{
                    //    var newObj = new AAppCitizenship()
                    //    {
                    //        Id = BaseEntity.GenerateNewId(),
                    //        ApplicationId = appl.Id,
                    //        CountryId = v.CountryId
                    //    };
                    //    appl.AAppCitizenships.Add(newObj);
                    _dbContext.AApplications.Update(application);
                    //( cache.BirthCountryCode == null ? null :  cache.BirthCountryCode.ToUpper())
                }
            }
            if (!string.IsNullOrEmpty(request.WApplicationId))
            {
                var wapplication = await _dbContext.WApplications.FirstOrDefaultAsync(a => a.Id == request.WApplicationId);
                if (wapplication != null)
                {
                    wapplication.Firstname = cache.Firstname;
                    wapplication.FirstnameLat = cache.FirstnameLat; ;
                    wapplication.Surname = cache.Surname;
                    wapplication.SurnameLat = cache.SurnameLat;
                    wapplication.Familyname = cache.Familyname;
                    wapplication.FamilynameLat = cache.FamilynameLat;
                    wapplication.Egn = cache.Egn;
                    wapplication.Lnch = cache.Lnch;
                    wapplication.BirthDate = cache.BirthDate;
                    wapplication.BirthPlaceOther = cache.BirthDistrictName + " " + cache.BirthMunName + " " + cache.BirthCityName + " " + cache.BirthPlace;
                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        var country = await _dbContext.GCountries.FirstOrDefaultAsync(x => x.Iso3166Alpha2 == cache.BirthCountryCode.ToUpper());
                        if (country != null)
                        {
                            wapplication.BirthCountry = country;
                            wapplication.BirthCountryId = country.Id;
                        }
                    }
                    _dbContext.WApplications.Update(wapplication);

                }
            }
        }

        private void CallRegixLNCH(EWebRequest request, string webServiceName, string citizenLNCH)
        {
            var cachedResponse = CheckForCachedResponseLNCH(citizenLNCH, webServiceName);
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
                    var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, webServiceName, callContext, citizenLNCH);
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


        private ERegixCache CheckForCachedResponseLNCH(string citizenLNCH, string webServiceName)
        {
            var daysCache = GetRegixDaysCache();
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches
                .FirstOrDefault(r => r.Lnch == citizenLNCH && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
            return cachedResponse;
        }
        private ERegixCache CheckForCachedResponse(string egn, string webServiceName)
        {
            var daysCache = GetRegixDaysCache();
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches
                .FirstOrDefault(r => r.Egn == egn && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
            return cachedResponse;
        }

        private ERegixCache AddOrUpdateCacheLNCH(EWebRequest request, string webServiceName, string lnch)
        {
            var regixCache = _dbContext.ERegixCaches.FirstOrDefault(r => r.Lnch == lnch && r.WebServiceName == webServiceName);
            if (regixCache == null)
            {
                regixCache = new ERegixCache()
                {
                    Id = BaseEntity.GenerateNewId(),
                    Lnch = lnch,
                };

                _dbContext.ERegixCaches.Add(regixCache);
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.WebServiceName = webServiceName;
            regixCache.ExecutionDate = DateTime.Now;
            var responseObject = XmlUtils.DeserializeXml<ForeignIdentityInfoResponseType>(request.ResponseXml);

            if (webServiceName.EndsWith("GetForeignIdentityV2"))
            {
                regixCache.Egn = responseObject.EGN;

                regixCache.FirstnameLat = (string)responseObject.PersonNames.FirstNameLatin;
                regixCache.SurnameLat = (string)responseObject.PersonNames.SurnameLatin;
                regixCache.FamilynameLat = (string)responseObject.PersonNames.LastNameLatin;

                regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
                regixCache.Surname = (string)responseObject.PersonNames.Surname;
                regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

                regixCache.BirthCountryName = responseObject.BirthPlace.CountryName;
                regixCache.BirthCountryCode = responseObject.BirthPlace.CountryCode;

                regixCache.BirthMunName = responseObject.BirthPlace.MunicipalityName;
            }


            // TODO: based on operation, parse different objects and fill data

            return regixCache;
        }
        private ERegixCache AddOrUpdateCache(EWebRequest request, string webServiceName, string egn)
        {
            var regixCache = _dbContext.ERegixCaches.FirstOrDefault(r => r.Egn == egn && r.WebServiceName == webServiceName);
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
            regixCache.WebServiceName = webServiceName;
            regixCache.ExecutionDate = DateTime.Now;
            var responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);


            if (webServiceName.EndsWith("PersonDataSearch"))
            {
                regixCache.BirthDate = responseObject.BirthDate;
                regixCache.Egn = responseObject.EGN;

                regixCache.FirstnameLat = (string)responseObject.LatinNames.FirstName;
                regixCache.SurnameLat = (string)responseObject.LatinNames.SurName;
                regixCache.FamilynameLat = (string)responseObject.LatinNames.FamilyName;

                regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
                regixCache.Surname = (string)responseObject.PersonNames.SurName;
                regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

                regixCache.BirthCountryName = responseObject.Nationality.NationalityName;
                regixCache.BirthCountryCode = responseObject.Nationality.NationalityCode;
                regixCache.BirthPlace = responseObject.PlaceBirth;

            }

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
            else if (request.WApplicationId != null)
            {
                serviceURI = "Във връзка с електронно заявление: " + request.WApplicationId;
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
