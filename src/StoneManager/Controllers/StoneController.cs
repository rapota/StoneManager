using Microsoft.AspNetCore.Mvc;
using StoneManager.Services;

namespace StoneManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoneController : ControllerBase
{
    private readonly FailedCounter _failedCounter;
    private readonly ILogger<StoneController> _logger;

    public StoneController(FailedCounter failedCounter, ILogger<StoneController> logger)
    {
        _failedCounter = failedCounter;
        _logger = logger;
    }

    //[HttpPost]
    //public void SetErrors([FromQuery] int count)
    //{
    //    _failedCounter.Counter = count;
    //    _logger.LogInformation("Failed counter set to {0}", count);
    //}
}