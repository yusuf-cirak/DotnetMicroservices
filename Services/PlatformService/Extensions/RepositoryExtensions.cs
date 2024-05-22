using PlatformService.Data.Abstractions;
using PlatformService.Data.Concetes;

namespace PlatformService.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IPlatformRepository, PlatformRepository>();
    }
}