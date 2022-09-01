using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.XmlData;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DataAccess.Entities;
using MJ_CAIS.RegiX;
using System.Reflection;
using System.Runtime.Serialization;
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
        private readonly ILogger<RegixService> _logger;
        private Dictionary<string, (string Id, string WebServiceName)> webServiceTypes;

        public RegixService(CaisDbContext dbContext, IConfiguration config, ILogger<RegixService> logger)
        {
            _dbContext = dbContext;
            _config = config;
            _client = new WebServiceClient(config);
            _logger = logger;
        }

        public List<EWebRequest> GetRequestsForAsyncExecution()
        {
            var attempts = GetRegixAttempts();
            var result = _dbContext.EWebRequests.AsNoTracking()
                .Include(x => x.WebService).AsNoTracking()
                .Include(x => x.WApplication).AsNoTracking()
                .Where(x => x.IsAsync == true || x.IsAsync == null)
                .Where(x => x.Status == WebRequestStatusConstants.Pending ||
                            x.Status == WebRequestStatusConstants.Rejected)
                .Where(x=>x.WApplicationId != null)
                .Where(x => x.Attempts < attempts)
                .ToList();

            return result;
        }


        public void CreateRegixRequests(string egn,
            string wApplicationId)
        {
            var operationPDS = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);
            EWebRequest eWRequestPDS = FactoryRegix.CreatePersonWebRequest(egn: egn, isAsync: true, webServiceId: operationPDS.Id, wApplicationId: wApplicationId);

            var operationRS = GetOperationByType(WebServiceEnumConstants.REGIX_RelationsSearch);
            EWebRequest eWRequestRS = FactoryRegix.CreatePersonRelationsWebRequest(egn: egn, isAsync: true, webServiceId: operationRS.Id, wApplicationId: wApplicationId);

            _dbContext.ApplyChanges(eWRequestPDS, new List<IBaseIdEntity>());
            _dbContext.ApplyChanges(eWRequestRS, new List<IBaseIdEntity>());
            //todo: дали не е добре да е async
            _dbContext.SaveChanges();
        }

        public async Task<(PersonDataResponseType, EWebRequest)> SyncCallPersonDataSearch(string egn,
            string applicationId, string registrationNumber, string reportApplicationId)
        {

            var operationPDS = GetOperationByType(WebServiceEnumConstants.REGIX_PersonDataSearch);
            EWebRequest eWRequestPDS = FactoryRegix.CreatePersonWebRequest(egn: egn, isAsync: false, operationPDS.Id, applicationId, wApplicationId: null, reportApplicationId: reportApplicationId);
            eWRequestPDS.EntityState = Common.Enums.EntityStateEnum.Added;
            var responsePDS = await ExecutePersonDataSearch(eWRequestPDS, operationPDS.WebServiceName, egn, registrationNumber);

            var operationRS = GetOperationByType(WebServiceEnumConstants.REGIX_RelationsSearch);
            EWebRequest eWRequestRS = FactoryRegix.CreatePersonRelationsWebRequest(egn: egn, isAsync: false, operationRS.Id, applicationId, wApplicationId: null, reportApplicationId: reportApplicationId);
            eWRequestRS.EntityState = Common.Enums.EntityStateEnum.Added;
            var responseRS = await ExecuteRelationsSearch(eWRequestRS, operationRS.WebServiceName, egn, registrationNumber);

            return (responsePDS, eWRequestPDS);
        }

        public async Task<(ForeignIdentityInfoResponseType, EWebRequest)> SyncCallForeignIdentitySearchV2(string lnch,
            string applicationId, string registrationNumber, string reportApplicationId)
        {
            var operationFI = GetOperationByType(WebServiceEnumConstants.REGIX_ForeignIdentityV2);
            EWebRequest eWRequestFI = FactoryRegix.CreateForeignPersonWebRequest(lnch: lnch, isAsync: true, webServiceId: operationFI.Id, applicationId: applicationId, reportApplicationId: reportApplicationId);
            eWRequestFI.EntityState = Common.Enums.EntityStateEnum.Added;
            //_dbContext.EWebRequests.Add(eWRequestFI);
            //_dbContext.SaveChanges();


            var responseFI = await ExecuteForeignIdentitySearchV2(eWRequestFI, operationFI.WebServiceName, lnch);

            
            return (responseFI, eWRequestFI);
        }

        /// <summary>
        /// Изпълнява заявка ExecutePersonDataSearch от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="webServiceName"></param>
        /// <param name="egn"></param>
        /// <returns></returns>
        public async Task<PersonDataResponseType> ExecutePersonDataSearch(EWebRequest request, string webServiceName,  string? egn = null, string? registrationNumber = null)
        {
            return await ExecuteSearchBase<PersonDataRequestType, PersonDataResponseType>(AddOrUpdateCachePersonDataSearch, "EGN", request, webServiceName, egn, registrationNumber);
            //string? citizenEgn = egn;
            //if (String.IsNullOrEmpty(egn))
            //{
            //    var empty = new PersonDataResponseType();
            //    var emptyXml = XmlUtils.SerializeToXml(empty);

            //    var requestDeserialized = XmlUtils.DeserializeXml<PersonDataRequestType>(request.RequestXml);
            //    citizenEgn = requestDeserialized.EGN;
            //}
            //if (citizenEgn != null)
            //{
            //    CallRegixAndUpdateRequest(request, webServiceName, citizenEgn, registrationNumber);
                
            //    PersonDataResponseType responseObject = null;
            //    if (request.HasError != true && request.ResponseXml != null)
            //    {
            //        responseObject = XmlUtils.DeserializeXml<PersonDataResponseType>(request.ResponseXml);
            //        var cache = AddOrUpdateCachePersonDataSearch(request, webServiceName, citizenEgn, responseObject);
                    
            //        if (request.WApplicationId != null)
            //        {
            //            WApplication application = await PopulateWApplication(request, cache);
            //            request.WApplication = application;
            //           // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //        }
            //        if (request.ApplicationId != null)
            //        {
            //            AApplication application = await PopulateAApplication(request, cache);
            //            request.Application = application;
            //            // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //        }

            //    }
            //    _dbContext.ApplyChanges(request, new List<IBaseIdEntity>());
            //    await _dbContext.SaveChangesAsync();
            //    return responseObject;
            //}
            //return null;
        }

        /// <summary>
        ///     Изпълнява заявка RelationsSearch от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="webServiceNameRelations"></param>
        public async Task<RelationsResponseType> ExecuteRelationsSearch(EWebRequest request, string webServiceNameRelations, string? egn = null, string? registrationNumber = null)
        {

            return await ExecuteSearchBase<RelationsRequestType, RelationsResponseType>(AddOrUpdateCacheRelationsSearch, "EGN", request, webServiceNameRelations, egn, registrationNumber);


            //string? citizenEgn = egn;
            //if (String.IsNullOrEmpty(egn))
            //{
            //    var empty = new RelationsRequestType();
            //    var emptyXml = XmlUtils.SerializeToXml(empty);

            //    if(request.RequestXml != null)
            //    {
            //        var requestDeserialized = XmlUtils.DeserializeXml<RelationsRequestType>(request.RequestXml);
            //        citizenEgn = requestDeserialized.EGN;
            //    }
            //}
            //if (citizenEgn != null)
            //{
            //    CallRegixAndUpdateRequest(request, webServiceNameRelations, citizenEgn, registrationNumber);
               
            //    RelationsResponseType responseObject = null;
            //    if (request.HasError != true && request.ResponseXml != null)
            //    {
            //        responseObject = XmlUtils.DeserializeXml<RelationsResponseType>(request.ResponseXml);
            //        var cache = AddOrUpdateCacheRelationsSearch(request, webServiceNameRelations, citizenEgn, responseObject);
                    
            //        if (request.WApplicationId != null)
            //        {
            //            WApplication application = await PopulateWApplication(request, cache);
            //            request.WApplication = application;
            //           // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //        }
            //        if (request.ApplicationId != null)
            //        {
            //            AApplication application = await PopulateAApplication(request, cache);
            //            request.Application = application;
            //           // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //        }
                    
            //    }
            //    _dbContext.ApplyChanges(request, new List<IBaseIdEntity>());
            //    await _dbContext.SaveChangesAsync();
            //    return responseObject;
            //}
            //return null;
        }

        public async Task<TResponce> ExecuteSearchBase<TRequest, TResponce>(Func<EWebRequest,string,string?,TResponce,ERegixCache> copyCache, string egnPropertyName, EWebRequest request, string webServiceNameRelations, string? egn = null, string? registrationNumber = null)
        
        {
            string? citizenEgn = egn;
            if (String.IsNullOrEmpty(egn))
            {
               
                
                //този код беше с reposnse някъде, но никъде не се ползва
                //TRequest empty = (TRequest)FormatterServices.GetUninitializedObject(typeof(TRequest));

                //var emptyXml = XmlUtils.SerializeToXml(empty);

                if (request.RequestXml != null)
                {
                    var requestDeserialized = XmlUtils.DeserializeXml<TRequest>(request.RequestXml);
                    citizenEgn = (string)typeof(TRequest).GetProperty(egnPropertyName).GetValue( requestDeserialized);
                    
                }
            }
            if (citizenEgn != null)
            {
                CallRegixAndUpdateRequest(request, webServiceNameRelations, citizenEgn, registrationNumber);
                _logger.LogTrace($"{request.Id}: CallRegix and update request executed.");
                TResponce responseObject = default;
                if (request.HasError != true && request.ResponseXml != null)
                {
                    responseObject = XmlUtils.DeserializeXml<TResponce>(request.ResponseXml);
                    //specific:
                    
                    var cache = copyCache(request, webServiceNameRelations, citizenEgn, responseObject);
                   
                    if (request.WApplicationId != null)
                    {
                        _logger.LogTrace($"{request.Id}: PopulateWApplication started.");
                        WApplication application = await PopulateWApplication(request, cache);
                        request.WApplication = application;
                        _logger.LogTrace($"{request.Id}: PopulateWApplication ended.");
                        if (request.WApplication?.WAppCitizenships != null)
                        {
                            _dbContext.ApplyChanges(request.WApplication?.WAppCitizenships);
                        }
                        if (request.WApplication?.WAppPersAliases != null)
                        {
                            _dbContext.ApplyChanges(request.WApplication?.WAppPersAliases);
                        }
                        // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
                    }
                    if (request.ApplicationId != null)
                    {
                        _logger.LogTrace($"{request.Id}: PopulateAApplication started.");
                        AApplication application = await PopulateAApplication(request, cache);
                        request.Application = application;
                        _logger.LogTrace($"{request.Id}: PopulateAApplication ended.");
                        if (request.Application?.AAppCitizenships != null)
                        {
                            _dbContext.ApplyChanges(request.Application?.AAppCitizenships);
                        }
                        if (request.Application?.AAppPersAliases != null)
                        {
                            _dbContext.ApplyChanges(request.Application?.AAppPersAliases);
                        }
                        // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
                    }
                  if(request.ARepApplId != null)
                    {
                        _logger.LogTrace($"{request.Id}: PopulateAReportApplication started.");
                        AReportApplication application = await PopulateAReportApplication(request, cache);
                        request.ARepAppl = application;
                        if (request.ARepAppl?.ARepCitizenships != null)
                        {
                            _dbContext.ApplyChanges(request.ARepAppl?.ARepCitizenships);
                        }
                     
                        _logger.LogTrace($"{request.Id}: PopulateAReportApplication ended.");
                    }
                   
                }
                _logger.LogTrace($"{request.Id}: Before ApplyChanges");
             
                _dbContext.ApplyChanges(request, new List<IBaseIdEntity>(), true);
                _logger.LogTrace($"{request.Id}: Before SaveChangesAsync");
                await _dbContext.SaveChangesAsync();
                _dbContext.ChangeTracker.Clear();
                return responseObject;
            }
            else
            {
                _logger.LogInformation($"{request.Id}: CitizenEgn is null.");
            }
            return default;
        }

        private async Task<AReportApplication> PopulateAReportApplication(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.ARepApplId))
            {
                var application =
                    await _dbContext.AReportApplications.AsNoTracking()
                    .Include(x => x.ARepCitizenships).AsNoTracking()
                   // .Include(x => x.AAppPersAliases).AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == request.ARepApplId);
                if (application.ModifiedProperties == null)
                {
                    application.ModifiedProperties = new List<string>();
                }
                if (application != null)
                {
                    application.Firstname = SetValue(cache.Firstname?.ToUpper(), application, nameof(application.Firstname), application.Firstname);
                    application.FirstnameLat = SetValue(cache.FirstnameLat?.ToUpper(), application, nameof(application.FirstnameLat), application.FirstnameLat);
                    application.Surname = SetValue(cache.Surname?.ToUpper(), application, nameof(application.Surname), application.Surname);
                    application.SurnameLat = SetValue(cache.SurnameLat?.ToUpper(), application, nameof(application.SurnameLat), application.SurnameLat);
                    application.Familyname = SetValue(cache.Familyname?.ToUpper(), application, nameof(application.Familyname), application.Familyname);
                    application.FamilynameLat = SetValue(cache.FamilynameLat?.ToUpper(), application, nameof(application.FamilynameLat), application.FamilynameLat);

                    application.MotherFirstname = SetValue(cache.MotherFirstname?.ToUpper(), application, nameof(application.MotherFirstname), application.MotherFirstname);
                    application.MotherSurname = SetValue(cache.MotherSurname?.ToUpper(), application, nameof(application.MotherSurname), application.MotherSurname);
                    application.MotherFamilyname = SetValue(cache.MotherFamilyname?.ToUpper(), application, nameof(application.MotherFamilyname), application.MotherFamilyname);
                    application.FatherFirstname = SetValue(cache.FatherFirstname?.ToUpper(), application, nameof(application.FatherFirstname), application.FatherFirstname);
                    application.FatherSurname = SetValue(cache.FatherSurname?.ToUpper(), application, nameof(application.FatherSurname), application.FatherSurname);
                    application.FatherFamilyname = SetValue(cache.FatherFamilyname?.ToUpper(), application, nameof(application.FatherFamilyname), application.FatherFamilyname);


                    application.Egn = SetValue(cache.Egn?.ToUpper(), application, nameof(application.Egn), application.Egn);
                    application.Lnch = SetValue(cache.Lnch?.ToUpper(), application, nameof(application.Lnch), application.Lnch);

                    //if (application.AAppPersAliases?.Count == 0)
                    //{
                    //    if ((!string.IsNullOrEmpty(cache.ForeignFirstname)
                    //    || !string.IsNullOrEmpty(cache.ForeignSurname)
                    //    || !string.IsNullOrEmpty(cache.ForeignFamilyname)
                    //    || !string.IsNullOrEmpty(cache.Alias))
                    //    )
                    //    {
                    //        var newAlias = new AAppPersAlias
                    //        {
                    //            Id = BaseEntity.GenerateNewId(),
                    //            ApplicationId = application.Id,
                    //            Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                    //            Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                    //            Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                    //            Type = "previous" //todo да стане foreign
                    //        };
                    //        newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                    //        application.AAppPersAliases.Add(newAlias);
                    //        // _dbContext.AAppPersAliases.Add(newAlias);
                    //    }
                    //    if (!string.IsNullOrEmpty(cache.Alias))
                    //    {
                    //        var newAlias = new AAppPersAlias
                    //        {
                    //            Id = BaseEntity.GenerateNewId(),
                    //            ApplicationId = application.Id,
                    //            Fullname = cache.Alias.ToUpper(),
                    //            Type = "nickname" //todo да стане constant
                    //        };
                    //        newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                    //        application.AAppPersAliases.Add(newAlias);
                    //        //_dbContext.AAppPersAliases.Add(newAlias);
                    //    }
                    //}

                    decimal result;
                    if (decimal.TryParse(cache.GenderCode, out result))
                    {
                        application.Sex = result;
                        application.ModifiedProperties.Add(nameof(application.Sex));
                    }
                    if (cache.BirthDate != null)
                    {
                        application.BirthDate = cache.BirthDate;
                        application.ModifiedProperties.Add(nameof(application.BirthDate));
                    }
                    string? birtCityId = null;
                    if (cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
                    {
                        birtCityId = await TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim());
                        if (!string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                        }
                    }

                    if (birtCityId == null)
                    {
                        string birthPlaceOther = (cache.BirthDistrictName != null ? (cache.BirthDistrictName + " ") : "")
                            + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                            + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                            + cache.BirthPlace;
                        if (!string.IsNullOrEmpty(birthPlaceOther))
                        {
                            application.BirthPlaceOther = birthPlaceOther;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }
                    else
                    {
                        if (cache.BirthPlace != null)
                        {
                            application.BirthPlaceOther = cache.BirthPlace;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }

                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        string? countryId = await GetCounrtyByCode(cache.BirthCountryCode.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;
                            application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                        }
                        else
                        {
                            if (application.BirthCityId != null)
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                    }
                    if (application.ARepCitizenships?.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(cache.NationalityCode1))
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode1.ToUpper());
                            if (countryId != null)
                            {

                                var newCountry = new ARepCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    AReportApplId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.ARepCitizenships.Add(newCountry);
                                // _dbContext.AAppCitizenships.Add(newCountry);
                            }
                        }
                        if (!string.IsNullOrEmpty(cache.NationalityCode2) && cache.NationalityCode1 != cache.NationalityCode2)
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode2.ToUpper());
                            if (countryId != null)
                            {

                                var newCountry = new ARepCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    AReportApplId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.ARepCitizenships.Add(newCountry);
                                //_dbContext.AAppCitizenships.Add(newObj);
                            }
                        }
                    }

                    //допълваме резултатите със липсващи данни за ЕКАТТЕ от ГРАО
                    if (application.Egn != null &&
                        (application.BirthCityId == null //|| application.MotherFirstname == null || application.FatherFirstname == null
                        )
                        )
                    {
                        var graoData = await GetCityMotherFatherFromGraoByEGN(application.Egn);
                        birtCityId = graoData.Item1;
                        //тук идеятя е, ако не са попълнени място на раждане, майка, баща от regix(application), само тогава да ги слагаме от GRAO_PERSON
                        if (string.IsNullOrEmpty(application.BirthCityId) && !string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                            if (string.IsNullOrEmpty(application.BirthCountryId))
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                        //if (string.IsNullOrEmpty(application.MotherFirstname) && !string.IsNullOrEmpty(graoData.Item2))
                        //{
                        //    application.MotherFullname = graoData.Item2;
                        //    application.ModifiedProperties.Add(nameof(application.MotherFullname));
                        //}
                        //if (string.IsNullOrEmpty(application.FatherFirstname) && !string.IsNullOrEmpty(graoData.Item3))
                        //{
                        //    application.FatherFullname = graoData.Item3;
                        //    application.ModifiedProperties.Add(nameof(application.FatherFullname));
                        //}
                    }

                    if (application.BirthCityId != null && application.BirthCountryId == null)
                    {
                        application.BirthCountryId = GlobalConstants.BGCountryId;
                        application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                    }

                    //todo add identity document or travel document
                    
                    application.EntityState = Common.Enums.EntityStateEnum.Modified;
                    //_dbContext.AApplications.Update(application);
                    return application;
                }
            }
            return null;
        }

        /// <summary>
        ///     Изпълнява заявка ForeignIdentitySearchV2 от таблицата с web requests
        /// </summary>
        /// <param name="request"></param>
        /// <param name="serviceURI"></param>
        public async Task<ForeignIdentityInfoResponseType> ExecuteForeignIdentitySearchV2(EWebRequest request, string webServiceName, string? egn = null, string? registrationNumber = null)
        {
            return await ExecuteSearchBase<ForeignIdentityInfoRequestType, ForeignIdentityInfoResponseType>(AddOrUpdateCacheForeignIdentity, "Identifier", request, webServiceName, egn, registrationNumber);

            //string? citizenLNCH = egn;
            //if (String.IsNullOrEmpty(egn))
            //{
            //    var empty = new ForeignIdentityInfoResponseType();
            //    var emptyXml = XmlUtils.SerializeToXml(empty);

            //    if(request.RequestXml != null)
            //    {
            //        var requestDeserialized = XmlUtils.DeserializeXml<ForeignIdentityInfoRequestType>(request.RequestXml);
            //        citizenLNCH = requestDeserialized.Identifier;
            //    }
            //}
            //if(citizenLNCH != null)
            //{
            //    CallRegixAndUpdateRequest(request, webServiceName, citizenLNCH, registrationNumber);
                

            //    ForeignIdentityInfoResponseType responseObject = null;
            //    if (request.HasError != true)
            //    {
            //        if (request.ResponseXml != null)
            //        {
            //            responseObject = XmlUtils.DeserializeXml<ForeignIdentityInfoResponseType>(request.ResponseXml);
            //            var cache = AddOrUpdateCacheForeignIdentity(request, webServiceName, citizenLNCH, responseObject);
                       
            //            if (request.WApplicationId != null)
            //            {
            //                WApplication application = await PopulateWApplication(request, cache);
            //                request.WApplication=application;
            //               // _dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //            }
            //            if (request.ApplicationId != null)
            //            {
            //                AApplication application = await PopulateAApplication(request, cache);
            //                request.Application = application;
            //                //_dbContext.ApplyChanges(application, new List<IBaseIdEntity>());
            //            }
            //        }
            //    }
            //    _dbContext.ApplyChanges(request, new List<IBaseIdEntity>());
            //    await _dbContext.SaveChangesAsync();
            //    return responseObject;
            //}
            //return null;
        }

        private static string SetValue(string? newValue, object application, string propertyName, string? currentValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                ((BaseEntity)application).ModifiedProperties.Add(propertyName);
                return newValue;
            }
            return currentValue;
        }

        private async Task<AApplication> PopulateAApplication(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.ApplicationId))
            {
                var application =
                    await _dbContext.AApplications.AsNoTracking()
                    .Include(x => x.AAppCitizenships).AsNoTracking()
                    .Include(x => x.AAppPersAliases).AsNoTracking()
                    .FirstOrDefaultAsync(a => a.Id == request.ApplicationId);
                if (application.ModifiedProperties == null)
                {
                    application.ModifiedProperties = new List<string>();
                }
                if (application != null)
                {
                    application.Firstname = SetValue(cache.Firstname?.ToUpper(), application, nameof(application.Firstname), application.Firstname);
                    application.FirstnameLat = SetValue(cache.FirstnameLat?.ToUpper(), application, nameof(application.FirstnameLat), application.FirstnameLat);
                    application.Surname = SetValue(cache.Surname?.ToUpper(), application, nameof(application.Surname), application.Surname);
                    application.SurnameLat = SetValue(cache.SurnameLat?.ToUpper(), application, nameof(application.SurnameLat), application.SurnameLat);
                    application.Familyname = SetValue(cache.Familyname?.ToUpper(), application, nameof(application.Familyname), application.Familyname);
                    application.FamilynameLat = SetValue(cache.FamilynameLat?.ToUpper(), application, nameof(application.FamilynameLat), application.FamilynameLat);

                    application.MotherFirstname = SetValue(cache.MotherFirstname?.ToUpper(), application, nameof(application.MotherFirstname), application.MotherFirstname);
                    application.MotherSurname = SetValue(cache.MotherSurname?.ToUpper(), application, nameof(application.MotherSurname), application.MotherSurname);
                    application.MotherFamilyname = SetValue(cache.MotherFamilyname?.ToUpper(), application, nameof(application.MotherFamilyname), application.MotherFamilyname);
                    application.FatherFirstname = SetValue(cache.FatherFirstname?.ToUpper(), application, nameof(application.FatherFirstname), application.FatherFirstname);
                    application.FatherSurname = SetValue(cache.FatherSurname?.ToUpper(), application, nameof(application.FatherSurname), application.FatherSurname);
                    application.FatherFamilyname = SetValue(cache.FatherFamilyname?.ToUpper(), application, nameof(application.FatherFamilyname), application.FatherFamilyname);


                    application.Egn = SetValue(cache.Egn?.ToUpper(), application, nameof(application.Egn), application.Egn);
                    application.Lnch = SetValue(cache.Lnch?.ToUpper(), application, nameof(application.Lnch), application.Lnch);
                    
                    if ( application.AAppPersAliases?.Count == 0)
                    {
                        if ((!string.IsNullOrEmpty(cache.ForeignFirstname)
                        || !string.IsNullOrEmpty(cache.ForeignSurname)
                        || !string.IsNullOrEmpty(cache.ForeignFamilyname)
                        || !string.IsNullOrEmpty(cache.Alias))
                        )
                        {
                            var newAlias = new AAppPersAlias
                            {
                                Id = BaseEntity.GenerateNewId(),
                                ApplicationId = application.Id,
                                Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                                Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                                Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                                Type = "previous" //todo да стане foreign
                            };
                            newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                            application.AAppPersAliases.Add(newAlias);
                            // _dbContext.AAppPersAliases.Add(newAlias);
                        }
                        if (!string.IsNullOrEmpty(cache.Alias))
                        {
                            var newAlias = new AAppPersAlias
                            {
                                Id = BaseEntity.GenerateNewId(),
                                ApplicationId = application.Id,
                                Fullname = cache.Alias.ToUpper(),
                                Type = "nickname" //todo да стане constant
                            };
                            newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                            application.AAppPersAliases.Add(newAlias);
                            //_dbContext.AAppPersAliases.Add(newAlias);
                        }
                    }
                    
                    decimal result;
                    if (decimal.TryParse(cache.GenderCode, out result))
                    {
                        application.Sex = result;
                        application.ModifiedProperties.Add(nameof(application.Sex));
                    }
                    if (cache.BirthDate != null)
                    {
                        application.BirthDate = cache.BirthDate;
                        application.ModifiedProperties.Add(nameof(application.BirthDate));
                    }
                    string? birtCityId = null;
                    if (cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
                    {
                        birtCityId = await TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim());
                        if (!string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                        }
                    }

                    if (birtCityId == null)
                    {
                        string birthPlaceOther = (cache.BirthDistrictName != null ? (cache.BirthDistrictName + " ") : "")
                            + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                            + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                            + cache.BirthPlace;
                        if (!string.IsNullOrEmpty(birthPlaceOther))
                        {
                            application.BirthPlaceOther = birthPlaceOther;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }
                    else
                    {
                        if (cache.BirthPlace != null)
                        {
                            application.BirthPlaceOther = cache.BirthPlace;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }

                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        string? countryId = await GetCounrtyByCode(cache.BirthCountryCode.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;
                            application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                        }
                        else
                        {
                            if (application.BirthCityId != null)
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                    }
                    if (application.AAppCitizenships?.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(cache.NationalityCode1))
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode1.ToUpper());
                            if (countryId != null)
                            {
                             
                                var newCountry = new AAppCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    ApplicationId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.AAppCitizenships.Add(newCountry);
                                // _dbContext.AAppCitizenships.Add(newCountry);
                            }
                        }
                        if (!string.IsNullOrEmpty(cache.NationalityCode2) && cache.NationalityCode1 != cache.NationalityCode2)
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode2.ToUpper());
                            if (countryId != null)
                            {
                              
                                var newCountry = new AAppCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    ApplicationId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.AAppCitizenships.Add(newCountry);
                                //_dbContext.AAppCitizenships.Add(newObj);
                            }
                        }
                    }

                    //допълваме резултатите със липсващи данни за ЕКАТТЕ от ГРАО
                    if (application.Egn != null &&
                        (application.BirthCityId == null //|| application.MotherFirstname == null || application.FatherFirstname == null
                        )
                        )
                    {
                        var graoData = await GetCityMotherFatherFromGraoByEGN(application.Egn);
                        birtCityId = graoData.Item1;
                        //тук идеятя е, ако не са попълнени място на раждане, майка, баща от regix(application), само тогава да ги слагаме от GRAO_PERSON
                        if (string.IsNullOrEmpty(application.BirthCityId) && !string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                            if (string.IsNullOrEmpty(application.BirthCountryId))
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                        //if (string.IsNullOrEmpty(application.MotherFirstname) && !string.IsNullOrEmpty(graoData.Item2))
                        //{
                        //    application.MotherFullname = graoData.Item2;
                        //    application.ModifiedProperties.Add(nameof(application.MotherFullname));
                        //}
                        //if (string.IsNullOrEmpty(application.FatherFirstname) && !string.IsNullOrEmpty(graoData.Item3))
                        //{
                        //    application.FatherFullname = graoData.Item3;
                        //    application.ModifiedProperties.Add(nameof(application.FatherFullname));
                        //}
                    }

                    if (application.BirthCityId != null && application.BirthCountryId == null)
                    {
                        application.BirthCountryId = GlobalConstants.BGCountryId;
                        application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                    }

                    //todo add identity document or travel document

                    application.EntityState = Common.Enums.EntityStateEnum.Modified;
                    //_dbContext.AApplications.Update(application);
                    return application;
                }
            }
            return null;
        }

        
        private async Task<WApplication> PopulateWApplication(EWebRequest request, ERegixCache cache)
        {
            if (!string.IsNullOrEmpty(request.WApplicationId))
            {
                var application =
                    await _dbContext.WApplications.AsNoTracking()
                    .Include(x => x.WAppCitizenships).AsNoTracking()
                    .Include(x => x.WAppPersAliases).AsNoTracking().FirstOrDefaultAsync(a => a.Id == request.WApplicationId);
                if (application.ModifiedProperties == null)
                {
                    application.ModifiedProperties = new List<string>();
                }
                if (application != null)
                {
                    application.Firstname = SetValue(cache.Firstname?.ToUpper(), application, nameof(application.Firstname), application.Firstname);
                    application.FirstnameLat = SetValue(cache.FirstnameLat?.ToUpper(), application, nameof(application.FirstnameLat), application.FirstnameLat);
                    application.Surname = SetValue(cache.Surname?.ToUpper(), application, nameof(application.Surname), application.Surname);
                    application.SurnameLat = SetValue(cache.SurnameLat?.ToUpper(), application, nameof(application.SurnameLat), application.SurnameLat);
                    application.Familyname = SetValue(cache.Familyname?.ToUpper(), application, nameof(application.Familyname), application.Familyname);
                    application.FamilynameLat = SetValue(cache.FamilynameLat?.ToUpper(), application, nameof(application.FamilynameLat), application.FamilynameLat);
                    application.Egn = SetValue(cache.Egn?.ToUpper(), application, nameof(application.Egn), application.Egn);
                    application.Lnch = SetValue(cache.Lnch?.ToUpper(), application, nameof(application.Lnch), application.Lnch);

                    application.MotherFirstname = SetValue(cache.MotherFirstname?.ToUpper(), application, nameof(application.MotherFirstname), application.MotherFirstname);
                    application.MotherSurname = SetValue(cache.MotherSurname?.ToUpper(), application, nameof(application.MotherSurname), application.MotherSurname);
                    application.MotherFamilyname = SetValue(cache.MotherFamilyname?.ToUpper(), application, nameof(application.MotherFamilyname), application.MotherFamilyname);
                    application.FatherFirstname = SetValue(cache.FatherFirstname?.ToUpper(), application, nameof(application.FatherFirstname), application.FatherFirstname);
                    application.FatherSurname = SetValue(cache.FatherSurname?.ToUpper(), application, nameof(application.FatherSurname), application.FatherSurname);
                    application.FatherFamilyname = SetValue(cache.FatherFamilyname?.ToUpper(), application, nameof(application.FatherFamilyname), application.FatherFamilyname);


                    if (application.WAppPersAliases?.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(cache.ForeignFirstname)
                        || !string.IsNullOrEmpty(cache.ForeignSurname)
                        || !string.IsNullOrEmpty(cache.ForeignFamilyname)
                        || !string.IsNullOrEmpty(cache.Alias)
                        )
                        {
                            var newAlias = new WAppPersAlias
                            {
                                Id = BaseEntity.GenerateNewId(),
                                WApplicationId = application.Id,
                                Firstname = !string.IsNullOrEmpty(cache.ForeignFirstname) ? cache.ForeignFirstname.ToUpper() : null,
                                Surname = !string.IsNullOrEmpty(cache.ForeignSurname) ? cache.ForeignSurname.ToUpper() : null,
                                Familyname = !string.IsNullOrEmpty(cache.ForeignFamilyname) ? cache.ForeignFamilyname.ToUpper() : null,
                                Type = "previous" //todo да стане foreign
                            };
                            //newAlias.ModifiedProperties = new List<string>();
                            newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                            application.WAppPersAliases.Add(newAlias);
                            // _dbContext.AAppPersAliases.Add(newAlias);
                        }
                    
                        if (!string.IsNullOrEmpty(cache.Alias))
                        {
                            var newAlias = new WAppPersAlias
                            {
                                Id = BaseEntity.GenerateNewId(),
                                WApplicationId = application.Id,
                                Fullname = cache.Alias.ToUpper(),
                                Type = "nickname" //todo да стане constant
                            };
                            newAlias.EntityState = Common.Enums.EntityStateEnum.Added;
                            application.WAppPersAliases.Add(newAlias);
                            //_dbContext.AAppPersAliases.Add(newAlias);
                        }
                    }
                    decimal result;
                    if (decimal.TryParse(cache.GenderCode, out result))
                    {
                        application.Sex = result;
                        application.ModifiedProperties.Add(nameof(application.Sex));
                    }
                    if (cache.BirthDate != null)
                    {
                        application.BirthDate = cache.BirthDate;
                        application.ModifiedProperties.Add(nameof(application.BirthDate));
                    }
                    string? birtCityId = null;
                    if (cache.BirthDistrictName != null && cache.BirthMunName != null && cache.BirthCityName != null)
                    {
                        birtCityId = await TryGetCityIdByNames(cache.BirthDistrictName.Trim(), cache.BirthMunName.Trim(), cache.BirthCityName.Trim());
                        if (!string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                        }
                    }

                    if (birtCityId == null)
                    {
                        string birthPlaceOther = (cache.BirthDistrictName != null ? (cache.BirthDistrictName + " ") : "")
                            + (cache.BirthMunName != null ? (cache.BirthMunName + " ") : "")
                            + (cache.BirthCityName != null ? (cache.BirthCityName + " ") : "")
                            + cache.BirthPlace;
                        if (birthPlaceOther != null)
                        {
                            application.BirthPlaceOther = birthPlaceOther;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }
                    else
                    {
                        if (cache.BirthPlace != null)
                        {
                            application.BirthPlaceOther = cache.BirthPlace;
                            application.ModifiedProperties.Add(nameof(application.BirthPlaceOther));
                        }
                    }

                    if (!string.IsNullOrEmpty(cache.BirthCountryCode))
                    {
                        string? countryId = await GetCounrtyByCode(cache.BirthCountryCode.ToUpper());
                        if (countryId != null)
                        {
                            application.BirthCountryId = countryId;
                            application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                        }
                        else
                        {
                            if (application.BirthCityId != null)
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                    }
                    if (application.WAppCitizenships?.Count == 0)
                    {
                        if (!string.IsNullOrEmpty(cache.NationalityCode1))
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode1.ToUpper());
                            if (countryId != null)
                            {
                                
                                var newCountry = new WAppCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    WApplicationId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.WAppCitizenships.Add(newCountry);
                                // _dbContext.AAppCitizenships.Add(newCountry);
                            }
                        }
                        if (!string.IsNullOrEmpty(cache.NationalityCode2) && cache.NationalityCode1 != cache.NationalityCode2)
                        {
                            string? countryId = await GetCounrtyByCode(cache.NationalityCode2.ToUpper());
                            if (countryId != null)
                            {
                                var newCountry = new WAppCitizenship
                                {
                                    Id = BaseEntity.GenerateNewId(),
                                    WApplicationId = application.Id,
                                    CountryId = countryId
                                };
                                newCountry.EntityState = Common.Enums.EntityStateEnum.Added;
                                application.WAppCitizenships.Add(newCountry);
                                //_dbContext.AAppCitizenships.Add(newObj);
                            }
                        }
                    }

                    //допълваме резултатите със липсващи данни за ЕКАТТЕ от ГРАО
                    //тук идеятя е, ако не са попълнени място на раждане, майка, баща от regix(application) -
                    //от RegiX имената на майка и баща са по 3 броя, съответно проверяваме за първо име, само тогава да ги слагаме от GRAO_PERSON
                    if (application.Egn != null &&
                        (application.BirthCityId == null || application.MotherFirstname == null || application.FatherFirstname == null)
                        )
                    {
                        var graoData = await GetCityMotherFatherFromGraoByEGN(application.Egn);
                        birtCityId = graoData.Item1;
                        //тук идеятя е, ако не са попълнени място на раждане, майка, баща от regix(application), само тогава да ги слагаме от GRAO_PERSON
                        if (string.IsNullOrEmpty(application.BirthCityId) && !string.IsNullOrEmpty(birtCityId))
                        {
                            application.BirthCityId = birtCityId;
                            application.ModifiedProperties.Add(nameof(application.BirthCityId));
                            if (string.IsNullOrEmpty(application.BirthCountryId))
                            {
                                application.BirthCountryId = GlobalConstants.BGCountryId;
                                application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                            }
                        }
                        if (string.IsNullOrEmpty(application.MotherFirstname) && !string.IsNullOrEmpty(graoData.Item2))
                        {
                            application.MotherFullname = graoData.Item2;
                            application.ModifiedProperties.Add(nameof(application.MotherFullname));
                        }
                        if (string.IsNullOrEmpty(application.FatherFirstname) && !string.IsNullOrEmpty(graoData.Item3))
                        {
                            application.FatherFullname = graoData.Item3;
                            application.ModifiedProperties.Add(nameof(application.FatherFullname));
                        }
                    }

                    if (application.BirthCityId != null && application.BirthCountryId == null)
                    {
                        application.BirthCountryId = GlobalConstants.BGCountryId;
                        application.ModifiedProperties.Add(nameof(application.BirthCountryId));
                    }

                    application.EntityState = Common.Enums.EntityStateEnum.Modified;
                    //_dbContext.AApplications.Update(application);
                    return application;
                }
            }
            return null;
        }

        private async Task<(string?, string?, string?)> GetCityMotherFatherFromGraoByEGN(string egn)
        {
            var graoPerson = await _dbContext.GraoPeople.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Egn == egn);
            if (graoPerson != null )
            {
                if ( graoPerson.BirthplaceCode != null)
                {
                    var gCity = await _dbContext.GCities.AsNoTracking()
                                        .FirstOrDefaultAsync(a => a.EkatteCode == graoPerson.BirthplaceCode);
                    if (gCity != null)
                    {
                        return (gCity.Id, graoPerson.MothersNames, graoPerson.FathersNames);
                    }
                }
                return (null, graoPerson.MothersNames, graoPerson.FathersNames);
            }
            return (null, null, null);
        }
        private async Task<string?> TryGetCityIdByNames(string districtName, string munName, string cityname)
        {
           string ? districtId = await GetDistrictIdByName(districtName);
            if(districtId != null)
            {
               string? munId = await GetMunicipalityIdByName(munName, districtId);
                if(munId != null)
                {
                    string? cityId = await GetCityIdByName(cityname, munId);
                    return cityId;
                }
            }
            return null;
        }
        private async Task<string?> GetDistrictIdByName(string name)
        {
            var district = await _dbContext.GBgDistricts.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper());
            if (district != null )
            {
                return district.Id;
            }
            return null;
        }

        private async Task<string?> GetMunicipalityIdByName(string name, string? districtId)
        {
            var municipality = await _dbContext.GBgMunicipalities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Name.ToUpper() == name.ToUpper() && x.DistrictId == districtId);
            if (municipality != null )
            {
                return municipality.Id;
            }
            return null;
        }

        private async Task<string?> GetCityIdByName(string name, string? municipalityId)
        {
            var municipality = await _dbContext.GCities.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                       .FirstOrDefaultAsync(x =>
                                                x.Name.ToUpper().EndsWith(". " + name.ToUpper())  && x.MunicipalityId == municipalityId);
            if (municipality != null )
            {
                return municipality.Id;
            }
            return null;
        }

        private async Task<string?> GetCounrtyByCode(string code)
        {
            int codeLen = code.Length;
            if(codeLen == 2)
            {
                var country = await _dbContext.GCountries.AsNoTracking().OrderByDescending(c=> c.ValidFrom)
                                       .FirstOrDefaultAsync(x => x.Iso3166Alpha2 == code);
                if(country != null && country != null)
                {
                    return country.Id;
                }   
            }
            if(codeLen == 3)
            {
                var country = await _dbContext.GCountries.AsNoTracking().OrderByDescending(c => c.ValidFrom)
                                        .FirstOrDefaultAsync(x => x.Iso31662Code == code);
                if (country != null)
                {
                    return country.Id;
                }
            }
            return null;
        }

        

        
        private void CallRegixAndUpdateRequest(EWebRequest request, string webServiceName, string citizenIdentifier, string registrationNumber)
        {
            ERegixCache cachedResponse = null;
            cachedResponse = CheckForCachedResponse(citizenIdentifier, webServiceName);
            if(request.ModifiedProperties == null)
            {
                request.ModifiedProperties = new List<string>();
            }
            if(request.EntityState != Common.Enums.EntityStateEnum.Added)
            {
                request.EntityState = Common.Enums.EntityStateEnum.Modified;
            }
            
            if (cachedResponse != null)
            {
                request.ResponseXml = cachedResponse.ResponseXml;
                request.ModifiedProperties.Add(nameof(request.ResponseXml));
                request.IsFromCache = true;
                request.ModifiedProperties.Add(nameof(request.IsFromCache));
            }
            else
            {
                request.Attempts += 1;
                request.ModifiedProperties.Add(nameof(request.Attempts));
                request.IsFromCache = false;
                request.ModifiedProperties.Add(nameof(request.IsFromCache));

                try
                {
                    if(request.RequestXml != null)
                    {
                        var callContext = CreateCallContext(request, registrationNumber);
                        var resultData = _client.CallRegixExecuteSynchronous(request.RequestXml, webServiceName,
                            callContext, citizenIdentifier);
                        request.ApiServiceCallId = resultData.ServiceCallID.ToString();
                        request.ModifiedProperties.Add(nameof(request.ApiServiceCallId));

                        request.CallContext = XmlUtils.SerializeToXml(callContext);
                        request.ModifiedProperties.Add(nameof(request.CallContext));
                        // request.CallContext = callContext
                        request.ResponseXml = resultData.Data.Response.Any.OuterXml;
                        request.ResponseXml = AddXmlSchema(request.ResponseXml);
                        request.ModifiedProperties.Add(nameof(request.ResponseXml));
                        //todo: Надя,дали това трябва да е така?! или през статуса да се управлява?!
                        request.HasError = false;
                        request.ModifiedProperties.Add(nameof(request.HasError));
                    }
                }
                catch (Exception ex)
                {
                    request.HasError = true;
                    request.Error = ex.Message;
                    request.StackTrace = ex.StackTrace;
                    
                    request.ModifiedProperties.Add(nameof(request.HasError));
                    request.ModifiedProperties.Add(nameof(request.Error));
                    request.ModifiedProperties.Add(nameof(request.StackTrace));
                }
            }

            request.ExecutionDate = DateTime.Now;
            request.Status = request.HasError == true
                ? WebRequestStatusConstants.Rejected
                : WebRequestStatusConstants.Accepted;
            request.ModifiedProperties.Add(nameof(request.ExecutionDate));
            request.ModifiedProperties.Add(nameof(request.Status));
            //_dbContext.ApplyChanges(request, new List<IBaseIdEntity>());
        }

        private ERegixCache CheckForCachedResponse(string identifier, string webServiceName)
        {
            var daysCache = GetRegixDaysCache();
            var yesterday = DateTime.Now.AddDays(-daysCache);

            var cachedResponse = _dbContext.ERegixCaches.AsNoTracking()
                .FirstOrDefault(r => r.ReqIdentifier == identifier && r.ExecutionDate > yesterday && r.WebServiceName == webServiceName);
            return cachedResponse;
        }

        private ERegixCache GetRegixCacheObject(string webServiceName, string reqIdentifier)
        {
            var regixCache =
                _dbContext.ERegixCaches.AsNoTracking().FirstOrDefault(r => r.ReqIdentifier == reqIdentifier && r.WebServiceName == webServiceName);
            if (regixCache == null)
            {
                var newRegixCache = new ERegixCache
                {
                    Id = BaseEntity.GenerateNewId(),
                    WebServiceName = webServiceName,
                    ReqIdentifier = reqIdentifier
                };
                newRegixCache.EntityState = Common.Enums.EntityStateEnum.Added;
                return newRegixCache;
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

            if(regixCache.EntityState != Common.Enums.EntityStateEnum.Added)
            {
                regixCache.EntityState = Common.Enums.EntityStateEnum.Modified;
            }
            if (regixCache.ModifiedProperties == null)
            {
                regixCache.ModifiedProperties = new List<string>();
            }
            
            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;

            regixCache.ModifiedProperties.Add(nameof(regixCache.RequestXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ResponseXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ExecutionDate));

            if (responseObject.BirthDateSpecified)
            {
                regixCache.BirthDate = responseObject.BirthDate;
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthDate));
            }
            
            regixCache.Egn = responseObject.EGN;
            regixCache.ModifiedProperties.Add(nameof(regixCache.Egn));

            if (responseObject.PersonNames != null)
            {
                regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
                regixCache.Surname = (string)responseObject.PersonNames.SurName;
                regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.Firstname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.Surname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.Familyname));
            }
            if (responseObject.LatinNames != null)
            {

                regixCache.FirstnameLat = (string)responseObject.LatinNames.FirstName;
                regixCache.SurnameLat = (string)responseObject.LatinNames.SurName;
                regixCache.FamilynameLat = (string)responseObject.LatinNames.FamilyName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.FirstnameLat));
                regixCache.ModifiedProperties.Add(nameof(regixCache.SurnameLat));
                regixCache.ModifiedProperties.Add(nameof(regixCache.FamilynameLat));
            }
            if (responseObject.ForeignNames != null)
            {
                regixCache.ForeignFirstname = (string)responseObject.ForeignNames.FirstName;
                regixCache.ForeignSurname = (string)responseObject.ForeignNames.SurName;
                regixCache.ForeignFamilyname = (string)responseObject.ForeignNames.FamilyName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.ForeignFirstname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.ForeignSurname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.ForeignFamilyname));
            }
            if (responseObject.Nationality != null)
            {
                regixCache.NationalityCode1 = responseObject.Nationality.NationalityCode;
                regixCache.NationalityName1 = responseObject.Nationality.NationalityName;

                regixCache.NationalityCode2 = responseObject.Nationality.NationalityCode2;
                regixCache.NationalityName2 = responseObject.Nationality.NationalityName2;

                regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityCode1));
                regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityName1));
                regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityCode2));
                regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityName2));
            }

            if(responseObject.PlaceBirth != null)
            {
                regixCache.BirthPlace = responseObject.PlaceBirth;
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthPlace));
            }
            if (responseObject.Gender != null && responseObject.Gender.GenderCodeSpecified)
            {
                regixCache.GenderCode = responseObject.Gender.GenderCode.ToString();
                regixCache.ModifiedProperties.Add(nameof(regixCache.GenderCode));
            }
            if(responseObject.Alias != null)
            {
                regixCache.Alias = responseObject.Alias;
                regixCache.ModifiedProperties.Add(nameof(regixCache.Alias));
            }

            _dbContext.ApplyChanges(regixCache, new List<IBaseIdEntity>());
            return regixCache;
        }

        private ERegixCache AddOrUpdateCacheRelationsSearch(EWebRequest request, string webServiceName, string reqIdentifier, RelationsResponseType responseObject)
        {
            var regixCache = GetRegixCacheObject(webServiceName, reqIdentifier);
            if (request.IsFromCache == true)
            {
                return regixCache;
            }
            if (regixCache.EntityState != Common.Enums.EntityStateEnum.Added)
            {
                regixCache.EntityState = Common.Enums.EntityStateEnum.Modified;
            }
            if (regixCache.ModifiedProperties == null)
            {
                regixCache.ModifiedProperties = new List<string>();
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;
            

            regixCache.ModifiedProperties.Add(nameof(regixCache.RequestXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ResponseXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ExecutionDate));

            //todo: Надя, може би заради лошото извикване тук се получаваше рег номер на справката да се записва в поле егн
            //да се прегледа
            regixCache.Egn = regixCache.ReqIdentifier;
          
            regixCache.ModifiedProperties.Add(nameof(regixCache.Egn));

            foreach (var personRelation in responseObject.PersonRelations)
            {
                if (personRelation.RelationCode == RelationType.Майка)
                {
                    regixCache.MotherFirstname = personRelation.FirstName;
                    regixCache.MotherSurname = personRelation.SurName;
                    regixCache.MotherFamilyname = personRelation.FamilyName;

                    regixCache.ModifiedProperties.Add(nameof(regixCache.MotherFirstname));
                    regixCache.ModifiedProperties.Add(nameof(regixCache.MotherSurname));
                    regixCache.ModifiedProperties.Add(nameof(regixCache.MotherFamilyname));
                }

                if (personRelation.RelationCode == RelationType.Баща)
                {
                    regixCache.FatherFirstname = personRelation.FirstName;
                    regixCache.FatherSurname = personRelation.SurName;
                    regixCache.FatherFamilyname = personRelation.FamilyName;

                    regixCache.ModifiedProperties.Add(nameof(regixCache.FatherFirstname));
                    regixCache.ModifiedProperties.Add(nameof(regixCache.FatherSurname));
                    regixCache.ModifiedProperties.Add(nameof(regixCache.FatherFamilyname));
                }
            }
            _dbContext.ApplyChanges(regixCache, new List<IBaseIdEntity>());
            return regixCache;
        }

        private ERegixCache AddOrUpdateCacheForeignIdentity(EWebRequest request, string webServiceName, string reqIdentifier, ForeignIdentityInfoResponseType responseObject)
        {
            var regixCache = GetRegixCacheObject(webServiceName, reqIdentifier);
            if (request.IsFromCache == true)
            {
                return regixCache;
            }
            if (regixCache.EntityState != Common.Enums.EntityStateEnum.Added)
            {
                regixCache.EntityState = Common.Enums.EntityStateEnum.Modified;
            }
            if (regixCache.ModifiedProperties == null)
            {
                regixCache.ModifiedProperties = new List<string>();
            }

            regixCache.RequestXml = request.RequestXml;
            regixCache.ResponseXml = request.ResponseXml;
            regixCache.ExecutionDate = DateTime.Now;

            regixCache.ModifiedProperties.Add(nameof(regixCache.RequestXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ResponseXml));
            regixCache.ModifiedProperties.Add(nameof(regixCache.ExecutionDate));

            regixCache.Egn = responseObject.EGN;
            regixCache.Lnch = responseObject.LNCh;

            regixCache.ModifiedProperties.Add(nameof(regixCache.Egn));
            regixCache.ModifiedProperties.Add(nameof(regixCache.Lnch));

            if (responseObject.PersonNames != null)
            {
                regixCache.Firstname = (string)responseObject.PersonNames.FirstName;
                regixCache.Surname = (string)responseObject.PersonNames.Surname;
                regixCache.Familyname = (string)responseObject.PersonNames.FamilyName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.Firstname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.Surname));
                regixCache.ModifiedProperties.Add(nameof(regixCache.Familyname));

                regixCache.FirstnameLat = (string)responseObject.PersonNames.FirstNameLatin;
                regixCache.SurnameLat = (string)responseObject.PersonNames.SurnameLatin;
                regixCache.FamilynameLat = (string)responseObject.PersonNames.LastNameLatin;

                regixCache.ModifiedProperties.Add(nameof(regixCache.FirstnameLat));
                regixCache.ModifiedProperties.Add(nameof(regixCache.SurnameLat));
                regixCache.ModifiedProperties.Add(nameof(regixCache.FamilynameLat));
            }
            

            DateTime result;
            if (DateTime.TryParse(responseObject.BirthDate, out result))
            {
                regixCache.BirthDate = result;
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthDate));
            }

            if(responseObject.BirthPlace != null)
            {
                regixCache.BirthCountryName = responseObject.BirthPlace.CountryName;
                regixCache.BirthCountryCode = responseObject.BirthPlace.CountryCode;
                regixCache.BirthMunName = responseObject.BirthPlace.MunicipalityName;
                regixCache.BirthDistrictName = responseObject.BirthPlace.DistrictName;
                regixCache.BirthCityName = responseObject.BirthPlace.TerritorialUnitName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthCountryName));
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthCountryCode));
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthMunName));
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthDistrictName));
                regixCache.ModifiedProperties.Add(nameof(regixCache.BirthCityName));
            }
            
            
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

                        regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityCode1));
                        regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityName1));
                    }
                    if(i==2)
                    {
                        regixCache.NationalityCode2 = nationality.NationalityCode;
                        regixCache.NationalityName2 = nationality.NationalityName;

                        regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityCode2));
                        regixCache.ModifiedProperties.Add(nameof(regixCache.NationalityName2));
                    }
                    i++;
                }
            }
            if(responseObject.IdentityDocument != null)
            {
                regixCache.IdDocType = responseObject.IdentityDocument.DocumentType;
                regixCache.IdDocNumber = responseObject.IdentityDocument.IdentityDocumentNumber;
                
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocType));
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocNumber));

                if (responseObject.IdentityDocument.IssueDateSpecified)
                {
                    regixCache.IdDocIssueDate = responseObject.IdentityDocument.IssueDate;
                    regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocIssueDate));
                }
                regixCache.IdDocIssuePlace = responseObject.IdentityDocument.IssuePlace;
                regixCache.IdDocIssuerName = responseObject.IdentityDocument.IssuerName;

                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocIssuePlace));
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocIssuerName));

                if (responseObject.IdentityDocument.ValidDateSpecified)
                {
                    regixCache.IdDocValidDate = responseObject.IdentityDocument.ValidDate;
                    regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocValidDate));
                }
                
                if(responseObject.IdentityDocument.RPRemarks != null)
                {
                    string? rpremarks = null;
                    foreach (var rpremark in responseObject.IdentityDocument.RPRemarks)
                    {
                        rpremarks += rpremark.Concat(";");
                    }
                    regixCache.IdDocPrRemarks = rpremarks;
                    regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocPrRemarks));
                }
                regixCache.IdDocTypeOfPermit = responseObject.IdentityDocument.RPTypeOfPermit;
                regixCache.IdDocReason = responseObject.IdentityDocument.StatusReasonCyrillic != null ? responseObject.IdentityDocument.StatusReasonCyrillic.ToString() : null;
                regixCache.IdDocStatus = responseObject.IdentityDocument.StatusCyrillic;
                regixCache.IdDocStatusDate = responseObject.IdentityDocument.StatusDate;

                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocTypeOfPermit));
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocReason));
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocStatus));
                regixCache.ModifiedProperties.Add(nameof(regixCache.IdDocStatusDate));
            }
            if (responseObject.TravelDocument != null)
            {
                regixCache.TrDocType = responseObject.TravelDocument.DocumentType;
                regixCache.TrDocNumber = responseObject.TravelDocument.TravelDocumentNumber;
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocType));
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocNumber));
                if (responseObject.TravelDocument.IssueDateSpecified)
                {
                    regixCache.TrDocIssueDate = responseObject.TravelDocument.IssueDate;
                    regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocIssueDate));
                }
                regixCache.TrDocIssuePlace = responseObject.TravelDocument.IssuePlace;
                regixCache.TrDocIssuerName = responseObject.TravelDocument.IssuerName;
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocIssuePlace));
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocIssuerName));
                if (responseObject.TravelDocument.ValidDateSpecified)
                {
                    regixCache.TrDocValidDate = responseObject.TravelDocument.ValidDate;
                    regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocValidDate));
                }
                regixCache.TrDocSeries = responseObject.TravelDocument.TravelDocumentSeries;
                regixCache.TrDocReason = responseObject.TravelDocument.StatusReasonCyrillic;
                regixCache.TrDocStatus = responseObject.TravelDocument.StatusCyrillic;
                regixCache.TrDocStatusDate = responseObject.TravelDocument.StatusDate;
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocSeries));
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocReason));
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocStatus));
                regixCache.ModifiedProperties.Add(nameof(regixCache.TrDocStatusDate));
            }
           
            _dbContext.ApplyChanges(regixCache, new List<IBaseIdEntity>());
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

        private CallContext CreateCallContext(EWebRequest request, string registrationNumber)
        {
            // Default value
            var serviceURI = registrationNumber;//"ЦАИС_" + DateTime.Now.Date.ToString("dd-MM-yyyy");
            var callContext = _client.CreateSampleCallContext(serviceURI);
            // TODO: fill other data for user that created the web request
            
            if ((request.ApplicationId != null || request.ARepApplId!=null) && request.CreatedBy != null)
            {
                var user = _dbContext.GUsers.AsNoTracking().
                    Include(x=> x.CsAuthority).AsNoTracking()
                    .FirstOrDefault(x => x.Id == request.CreatedBy);
                if(user != null)
                {
                    callContext.EmployeeIdentifier = user.Email;
                    callContext.EmployeeAditionalIdentifier = user.Id;
                    callContext.EmployeeNames = user.Firstname + " " + user.Surname + " " + user.Familyname;
                    callContext.EmployeePosition = user.Position + " в "+ user.CsAuthority?.Name;
                    if(user.CsAuthority != null)
                    {
                        callContext.AdministrationName = user.CsAuthority?.Name;
                    }
                    
                }
                callContext.Remark = "Във връзка с издаване на свидетелство/справка за съдимост";
            }

           

            if (request.WApplicationId != null && request.WApplication != null && request.WApplication.UserExtId != null)
            {
                var user = _dbContext.GUsersExts.AsNoTracking()
                    .Include(x => x.Administration).AsNoTracking()
                    .FirstOrDefault(x => x.Id == request.WApplication.UserExtId);
                if (user != null)
                {
                    callContext.EmployeeIdentifier = user.Email;
                    callContext.EmployeeAditionalIdentifier = user.Id;
                    callContext.EmployeeNames = user.Name;
                    callContext.EmployeePosition = user.Position;
                    if (user.Administration != null)
                    {
                        callContext.AdministrationName = user.Administration?.Name;
                    }
                }
                callContext.Remark = "Във връзка с издаване на Електронно служебно свидетелство за съдимост";
            }
            if (request.WApplicationId != null && request.WApplication != null && request.WApplication.UserCitizenId != null)
            {
                var user = _dbContext.GUsersCitizens.AsNoTracking()
                    .FirstOrDefault(x => x.Id == request.WApplication.UserCitizenId);
                
                callContext.EmployeeIdentifier = user?.Email;
                callContext.EmployeeAditionalIdentifier = request.WApplication.UserCitizenId;
                callContext.EmployeeNames = user?.Name;
                callContext.EmployeePosition = "гражданин в лично качество";
                callContext.AdministrationName = "Публичен портал за електронни свидетелства за съдимост за граждани";
                callContext.Remark = "Във връзка с издаване на Електронно свидетелство за съдимост, заявено от гражданин в лично качество";
            }
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