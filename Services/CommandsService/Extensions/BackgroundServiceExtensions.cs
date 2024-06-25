using CommandsService.AsyncDataServices;

namespace CommandsService.Extensions;

public static class BackgroundServiceExtensions{
    public static void AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHostedService<MessageBusSubscriber>();
    }
}