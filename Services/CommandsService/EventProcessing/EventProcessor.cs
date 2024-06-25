using System.Text.Json;
using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using CommandsService.Models;

namespace CommandsService.EventProcessing;
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }




public sealed class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMapper _mapper;

    public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _mapper = mapper;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.PlatformPublished:
                break;
            default:
                break;
        }
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        System.Console.WriteLine("--> Determining Event");
        var genericEvent = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);


        switch (genericEvent.Event)
        {
            case "Platform_Published":
                System.Console.WriteLine("--> Platform Published Event Detected");
                this.AddPlatform(notificationMessage);
                return EventType.PlatformPublished;
            default:
                System.Console.WriteLine("--> Could not determine the event type");
                return EventType.Undetermined;
        }
    }

    private void AddPlatform(string platformPublishedMessage){
        using var scope = _serviceScopeFactory.CreateScope();

        var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

        var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

        try
        {
            var platform = _mapper.Map<Platform>(platformPublishedDto);

            if(!repo.ExternalPlatformExists(platform.ExternalID)){
                repo.CreatePlatform(platform);
                repo.SaveChanges();
                System.Console.WriteLine("--> Platform added to database");
            }
            else{
                System.Console.WriteLine("--> Platform already exists in the database");
            }
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"--> Could not add Platform to DB: {ex.Message}");
        }
    }
}

enum EventType{
        PlatformPublished,
        Undetermined
    }