namespace PlatformService.Data.Abstractions;

public interface IRepository
{
    int SaveChanges();

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}