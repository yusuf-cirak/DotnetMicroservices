using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.Services.Clients;

public interface ICommandDataClientService
{
    Task SendPlatformToCommandAsync(GetPlatformDto platformDto);
}

public sealed class CommandDataClientService(IHttpClientFactory clientFactory, IConfiguration configuration)
    : ICommandDataClientService
{
    public async Task SendPlatformToCommandAsync(GetPlatformDto platformDto)
    {
        // Serialize the object to JSON
        string json = JsonSerializer.Serialize(platformDto);

        // Create StringContent with JSON payload
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

        using var client = clientFactory.CreateClient();
        var baseUrl = configuration["CommandService"];

        var res = await client.PostAsync($"{baseUrl}/_api/commands/platform", httpContent);

        Console.WriteLine(res.IsSuccessStatusCode
            ? "--> Sync POST to CommandService was OK!"
            : "--> Sync POST to CommandService was not OK!");
    }
}