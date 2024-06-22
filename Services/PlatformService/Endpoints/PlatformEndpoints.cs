using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Abstractions;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Services.Clients;

namespace PlatformService.Endpoints;

public static class PlatformEndpoints
{
    public static void MapPlatformEndpoints(this IEndpointRouteBuilder builder)
    {
        var groupBuilder = builder.MapGroup("_api/platforms");

        groupBuilder.MapGet("/",
                (IPlatformRepository platformRepository, IMapper mapper) =>
                {
                    Console.WriteLine("--> Getting Platforms...");

                    var platforms = mapper.Map<IEnumerable<GetPlatformDto>>(platformRepository.GetAllPlatforms());

                    return platforms;
                })
            .WithTags("Platforms");

        groupBuilder.MapGet("/{id:int}",
                (IPlatformRepository platformRepository, IMapper mapper, int id) =>
                {
                    var platform = platformRepository.GetPlatformById(id);

                    if (platform is null)
                    {
                        return Results.NotFound();
                    }

                    return Results.Ok(platform);
                })
            .WithTags("Platforms")
            .WithName("GetPlatformById");

        groupBuilder.MapPost("/",
                async (IPlatformRepository platformRepository, ICommandDataClientService commandDataClientService,
                    IMapper mapper,
                    [FromBody] CreatePlatformDto createPlatformDto) =>
                {
                    var platform = mapper.Map<Platform>(createPlatformDto);

                    platformRepository.CreatePlatform(platform);

                    await platformRepository.SaveChangesAsync();

                    var platformDto = mapper.Map<GetPlatformDto>(platform);
                    try
                    {
                        await commandDataClientService.SendPlatformToCommandAsync(platformDto);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"--> Couldn't send synchronously: {e.Message}");
                    }

                    return Results.Created(string.Empty, platformDto);
                })
            .WithTags("Platforms");
    }
}