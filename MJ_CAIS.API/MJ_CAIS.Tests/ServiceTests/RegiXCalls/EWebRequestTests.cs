using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.Common.Constants;
using MJ_CAIS.Common.Enums;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.DTO.Person;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Factories;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DIContainer;
using MJ_CAIS.ExternalWebServices.DbServices;
using TechnoLogica.RegiX.MVRERChAdapterV2;
using MJ_CAIS.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace MJ_CAIS.Tests.ServiceTests.RegiXCalls
{
    internal class EWebRequestTests
    {
        private IRegixService _regixService;
        private CaisDbContext _dbContext;

        [SetUp]
        public void Setup()
        {
            //var mapper = InitObjectHelper.GetMapper<PersonProfile>();
            //_repository = new Mock<IPersonRepository>();
            //_peopleService = new ManagePersonService(_repository.Object, mapper);

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                .ConfigureServices(services => services.AddTransient<IRegixService, RegixService>())
                .Build();

            //_dbContext = host.Services.GetService<CaisDbContext>();

            
            //using (CaisDbContext db = host.Services.GetService<CaisDbContext>())
            //{
            //    db.ERegixCaches.RemoveRange(db.ERegixCaches);
            //    db.SaveChanges();
            //    foreach (var req in db.EWebRequests.Where(w => w.WApplicationId != null).ToList())
            //    {
            //        req.Status = "Pending";
            //        //  req.Attempts = 0;
            //        db.EWebRequests.Update(req);
            //    }
            //    db.SaveChanges();
            //}
            _dbContext = host.Services.GetService<CaisDbContext>();
            _regixService = host.Services.GetService<IRegixService>();
        }


        [Test]
        public void TestLNCHWithData()
        {

            (ForeignIdentityInfoResponseType, EWebRequest) result = _regixService.SyncCallForeignIdentitySearchV2("1001001001", applicationId: "2cc9e1a6-6dfd-4954-a9b1-25e457696ab3", registrationNumber: "220728660123000000123",null).Result;
            if (result.Item1.LNCh == null) //TODO: shoud be ==
            {
                Assert.Fail();//throw new BusinessLogicException($"Няма намерени данни:");
            }

            if (result.Item2.HasError == true)
            {
                //throw new BusinessLogicException($"RegiX e недостъпен");
            }
            if(result.Item1.PersonNames.FirstName != result.Item2.Application.Firstname)
            {
                Assert.Fail(); 
            }
            else
            {
                Assert.True(true);
            }
        }
        [Test]
        public void TestLNCHNoData()
        {

            (ForeignIdentityInfoResponseType, EWebRequest) result = _regixService.SyncCallForeignIdentitySearchV2("10000", applicationId: "2cc9e1a6-6dfd-4954-a9b1-25e457696ab3", registrationNumber: "220728660123000000123",null).Result;
            if (result.Item2.HasError == true)
            {
                //throw new BusinessLogicException($"RegiX e недостъпен");
            }
            if (result.Item1 != null && result.Item1.LNCh == null ) //TODO: shoud be ==
            {
                Assert.True(true);//throw new BusinessLogicException($"Няма намерени данни:");
            }
        }

        [Test]
        public void TestEGN()
        {
            //foreach(var application in _dbContext.AApplications.Where(a=> a.ServiceMigrationId == null && a.Egn != null && a.Lnch == null))
            //{
                try
                {
                //var result = _regixService.SyncCallPersonDataSearch(application.Egn, applicationId: application.Id, registrationNumber: "220728660123000000123").Result;
                var result = _regixService.SyncCallPersonDataSearch("1212124563", applicationId: "329812b4-4530-466a-8d46-2dc571cb70d3", registrationNumber: "220731660101000001024",null).Result;

                if (result.Item1.EGN == null) //TODO: shoud be ==
                    {
                        Assert.Fail();//throw new BusinessLogicException($"Няма намерени данни:");
                    }

                    if (result.Item2.HasError == true)
                    {
                        Assert.Fail();//throw new BusinessLogicException($"RegiX e недостъпен");
                    }
                    if (result.Item1.PersonNames.FirstName.ToString().ToUpper() != result.Item2.Application.Firstname
                            && result.Item2.Application.MotherFirstname == null
                            && result.Item2.Application.AAppCitizenships.FirstOrDefault().CountryId != GlobalConstants.BGCountryId)
                    {
                        Assert.Fail();
                    }
                    else
                    {
                        // Assert.True(true);
                    }
                }
                catch(Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
           // }
            Assert.True(true);
        }
        [Test]
        public void TestCreateWebRequests()
        {
            _regixService.CreateRegixRequests("8310188539", "5ee90ac2-da63-48df-ae37-dc0a99d91730");
            Assert.True(true);
        }

        [Test]
        public void TestExecuteWebRequests()
        {
            foreach (var webRequest in _dbContext.EWebRequests.Include(x => x.WebService).Include(x => x.WApplication)
                            .Where(x => x.IsAsync == true || x.IsAsync == null)
                            .Where(x => x.Status == WebRequestStatusConstants.Pending ||
                                        x.Status == WebRequestStatusConstants.Rejected)
                            //.Where(x => x.Attempts < attempts
                            //.Where(x => x.CallContext == null)
                            //.Where(x => x.IsFromCache == false)
                            .ToList())
            {
                if (webRequest.WebService != null)
                {
                    if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_PersonDataSearch)
                    {
                        _regixService.ExecutePersonDataSearch(webRequest, webRequest.WebService.WebServiceName);
                    }
                    if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_RelationsSearch)
                    {
                        _regixService.ExecuteRelationsSearch(webRequest, webRequest.WebService.WebServiceName);
                    }
                    if (webRequest.WebService.TypeCode == WebServiceEnumConstants.REGIX_ForeignIdentityV2)
                    {
                        _regixService.ExecuteForeignIdentitySearchV2(webRequest, webRequest.WebService.WebServiceName);
                    }
                }
            }
        }
    }
}
