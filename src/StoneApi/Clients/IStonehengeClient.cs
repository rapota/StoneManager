using StoneMessages;

namespace StoneApi.Clients;

public interface IStonehengeClient
{
    Task<List<Stone>> GetStones(int? count, CancellationToken ct = default);
}