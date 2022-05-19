using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.DataAccess;
using MJ_CAIS.EcrisObjectsServices;
using MJ_CAIS.EcrisObjectsServices.Contracts;
using MJ_CAIS.ExternalWebServices;
using MJ_CAIS.ExternalWebServices.Contracts;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Repositories.Impl;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using System.Reflection;
using TL.JasperReports.Integration;
using TL.JasperReports.Integration.Interfaces;
using TL.Signer;

namespace MJ_CAIS.DIContainer
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CaisConnectionString");
            var oracleCompatibility = configuration.GetValue<string>("OracleSQLCompatibility");

            services.AddDbContext<CaisDbContext>(x => x.UseOracle(connectionString, opt => opt.UseOracleSQLCompatibility(oracleCompatibility)));

            var servicesTypes = typeof(BulletinService).Assembly.GetClassTypes("Service");
            var interfaceTypes = typeof(IBulletinService).Assembly.GetInterfaceTypes("Service");
            AddTransientTypes(services, servicesTypes, interfaceTypes);

            var repositoryTypes = typeof(BulletinRepository).Assembly.GetClassTypes("Repository");
            var interfaceRepositoryTypes = typeof(IBulletinRepository).Assembly.GetInterfaceTypes("Repository");

            AddTransientTypes(services, repositoryTypes, interfaceRepositoryTypes);

            var servicesTypesECRIS = typeof(NotificationService).Assembly.GetClassTypes("Service");
            var interfaceTypesECRIS = typeof(INotificationService).Assembly.GetInterfaceTypes("Service");
            AddTransientTypes(services, servicesTypesECRIS, interfaceTypesECRIS);

            services.AddJasperReporting();
            services.AddSingleton<IPdfSigner, PdfSigner>();
            services.AddTransient<ICertificateGenerationService, CertificateGenerationService>();
            //var servicesTypesExternal = typeof(CertificateGenerationService).Assembly.GetClassTypes("Service");
            //var interfaceTypesExternal = typeof(ICertificateGenerationService).Assembly.GetInterfaceTypes("Service");
            //AddTransientTypes(services, servicesTypesExternal, interfaceTypesExternal);
        }

        private static List<Type> GetClassTypes(this Assembly assembly, string endingName)
        {
            var result = assembly.GetTypes().Where(x => x.IsClass).Where(x => x.Name.EndsWith(endingName)).ToList();
            return result;
        }

        private static List<Type> GetInterfaceTypes(this Assembly assembly, string endingName)
        {
            var result = assembly.GetTypes().Where(x => x.IsInterface).Where(x => x.Name.EndsWith(endingName)).ToList();
            return result;
        }

        private static void AddTransientTypes(IServiceCollection services, List<Type> serviceTypes, List<Type> interfaceTypes)
        {
            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = interfaceTypes.FirstOrDefault(x => x.Name.Substring(1) == serviceType.Name);
                services.AddTransient(interfaceType, serviceType);
            }
        }
    }
}