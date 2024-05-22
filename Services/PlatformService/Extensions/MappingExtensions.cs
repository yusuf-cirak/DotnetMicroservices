namespace PlatformService.Extensions;

public static class MappingExtensions
{
    public static void AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}