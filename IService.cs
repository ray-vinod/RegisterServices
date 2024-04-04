using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public interface IService
{
    void Services(IServiceCollection services);
}