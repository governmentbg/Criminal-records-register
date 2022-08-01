using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MJ_CAIS.AutoMapperContainer;
using MJ_CAIS.AutoMapperContainer.MappingProfiles;
using MJ_CAIS.DataAccess;
using MJ_CAIS.DIContainer;
using MJ_CAIS.DTO.Application;
using MJ_CAIS.ExternalWebServices.DbServices;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using MJ_CAIS.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJ_CAIS.Tests.ServiceTests.Application
{
    internal class ApplicationTests
    {
        private ISearchByIdentifierService _searchByIdentifierService;
        private IRegixService _regixService;
        private IRegisterTypeService _registerTypeService;
       // private IUserContext _userContext;
      //  private Mock<IUserContext> _userContext;
        private IApplicationService _applicationService;
        private CaisDbContext _dbContext;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {

            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => ContainerExtension.Initialize(services, config))
                //CaisMapper
                .ConfigureServices(services => services.AddTransient<ISearchByIdentifierService, SearchByIdentifierService>())
                .ConfigureServices(services => services.AddTransient<IRegixService, RegixService>())
              //  .ConfigureServices(services => services.AddTransient<IUserService, UserService>())
                .ConfigureServices(services => services.AddTransient<IRegisterTypeService, RegisterTypeService>())
                .ConfigureServices(services => services.AddTransient<IApplicationService, ApplicationService>())
                .ConfigureServices(services => services.AddAutoMapper(typeof(ApplicationProfile).Assembly))
                .ConfigureServices(services => services.AddSingleton<IUserContext>(new UserContext()
                {
                    UserId = config.GetValue<string>("ContextUser:UserId"),
                    UserName = config.GetValue<string>("ContextUser:UserName")
                }))

                .Build();

            _mapper = host.Services.GetService<IMapper>();
            _dbContext = host.Services.GetService<CaisDbContext>();

            _regixService = host.Services.GetService<IRegixService>();
            _registerTypeService = host.Services.GetService<IRegisterTypeService>();
           // _userContext =  new Mock<IUserContext>();
            _applicationService = host.Services.GetService<IApplicationService>();
            _searchByIdentifierService = host.Services.GetService<ISearchByIdentifierService>();
            
        }


        [Test]
        public void TestSearchByIdentifier()
        {
            try
            {
                var result = _searchByIdentifierService.SearchByIdentifier("1212124563").Result;
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void TestSearchByIdentifierLNCH()
        {
            try
            {
                var result = _searchByIdentifierService.SearchByIdentifierLNCH("1001001001").Result;
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

        [Test]
        public void TestApplicationFinalEdit()
        {
            try
            {
                string appId = _searchByIdentifierService.SearchByIdentifierLNCH("1001001001").Result;
                ApplicationOutDTO applOut = _applicationService.SelectAsync(appId).Result;
                ApplicationInDTO aInDto = new ApplicationInDTO()
                {
                    Person = applOut.Person,
                    PurposeId = "work",
                    Purpose = "работодател: Технологика, длъжност: Ръководител екип",
                    AddrDistrict = "AddrDistrict",
                    AddrEmail = "AddrEmail",
                    Address = "Address",
                    AddrName = "AddrName",
                    AddrPhone = "AddrPhone",
                    AddrState = "AddrState",
                    AddrStr = "AddrStr",
                    AddrTown = "AddrTown",
                    ApplicantName = "ApplicantName",
                    ApplicationTypeId = "6",
                    BirthCityId = applOut.BirthCityId,
                    BirthCountryId = applOut.BirthCountryId,
                    BirthDate = applOut.BirthDate,
                    BirthDatePrecision = applOut.BirthDatePrecision,
                    CsAuthorityId = applOut.CsAuthorityId,
                    RegistrationNumber = applOut.RegistrationNumber,
                    BirthPlaceOther = applOut.BirthPlaceOther,
                    Description = applOut.Description,
                    StatusCode = applOut.StatusCode,
                    UserId = applOut.UserId,
                    UserExtId = applOut.UserExtId,
                    UserCitizenId = applOut.UserCitizenId,
                    Version = applOut.Version,
                    Email = applOut.Email,
                    FromCosul = false,
                    PaymentMethodId = "LocalPosTerminal",
                    SrvcResRcptMethId = "InternalMail",
                    Id = appId
                };
                
                _applicationService.UpdateAsync(aInDto, true);
               // Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

        }

    }
}
