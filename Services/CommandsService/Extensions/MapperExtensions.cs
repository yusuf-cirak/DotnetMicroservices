namespace CommandsService.Extensions;

public static class MapperExtensions
{
    public static void AddMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}