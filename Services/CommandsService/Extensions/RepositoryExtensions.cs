using CommandsService.Data;

namespace CommandsService.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<ICommandRepo, CommandRepo>();
    }
}