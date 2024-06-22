using PlatformService.Services.Clients;

namespace PlatformService.Extensions;

public static class ServiceExtensions
{
    public static void AddClientServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandDataClientService, CommandDataClientService>();
    }
}