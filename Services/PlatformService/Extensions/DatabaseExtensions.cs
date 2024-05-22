using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Extensions;

public static class DatabaseExtensions
{
    public static void AddDbContextServices(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemory"));
    }


    public static void PrepPopulation(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        SeedData(services.ServiceProvider.GetService<AppDbContext>()!);
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data...");

            context.Platforms.AddRange(
                new Platform()
                    { Name = "Dotnet", Publisher = "Microsoft", Cost = "Free" },
                new Platform()
                    { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                new Platform()
                    { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }
}