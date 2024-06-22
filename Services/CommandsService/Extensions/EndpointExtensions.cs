using CommandsService.Endpoints;

namespace CommandsService.Extensions;

public static class EndpointExtensions
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapCommandEndpoints();
    }
}