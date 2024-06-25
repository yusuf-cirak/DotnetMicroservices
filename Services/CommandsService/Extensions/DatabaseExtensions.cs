using CommandsService.Data;
using CommandsService.Models;
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

}