using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public interface IService
{
    void Service(IServiceCollection services);
}