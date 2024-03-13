using Microservices.Infra.UnityOfWok;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.CrossCutting.Infra;

public static class RepositoriesInjection
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnityOfWork,UnityOfWork>();
    }
}
