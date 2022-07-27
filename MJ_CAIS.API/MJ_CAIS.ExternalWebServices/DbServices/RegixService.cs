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
            var attempts = GetRegixAttempts();
            var result = _dbContext.EWebRequests.Include(x => x.WebService)
                .Where(x => x.IsAsync == true || x.IsAsync == null)
                .Where(x => x.Status == WebRequestStatusConstants.Pending ||
                            x.Status == WebRequestStatusConstants.Rejected)
                .Where(x => x.Attempts < attempts)
                .ToList();

            return result;
        }


        public void CreateRegixRequests(string egn,
            string wApplicationId)
        {
            var operationPDS = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);
            EWebRequest eWRequestPDS = FactoryRegix.CreatePersonWebRequest(egn: egn, isAsync: true, operationPDS.Id, null, wApplicationId: wApplicationId);

            var operationRS = GetOperationByType(WebServiceEnumConstants.REGIX_RelationsSearch);
            EWebRequest eWRequestRS = FactoryRegix.CreatePersonRelationsWebRequest(egn: egn, isAsync: true, operationRS.Id, null, wApplicationId: wApplicationId);

            _dbContext.SaveChanges();
        }

        public (PersonDataResponseType, EWebRequest) SyncCallPersonDataSearch(string egn,
            string? applicationId = null,
            string? wApplicationId = null)
        {

            var operationPDS = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);
            EWebRequest eWRequestPDS = FactoryRegix.CreatePersonWebRequest(egn: egn, isAsync: true, operationPDS.Id, applicationId, wApplicationId: wApplicationId);

            var operationRS = GetOperationByType(WebServiceEnumConstants.REGIX_RelationsSearch);
            EWebRequest eWRequestRS = FactoryRegix.CreatePersonRelationsWebRequest(egn: egn, isAsync: true, operationRS.Id, applicationId, wApplicationId: wApplicationId);

            _dbContext.SaveChanges();

            
            var responsePDS = ExecutePersonDataSearch(eWRequestPDS, operationPDS.WebServiceName, egn);
            var responseRS = ExecuteRelationsSearch(eWRequestRS, operationRS.WebServiceName, egn);

            return (responsePDS, eWRequestPDS);
        }

        public (ForeignIdentityInfoResponseType, EWebRequest) SyncCallForeignIdentitySearchV2(string egn,
            string? applicationId = null,
            string? wApplicationId = null)
        {
            var operationFI = GetOperationByType(WebServiceEnumConstants.REGIX_ForeignIdentityV2);
            EWebRequest eWRequestFI = FactoryRegix.CreateForeignPersonWebRequest(lnch: egn, isAsync: true, operationFI.Id, applicationId, wApplicationId: wApplicationId);

            _dbContext.SaveChanges();


            var responseFI = ExecuteForeignIdentitySearchV2(eWRequestFI, operationFI.WebServiceName, egn);

            
            return (responseFI, eWRequestFI);
        }

        /// <summary>
        ///     Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public PersonDataResponseType ExecutePersonDataSearch(EWebRequest request, string webServiceName,  string? egn = null)
        {
            string? citizenEgn = egn;
            if (String.IsNullOrEmpty(egn))
            {
                var empty = new PersonDataResponseType();
                var emptyXml = XmlUtils.SerializeToXml(empty);

                var requestDeserialized = XmlUtils.DeserializeXml<PersonDataRequestType>(request.RequestXml);
                citizenEgn = requestDeserialized.EGN;
            }
            
            CallRegix(request, webServiceName, citizenEgn);
           
            PersonDataResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);
                var cache = AddOrUpdateCachePersonDataSearch(request, webServiceName, citizenEgn, responseObject);
                PopulateObjects(request, cache);
            }

            _dbContext.SaveChanges();
            return responseObject;
        }

        /// <summary>
        ///     Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public RelationsResponseType ExecuteRelationsSearch(EWebRequest request, string webServiceNameRelations, string? egn = null)
        {
            string? citizenEgn = egn;
            if (String.IsNullOrEmpty(egn))
            {
                var empty = new RelationsRequestType();
                var emptyXml = XmlUtils.SerializeToXml(empty);

                var requestDeserialized = XmlUtils.DeserializeXml<RelationsRequestType>(request.RequestXml);
                citizenEgn = requestDeserialized.EGN;
            }

            CallRegix(request, webServiceNameRelations, citizenEgn);

            RelationsResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<RelationsResponseType>(request.ResponseXml);
                var cache = AddOrUpdateCacheRelationsSearch(request, webServiceNameRelations, citizenEgn, responseObject);
                PopulateObjects(request, cache);
            }

            _dbContext.SaveChanges();
            return responseObject;
        }


        /// <summary>
        ///     Изпълнява заявка от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public ForeignIdentityInfoResponseType ExecuteForeignIdentitySearchV2(EWebRequest request, string webServiceName, string? egn = null)
        {
            string? citizenLNCH = egn;
            if (String.IsNullOrEmpty(egn))
            {
                var empty = new ForeignIdentityInfoResponseType();
                var emptyXml = XmlUtils.SerializeToXml(empty);

                var requestDeserialized = XmlUtils.DeserializeXml<ForeignIdentityInfoRequestType>(request.RequestXml);
                citizenLNCH = requestDeserialized.Identifier;
            }
            
            CallRegix(request, webServiceName, citizenLNCH);

            ForeignIdentityInfoResponseType responseObject = null;
            if (request.HasError != true)
            {
                responseObject = XmlUtils.DeserializeXml<ForeignIdentityInfoResponseType>(request.ResponseXml);
                var cache = AddOrUpdateCacheForeignIdentity(request, webServiceName, citizenLNCH, responseObject);
                PopulateObjects(request, cache);
            }

            _dbContext.SaveChanges();
            return responseObject;
        }

        private async void PopulateAApplication(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.ApplicationId))
            {
                var application =
                    await _dbContext.AApplications.FirstOrDefaultAsync(a => a.Id == request.ApplicationId);
                if (application != null)
                {
                    application.Firstname = !string.IsNullOrEmpty(cache.Firstname) ?  cache.Firstname.ToUpper() : null;
                    application.FirstnameLat = !string.IsNullOrEmpty(cache.FirstnameLat) ? cache.FirstnameLat.ToUpper() : null;
                    application.Surname = !string.IsNullOrEmpty(cache.Surname) ? cache.Surname.ToUpper() : null;
                    application.SurnameLat = !string.IsNullOrEmpty(cache.SurnameLat) ? cache.SurnameLat.ToUpper() : null;
                    application.Familyname = !string.IsNullOrEmpty(cache.Familyname) ? cache.Familyname.ToUpper() : null;
                    application.FamilynameLat = !string.IsNullOrEmpty(cache.FamilynameLat) ? cache.FamilynameLat.ToUpper() : null;
                    application.Egn = !string.IsNullOrEmpty(cache.Egn) ? cache.Egn.ToUpper() : null;
                    application.Lnch = !string.IsNullOrEmpty(cache.Lnch) ? cache.Lnch.ToUpper() : null;
                    if(!string.IsNullOrEmpty(cache.ForeignFirstname) 
                        || !string.IsNullOrEmpty(cache.ForeignSurname) 
                        || !string.IsNullOrEmpty(cache.ForeignFamilyname)
                        || !string.IsNullOrEmpty(cache.Alias)
                        )
                    {
                        var newObj = new AAppPersAlias
                        {
                            Id = BaseEntity.GenerateNewId(),
                            ApplicationId = application.Id,
                            Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                            Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                            Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                            Type = "previous" //todo да стане foreign
                        };
                        _dbContext.AAppPersAliases.Add(newObj);
                    }
                    if ( !string.IsNullOrEmpty(cache.Alias) )
                    {
                        var newObj = new AAppPersAlias
                        {
                            Id = BaseEntity.GenerateNewId(),
                            ApplicationId = application.Id,
                            Fullname =  cache.Alias.ToUpper(),
                            Type = "nickname" //todo да стане constant
                        };
                        _dbContext.AAppPersAliases.Add(newObj);
                    }
                    decimal result;
                    if (decimal.TryParse(cache.GenderCode, out result))
                    {
                        application.Sex = result;
                    }

                    application.BirthDate = cache.BirthDate;
                    if(cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
                    {
                        application.BirthCityId = TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim());
                    }
                    
                    if(application.BirthCityId == null)
                    {
                        application.BirthPlaceOther = (cache.BirthDistrictName != null  ? (cache.BirthDistrictName + " ") : "") 
                            + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                            + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                            + cache.BirthPlace;
                    }
                    else
                    {
                        application.BirthPlaceOther = cache.BirthPlace;
                    }
                   
                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        string? countryId = GetCounrtyByCode(cache.BirthCountryCode.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;
                        }
                        else
                        {
                            if( application.BirthCityId != null )
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(cache.NationalityCode1))
                    {
                        string? countryId = GetCounrtyByCode(cache.NationalityCode1.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;

                            var newObj = new AAppCitizenship
                            {
                                Id = BaseEntity.GenerateNewId(),
                                ApplicationId = application.Id,
                                CountryId = countryId
                            };
                            _dbContext.AAppCitizenships.Add(newObj);
                        }
                    }
                    if (!string.IsNullOrEmpty(cache.NationalityCode2))
                    {
                        string? countryId = GetCounrtyByCode(cache.NationalityCode2.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;

                            var newObj = new AAppCitizenship
                            {
                                Id = BaseEntity.GenerateNewId(),
                                ApplicationId = application.Id,
                                CountryId = countryId
                            };
                            _dbContext.AAppCitizenships.Add(newObj);
                        }
                    }
                    //допълваме резултатите със липсващи данни за ЕКАТТЕ от ГРАО
                    if(application.Egn != null && application.BirthCityId == null)
                    {
                        application.BirthCityId =  await GetCityIdFromGraoByEGN(application.Egn);
                    }
                    
                    //todo add identity document or travel document

                   // _dbContext.AApplications.Update(application);
                }
            }
        }

        private async void PopulateWApplication(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.WApplicationId))
            {
                var application =
                    await _dbContext.WApplications.FirstOrDefaultAsync(a => a.Id == request.WApplicationId);
                if (application != null)
                {
                    application.Firstname = !string.IsNullOrEmpty(cache.Firstname) ? cache.Firstname.ToUpper() : null;
                    application.FirstnameLat = !string.IsNullOrEmpty(cache.FirstnameLat) ? cache.FirstnameLat.ToUpper() : null;
                    application.Surname = !string.IsNullOrEmpty(cache.Surname) ? cache.Surname.ToUpper() : null;
                    application.SurnameLat = !string.IsNullOrEmpty(cache.SurnameLat) ? cache.SurnameLat.ToUpper() : null;
                    application.Familyname = !string.IsNullOrEmpty(cache.Familyname) ? cache.Familyname.ToUpper() : null;
                    application.FamilynameLat = !string.IsNullOrEmpty(cache.FamilynameLat) ? cache.FamilynameLat.ToUpper() : null;
                    application.Egn = !string.IsNullOrEmpty(cache.Egn) ? cache.Egn.ToUpper() : null;
                    application.Lnch = !string.IsNullOrEmpty(cache.Lnch) ? cache.Lnch.ToUpper() : null;
                    if (!string.IsNullOrEmpty(cache.ForeignFirstname)
                        || !string.IsNullOrEmpty(cache.ForeignSurname)
                        || !string.IsNullOrEmpty(cache.ForeignFamilyname)
                        || !string.IsNullOrEmpty(cache.Alias)
                        )
                    {
                        var newObj = new WAppPersAlias
                        {
                            Id = BaseEntity.GenerateNewId(),
                            WApplicationId = application.Id,
                            Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                            Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                            Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                            Type = "previous" //todo да стане foreign
                        };
                        _dbContext.WAppPersAliases.Add(newObj);
                    }
                    if (!string.IsNullOrEmpty(cache.Alias))
                    {
                        var newObj = new WAppPersAlias
                        {
                            Id = BaseEntity.GenerateNewId(),
                            WApplicationId = application.Id,
                            Fullname = cache.Alias.ToUpper(),
                            Type = "nickname" //todo да стане constant
                        };
                        _dbContext.WAppPersAliases.Add(newObj);
                    }
                    decimal result;
                    if (decimal.TryParse(cache.GenderCode, out result))
                    {
                        application.Sex = result;
                    }

                    application.BirthDate = cache.BirthDate;
                    if (cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
                    {
                        application.BirthCityId = TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim());
                    }

                    if (application.BirthCityId == null)
                    {
                        application.BirthPlaceOther = (cache.BirthDistrictName != null ? (cache.BirthDistrictName + " ") : "")
                            + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                            + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                            + cache.BirthPlace;
                    }
                    else
                    {
                        application.BirthPlaceOther = cache.BirthPlace;
                    }

                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        string? countryId = GetCounrtyByCode(cache.BirthCountryCode.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;
                        }
                        else
                        {
                            if (application.BirthCityId != null)
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(cache.NationalityCode1))
                    {
                        string? countryId = GetCounrtyByCode(cache.NationalityCode1.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;

                            var newObj = new WAppCitizenship
                            {
                                Id = BaseEntity.GenerateNewId(),
                                WApplicationId = application.Id,
                                CountryId = countryId
                            };
                            _dbContext.WAppCitizenships.Add(newObj);
                        }
                    }
                    if (!string.IsNullOrEmpty(cache.NationalityCode2))
                    {
                        string? countryId = GetCounrtyByCode(cache.NationalityCode2.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;

                            var newObj = new WAppCitizenship
                            {
                                Id = BaseEntity.GenerateNewId(),
                                WApplicationId = application.Id,
                                CountryId = countryId
                            };
                            _dbContext.WAppCitizenships.Add(newObj);
                        }
                    }
                    //допълваме резултатите със липсващи данни за ЕКАТТЕ от ГРАО
                    if (application.Egn != null && application.BirthCityId == null)
                    {
                        application.BirthCityId = await GetCityIdFromGraoByEGN(application.Egn);
                    }

                    //todo add identity document or travel document

                    //_dbContext.WApplications.Update(application);
                }
            }
        }

        private async Task<string?> GetCityIdFromGraoByEGN(string egn)
        {
            var graoPerson = await _dbContext.GraoPeople.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Egn == egn);
            if (graoPerson != null && graoPerson.BirthplaceCode != null)
            {
                var gCity = await _dbContext.GCities
                    .FirstOrDefaultAsync(a => a.EkatteCode == graoPerson.BirthplaceCode);
                if (gCity != null)
                {
                    return gCity.Id;
                }
            }
            return null;
        }
        private string? TryGetCityIdByNames(string districtName, string munName, string cityname)
        {
           string ? districtId = GetDistrictIdByName(districtName);
            if(districtId != null)
            {
               string? munId =  GetMunicipalityIdByName(munName, districtId);
                if(munId != null)
                {
                    string? cityId = GetCityIdByName(cityname, munId);
                    return cityId;
                }
            }
            return null;
        }
        private string? GetDistrictIdByName(string name)
        {
            var district = _dbContext.GBgDistricts.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            if (district != null && district.Result != null)
            {
                return district.Result.Id;
            }
            return null;
        }

        private string? GetMunicipalityIdByName(string name, string? districtId)
        {
            var municipality = _dbContext.GBgMunicipalities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper() && x.DistrictId == districtId);
            if (municipality != null && municipality.Result != null)
            {
                return municipality.Result.Id;
            }
            return null;
        }

        private string? GetCityIdByName(string name, string? municipalityId)
        {
            var municipality = _dbContext.GCities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x =>
                                                x.Name.ToUpper().EndsWith(". " + name.ToUpper())  && x.MunicipalityId == municipalityId);
            if (municipality != null && municipality.Result != null)
            {
                return municipality.Result.Id;
            }
            return null;
        }

        private string? GetCounrtyByCode(string code)
        {
            int codeLen = code.Length;
            if(codeLen == 2)
            {
                var country = _dbContext.GCountries.AsNoTracking().OrderByDescending(c=> c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Iso3166Alpha2 == code);
                if(country != null && country.Result != null)
                {
                    return country.Result.Id;
                }   
            }
            if(codeLen == 3)
            {
                var country = _dbContext.GCountries.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                        .FirstOrDefaultAsync(x => x.Iso31662Code == code);
                if (country != null && country.Result != null)
                {
                    return country.Result.Id;
                }
            }
            return null;
        }

        private async void PopulateObjects(EWebRequest request, ERegixCache cache)
        {
            if (request.WApplicationId != null)
            {
                PopulateWApplication(request, cache);
            }
            if (request.ApplicationId != null)
            {
                PopulateAApplication(request, cache);
            }
        }

        //private void CallRegixLNCH(EWebRequest request, string webServiceName, string citizenLNCH)
        //{
        //    var cachedResponse = CheckForCachedResponseLNCH(citizenLNCH, webServiceName);
        //    if (cachedResponse != null)
        //    {
        //        request.ResponseXml = cachedResponse.ResponseXml;
        //        request.IsFromCache = true;
        //    }
        //    else
        //    {
        //        request.Attempts += 1;
        //        request.IsFromCache = false;

        //        try
        //        {
        //            var callContext = CreateCallContext(request);
        //            var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, webServiceName,
        //                callContext, citizenLNCH);
        //            request.ResponseXml = resultData.Data.Response.Any.OuterXml;
        //            request.ResponseXml = AddXmlSchema(request.ResponseXml);
        //        }
        //        catch (Exception ex)
        //        {
        //            request.HasError = true;
        //            request.Error = ex.Message;
        //            request.StackTrace = ex.StackTrace;
        //        }
        //    }

        //    request.ExecutionDate = DateTime.Now;
        //    request.Status = request.HasError == true
        //        ? WebRequestStatusConstants.Rejected
        //        : WebRequestStatusConstants.Accepted;
        //}

        private void CallRegix(EWebRequest request, string webServiceName, string? citizenIdentifier)
        {
            ERegixCache cachedResponse = null;
            cachedResponse = CheckForCachedResponse(citizenIdentifier, webServiceName);

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
                    var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, webServiceName,
                        callContext,  citizenIdentifier);
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
            request.Status = request.HasError == true
                ? WebRequestStatusConstants.Rejected
                : WebRequestStatusConstants.Accepted;
        }

        //private ERegixCache CheckForCachedResponseLNCH(string citizenLNCH, string webServiceName)
        //{
        //    var daysCache = GetRegixDaysCache();
        //    var yesterday = DateTime.Now.AddDays(-daysCache);

        //    var cachedResponse = _dbContext.ERegixCaches
        //        .FirstOrDefault(r =>
        //            r.Lnch == citizenLNCH && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
        //    return cachedResponse;
        //}

        //private ERegixCache CheckForCachedResponse(string egn, string webServiceName)
        //{
        //    var daysCache = GetRegixDaysCache();
        //    var yesterday = DateTime.Now.AddDays(-daysCache);

        //    var cachedResponse = _dbContext.ERegixCaches
        //        .FirstOrDefault(r => r.Egn == egn && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
        //    return cachedResponse;
        //}

        private ERegixCache CheckForCachedResponse(string identifier, string webServiceName)
        {
            var daysCache = GetRegixDaysCache();
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches
                .FirstOrDefault(r => r.ReqIdentifier == identifier && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
            return cachedResponse;
        }

        private ERegixCache GetRegixCacheObject(string webServiceName, string reqIdentifier)
        {
            var regixCache =
                _dbContext.ERegixCaches.FirstOrDefault(r => r.ReqIdentifier == reqIdentifier && r.WebServiceName == webServiceName);
            if (regixCache == null)
            {
                regixCache = new ERegixCache
                {
                    Id = BaseEntity.GenerateNewId(),
                    WebServiceName = webServiceName,
                    ReqIdentifier = reqIdentifier
                };

                _dbContext.ERegixCaches.Add(regixCache);
            }
            return regixCache;
        }
        private ERegixCache AddOrUpdateCachePersonDataSearch(EWebRequest request, string webServiceName, string reqIdentifier, PersonDataResponseType responseObject)
        {
            var regixCache = GetRegixCacheObject(webServiceName, reqIdentifier);
            if (request.IsFromCache == true)
            {
                return regixCache;
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;

            regixCache.BirthDate = responseObject.BirthDate;
            regixCache.Egn = responseObject.EGN;

            regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
            regixCache.Surname = (string)responseObject.PersonNames.SurName;
            regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

            regixCache.FirstnameLat = (string)responseObject.LatinNames.FirstName;
            regixCache.SurnameLat = (string)responseObject.LatinNames.SurName;
            regixCache.FamilynameLat = (string)responseObject.LatinNames.FamilyName;

            regixCache.ForeignFirstname = (string)responseObject.ForeignNames.FirstName;
            regixCache.ForeignSurname = (string)responseObject.ForeignNames.SurName;
            regixCache.ForeignFamilyname = (string)responseObject.ForeignNames.FamilyName;

            regixCache.NationalityCode1 = responseObject.Nationality.NationalityName;
            regixCache.NationalityName1 = responseObject.Nationality.NationalityCode;

            regixCache.NationalityCode2 = responseObject.Nationality.NationalityCode2;
            regixCache.NationalityName2 = responseObject.Nationality.NationalityName2;

            regixCache.BirthPlace = responseObject.PlaceBirth;
            regixCache.GenderCode = responseObject.Gender.GenderCode.ToString();

            regixCache.Alias = responseObject.Alias;

            return regixCache;
        }

        private ERegixCache AddOrUpdateCacheRelationsSearch(EWebRequest request, string webServiceName, string reqIdentifier, RelationsResponseType responseObject)
        {
            var regixCache = GetRegixCacheObject(webServiceName, reqIdentifier);
            if (request.IsFromCache == true)
            {
                return regixCache;
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;

            foreach (var personRelation in responseObject.PersonRelations)
            {
                if (personRelation.RelationCode == RelationType.Майка)
                {
                    regixCache.MotherFirstname = personRelation.FirstName;
                    regixCache.MotherSurname = personRelation.SurName;
                    regixCache.MotherFamilyname = personRelation.FamilyName;
                }

                if (personRelation.RelationCode == RelationType.Баща)
                {
                    regixCache.FatherFirstname = personRelation.FirstName;
                    regixCache.FatherSurname = personRelation.SurName;
                    regixCache.FatherFamilyname = personRelation.FamilyName;
                }
            }
            return regixCache;
        }

        private ERegixCache AddOrUpdateCacheForeignIdentity(EWebRequest request, string webServiceName, string reqIdentifier, ForeignIdentityInfoResponseType responseObject)
        {
            var regixCache = GetRegixCacheObject(webServiceName, reqIdentifier);
            if (request.IsFromCache == true)
            {
                return regixCache;
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;

            regixCache.Egn = responseObject.EGN;
            regixCache.Lnch = responseObject.LNCh;

            regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
            regixCache.Surname = (string)responseObject.PersonNames.Surname;
            regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

            regixCache.FirstnameLat = (string)responseObject.PersonNames.FirstNameLatin;
            regixCache.SurnameLat = (string)responseObject.PersonNames.SurnameLatin;
            regixCache.FamilynameLat = (string)responseObject.PersonNames.LastNameLatin;

            DateTime result;
            if (DateTime.TryParse(responseObject.BirthDate, out result))
            {
                regixCache.BirthDate = result;
            }

            regixCache.BirthCountryName = responseObject.BirthPlace.CountryName;
            regixCache.BirthCountryCode = responseObject.BirthPlace.CountryCode;

            regixCache.BirthMunName = responseObject.BirthPlace.MunicipalityName;
            regixCache.BirthDistrictName = responseObject.BirthPlace.DistrictName;
            regixCache.BirthCityName = responseObject.BirthPlace.TerritorialUnitName;
            switch(responseObject.GenderName) 
            {
                case "Мъж":
                    regixCache.GenderCode = "1";
                    break;
                case "Жена":
                    regixCache.GenderCode = "2";
                    break;
                default:
                    regixCache.GenderCode = "0";
                    break;
            }
            if (responseObject.NationalityList != null)
            {
                int i = 1;
                foreach (var nationality in responseObject.NationalityList)
                {
                    if(i == 1)
                    {
                        regixCache.NationalityCode1 = nationality.NationalityCode;
                        regixCache.NationalityName1 = nationality.NationalityName;
                    }
                    if(i==2)
                    {
                        regixCache.NationalityCode2 = nationality.NationalityCode;
                        regixCache.NationalityName2 = nationality.NationalityName;
                    }
                    i++;
                }
            }
            if(responseObject.IdentityDocument != null)
            {
                regixCache.IdDocType = responseObject.IdentityDocument.DocumentType;
                regixCache.IdDocNumber = responseObject.IdentityDocument.IdentityDocumentNumber;
                regixCache.IdDocIssueDate = responseObject.IdentityDocument.IssueDate;
                regixCache.IdDocIssuePlace = responseObject.IdentityDocument.IssuePlace;
                regixCache.IdDocIssuerName = responseObject.IdentityDocument.IssuerName;
                regixCache.IdDocValidDate = responseObject.IdentityDocument.ValidDate;
                if(responseObject.IdentityDocument.RPRemarks != null)
                {
                    string? rpremarks = null;
                    foreach (var rpremark in responseObject.IdentityDocument.RPRemarks)
                    {
                        rpremarks += rpremark.Concat(";");
                    }
                    regixCache.IdDocPrRemarks = rpremarks;
                }
                regixCache.IdDocTypeOfPermit = responseObject.IdentityDocument.RPTypeOfPermit;
                regixCache.IdDocReason = responseObject.IdentityDocument.StatusReasonCyrillic.ToString();
                regixCache.IdDocStatus = responseObject.IdentityDocument.StatusCyrillic;
                regixCache.IdDocStatusDate = responseObject.IdentityDocument.StatusDate;
            }
            if (responseObject.TravelDocument != null)
            {
                regixCache.TrDocType = responseObject.TravelDocument.DocumentType;
                regixCache.TrDocNumber = responseObject.TravelDocument.TravelDocumentNumber;
                regixCache.TrDocIssueDate = responseObject.TravelDocument.IssueDate;
                regixCache.TrDocIssuePlace = responseObject.TravelDocument.IssuePlace;
                regixCache.TrDocIssuerName = responseObject.TravelDocument.IssuerName;
                regixCache.TrDocValidDate = responseObject.TravelDocument.ValidDate;
                regixCache.TrDocSeries = responseObject.TravelDocument.TravelDocumentSeries;
                regixCache.TrDocReason = responseObject.TravelDocument.StatusReasonCyrillic;
                regixCache.TrDocStatus = responseObject.TravelDocument.StatusCyrillic;
                regixCache.TrDocStatusDate = responseObject.TravelDocument.StatusDate;
            }

            return regixCache;
        }

        //private ERegixCache AddOrUpdateCache(EWebRequest request, string webServiceName, string reqIdentifier)
        //{
        //    var regixCache =
        //        _dbContext.ERegixCaches.FirstOrDefault(r => r.ReqIdentifier == reqIdentifier && r.WebServiceName == webServiceName);
        //    if (regixCache == null)
        //    {
        //        regixCache = new ERegixCache
        //        {
        //            Id = BaseEntity.GenerateNewId(),
        //            WebServiceName = webServiceName,    
        //            ReqIdentifier = reqIdentifier
        //        };

        //        _dbContext.ERegixCaches.Add(regixCache);
        //    }

        //    regixCache.RequestXml = request.RequestXml;
        //    regixCache.ResponseXml = request.ResponseXml;
        //    regixCache.ExecutionDate = DateTime.Now;


        //    if (webServiceName.EndsWith("PersonDataSearch"))
        //    {
        //        var responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);
        //        regixCache.BirthDate = responseObject.BirthDate;
        //        regixCache.Egn = responseObject.EGN;

        //        regixCache.FirstnameLat = (string)responseObject.LatinNames.FirstName;
        //        regixCache.SurnameLat = (string)responseObject.LatinNames.SurName;
        //        regixCache.FamilynameLat = (string)responseObject.LatinNames.FamilyName;

        //        regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
        //        regixCache.Surname = (string)responseObject.PersonNames.SurName;
        //        regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

        //        regixCache.BirthCountryName = responseObject.Nationality.NationalityName;
        //        regixCache.BirthCountryCode = responseObject.Nationality.NationalityCode;
        //        regixCache.BirthPlace = responseObject.PlaceBirth;
        //        regixCache.GenderCode = responseObject.Gender.GenderCode.ToString();
        //    }

        //    if (webServiceName.EndsWith("RelationsSearch"))
        //    {
        //        var responseObject = XmlUtils.DeserializeXml<RelationsResponseType>(request.ResponseXml);
        //        foreach (var personRelation in responseObject.PersonRelations)
        //        {
        //            if (personRelation.RelationCode == RelationType.Майка)
        //            {
        //                regixCache.MotherFirstname = personRelation.FirstName;
        //                regixCache.MotherSurname = personRelation.SurName;
        //                regixCache.MotherFamilyname = personRelation.FamilyName;
        //            }

        //            if (personRelation.RelationCode == RelationType.Баща)
        //            {
        //                regixCache.FatherFirstname = personRelation.FirstName;
        //                regixCache.FatherSurname = personRelation.SurName;
        //                regixCache.FatherFamilyname = personRelation.FamilyName;
        //            }
        //        }
        //    }

        //    return regixCache;
        //}

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

            var result = maxNumberOfAttempts != null ? (int)maxNumberOfAttempts : 5;
            return result;
        }

        private int GetRegixDaysCache()
        {
            var maxNumberOfAttempts = _dbContext.GSystemParameters.AsNoTracking()
                .FirstOrDefault(x => x.Code == SystemParametersConstants.SystemParametersNames.REGIX_NUMBER_OF_ATTEMPTS)
                ?.ValueNumber;

            var result = maxNumberOfAttempts != null ? (int)maxNumberOfAttempts : 5;
            return result;
        }

        private CallContext CreateCallContext(EWebRequest request)
        {
            // Default value
            var serviceURI = "ЦАИС_" + DateTime.Now.Date.ToString("dd-MM-yyyy");

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