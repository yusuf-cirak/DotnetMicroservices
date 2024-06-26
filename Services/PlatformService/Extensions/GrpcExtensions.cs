using PlatformService.Server;

namespace PlatformService.Extensions
{
    public static class GrpcExtensions
    {

        public static void AddGrpcServerServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
            });
        }

        public static void MapGrpcServerServices(this IEndpointRouteBuilder app)
        {
            app.MapGrpcService<GrpcPlatformService>();

            app.MapGet("/_api/protos/platforms.proto", async context =>
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "Protos", "platforms.proto");
                var proto = await File.ReadAllTextAsync(file);
                await context.Response.WriteAsync(proto);
            });
        }
    }
}