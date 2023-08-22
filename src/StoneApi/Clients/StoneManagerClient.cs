using Grpc.Net.Client;
using StoneManager.Protos;
using System.Threading.Channels;
using Google.Protobuf.Collections;
using Grpc.Core;

namespace StoneApi.Clients;

internal sealed class StoneManagerClient : IStoneManagerClient
{
    private readonly StoneService.StoneServiceClient _client;

    public StoneManagerClient(StoneService.StoneServiceClient client)
    {
        _client = client;
    }

    public async Task<List<StoneMessages.Stone>> GetStonesAsync(int? count, CancellationToken ct = default)
    {
        StonesRequest stonesRequest = new()
        {
            Count = count
        };

        StonesResponse response = await _client.GetStonesAsync(stonesRequest, new CallOptions(cancellationToken: ct));

        return response.Stones
            .Select(x => new StoneMessages.Stone
            {
                Id = Guid.Parse(x.Id),
                Weight = x.Weight
            })
            .ToList();
    }
}