using CommandsService.Data;
using CommandsService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandsService.Extensions;

public static class DatabaseExtensions
{
    public static void AddDbContextServices(this IServiceCollection services,IConfiguration configuration,IWebHostEnvironment hostEnv)
    {
        services.AddDbContext<AppDbContext>(opt => {
            if (hostEnv.IsDevelopment())
            {
                opt.UseInMemoryDatabase("InMem");
                Console.WriteLine("--> Using InMemory Database");
            }
            else
            {
                opt.UseSqlServer(configuration.GetConnectionString("PlatformsConn"));
                Console.WriteLine($"--> Using SQL Server Database");
            }


        
        });
        
    }


    public static void PrepPopulation(this WebApplication app)
    {

        using var services = app.Services.CreateScope();

        var dbContext = services.ServiceProvider.GetService<AppDbContext>()!;

        if (app.Environment.IsProduction())
        {
            Console.WriteLine("--> Applying Migrations...");
            try
            {
            dbContext.Database.Migrate();
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

        }

            SeedData(dbContext);
    }



        private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine("--> Seeding data...");

            context.Platforms.AddRange(
                new Platform()
                    { Name = "Dotnet",ExternalID = 1,Id=1}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("--> We already have data");
        }
    }

}