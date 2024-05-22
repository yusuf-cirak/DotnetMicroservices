using PlatformService.Models;

namespace PlatformService.Data.Abstractions;

public interface IPlatformRepository : IRepository
{
    IEnumerable<Platform> GetAllPlatforms();

    Platform? GetPlatformById(int id);

    void CreatePlatform(Platform platform);
}