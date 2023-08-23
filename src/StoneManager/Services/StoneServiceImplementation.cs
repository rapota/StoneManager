using Grpc.Core;
using StoneManager.Protos;

namespace StoneManager.Services;

internal sealed class StoneServiceImplementation : StoneService.StoneServiceBase
{
    private readonly FailedCounter _failedCounter;
    private readonly ILogger<StoneServiceImplementation> _logger;

    public StoneServiceImplementation(FailedCounter failedCounter, ILogger<StoneServiceImplementation> logger)
    {
        _failedCounter = failedCounter;
        _logger = logger;
    }

    public override Task<StonesResponse> GetStones(StonesRequest request, ServerCallContext context)
    {
        int count = request.Count ?? 3;

        // start fail
        if (count > 10 && _failedCounter.Counter == 0)
        {
            _failedCounter.Counter = count - 10;
            _logger.LogInformation("Failed counter set to {0}", _failedCounter.Counter);
            throw new InvalidOperationException();
        }

        // fail
        if (_failedCounter.Counter > 0)
        {
            _failedCounter.Counter--;
            _logger.LogInformation("Failed counter updated to {0}", _failedCounter.Counter);

            if (_failedCounter.Counter > 0)
            {
                throw new InvalidOperationException("Stone error.");
            }
        }

        StonesResponse response = new();
        for (int i = 0; i < count; i++)
        {
            Stone stone = new()
            {
                Id = Guid.NewGuid().ToString(),
                Weight = Random.Shared.Next(0, 100)
            };

            response.Stones.Add(stone);
        }

        _logger.LogInformation("Provide response for {0} stones.", count);

        return Task.FromResult(response);
    }
}