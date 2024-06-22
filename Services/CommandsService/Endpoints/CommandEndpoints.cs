namespace CommandsService.Endpoints;

public static class CommandEndpoints
{
    public static void MapCommandEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/commands");
        
        groupBuilder.MapPost("/platform",
                () => TypedResults.Ok())
            .WithTags("Commands");
    }
}