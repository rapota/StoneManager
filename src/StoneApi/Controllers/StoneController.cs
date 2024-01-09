using Microsoft.AspNetCore.Mvc;
using StoneApi.Clients;
using StoneMessages;

namespace StoneApi.Controllers;

[ApiController]
[Route("[controller]")]
public class StoneController : ControllerBase
{
    private readonly IStonehengeClient _stonehengeClient;
    private readonly IStoneManagerClient _stoneManagerClient;
    private readonly ILogger<StoneController> _logger;
    private readonly IStonehengeRefitClient _stonehengeRefitClient;

    public StoneController(IStonehengeClient stonehengeClient, IStoneManagerClient stoneManagerClient, ILogger<StoneController> logger, IStonehengeRefitClient stonehengeRefitClient)
    {
        _stonehengeClient = stonehengeClient;
        _stoneManagerClient = stoneManagerClient;
        _logger = logger;
        _stonehengeRefitClient = stonehengeRefitClient;
    }

    [HttpGet]
    public async Task<List<Stone>> Get([FromQuery] int? count, CancellationToken ct)
    {
        _logger.LogInformation("Get {0} stones request.", count);
        List<Stone> stones = await _stoneManagerClient.GetStonesAsync(count, ct);
        _logger.LogInformation("Received {0} stones.", count);
        return stones;
    }

    [HttpGet("Legacy")]
    public async Task<List<Stone>> GetLegacy([FromQuery] int? count, CancellationToken ct)
    {
        _logger.LogInformation("Get {0} legacy stones request.", count);
        //List<Stone> stones = await _stonehengeClient.GetStones(count, ct);
        List<Stone> stones = await _stonehengeRefitClient.GetStones(count, ct);
        _logger.LogInformation("Received {0} legacy stones.", count);
        return stones;
    }
}