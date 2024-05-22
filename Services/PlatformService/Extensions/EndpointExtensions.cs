using PlatformService.Endpoints;

namespace PlatformService.Extensions;

public static class EndpointExtensions
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapPlatformEndpoints();
    }
}