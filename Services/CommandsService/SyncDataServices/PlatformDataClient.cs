using AutoMapper;
using CommandsService.Models;
using Grpc.Net.Client;
using PlatformService;

namespace CommandsService.SyncDataServices;

public interface IPlatformDataClient
{
    IEnumerable<Platform> ReturnAllPlatforms();
}

public sealed class PlatformDataClient : IPlatformDataClient
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public PlatformDataClient(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public IEnumerable<Platform> ReturnAllPlatforms()
    {
        var grpcAddress = _configuration["GrpcPlatform"] ?? string.Empty;
        Console.WriteLine($"--> Calling gRPC Server: {grpcAddress}");

        try
        {
            using var channel = GrpcChannel.ForAddress(grpcAddress);

            var client = new GrpcPlatform.GrpcPlatformClient(channel);

            var request = new GetAllRequest();

            var reply = client.GetAllPlatform(request);


            return _mapper.Map<IEnumerable<Platform>>(reply?.Platforms)!;

        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"--> Could not call GRPC Server. {ex.Message}");
            return [];
        }
    }
}