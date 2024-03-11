using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public static class ServiceExtentions
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var appServices = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(IService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var serviceType in appServices)
        {
            if (Activator.CreateInstance(serviceType) is IService serviceInstance)
            {
                serviceInstance.Services(services, configuration);
            }
        }
    }
}