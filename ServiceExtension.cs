using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        foreach (var serviceInstance in ScanForServices())
        {
            serviceInstance.Service(services);
        }
    }

    private static IEnumerable<IService> ScanForServices()
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => typeof(IService).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            .Select(CreateInstance<IService>);
    }

    private static T CreateInstance<T>(Type type)
    {
        try
        {
            return (T)Activator.CreateInstance(type)!;
        }
        catch (MissingMethodException)
        {
            var configConstructor = type.GetConstructors()
                .FirstOrDefault(ctor => ctor.GetParameters()
                .Any(param => param.ParameterType == typeof(IConfiguration)));

            if (configConstructor != null)
            {
                var configuration = BuildConfiguration();
                return (T)Activator.CreateInstance(type, configuration)!;
            }

            throw new InvalidOperationException(
                $"{type.FullName} does not have a parameterless constructor or a constructor accepting IConfiguration.");
        }
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
    }
}