using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RegisterServices;

public interface IService
{
    void Services(IServiceCollection services,IConfiguration configuration);
}