using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public interface IService
{
    void AddServices(IServiceCollection services);
}