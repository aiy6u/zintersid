using System.Reflection;
using Newtonsoft.Json;
using zintersid.Common.IoC;

namespace zintersid.Configurations
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection RegisterProjectServices(this IServiceCollection services, Serilog.ILogger logger)
        {
            try
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var allTypes = assemblies.SelectMany(s => s.GetTypes()).ToList();

                RegisterRepositoryTypes(services, logger, allTypes);
                RegisterAppServiceTypes(services, logger, allTypes);
            }
            catch (ReflectionTypeLoadException ex)
            {
                logger.Error("Error loading types: {0}", ex.LoaderExceptions);
            }
            catch (Exception ex)
            {
                logger.Error("An error occurred during service registration: {0}", ex.Message);
            }

            return services;
        }

        private static void RegisterRepositoryTypes(IServiceCollection services, Serilog.ILogger logger, List<Type> allTypes)
        {
            var repositoryTypes = allTypes
                .Where(p => p.BaseType != null && p.BaseType.IsGenericType &&
                            p.BaseType.GetGenericTypeDefinition() == typeof(RepositoryBase<>) &&
                            p.IsClass && !p.IsAbstract)
                .ToList();

            foreach (var type in repositoryTypes)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(i => !i.IsGenericType && i.GetType().IsInstanceOfType(typeof(IRepository<>)));

                if (interfaceType != null)
                {
                    // logger.Information($"[REPO DI] {interfaceType} <<= {type}");
                    services.RegisterLifetime(interfaceType, type);
                }
            }
        }

        private static void RegisterAppServiceTypes(IServiceCollection services, Serilog.ILogger logger, List<Type> allTypes)
        {
            var appServiceTypes = allTypes
                .Where(p => typeof(AppServiceBase).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
                .ToList();

            foreach (var type in appServiceTypes)
            {
                var interfaceType = type.GetInterfaces()
                    .FirstOrDefault(i => i.IsAssignableTo(typeof(IAppService)) && i.Name != nameof(IAppService));

                if (interfaceType != null)
                {
                    // logger.Information($"[SERVICE DI] {interfaceType} <<= {type}");
                    services.RegisterLifetime(interfaceType, type);
                }
            }
        }

        private static void RegisterLifetime(this IServiceCollection services, Type interfaceType, Type implementationType)
        {
            var crrType = interfaceType.GetInterfaces().FirstOrDefault(i => i.IsAssignableTo(typeof(ISingletonRegister)) || i.IsAssignableTo(typeof(IScopedRegister)) || i.IsAssignableTo(typeof(ITransientRegister)));
            switch (crrType?.Name)
            {
                case nameof(ISingletonRegister):
                    // Console.WriteLine($"[ISingletonRegister] {interfaceType}, {implementationType}");
                    services.AddSingleton(interfaceType, implementationType);
                    break;

                case nameof(IScopedRegister):
                    // Console.WriteLine($"[IScopedRegister] {interfaceType}, {implementationType}");
                    services.AddScoped(interfaceType, implementationType);
                    break;

                case nameof(ITransientRegister):
                    // Console.WriteLine($"[ITransientRegister] {interfaceType}, {implementationType}");
                    services.AddTransient(interfaceType, implementationType);
                    break;

                default:
                    throw new Exception("Please define which lifetime to use for the interface and implementation types.");
            }
        }
    }
}