using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        foreach (var serviceInstance in ScanService())
        {
            serviceInstance.Services(services);
        }
    }
    private static IEnumerable<IService> ScanService()
    {
        var appServices = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(IService).IsAssignableFrom(t) && !t.IsInterface)
            .Select(CreateInstance<IService>)
            .Cast<IService>();

        return appServices;
    }
    private static TInterface CreateInstance<TInterface>(Type type)
    {
        try
        {
            return (TInterface)Activator.CreateInstance(type)!;
        }
        catch (MissingMethodException)
        {
            var configConstructor = type.GetConstructors()
           .FirstOrDefault(ctor => ctor.GetParameters().Any(p => p.ParameterType == typeof(IConfiguration)));

            if (configConstructor != null)
            {
                var configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json")
                               .Build();

                return (TInterface)Activator.CreateInstance(type, configuration)!;
            }

            throw new InvalidOperationException($"{type.Name} has only allowed injection of IConfiguration.");
        }
    }
}