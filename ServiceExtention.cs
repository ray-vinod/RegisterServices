using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices ;

public static class ServiceExtentions
{
    public static void AppServices(this IServiceCollection services)
    {
        var appServices = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(t => typeof(IService).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var serviceType in appServices)
        {
            if (Activator.CreateInstance(serviceType) is IService serviceInstance)
            {
                serviceInstance.AddServices(services);
            }
        }
    }
}