using Microsoft.AspNetCore.Mvc;
using StoneMessages;

namespace Stonehenge.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoneController : ControllerBase
{
    private readonly ILogger<StoneController> _logger;
    private readonly ServiceBlocker _serviceBlocker;

    public StoneController(ILogger<StoneController> logger, ServiceBlocker serviceBlocker)
    {
        _logger = logger;
        _serviceBlocker = serviceBlocker;
    }

    [HttpGet]
    public List<Stone> Get([FromQuery] int? count)
    {
        int c = count ?? 3;

        // start fail
        if (c > 10 && _serviceBlocker.BlockedDateTimeOffset == null)
        {
            _serviceBlocker.BlockedDateTimeOffset = DateTimeOffset.UtcNow.AddSeconds(c - 10);
            _logger.LogInformation("Service is blocked for {0} seconds", c - 10);
            throw new InvalidOperationException();
        }

        if (_serviceBlocker.BlockedDateTimeOffset != null)
        {
            // fail
            DateTimeOffset dateTimeOffset = (DateTimeOffset)_serviceBlocker.BlockedDateTimeOffset;
            if (DateTimeOffset.UtcNow < dateTimeOffset)
            {
                _logger.LogInformation("Server is blocked for: {0}", dateTimeOffset - DateTimeOffset.UtcNow);
                throw new InvalidOperationException("Stone error.");
            }

            // stop fail
            _serviceBlocker.BlockedDateTimeOffset = null;
            _logger.LogInformation("Server unblocked.");
        }

        List<Stone> stones = Enumerable.Range(1, c)
            .Select(_ => new Stone
            {
                Id = Guid.NewGuid(),
                Weight = Random.Shared.Next(0, 100)
            })
            .ToList();

        _logger.LogInformation("Provide response for {0} stones.", stones.Count);

        return stones;
    }

    //[HttpPost]
    //public void SetBlock([FromQuery] int count)
    //{
    //    _serviceBlocker.BlockedDateTimeOffset = DateTimeOffset.UtcNow.AddSeconds(count);
    //    _logger.LogInformation("Server blocked till: {0}", _serviceBlocker.BlockedDateTimeOffset);
    //}
}