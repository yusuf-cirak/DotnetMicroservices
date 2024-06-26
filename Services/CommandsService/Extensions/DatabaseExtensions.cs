using CommandsService.Data;
using CommandsService.Models;
using CommandsService.SyncDataServices;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Extensions;

public static class DatabaseExtensions
{
    public static void AddDbContextServices(this IServiceCollection services,IConfiguration configuration,IWebHostEnvironment hostEnv)
    {
        services.AddDbContext<AppDbContext>(opt => {
                opt.UseInMemoryDatabase("InMem");
                Console.WriteLine("--> Using InMemory Database");
        
        });
        
    }


    public static void PrepPopulation(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();

        var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

        var platforms = grpcClient!.ReturnAllPlatforms();
        SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>()!,platforms);
    }

    public static void SeedData(ICommandRepo repo,IEnumerable<Platform> platforms){

        Console.WriteLine("--> Seeding Data...");

        foreach (var platform in platforms)
        {
                if (!repo.ExternalPlatformExists(platform.ExternalID))
                {
                    repo.CreatePlatform(platform);
                }
        }

        repo.SaveChanges();
    }

}