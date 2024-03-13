using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.Application;
public static class ValidationInjection
{
    public static void AddValidations(this IServiceCollection services)
    {
        //services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
    }
}