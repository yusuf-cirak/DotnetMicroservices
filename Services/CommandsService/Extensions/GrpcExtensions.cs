using CommandsService.SyncDataServices;

namespace CommandsService.Extensions;

public static class GrpcExtensions {

    public static void AddGrpcClientServices(this IServiceCollection services){

        services.AddScoped<IPlatformDataClient, PlatformDataClient>();
    }
}