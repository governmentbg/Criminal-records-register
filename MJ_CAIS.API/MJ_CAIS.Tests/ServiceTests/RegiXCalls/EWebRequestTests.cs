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

namespace MJ_CAIS.Tests.ServiceTests.RegiXCalls
{
    internal class EWebRequestTests
    {
        private IManagePersonService _peopleService;
        private Mock<IPersonRepository> _repository;
        private Mock<IApplicationRepository> _appRepository;
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

            _dbContext = host.Services.GetService<CaisDbContext>();

            _regixService = host.Services.GetService<IRegixService>();
        }


        [Test]
        public void TestLNCH()
        {

            (ForeignIdentityInfoResponseType, EWebRequest) result = _regixService.SyncCallForeignIdentitySearchV2("1001001001", applicationId: "2cc9e1a6-6dfd-4954-a9b1-25e457696ab3").Result;
            if (result.Item1.LNCh == null) //TODO: shoud be ==
            {
                Assert.Fail();//throw new BusinessLogicException($"Няма намерени данни:");
            }

            if (result.Item2.HasError == true)
            {
                Assert.Fail();//throw new BusinessLogicException($"RegiX e недостъпен");
            }
            Assert.True(true);

        }

        [Test]
        public void TestEGN()
        {

            var result = _regixService.SyncCallPersonDataSearch("8310188539", applicationId: "2cc9e1a6-6dfd-4954-a9b1-25e457696ab3").Result;
            if (result.Item1.EGN == null) //TODO: shoud be ==
            {
                Assert.Fail();//throw new BusinessLogicException($"Няма намерени данни:");
            }

            if (result.Item2.HasError == true)
            {
                Assert.Fail();//throw new BusinessLogicException($"RegiX e недостъпен");
            }
            Assert.True(true);

        }
    }
}
