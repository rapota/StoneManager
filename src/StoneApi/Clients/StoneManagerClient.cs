using Grpc.Core;
using Polly;
using Polly.Retry;
using StoneManager.Protos;

namespace StoneApi.Clients;

internal sealed class StoneManagerClient : IStoneManagerClient
{
    private readonly StoneService.StoneServiceClient _client;
    private readonly AsyncRetryPolicy _retryPolicy;

    public StoneManagerClient(StoneService.StoneServiceClient client)
    {
        _client = client;

        _retryPolicy = Policy
            .Handle<RpcException>()
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(5)
            });
    }

    public async Task<List<StoneMessages.Stone>> GetStonesAsync(int? count, CancellationToken ct = default)
    {
        StonesRequest stonesRequest = new()
        {
            Count = count
        };

        StonesResponse response = await _retryPolicy.ExecuteAsync(async token => await _client.GetStonesAsync(stonesRequest, new CallOptions(cancellationToken: token)), ct);

        return response.Stones
            .Select(x => new StoneMessages.Stone
            {
                Id = Guid.Parse(x.Id),
                Weight = x.Weight
            })
            .ToList();
    }
}