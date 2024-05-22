using PlatformService.Data.Abstractions;
using PlatformService.Models;

namespace PlatformService.Data.Concetes;

public sealed class PlatformRepository : IPlatformRepository
{
    private readonly AppDbContext _dbContext;

    public PlatformRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int SaveChanges() => _dbContext.SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _dbContext.SaveChangesAsync(cancellationToken);

    public IEnumerable<Platform> GetAllPlatforms() => _dbContext.Platforms.AsEnumerable();

    public Platform? GetPlatformById(int id) => _dbContext.Platforms.SingleOrDefault(p => p.Id == id);

    public void CreatePlatform(Platform platform)
    {
        _dbContext.Platforms.Add(platform);
    }
}