using AutoMapper;
using Grpc.Core;
using PlatformService.Data.Abstractions;

namespace PlatformService.Server;

public sealed class GrpcPlatformService: GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IPlatformRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<PlatformResponse> GetAllPlatform(GetAllRequest request, ServerCallContext context)
    {
        var res = new PlatformResponse();

        var platforms = _repository
        .GetAllPlatforms()
        .Select(_mapper.Map<GrpcPlatformModel>);

        res.Platforms.AddRange(platforms);

        return Task.FromResult(res);
    }
}