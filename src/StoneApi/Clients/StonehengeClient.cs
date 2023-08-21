using StoneMessages;

namespace StoneApi.Clients;

internal sealed class StonehengeClient : IStonehengeClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<StonehengeClient> _logger;

    public StonehengeClient(IHttpClientFactory clientFactory, ILogger<StonehengeClient> logger)
    {
        _clientFactory = clientFactory;
        _logger = logger;
    }

    public async Task<List<Stone>> GetStones(int? count, CancellationToken ct = default)
    {
        HttpClient client = _clientFactory.CreateClient(nameof(StonehengeClient));

        _logger.LogInformation("Requesting {0} stones...", count);
        List<Stone> stones = await client.GetFromJsonAsync<List<Stone>>("api/stone", ct) ?? new List<Stone>();

        _logger.LogInformation("Received {0} stones.", stones.Count);

        return stones;
    }
}