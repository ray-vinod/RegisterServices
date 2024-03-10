# Register Services

> It's register all services in the project with minimal configuration.

## How to use this

> In Program.cs

```code
var builder = WebApplication.CreateBuilder(args);
{
    ...others code ...

    builder.Services.AppServices();
}
```

> In HomeService.cs

```code
public class HomeService : IService
{
    public void Endpoints(IServiceCollection services)
    {
        services.AddScoped<IHomeService,HomeService>();
    }
}
```
