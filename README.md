# Register Services

> It's register all services in the project with minimal configuration.

## How to use this

> In Program.cs

```code
var builder = WebApplication.CreateBuilder(args);
{
    ...others code ...

    builder.Services.AddServices(builder.Configuration);
}
```

> In HomeService.cs

```code
public class HomeService : IService
{
    public void Services(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IHomeService,HomeService>();
    }
}
```

```code
public class OtherService : IService
{
    public void Services(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOtherService, OtherService>();

        services.AddDbContext<YourDbContext>(options
            => options.UseSqlite(configuration?.GetConnectionString("DefaultConnection")));
    }
}
```
