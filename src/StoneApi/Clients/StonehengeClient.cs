using StoneMessages;
using System.Web;

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

        var ub = new UriBuilder
        {
            Path = "api/stone"
        };

        if (count != null)
        {
            QueryString queryString = QueryString.Create("count", count.GetValueOrDefault().ToString());
            ub.Query = queryString.ToString();
        }

        _logger.LogInformation("Requesting {0} stones...", count);
        List<Stone> stones = await client.GetFromJsonAsync<List<Stone>>(ub.Uri.PathAndQuery, ct) ?? new List<Stone>();

        _logger.LogInformation("Received {0} stones.", stones.Count);

        return stones;
    }
}