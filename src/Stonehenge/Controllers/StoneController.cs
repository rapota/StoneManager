using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoneMessages;

namespace Stonehenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoneController : ControllerBase
{
    private readonly ILogger<StoneController> _logger;
    private readonly FailedCounter _failedCounter;

    public StoneController(ILogger<StoneController> logger, FailedCounter failedCounter)
    {
        _logger = logger;
        _failedCounter = failedCounter;
    }

    [HttpGet]
    public async Task<List<Stone>> Get([FromQuery] int? count, CancellationToken ct)
    {
        if (_failedCounter.Counter > 0)
        {
            _failedCounter.Counter--;
            _logger.LogInformation("Failed counter set to {0}", _failedCounter.Counter);
            throw new InvalidOperationException("Stone error.");
        }

        if (count >= 10)
        {
            throw new InvalidOperationException();
        }

        if (count >= 5)
        {
            await Task.Delay(
                TimeSpan.FromSeconds(count.GetValueOrDefault()),
                ct);
        }

        List<Stone> stones = Enumerable.Range(1, count ?? 3)
            .Select(_ => new Stone
            {
                Id = Guid.NewGuid(),
                Weight = Random.Shared.Next(0, 100)
            })
            .ToList();

        return stones;
    }

    [HttpPost]
    public void SetErrors([FromQuery] int count)
    {
        _failedCounter.Counter = count;
        _logger.LogInformation("Failed counter set to {0}", count);
    }
}