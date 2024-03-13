using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.Application;
public static class ServicesInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        //services.AddScoped<ILoginServices, LoginServices>();
    }
}

