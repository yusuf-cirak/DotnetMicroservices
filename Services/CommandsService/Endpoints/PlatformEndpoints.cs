using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;

namespace CommandsService.Endpoints;

public static class PlatformEndpoints
{
    public static void MapPlatformEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/c/platforms");

        groupBuilder.MapGet("/",
                (ICommandRepo commandRepo,IMapper mapper) => {
                    Console.WriteLine("--> Getting Platforms from CommandsService");

                    var platforms = mapper.Map<IEnumerable<PlatformReadDto>>(commandRepo.GetAllPlatforms());
                    return TypedResults.Ok(platforms);
                }) 
            .WithTags("Queries");
        
        groupBuilder.MapPost("/",
                () => TypedResults.Ok())
            .WithTags("Commands");
    }
}