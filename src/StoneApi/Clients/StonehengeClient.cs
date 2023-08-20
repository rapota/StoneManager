using StoneMessages;

namespace StoneApi.Clients;

internal sealed class StonehengeClient : IStonehengeClient
{
    private readonly string _connectionString;

    public StonehengeClient(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("StonehengeClient") ?? string.Empty;
    }

    public async Task<List<Stone>> GetStones(int? count, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}