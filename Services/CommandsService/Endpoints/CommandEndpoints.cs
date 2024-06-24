using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Endpoints;

public static class CommandEndpoints
{
    public static void MapCommandEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/c");

        groupBuilder.MapGet("/platforms/{platformId:int}/commands", (int platformId, ICommandRepo commandRepo, IMapper mapper) =>
        {
            Console.WriteLine("--> Hit GetCommandsForPlatform: " + platformId + " from CommandsService");

            if (!commandRepo.PlatformExists(platformId))
            {
                return Results.NotFound();
            }

            var commands = mapper.Map<IEnumerable<CommandReadDto>>(commandRepo.GetCommandsForPlatform(platformId));
            return Results.Ok(commands);
        })
        .WithTags("Commands");

        
        groupBuilder.MapGet("/platforms/{platformId:int}/commands/{commandId:int}", (int platformId, int commandId,ICommandRepo commandRepo, IMapper mapper) =>
        {
            Console.WriteLine("--> Hit GetCommandsForPlatform: " + platformId + " from CommandsService");

            if (!commandRepo.PlatformExists(platformId))
            {
                return TypedResults.NotFound();
            }
            var command = commandRepo.GetCommand(platformId,commandId);

            if (command is null)
            {
                return TypedResults.NotFound();
            }

            return Results.Ok(mapper.Map<CommandReadDto>(command));
        })
        .WithTags("Commands");

            groupBuilder.MapPost("/platforms/{platformId:int}/commands", ([FromRoute]int platformId,[FromBody] CommandCreateDto commandCreateDto,ICommandRepo commandRepo, IMapper mapper) =>
        {
            Console.WriteLine("--> Hit GetCommandsForPlatform: " + platformId + " from CommandsService");

            if (!commandRepo.PlatformExists(platformId))
            {
                return TypedResults.NotFound();
            }
            var newCommand = mapper.Map<Command>(commandCreateDto);

            commandRepo.CreateCommand(platformId,newCommand!);

            if (!commandRepo.SaveChanges())
            {
                return TypedResults.BadRequest("Could not add Command to DB");
            }

            return Results.Ok(mapper.Map<CommandReadDto>(newCommand));
        })
        .WithTags("Commands");
        
        groupBuilder.MapPost("/platform",
                () => TypedResults.Ok())
            .WithTags("Commands");
    }
}