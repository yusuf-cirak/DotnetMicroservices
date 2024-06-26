
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Data;
    public interface ICommandRepo
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();

        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExists(int platformId);
        bool ExternalPlatformExists(int externalPlatformId);
        Task<bool> ExternalPlatformExistsAsync(int externalPlatformId);

        // Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
        
    }


public sealed class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;

    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }

    public void CreateCommand(int platformId, Command command)
    {
        command.PlatformId = platformId;

        _context.Commands.Add(command);
    }

    public void CreatePlatform(Platform platform)
    {
        _context.Platforms.Add(platform);
    }

    public bool ExternalPlatformExists(int externalPlatformId)
    {
        return _context.Platforms.Any(p => p.ExternalID == externalPlatformId);
    }

    public Task<bool> ExternalPlatformExistsAsync(int externalPlatformId)
    {
        return _context.Platforms.AnyAsync(p => p.ExternalID == externalPlatformId);
    }

    public IEnumerable<Platform> GetAllPlatforms()
    {
        return _context.Platforms;
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands.SingleOrDefault(c => c.PlatformId == platformId && c.Id == commandId);
    }

    public IEnumerable<Command> GetCommandsForPlatform(int platformId)
    {
       return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.PlatformId);
    }

    public bool PlatformExists(int platformId)
    {
       return _context.Platforms.Any(p => p.Id == platformId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() >= 0;
    }
}
