using Grpc.Core;
using StoneManager.Protos;

namespace StoneManager.Services;

internal sealed class StoneServiceImplementation : StoneService.StoneServiceBase
{
    public override Task<StonesResponse> GetStones(StonesRequest request, ServerCallContext context)
    {
        Stone stone = new()
        {
            Id = Guid.NewGuid().ToString(),
            Weight = 1001
        };

        StonesResponse response = new();
        response.Stones.Add(stone);

        return Task.FromResult(response);
    }
}