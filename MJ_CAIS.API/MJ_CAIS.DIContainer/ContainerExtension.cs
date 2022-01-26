using Microsoft.Extensions.DependencyInjection;
using MJ_CAIS.DataAccess;
using MJ_CAIS.Repositories.Contracts;
using MJ_CAIS.Repositories.Impl;
using MJ_CAIS.Services;
using MJ_CAIS.Services.Contracts;
using System.Reflection;

namespace MJ_CAIS.DIContainer
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, string connectionString)
        {
            // TODO:
            // services.AddDbContext<CaisDbContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());
            services.AddDbContext<CaisDbContext>();

            var servicesTypes = typeof(BulletinService).Assembly.GetClassTypes("Service");
            var interfaceTypes = typeof(IBulletinService).Assembly.GetInterfaceTypes("Service");
            AddTransientTypes(services, servicesTypes, interfaceTypes);

            var repositoryTypes = typeof(BulletinRepository).Assembly.GetClassTypes("Repository");
            var interfaceRepositoryTypes = typeof(IBulletinRepository).Assembly.GetInterfaceTypes("Repository");
            AddTransientTypes(services, repositoryTypes, interfaceRepositoryTypes);
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