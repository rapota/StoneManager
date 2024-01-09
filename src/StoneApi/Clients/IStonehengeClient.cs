using Refit;
using StoneMessages;

namespace StoneApi.Clients;

public interface IStonehengeClient
{
    Task<List<Stone>> GetStones(int? count, CancellationToken ct = default);
}

public interface IStonehengeRefitClient
{
    [Get("/api/stone")]
    Task<List<Stone>> GetStones(int? count, CancellationToken ct = default);
}
