using StoneMessages;

namespace StoneApi.Clients;

public interface IStoneManagerClient
{
    Task<List<Stone>> GetStonesAsync(int? count, CancellationToken ct = default);
}